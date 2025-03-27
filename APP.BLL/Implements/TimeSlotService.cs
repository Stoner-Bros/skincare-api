using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace APP.BLL.Implements
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TimeSlotService> _logger;

        public TimeSlotService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TimeSlotService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> DeleteMultipleAsync(int[] ids)
        {
            var slotsToDelete = await _unitOfWork.TimeSlots.FindAllAsync(slot => ids.Contains(slot.TimeSlotId));
            if (!slotsToDelete.Any()) return false;
            _unitOfWork.TimeSlots.DeleteRange(slotsToDelete);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<TimeSlotResponse>> GetAllAsync()
        {
            var timeSlots = await _unitOfWork.TimeSlots.GetAllAsync();
            return _mapper.Map<IEnumerable<TimeSlotResponse>>(timeSlots);
        }

        public async Task<TimeSlotResponse?> GetByIDAsync(int id)
        {
            var timeSlot = await _unitOfWork.TimeSlots.GetByIDAsync(id);
            return timeSlot == null ? null : _mapper.Map<TimeSlotResponse>(timeSlot);
        }

        public async Task<bool> InitAgainAsync(TimeSlotInitRequest request)
        {
            // Lấy tất cả slot hiện có
            var existingSlots = await _unitOfWork.TimeSlots.GetAllAsync();

            var currentTime = TimeSpan.FromHours(request.StartTime);
            var endTime = TimeSpan.FromHours(request.EndTime);
            var gapTime = TimeSpan.FromMinutes(request.GapTime);

            // Tạo danh sách slot mới dựa trên request
            var newSlots = new List<TimeSlot>();
            while (currentTime + gapTime <= endTime)
            {
                newSlots.Add(new TimeSlot
                {
                    StartTime = currentTime,
                    EndTime = currentTime + gapTime,
                    IsAvailable = true,
                    Notes = ""
                });
                currentTime += gapTime;
            }

            // Xử lý slot cũ: Disable nếu không nằm trong danh sách slot mới
            foreach (var slot in existingSlots)
            {
                var match = newSlots.FirstOrDefault(s => s.StartTime == slot.StartTime && s.EndTime == slot.EndTime);
                if (match == null)
                {
                    slot.IsAvailable = false; // Disable slot cũ không tồn tại trong danh sách mới
                    _unitOfWork.TimeSlots.Update(slot);
                }
                else if (!slot.IsAvailable)
                {
                    slot.IsAvailable = true; // Bỏ disable nếu slot đã tồn tại nhưng đang bị disable
                    _unitOfWork.TimeSlots.Update(slot);
                }
            }

            // Thêm slot mới chưa có trong database
            foreach (var newSlot in newSlots)
            {
                if (!existingSlots.Any(s => s.StartTime == newSlot.StartTime && s.EndTime == newSlot.EndTime))
                {
                    await _unitOfWork.TimeSlots.CreateAsync(newSlot);
                }
            }

            return await _unitOfWork.SaveAsync() > 0;
        }


        public async Task<bool> Toogle(int id, string note)
        {
            var timeSlot = await _unitOfWork.TimeSlots.GetByIDAsync(id);
            if (timeSlot == null) return false;

            timeSlot.IsAvailable = !timeSlot.IsAvailable;
            timeSlot.Notes = note;
            _unitOfWork.TimeSlots.Update(timeSlot);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
