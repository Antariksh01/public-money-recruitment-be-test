using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services.Interface
{
    public interface ICalendarService
    {
        CalendarViewModel GetCalendar(int rentalId, DateTime start, int nights);
    }
}
