using Microsoft.Extensions.Configuration;
using ShModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingData.DatabaseLayer
{

    public class BookingDatabaseAccess : IBookingAccess
    {
        readonly string? _connectionString;

        public BookingDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public BookingDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public int CreateBooking(Booking aBooking)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO Booking (startDateTime, hoursToPlay, bookingNumber, noOfPlayers) OUTPUT INSERTED.ID VALUES (@StartDateTime, @HoursToPlay, @BookingNumber, @NoOfPlayers)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                SqlParameter startDateTimeParam = new SqlParameter("@StartDateTime", aBooking.StartDateTime);
                createCommand.Parameters.Add(startDateTimeParam);

                SqlParameter hoursToPlayParam = new SqlParameter("@HoursToPlay", aBooking.HoursToPlay);
                createCommand.Parameters.Add(hoursToPlayParam);

                SqlParameter bookingNumberParam = new SqlParameter("@BookingNumber", aBooking.BookingNumber);
                createCommand.Parameters.Add(bookingNumberParam);

                SqlParameter noOfPlayersParam = new SqlParameter("@NoOfPlayers", aBooking.NoOfPlayers);
                createCommand.Parameters.Add(noOfPlayersParam);

                con.Open();
                insertedId = (int)createCommand.ExecuteScalar();
            }

            return insertedId;
        }

        public bool DeleteBookingById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM Booking WHERE id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> foundBookings;
            Booking readBooking;
            string queryString = "SELECT id, startDateTime, hoursToPlay, bookingNumber, noOfPlayers FROM Booking";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                SqlDataReader bookingReader = readCommand.ExecuteReader();
                foundBookings = new List<Booking>();

                while (bookingReader.Read())
                {
                    readBooking = GetBookingFromReader(bookingReader);
                    foundBookings.Add(readBooking);
                }
            }

            return foundBookings;
        }

        public Booking GetBookingById(int id)
        {
            Booking foundBooking;
            string queryString = "SELECT id, startDateTime, hoursToPlay, bookingNumber, noOfPlayers FROM Booking WHERE id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);

                con.Open();
                SqlDataReader bookingReader = readCommand.ExecuteReader();
                foundBooking = new Booking();

                while (bookingReader.Read())
                {
                    foundBooking = GetBookingFromReader(bookingReader);
                }
            }

            return foundBooking;
        }
        public bool UpdateBooking(Booking bookingToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Booking SET startDateTime = @StartDateTime, hoursToPlay = @HoursToPlay, bookingNumber = @BookingNumber, noOfPlayers = @NoOfPlayers WHERE id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", bookingToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@StartDateTime", bookingToUpdate.StartDateTime);
                updateCommand.Parameters.AddWithValue("@HoursToPlay", bookingToUpdate.HoursToPlay);
                updateCommand.Parameters.AddWithValue("@BookingNumber", bookingToUpdate.BookingNumber);
                updateCommand.Parameters.AddWithValue("@NoOfPlayers", bookingToUpdate.NoOfPlayers);

                con.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private Booking GetBookingFromReader(SqlDataReader bookingReader)
        {
            Booking foundBooking;
            int tempId = bookingReader.GetInt32(bookingReader.GetOrdinal("id"));
            DateTime tempStartDateTime = bookingReader.GetDateTime(bookingReader.GetOrdinal("startDateTime"));
            int tempHoursToPlay = bookingReader.GetInt32(bookingReader.GetOrdinal("hoursToPlay"));
            int tempBookingNumber = bookingReader.GetInt32(bookingReader.GetOrdinal("bookingNumber"));
            int tempNoOfPlayers = bookingReader.GetInt32(bookingReader.GetOrdinal("noOfPlayers"));

            foundBooking = new Booking(tempId, tempStartDateTime, tempHoursToPlay, tempBookingNumber, tempNoOfPlayers);
            return foundBooking;
        }
    }
}