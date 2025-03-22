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
            var allSlots = await _unitOfWork.TimeSlots.GetAllAsync();
            foreach (var slot in allSlots)
            {
                slot.IsAvailable = false;
                _unitOfWork.TimeSlots.Update(slot);
            }

            var currentTime = TimeSpan.FromHours(request.StartTime);
            var endTime = TimeSpan.FromHours(request.EndTime);
            var gapTime = TimeSpan.FromMinutes(request.GapTime);

            while (currentTime + gapTime <= endTime)
            {
                var newSlot = new TimeSlot
                {
                    StartTime = currentTime,
                    EndTime = currentTime + gapTime,
                    IsAvailable = true,
                    Notes = ""
                };
                await _unitOfWork.TimeSlots.CreateAsync(newSlot);
                currentTime += gapTime;
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
