using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using VacationRental.Api.Services.Interface;

namespace VacationRental.Api.Services
{
    public class RentalService : IRentalService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        public RentalService(IDictionary<int, RentalViewModel> rentals)
        {
            _rentals = rentals;
        }
        public ResourceIdViewModel CreateRental(RentalBindingModel model) {

            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalViewModel
            {
                Id = key.Id,
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });

            return key;
        }
    }
}
