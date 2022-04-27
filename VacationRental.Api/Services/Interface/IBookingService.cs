using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services.Interface
{
    public interface IBookingService
    {
        ResourceIdViewModel CreateBooking(BookingBindingModel model);
        BookingViewModel GetBooking(int id);
    }
}
