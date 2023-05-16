﻿using BowlingApiService.BusinessLogicLayer;
using BowlingApiService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BowlingApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingData _businessLogicCtrl;

        public BookingsController(IBookingData inBusinessLogicCtrl)
        {
            _businessLogicCtrl = inBusinessLogicCtrl;
        }

        // URL: api/bookings
        [HttpGet]
        public ActionResult<List<BookingDto>> Get()
        {
            ActionResult<List<BookingDto>> foundReturn;
            // retrieve data - converted to DTO
            List<BookingDto>? foundBookings = _businessLogicCtrl.Get();
            // evaluate
            if (foundBookings != null)
            {
                if (foundBookings.Count > 0)
                {
                    foundReturn = Ok(foundBookings);                 // Statuscode 200
                }
                else
                {
                    foundReturn = new StatusCodeResult(204);    // Ok, but no content
                }
            }
            else
            {
                foundReturn = new StatusCodeResult(500);        // Internal server error
            }
            // send response back to client
            return foundReturn;
        }


        // URL: api/bookings/{id}
        [HttpGet, Route("{id}")]
        public ActionResult<BookingDto> Get(int id)
        {
            ActionResult<BookingDto> foundReturn;
            // retrieve data - converted to DTO
            BookingDto? foundBooking = _businessLogicCtrl.Get(id);
            // evaluate
            if (foundBooking != null)
            {
                foundReturn = Ok(foundBooking);       // Statuscode 200
            }
            else
            {
                foundReturn = new StatusCodeResult(404);    // Not found
            }
            // send response back to client
            return foundReturn;
        }

        // URL: api/bookings
        [HttpPost]
        public ActionResult<int> PostNewBooking(BookingDto inBookingDto)
        {
            ActionResult<int> foundReturn;
            int insertedId = -1;
            if (inBookingDto != null)
            {
                insertedId = _businessLogicCtrl.Add(inBookingDto);
            }
            // Evaluate
            if (insertedId > 0)
            {
                foundReturn = Ok(insertedId);
            }
            else if (insertedId == 0)
            {
                foundReturn = BadRequest();     // missing input
            }
            else
            {
                foundReturn = new StatusCodeResult(500);    // Internal server error
            }
            return foundReturn;
        }
        // URL: api/bookings/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ActionResult foundReturn;
            bool isDeleted = _businessLogicCtrl.Delete(id);
            // Evaluate
            if (isDeleted)
            {
                foundReturn = Ok(isDeleted);           // Statuscode 200
            }
            else
            {
                foundReturn = new StatusCodeResult(404);    // Not found
            }
            // send response back to client
            return foundReturn;
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] BookingDto updatedBookingDto)
        {
            if (updatedBookingDto == null)
            {
                return BadRequest();    // Bad request, missing input
            }

            // Retrieve the existing booking details
            BookingDto? existingBookingDto = _businessLogicCtrl.Get(id);

            if (existingBookingDto == null)
            {
                return NotFound();    // Booking not found
            }

            // Update the existing booking details with the new values
            existingBookingDto.StartDateTime = updatedBookingDto.StartDateTime;
            existingBookingDto.HoursToPlay = updatedBookingDto.HoursToPlay;
            existingBookingDto.NoOfPlayers = updatedBookingDto.NoOfPlayers;
            existingBookingDto.Customer = updatedBookingDto.Customer;

            bool isUpdated = _businessLogicCtrl.Put(existingBookingDto, id);
            if (isUpdated)
            {
                return Ok(isUpdated);     // Statuscode 200
            }
            else
            {
                return StatusCode(500); // Internal server error
            }
        }
    }
}