using Microsoft.Extensions.Configuration;
using BowlingData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
            string insertString = "INSERT INTO Booking (startDateTime, hoursToPlay, customerID, noOfPlayers) OUTPUT INSERTED.ID VALUES (@StartDateTime, @HoursToPlay, @Customer, @NoOfPlayers)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                SqlParameter startDateTimeParam = new SqlParameter("@StartDateTime", aBooking.StartDateTime);
                createCommand.Parameters.Add(startDateTimeParam);

                SqlParameter hoursToPlayParam = new SqlParameter("@HoursToPlay", aBooking.HoursToPlay);
                createCommand.Parameters.Add(hoursToPlayParam);

                SqlParameter CustomerNumberParam = new SqlParameter("@Customer", aBooking.Customer.Id);
                createCommand.Parameters.Add(CustomerNumberParam);

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
            string queryString = "select id, hoursToPlay, startDateTime, noOfPlayers, customerID from Booking";

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
        public List<Booking> GetBookingsByCustomerPhone(string phone)
        {
            List<Booking> bookings = new List<Booking>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string queryString = "SELECT b.*, c.* FROM Booking b INNER JOIN Customer c ON b.CustomerID = c.id WHERE phone = @Phone";
                    SqlCommand command = new SqlCommand(queryString, con);
                    command.Parameters.AddWithValue("@Phone", phone);

                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StartDateTime = reader.GetDateTime(reader.GetOrdinal("StartDateTime")),
                            HoursToPlay = reader.GetInt32(reader.GetOrdinal("HoursToPlay")),
                            NoOfPlayers = reader.GetInt32(reader.GetOrdinal("NoOfPlayers")),
                            Customer = new Customer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("customerID")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone"))
                            }
                        };
                        bookings.Add(booking);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately or log the error
                Console.WriteLine($"An error occurred while retrieving bookings for customer phone {phone}: {ex.Message}");
                return null;
            }

            return bookings;
        }
        public List<Booking> GetBookingsByCId(int customerId)
        {
            List<Booking> bookings = new List<Booking>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string queryString = "SELECT * FROM Booking WHERE CustomerId = @CustomerId";
                    SqlCommand command = new SqlCommand(queryString, con);
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            StartDateTime = reader.GetDateTime(reader.GetOrdinal("StartDateTime")),
                            HoursToPlay = reader.GetInt32(reader.GetOrdinal("HoursToPlay")),
                            NoOfPlayers = reader.GetInt32(reader.GetOrdinal("NoOfPlayers"))

                        };
                        Customer cus = GetCustomerById(customerId);
                        booking.Customer = cus;
                        bookings.Add(booking);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately or log the error
                Console.WriteLine($"An error occurred while retrieving bookings for customer ID {customerId}: {ex.Message}");
                return null;
            }

            return bookings;
        }

        public bool UpdateBooking(Booking bookingToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Booking SET startDateTime = @StartDateTime, hoursToPlay = @HoursToPlay, customerID = @CustomerID, noOfPlayers = @NoOfPlayers WHERE id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", bookingToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@StartDateTime", bookingToUpdate.StartDateTime);
                updateCommand.Parameters.AddWithValue("@HoursToPlay", bookingToUpdate.HoursToPlay);
                updateCommand.Parameters.AddWithValue("@CustomerID", bookingToUpdate.Customer.Id);
                updateCommand.Parameters.AddWithValue("@NoOfPlayers", bookingToUpdate.NoOfPlayers);

                con.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private Booking GetBookingFromReader(SqlDataReader bookingReader)
        {
            int customerId = bookingReader.GetInt32(bookingReader.GetOrdinal("CustomerID"));

            Customer customer = GetCustomerById(customerId); // Call a method to retrieve the customer by ID

            Booking foundBooking;
            int tempId = bookingReader.GetInt32(bookingReader.GetOrdinal("id"));
            DateTime tempStartDateTime = bookingReader.GetDateTime(bookingReader.GetOrdinal("StartDateTime"));
            int tempHoursToPlay = bookingReader.GetInt32(bookingReader.GetOrdinal("HoursToPlay"));
            int tempNoOfPlayers = bookingReader.GetInt32(bookingReader.GetOrdinal("NoOfPlayers"));

            foundBooking = new Booking(tempId, tempStartDateTime, tempHoursToPlay, tempNoOfPlayers, customer);
            return foundBooking;
        }

        private Customer GetCustomerById(int customerId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE id = @customerId", con))
            {
                command.Parameters.AddWithValue("@customerId", customerId);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Retrieve customer details from the reader and return the customer object
                    int id = reader.GetInt32(reader.GetOrdinal("id"));
                    string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                    string email = reader.GetString(reader.GetOrdinal("Email"));
                    string phone = reader.GetString(reader.GetOrdinal("Phone"));

                    return new Customer(id, firstName, lastName, email, phone);
                }
            }

            return null; // Customer not found
        }
        public bool CreatePriceBooking(int priceId, int bookingId)
        {
            bool isCreated = false;
            string insertString = "INSERT INTO PriceBooking (PriceId, BookingId) VALUES (@PriceId, @BookingId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@PriceId", priceId);
                createCommand.Parameters.AddWithValue("@BookingId", bookingId);

                con.Open();
                int rowsAffected = createCommand.ExecuteNonQuery();

                isCreated = (rowsAffected > 0);
            }

            return isCreated;
        }
        public bool CreateLaneBooking(int laneId, int bookingId)
        {
            bool isCreated = false;
            string insertString = "INSERT INTO LaneBooking (LaneId, BookingId) VALUES (@LaneId, @BookingId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand createCommand = new SqlCommand(insertString, con))
            {
                createCommand.Parameters.AddWithValue("@LaneId", laneId);
                createCommand.Parameters.AddWithValue("@BookingId", bookingId);

                con.Open();
                int rowsAffected = createCommand.ExecuteNonQuery();

                isCreated = (rowsAffected > 0);
            }

            return isCreated;
        }
        public Booking GetBookingById(int id)
        {
            Booking foundBooking;
            string queryString = @"SELECT b.Id, b.hoursToPlay, b.startDateTime, b.noOfPlayers, c.Id AS CustomerId, c.FirstName, c.LastName, c.Email, c.Phone, lb.LaneId, pb.PriceId
                           FROM Booking AS b
                           JOIN LaneBooking AS lb ON b.Id = lb.BookingId
                           JOIN PriceBooking AS pb ON b.Id = pb.BookingId
                           JOIN Customer AS c ON b.customerID = c.Id
                           WHERE b.Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader bookingReader = readCommand.ExecuteReader();
                foundBooking = null;

                while (bookingReader.Read())
                {
                    foundBooking = new Booking
                    {
                        Id = bookingReader.GetInt32(bookingReader.GetOrdinal("Id")),
                        HoursToPlay = bookingReader.GetInt32(bookingReader.GetOrdinal("hoursToPlay")),
                        StartDateTime = bookingReader.GetDateTime(bookingReader.GetOrdinal("startDateTime")),
                        NoOfPlayers = bookingReader.GetInt32(bookingReader.GetOrdinal("noOfPlayers")),
                    };

                    Customer cus = new Customer();
                    cus.Id = bookingReader.GetInt32(bookingReader.GetOrdinal("CustomerId"));
                    cus.FirstName = bookingReader.GetString(bookingReader.GetOrdinal("FirstName"));
                    cus.LastName = bookingReader.GetString(bookingReader.GetOrdinal("LastName"));
                    cus.Email = bookingReader.GetString(bookingReader.GetOrdinal("Email"));
                    cus.Phone = bookingReader.GetString(bookingReader.GetOrdinal("Phone"));
                    foundBooking.Customer = cus;

                    foundBooking.PriceId = bookingReader.GetInt32(bookingReader.GetOrdinal("PriceId"));
                    foundBooking.LaneId = bookingReader.GetInt32(bookingReader.GetOrdinal("LaneId"));
                }
            }

            return foundBooking;
        }

    }

}
