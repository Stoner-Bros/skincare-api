using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.DAL;
using APP.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.BLL.BackgroundTask
{
    public class BookingStatusUpdater : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BookingStatusUpdater> _logger;

        public BookingStatusUpdater(IServiceScopeFactory scopeFactory, ILogger<BookingStatusUpdater> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        var scheduleService = scope.ServiceProvider.GetRequiredService<ISkinTherapistScheduleService>();

                        var fifteenMinutesAgo = DateTime.Now.AddMinutes(-100);

                        var bookingsToUpdate = await dbContext.Bookings
                            .Where(b => b.Status == "Pending" && b.BookingAt <= fifteenMinutesAgo)
                            .ToListAsync(stoppingToken);

                        if (bookingsToUpdate.Any())
                        {
                            foreach (var booking in bookingsToUpdate)
                            {
                                booking.Status = "Cancelled";

                                var bookingDetail = await dbContext.Bookings
                                            .Include(b => b.Payment)
                                            .Include(b => b.SkinTherapist)
                                            .Include(b => b.BookingTimeSlots)
                                            .FirstOrDefaultAsync(b => b.BookingId == booking.BookingId);

                                if (bookingDetail.Payment != null)
                                {
                                    bookingDetail.Payment.PaymentStatus = "Cancelled";
                                }

                                if (booking.SkinTherapistId != null && booking.SkinTherapist != null && booking.BookingTimeSlots.Count != 0)
                                {
                                    await scheduleService.ReverseScheduleAsync(
                                        booking.SkinTherapist.AccountId,
                                        booking.BookingTimeSlots.First().Date,
                                        [.. booking.BookingTimeSlots.Select(b => b.TimeSlotId)]
                                    );
                                }
                            }

                            await dbContext.SaveChangesAsync(stoppingToken);
                            _logger.LogInformation($"{bookingsToUpdate.Count} bookings have been updated to 'Cancelled'.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating booking statuses.");
                }

                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
