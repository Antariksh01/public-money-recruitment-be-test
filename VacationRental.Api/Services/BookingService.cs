using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using VacationRental.Api.Services.Interface;

namespace VacationRental.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;

        public BookingService(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        public BookingViewModel GetBooking(int id)
        {
            if (!_bookings.ContainsKey(id))
                throw new ApplicationException("Booking not found");

            return _bookings[id];
        }
        public ResourceIdViewModel CreateBooking(BookingBindingModel model)
        {

            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");

            var rental = _rentals.Values.FirstOrDefault(x => x.Id == model.RentalId);
            if (rental == null)
                throw new ApplicationException("Rental not found");

            var units = GetAvailableUnits(model, rental);
           
            var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

            _bookings.Add(key.Id, new BookingViewModel
            {
                Id = key.Id,
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start.Date,
                Unit = units
            });

            return key;

        }

        public int GetAvailableUnits(BookingBindingModel model, RentalViewModel rental)
        {

            int unitsAvailable = 1;

            var bookings = _bookings.Values.Where(x => x.RentalId == model.RentalId && IsDateAvailable(x, model, rental));

            if (bookings == null || !bookings.Any())
            {
                return unitsAvailable;
            }


            for (int i = unitsAvailable; i <= rental.Units; i++)
            {
                if (bookings.All(x => x.Unit != i))
                {
                    return i;
                }
            }

            throw new ApplicationException("Not Available");

        }

        public bool IsDateAvailable(BookingViewModel booking, BookingBindingModel model, RentalViewModel rental)
        {

            if ((booking.Start <= model.Start.Date && booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays) > model.Start.Date)
                        || (booking.Start < model.Start.AddDays(model.Nights + rental.PreparationTimeInDays) && booking.Start.AddDays(booking.Nights) >= model.Start.AddDays(model.Nights))
                        || (booking.Start > model.Start && booking.Start.AddDays(booking.Nights) < model.Start.AddDays(model.Nights)))
            {
                return true;
            }

            else
                return false;

        }

    }
}
