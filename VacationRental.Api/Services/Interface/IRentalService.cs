using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services.Interface
{
    public interface IRentalService
    {
        ResourceIdViewModel CreateRental(RentalBindingModel model);
        RentalViewModel GetRental(int id);
        RentalViewModel UpdateRental(RentalBindingModel model, int rentalId);
    }
}
