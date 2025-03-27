using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APP.BLL.Implements
{
    public class OverviewService : IOverviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OverviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OverviewResponse> GetOverviewAsync(OverviewRequest request)
        {
            var startDate = request.StartDate.ToDateTime(TimeOnly.MinValue);
            var endDate = request.EndDate.ToDateTime(TimeOnly.MaxValue);

            var totalUsers = await _unitOfWork.Customers.GetQueryable()
                .CountAsync(a => a.Account.CreatedAt >= startDate && a.Account.CreatedAt <= endDate);

            var totalBookings = await _unitOfWork.Bookings.GetQueryable()
                .Where(b => b.Status == "Confirmed")
                .CountAsync(b => b.BookingAt >= startDate && b.BookingAt <= endDate);

            var totalBlogs = await _unitOfWork.Blogs.GetQueryable()
                .Where(b => b.PublishAt != null)
                .CountAsync(b => b.CreatedAt >= startDate && b.CreatedAt <= endDate);

            var totalRevenue = await _unitOfWork.Payments.GetQueryable()
                .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate
                        && p.PaymentStatus == "Paid")
                .SumAsync(p => p.Amount);

            return new OverviewResponse
            {
                TotalUsers = totalUsers,
                TotalBookings = totalBookings,
                TotalBlogs = totalBlogs,
                TotalRevenue = totalRevenue
            };
        }
    }
}
