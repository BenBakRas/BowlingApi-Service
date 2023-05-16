using BowlingApiService.DTOs;
using BowlingData.DatabaseLayer;
using Microsoft.AspNetCore.Mvc;
using BowlingData.ModelLayer;

namespace BowlingApiService.BusinessLogicLayer
{
    public class BookingdataControl : IBookingData
    {
        private readonly IBookingAccess _bookingAccess;

        public BookingdataControl(IBookingAccess inBookingAccess)
        {
            _bookingAccess = inBookingAccess;
        }

        public int Add(BookingDto newBooking)
        {
            int insertedId = 0;
            try
            {
                Booking? foundBooking = ModelConversion.BookingDtoConvert.ToBooking(newBooking);
                if (foundBooking != null)
                {
                    insertedId = _bookingAccess.CreateBooking(foundBooking);
                }
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

        public bool Delete(int id)
        {
            try
            {
                bool isDeleted = _bookingAccess.DeleteBookingById(id);
                return isDeleted;
            }
            catch
            {
                return false;
            }
        }

        public BookingDto? Get(int id)
        {
            BookingDto? foundBookingDto;
            try
            {
                Booking? foundBooking = _bookingAccess.GetBookingById(id);
                foundBookingDto = ModelConversion.BookingDtoConvert.FromBooking(foundBooking);
            }
            catch
            {
                foundBookingDto = null;
            }
            return foundBookingDto;
        }

        public List<BookingDto>? Get()
        {
            List<BookingDto>? foundDtos;
            try
            {
                List<Booking>? foundBookings = _bookingAccess.GetAllBookings();
                foundDtos = ModelConversion.BookingDtoConvert.FromBookingCollection(foundBookings);
            }
            catch
            {
                foundDtos = null;
            }
            return foundDtos;
        }
        public bool Put(BookingDto bookingToUpdate, int idToUpdate)
        {
            try
            {
                Booking? updatedBooking = ModelConversion.BookingDtoConvert.ToBooking(bookingToUpdate, idToUpdate);
                return _bookingAccess.UpdateBooking(updatedBooking);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine(ex);
                return false;
            }

        }
        
    }
}


