using BowlingData.DatabaseLayer;
using ShModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BowlingDataTest
{
    public class TestBookingDataAccess
    {
        private readonly ITestOutputHelper _extraOutput;

        readonly private IBookingAccess _bAccess;

        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=BowlingTest";

        public TestBookingDataAccess(ITestOutputHelper output)
        {
            _extraOutput = output;
            _bAccess = new BookingDatabaseAccess(_connectionString);
        }
        [Fact]
        public void TestGetAllBookings()
        {
            // Arrange

            // Act
            List<Booking> readBookings = _bAccess.GetAllBookings();
            bool pricesWereRead = (readBookings.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of bookings: " + readBookings.Count);

            // Assert
            Assert.True(pricesWereRead);
        }
    }
}
