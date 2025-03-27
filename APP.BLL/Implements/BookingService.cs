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
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingService> _logger;
        private readonly ISkinTherapistScheduleService _skinTherapistScheduleService;
        private readonly ISkinTherapistService _skinTherapistService;
        private readonly MomoService _momoService;
        private readonly IEmailService _emailService;

        public BookingService
            (IUnitOfWork unitOfWork, IMapper mapper, ILogger<BookingService> logger
            , ISkinTherapistScheduleService skinTherapistScheduleService
            , ISkinTherapistService skinTherapistService, MomoService momoService
            , IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _skinTherapistScheduleService = skinTherapistScheduleService;
            _skinTherapistService = skinTherapistService;
            _momoService = momoService;
            _emailService = emailService ?? throw new ArgumentException(nameof(emailService));
        }

        public async Task<PaginationModel<object>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Bookings.GetQueryable()
                         .AsNoTracking() // Tăng hiệu suất nếu chỉ đọc dữ liệu
                         .Select(b => new
                         {
                             b.BookingId,
                             b.BookingAt,
                             b.Status,
                             b.CheckinAt,
                             b.CheckoutAt,
                             b.TotalPrice,
                             b.Notes,

                             Treatment = new
                             {
                                 b.Treatment.TreatmentId,
                                 b.Treatment.TreatmentName,
                                 BelongToService = new
                                 {
                                     b.Treatment.Service.ServiceId,
                                     b.Treatment.Service.ServiceName,
                                 }
                             },

                             SkinTherapist = b.SkinTherapistId != null ? new
                             {
                                 b.SkinTherapist.AccountId,
                                 b.SkinTherapist.Account.AccountInfo.FullName,
                             } : null,

                             Staff = b.StaffId != null ? new
                             {
                                 b.Staff.AccountId,
                                 b.Staff.Account.AccountInfo.FullName,
                             } : null,

                             Customer = b.CustomerId != null ? new
                             {
                                 b.Customer.AccountId,
                                 b.Customer.Account.AccountInfo.FullName,
                             } : null,

                             Guest = b.GuestId != null ? new
                             {
                                 b.Guest.GuestId,
                                 b.Guest.FullName,
                             } : null,

                             TimeSlots = b.BookingTimeSlots.Select(bts => new
                             {
                                 bts.TimeSlot.TimeSlotId,
                                 bts.TimeSlot.StartTime,
                                 bts.TimeSlot.EndTime,
                             }).ToList(), // Giữ lại để đảm bảo dữ liệu dạng List trong kết quả

                             SlotDate = b.BookingTimeSlots.Select(bts => bts.Date).FirstOrDefault()
                         });

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var bookings = await query
                .OrderBy(a => a.BookingId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<object>
            {
                Items = bookings,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<PaginationModel<object>> GetAllByCustomerIdAsync(int customerId, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Bookings.GetQueryable()
                         .AsNoTracking() // Tăng hiệu suất nếu chỉ đọc dữ liệu
                         .Where(b => b.CustomerId == customerId)
                         .Select(b => new
                         {
                             b.BookingId,
                             b.BookingAt,
                             b.Status,
                             b.CheckinAt,
                             b.CheckoutAt,
                             b.TotalPrice,
                             b.Notes,

                             Treatment = new
                             {
                                 b.Treatment.TreatmentId,
                                 b.Treatment.TreatmentName,
                                 BelongToService = new
                                 {
                                     b.Treatment.Service.ServiceId,
                                     b.Treatment.Service.ServiceName,
                                 }
                             },

                             SkinTherapist = b.SkinTherapistId != null ? new
                             {
                                 b.SkinTherapist.AccountId,
                                 b.SkinTherapist.Account.AccountInfo.FullName,
                             } : null,

                             Staff = b.StaffId != null ? new
                             {
                                 b.Staff.AccountId,
                                 b.Staff.Account.AccountInfo.FullName,
                             } : null,

                             Customer = b.CustomerId != null ? new
                             {
                                 b.Customer.AccountId,
                                 b.Customer.Account.AccountInfo.FullName,
                             } : null,

                             Guest = b.GuestId != null ? new
                             {
                                 b.Guest.GuestId,
                                 b.Guest.FullName,
                             } : null,

                             TimeSlots = b.BookingTimeSlots.Select(bts => new
                             {
                                 bts.TimeSlot.TimeSlotId,
                                 bts.TimeSlot.StartTime,
                                 bts.TimeSlot.EndTime,
                             }).ToList(), // Giữ lại để đảm bảo dữ liệu dạng List trong kết quả

                             SlotDate = b.BookingTimeSlots.Select(bts => bts.Date).FirstOrDefault()
                         });

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var bookings = await query
                .OrderBy(a => a.BookingId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<object>
            {
                Items = bookings,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<PaginationModel<object>> GetAllByEmailAsync(string email, int pageNumber, int pageSize)
        {
            var query = _unitOfWork.Bookings.GetQueryable()
                         .AsNoTracking() // Tăng hiệu suất nếu chỉ đọc dữ liệu
                         .Where(b => b.Guest.Email == email || b.Customer.Account.Email == email)
                         .Select(b => new
                         {
                             b.BookingId,
                             b.BookingAt,
                             b.Status,
                             b.CheckinAt,
                             b.CheckoutAt,
                             b.TotalPrice,
                             b.Notes,

                             Treatment = new
                             {
                                 b.Treatment.TreatmentId,
                                 b.Treatment.TreatmentName,
                                 BelongToService = new
                                 {
                                     b.Treatment.Service.ServiceId,
                                     b.Treatment.Service.ServiceName,
                                 }
                             },

                             SkinTherapist = b.SkinTherapistId != null ? new
                             {
                                 b.SkinTherapist.AccountId,
                                 b.SkinTherapist.Account.AccountInfo.FullName,
                             } : null,

                             Staff = b.StaffId != null ? new
                             {
                                 b.Staff.AccountId,
                                 b.Staff.Account.AccountInfo.FullName,
                             } : null,

                             Customer = b.CustomerId != null ? new
                             {
                                 b.Customer.AccountId,
                                 b.Customer.Account.AccountInfo.FullName,
                             } : null,

                             Guest = b.GuestId != null ? new
                             {
                                 b.Guest.GuestId,
                                 b.Guest.FullName,
                             } : null,

                             TimeSlots = b.BookingTimeSlots.Select(bts => new
                             {
                                 bts.TimeSlot.TimeSlotId,
                                 bts.TimeSlot.StartTime,
                                 bts.TimeSlot.EndTime,
                             }).ToList(), // Giữ lại để đảm bảo dữ liệu dạng List trong kết quả

                             SlotDate = b.BookingTimeSlots.Select(bts => bts.Date).FirstOrDefault()
                         });

            var totalRecords = await query.CountAsync(); // Tổng số bản ghi
            var bookings = await query
                .OrderBy(a => a.BookingId) // Sắp xếp (có thể thay đổi)
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các bản ghi trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cần lấy
                .ToListAsync();

            return new PaginationModel<object>
            {
                Items = bookings,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<object?> GetByIDAsync(int id)
        {
            var booking = _unitOfWork.Bookings.GetQueryable()
                         .AsNoTracking() // Tăng hiệu suất nếu chỉ đọc dữ liệu
                         .Where(a => a.BookingId == id)
                         .Select(b => new
                         {
                             b.BookingId,
                             b.BookingAt,
                             b.Status,
                             b.CheckinAt,
                             b.CheckoutAt,
                             b.TotalPrice,
                             b.Notes,

                             Treatment = new
                             {
                                 b.Treatment.TreatmentId,
                                 b.Treatment.TreatmentName,
                                 BelongToService = new
                                 {
                                     b.Treatment.Service.ServiceId,
                                     b.Treatment.Service.ServiceName,
                                 }
                             },

                             SkinTherapist = b.SkinTherapistId != null ? new
                             {
                                 b.SkinTherapist.AccountId,
                                 b.SkinTherapist.Account.AccountInfo.FullName,
                             } : null,

                             Staff = b.StaffId != null ? new
                             {
                                 b.Staff.AccountId,
                                 b.Staff.Account.AccountInfo.FullName,
                             } : null,

                             Customer = b.CustomerId != null ? new
                             {
                                 b.Customer.AccountId,
                                 b.Customer.Account.AccountInfo.FullName,
                             } : null,

                             Guest = b.GuestId != null ? new
                             {
                                 b.Guest.GuestId,
                                 b.Guest.FullName,
                             } : null,

                             TimeSlots = b.BookingTimeSlots.Select(bts => new
                             {
                                 bts.TimeSlot.TimeSlotId,
                                 bts.TimeSlot.StartTime,
                                 bts.TimeSlot.EndTime,
                             }).ToList(), // Giữ lại để đảm bảo dữ liệu dạng List trong kết quả

                             SlotDate = b.BookingTimeSlots.Select(bts => bts.Date).FirstOrDefault()
                         });
            return booking.Any() ? booking.ElementAt(0) : null;
        }

        public async Task<object?> CreateAsync(BookingCreationRequest request)
        {
            var customerAccount = await _unitOfWork.Accounts.GetByEmailAsync(request.Email);
            if (customerAccount != null && customerAccount.Role != "Customer")
            {
                throw new Exception("Email exist in another role.");
            }

            var therapistPaging = await _skinTherapistService.GetAllFreeInSlotAsync(request.Date, request.TimeSlotIds, 1, 100);
            if (request.SkinTherapistId != null && !therapistPaging.Items.Any(s => s.AccountId == request.SkinTherapistId))
            {
                throw new Exception("Therapist is busy.");
            }

            var payment = new Payment
            {
                PaymentMethod = request.PaymentMethod,
            };
            string orderInfo = "Seoul Spa - ";

            var result = await _unitOfWork.SaveWithTransactionAsync(async () =>
            {
                var booking = _mapper.Map<Booking>(request);
                if (customerAccount == null)
                {
                    var existingGuest = await _unitOfWork.Guests.GetByEmailAsync(request.Email);
                    if (existingGuest == null)
                    {
                        var guest = new Guest
                        {
                            Email = request.Email,
                            Phone = request.Phone,
                            FullName = request.FullName,
                        };
                        var createdGuest = await _unitOfWork.Guests.CreateAsync(guest);
                        await _unitOfWork.SaveAsync();
                        booking.GuestId = createdGuest.GuestId;
                    }
                    else
                    {
                        booking.GuestId = existingGuest.GuestId;
                    }
                }
                else
                {
                    booking.CustomerId = customerAccount.AccountId;
                }

                var treatment = await _unitOfWork.Treatments.GetQueryable()
                    .Include(t => t.Service)
                    .FirstOrDefaultAsync(t => t.TreatmentId == request.TreatmentId);

                if (treatment != null && treatment.Price > 0)
                {
                    booking.TotalPrice = treatment.Price;
                    //booking.TotalPrice = treatment.Price * request.TimeSlotIds.Count();
                }
                orderInfo += treatment.TreatmentName;
                var createdBooking = await _unitOfWork.Bookings.CreateAsync(booking);
                await _unitOfWork.SaveAsync();

                payment.BookingId = createdBooking.BookingId;
                payment.Amount = createdBooking.TotalPrice;

                await _unitOfWork.Payments.CreateAsync(payment);
                await _unitOfWork.SaveAsync();

                if (request.SkinTherapistId != null)
                {
                    var skinTherapist = await _unitOfWork.SkinTherapists.GetByIDAsync(request.SkinTherapistId.Value);
                    if (skinTherapist != null)
                    {
                        await _skinTherapistScheduleService.UpdateScheduleAvailabilityAsync(skinTherapist.AccountId, request.Date, request.TimeSlotIds);
                    }
                }

                foreach (var timeSlotId in request.TimeSlotIds)
                {
                    var bookingTimeSlot = new BookingTimeSlot
                    {
                        BookingId = createdBooking.BookingId,
                        TimeSlotId = timeSlotId,
                        Date = request.Date,
                    };
                    await _unitOfWork.BookingTimeSlots.CreateAsync(bookingTimeSlot);
                }

                var timeSlotDetails = string.Join(", ", request.TimeSlotIds.Select(async timeSlotId =>
                {
                    var timeSlot = await _unitOfWork.TimeSlots.GetByIDAsync(timeSlotId);
                    return $"{timeSlot.StartTime} - {timeSlot.EndTime}";
                }).Select(t => t.Result));

                var placeholders = new Dictionary<string, string>
                {
                    { "Subject", "Booking Confirmation" },
                    { "UserName", request.FullName },
                    { "BookingDate", request.Date.ToString("dd-MM-yyyy") },
                    { "Time", timeSlotDetails },
                    { "TreatmentName", treatment.TreatmentName },
                    { "ServiceName", treatment.Service?.ServiceName ?? "N/A" },
                    { "SkinTherapist", request.SkinTherapistId.HasValue ? (await _unitOfWork.SkinTherapists.GetByIDAsync(request.SkinTherapistId.Value)).Account.AccountInfo.FullName : "N/A" },
                    { "TotalPrice", booking.TotalPrice.ToString("0") },
                    { "Notes", request.Notes ?? "N/A" }
};

                await _emailService.SendEmail(request.Email, "BookingEmail", placeholders);
            }) > 0;

            if (result)
            {
                var paymentUrl = await _momoService.CreatePaymentUrl
                    ($"{payment.BookingId}SS{DateTime.Now:yyyyMMddHHmmss}", payment.Amount.ToString("0"), orderInfo, payment.PaymentMethod);
                return paymentUrl;
            }

            return null;
        }

        public async Task<bool> UpdateAsync(int id, BookingUpdationRequest request)
        {
            var booking = await _unitOfWork.Bookings.GetByIDAsync(id);
            if (booking == null) return false;
            _mapper.Map(request, booking);
            _unitOfWork.Bookings.Update(booking);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIDAsync(id);
            if (booking == null) return false;
            //_unitOfWork.Bookings.Delete(booking);
            booking.Status = "Cancelled";
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
