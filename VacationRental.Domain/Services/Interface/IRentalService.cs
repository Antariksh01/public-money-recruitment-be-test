using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Domain.Models;

namespace VacationRental.Domain.Services.Interface
{
    public interface IRentalService
    {
        ResourceIdViewModel CreateRental(RentalBindingModel model);
        RentalViewModel GetRental(int id);
        RentalViewModel UpdateRental(int rentalId, RentalBindingModel model);
    }
}
