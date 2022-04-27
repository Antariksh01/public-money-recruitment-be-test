using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Api.Services;
using VacationRental.Api.Services.Interface;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;

        private readonly IRentalService _rentalService;

        public RentalsController(IDictionary<int, RentalViewModel> rentals, IRentalService rentalService)
        {
            _rentals = rentals;
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            //var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            //_rentals.Add(key.Id, new RentalViewModel
            //{
            //    Id = key.Id,
            //    Units = model.Units,
            //    PreparationTimeInDays = model.PreparationTimeInDays
            //});
            return _rentalService.CreateRental(model);
        }
    }
}
