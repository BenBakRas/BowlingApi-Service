using BowlingApiService.DTOs;
using BowlingData.DatabaseLayer;
using Microsoft.AspNetCore.Mvc;
using BowlingData.ModelLayer;
using BowlingApiService.ModelConversion;
using System.Diagnostics.Metrics;

namespace BowlingApiService.BusinessLogicLayer
{
    public class BookingdataControl : IBookingData
    {
        private readonly IBookingAccess _bookingAccess;

        public BookingdataControl(IBookingAccess inBookingAccess)
        {
            _bookingAccess = inBookingAccess;
        }
        /*
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
        */
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
                foundBookingDto = BookingDtoConvert.FromBooking(foundBooking);

                // Retrieve Price and Lane information from their respective tables
                if (foundBooking != null)
                {
                    foundBookingDto.PriceId = foundBooking.PriceId;
                    foundBookingDto.LaneId = foundBooking.LaneId;
                }
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

                // Retrieve Price and Lane information from their respective tables
                if (foundDtos != null)
                {
                    for (int i = 0; i < foundDtos.Count; i++)
                    {
                        Booking? foundBooking = foundBookings.ElementAtOrDefault(i);
                        if (foundBooking != null)
                        {
                            foundDtos[i].PriceId = foundBooking.PriceId;
                            foundDtos[i].LaneId = foundBooking.LaneId;
                            foundDtos[i].Id = foundBooking.Id;
                        }
                    }
                }
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
        public List<BookingDto>? GetBookingsByCustomerPhone(string phoneNumber)
        {
            List<BookingDto>? foundDtos;
            try
            {
                List<Booking>? foundBookings = _bookingAccess.GetBookingsByCustomerPhone(phoneNumber);
                foundDtos = ModelConversion.BookingDtoConvert.FromBookingCollection(foundBookings);

                // Assign the booking ID to each BookingDto
                if (foundDtos != null)
                {
                    for (int i = 0; i < foundDtos.Count; i++)
                    {
                        Booking? foundBooking = foundBookings.ElementAtOrDefault(i);
                        if (foundBooking != null)
                        {
                            foundDtos[i].Id = foundBooking.Id;
                        }
                    }
                }
            }
            catch
            {
                foundDtos = null;
            }
            return foundDtos;
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

                    // Create the price booking
                    bool priceBookingCreated = _bookingAccess.CreatePriceBooking(newBooking.PriceId, insertedId);

                    // Create the lane booking
                    bool laneBookingCreated = _bookingAccess.CreateLaneBooking(newBooking.LaneId, insertedId);

                    // Check if both price and lane bookings were created successfully
                    if (!priceBookingCreated || !laneBookingCreated)
                    {
                        // Handle the error, such as rolling back the database transaction or removing the created booking
                        // Set insertedId to -1 or throw an exception to indicate a failure
                    }
                }
            }
            catch
            {
                insertedId = -1;
            }
            return insertedId;
        }

    }

}
