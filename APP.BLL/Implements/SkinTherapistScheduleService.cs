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

        public async Task<PaginationModel<SkinTherapistScheduleResponse>> GetAllAsync(int therapistId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.SkinTherapistSchedules.GetQueryable()
                                    .Where(s => s.SkinTherapistId == therapistId);  // Truy vấn dữ liệu

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

        public async Task<SkinTherapistScheduleResponse?> CreateAsync(SkinTherapistScheduleCreationRequest request)
        {
            var schedule = _mapper.Map<SkinTherapistSchedule>(request);
            var timeSlot = await _unitOfWork.TimeSlots.GetByIDAsync(request.TimeSlotId);

            if (timeSlot == null) return null;
            schedule.StartTime = timeSlot.StartTime;
            schedule.EndTime = timeSlot.EndTime;

            try
            {
                await _unitOfWork.SkinTherapistSchedules.CreateAsync(schedule);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating schedule");
                return null;
            }
            return _mapper.Map<SkinTherapistScheduleResponse>(schedule);
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
