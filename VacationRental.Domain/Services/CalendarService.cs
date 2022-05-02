using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Domain.Exception;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services.Interface;

namespace VacationRental.Domain.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;

        public CalendarService(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }
        public CalendarViewModel GetCalendar(int rentalId, DateTime start, int nights)
        {
            try
            {

                if (nights < 0)
                    throw new ValidationException("Nights must be positive");

                if (!_rentals.ContainsKey(rentalId))
                    throw new NotFoundException("Rental not found");

                var rental = _rentals[rentalId];

                var result = new CalendarViewModel
                {
                    RentalId = rentalId,
                    Dates = new List<CalendarDateViewModel>()
                };

                for (var i = 0; i < nights; i++)
                {
                    var date = new CalendarDateViewModel
                    {
                        Date = start.Date.AddDays(i),
                        Bookings = new List<CalendarBookingViewModel>(),
                        PreparationTimeInDays = new List<PreparationTimeViewModel>()
                    };

                    foreach (var booking in _bookings.Values)
                    {
                        if (booking.RentalId == rentalId)
                        {
                            if (BookingOverlap(booking, date))
                            {
                                date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id, Unit = booking.Unit });
                            }
                            else if (PreparationOverlap(booking, date, rental))
                            {
                                date.PreparationTimeInDays.Add(new PreparationTimeViewModel { Unit = booking.Unit });
                            }
                        }
                    }

                    result.Dates.Add(date);
                }

                return result;
            }
            catch
            {
                throw;
            }

        }

        public bool BookingOverlap(BookingViewModel booking, CalendarDateViewModel date)
        {

            return booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date;

        }

        public bool PreparationOverlap(BookingViewModel booking, CalendarDateViewModel date, RentalViewModel rental)
        {

            return booking.Start <= date.Date && booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays) > date.Date;

        }
    }
}
