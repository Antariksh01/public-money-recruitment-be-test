using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Domain.Exception;
using VacationRental.Domain.Models;
using VacationRental.Domain.Services.Interface;

namespace VacationRental.Domain.Services
{
    public class RentalService : IRentalService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        public RentalService(IDictionary<int, RentalViewModel> rentals)
        {
            _rentals = rentals;
        }
        public RentalViewModel GetRental(int id)
        {

            if (!_rentals.ContainsKey(id))
                throw new NotFoundException("Rental not found");

            return _rentals[id];
        }
        public ResourceIdViewModel CreateRental(RentalBindingModel model)
        {

            if (model.Units <= 0)
                throw new ValidationException("Rentals should have minimum 1 unit");

            else if (model.PreparationTimeInDays < 0)
                throw new ValidationException("Preparation time cannot be negative");

            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalViewModel
            {
                Id = key.Id,
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });

            return key;
        }

        public RentalViewModel UpdateRental(int rentalId, RentalBindingModel model)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var rentalToUpdate = _rentals[rentalId];

            rentalToUpdate.Units = model.Units;
            rentalToUpdate.PreparationTimeInDays = model.PreparationTimeInDays;

            return rentalToUpdate;
        }
    }
}
