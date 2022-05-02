using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Domain.Models;

namespace VacationRental.Domain.Services.Interface
{
    public interface IBookingService
    {
        ResourceIdViewModel CreateBooking(BookingBindingModel model);
        BookingViewModel GetBooking(int id);
    }
}
