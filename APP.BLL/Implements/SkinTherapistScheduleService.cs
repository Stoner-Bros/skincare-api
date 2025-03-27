using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.Implements
{
    public class SkinTherapistScheduleService : ISkinTherapistScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkinTherapistScheduleService> _logger;

        public SkinTherapistScheduleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SkinTherapistScheduleService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationModel<SkinTherapistScheduleResponse>> GetAllAsync(DateOnly? date, int therapistId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.SkinTherapistSchedules.GetQueryable()
                                    .Where(s => s.SkinTherapistId == therapistId && s.WorkDate == date);  // Truy vấn dữ liệu

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var accounts = await query
                .OrderBy(a => a.ScheduleId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<SkinTherapistScheduleResponse>
            {
                Items = _mapper.Map<List<SkinTherapistScheduleResponse>>(accounts),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<SkinTherapistScheduleResponse?> GetByIDAsync(int id)
        {
            var schedule = await _unitOfWork.SkinTherapistSchedules.GetByIDAsync(id);
            return schedule != null ? _mapper.Map<SkinTherapistScheduleResponse>(schedule) : null;
        }

        public async Task<IEnumerable<SkinTherapistScheduleResponse>?> CreateAsync(SkinTherapistScheduleCreationRequest request)
        {
            var timeSlots = await _unitOfWork.TimeSlots.GetQueryable()
                                                 .Where(s => request.TimeSlotIds.Contains(s.TimeSlotId))
                                                 .ToListAsync();

            if (timeSlots.Count != request.TimeSlotIds.Length)
            {
                _logger.LogError("One or more time slots do not exist");
                return null;
            }

            var schedules = new List<SkinTherapistSchedule>();

            foreach (var date in request.WorkDates)
            {
                foreach (var timeSlot in timeSlots)
                {
                    var existingSchedule = await _unitOfWork.SkinTherapistSchedules.GetQueryable()
                        .Where(s => s.SkinTherapistId == request.SkinTherapistId
                                 && s.WorkDate == date
                                 && s.StartTime == timeSlot.StartTime
                                 && s.EndTime == timeSlot.EndTime)
                        .FirstOrDefaultAsync();

                    if (existingSchedule == null)
                    {
                        var schedule = new SkinTherapistSchedule
                        {
                            SkinTherapistId = request.SkinTherapistId,
                            WorkDate = date,
                            StartTime = timeSlot.StartTime,
                            EndTime = timeSlot.EndTime,
                            Notes = request.Notes
                        };
                        schedules.Add(schedule);
                    }
                }
            }

            try
            {
                await _unitOfWork.SkinTherapistSchedules.CreateRangeAsync(schedules);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating schedules");
                return null;
            }

            return _mapper.Map<IEnumerable<SkinTherapistScheduleResponse>>(schedules);
        }

        public async Task<bool> UpdateScheduleAvailabilityAsync(int therapistId, DateOnly date, int[] timeSlotIds)
        {
            if (timeSlotIds == null || timeSlotIds.Length == 0)
                throw new ArgumentException("At least one valid TimeSlotId is required.");

            var timeSlots = _unitOfWork.TimeSlots.GetQueryable()
                                    .Where(s => timeSlotIds.Contains(s.TimeSlotId));
            if (timeSlots.Count() != timeSlotIds.Length)
                throw new ArgumentException("One or more time slots not found.");

            var schedules = await _unitOfWork.SkinTherapistSchedules.GetQueryable()
                .Where(s => s.SkinTherapistId == therapistId
                         && s.WorkDate == date
                         && timeSlots.Any(ts => ts.StartTime == s.StartTime && ts.EndTime == s.EndTime))
                .ToListAsync();

            if (schedules.Count == 0)
                return false;

            foreach (var schedule in schedules)
            {
                schedule.IsAvailable = false;
            }

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> ReverseScheduleAsync(int therapistId, DateOnly date, int[] timeSlotIds)
        {
            if (timeSlotIds == null || timeSlotIds.Length == 0)
                throw new ArgumentException("At least one valid TimeSlotId is required.");

            var timeSlots = _unitOfWork.TimeSlots.GetQueryable()
                                    .Where(s => timeSlotIds.Contains(s.TimeSlotId));
            if (timeSlots.Count() != timeSlotIds.Length)
                throw new ArgumentException("One or more time slots not found.");

            var schedules = await _unitOfWork.SkinTherapistSchedules.GetQueryable()
                .Where(s => s.SkinTherapistId == therapistId
                         && s.WorkDate == date
                         && timeSlots.Any(ts => ts.StartTime == s.StartTime && ts.EndTime == s.EndTime))
                .ToListAsync();

            if (schedules.Count == 0)
                return false;

            foreach (var schedule in schedules)
            {
                schedule.IsAvailable = true;
            }

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, SkinTherapistScheduleUpdationRequest request)
        {
            var schedule = await _unitOfWork.SkinTherapistSchedules.GetByIDAsync(id);
            if (schedule == null) return false;

            _mapper.Map(request, schedule);
            _unitOfWork.SkinTherapistSchedules.Update(schedule);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _unitOfWork.SkinTherapistSchedules.GetByIDAsync(id);
            if (schedule == null) return false;

            _unitOfWork.SkinTherapistSchedules.Delete(schedule);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
