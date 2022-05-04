using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Domain.Models;

namespace VacationRental.Domain.Services.Interface
{
    public interface ICalendarService
    {
        CalendarViewModel GetCalendar(int rentalId, DateTime start, int nights);
    }
}
