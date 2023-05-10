using ShModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingData.DatabaseLayer
{
    public interface IBookingAccess
    {
        Booking GetBookingById(int id);
        List<Booking> GetAllBookings();
        int CreateBooking(Booking aBooking);
        bool UpdateBooking(Booking BookingToUpdate);
        bool DeleteBookingById(int id);
    }
}
