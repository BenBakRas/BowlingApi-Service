using BowlingData.ModelLayer;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Numerics;

namespace BowlingData.DatabaseLayer
{
    public class PriceDatabaseAccess : IPriceAccess
    {
        readonly string? _connectionString;

        public PriceDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public PriceDatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int CreatePrice(Price aPrice)
        {
            int insertedId = -1;
            //
            string insertString = "insert into Price(normalPrice, specialPrice, weekday) OUTPUT INSERTED.ID values(@NormalPrice, @SpecialPrice, @Weekday)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                SqlParameter aNPParam = new("@NormalPrice", aPrice.NormalPrice);
                CreateCommand.Parameters.Add(aNPParam);
                SqlParameter aSPParam = new("@specialPrice", aPrice.SpecialPrice);
                CreateCommand.Parameters.Add(aSPParam);
                SqlParameter aWDParam = new("@weekday", aPrice.Weekday);
                CreateCommand.Parameters.Add(aWDParam);
                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            return insertedId;
        }

        public bool DeletePriceById(int id)
        {
            bool isDeleted = false;
            //
            string deleteString = "DELETE FROM Price WHERE Id = @Id";
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

        public List<Price> GetAllPrices()
        {
            List<Price> foundPrices;
            Price readPrice;
            //
            string queryString = "select Id, NormalPrice, SpecialPrice, Weekday from Price";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader priceReader = readCommand.ExecuteReader();
                // Collect data
                foundPrices = new List<Price>();
                while (priceReader.Read())
                {
                    readPrice = GetPriceFromReader(priceReader);
                    foundPrices.Add(readPrice);
                }
            }
            return foundPrices;
        }

        public Price GetPriceById(int id)
        {
            Price foundPrice;
            //
            string queryString = "select Id, NormalPrice, SpecialPrice, Weekday from Price where Id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                //Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                //Execute reead
                SqlDataReader priceReader = readCommand.ExecuteReader();
                foundPrice = new Price();
                while (priceReader.Read())
                {
                    foundPrice = GetPriceFromReader(priceReader);
                }
            }
            return foundPrice;
        }

        public bool UpdatePrice(Price priceToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Price SET NormalPrice = @NormalPrice, SpecialPrice = @SpecialPrice, Weekday = @Weekday WHERE id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", priceToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@NormalPrice", priceToUpdate.NormalPrice);
                updateCommand.Parameters.AddWithValue("@SpecialPrice", priceToUpdate.SpecialPrice);
                updateCommand.Parameters.AddWithValue("@Weekday", priceToUpdate.Weekday);

                con.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (isUpdated = (rowsAffected > 0))
                {
                    return isUpdated;
                }
                else
                {
                    return false;
                }
            }
        }

        private Price GetPriceFromReader(SqlDataReader priceReader)
        {
            Price foundPrice;
            int tempID;
            double tempNormalPrice, tempSpecialPrice;
            string tempWeekDay;
            // Fetch values
            tempID = priceReader.GetInt32(priceReader.GetOrdinal("Id"));
            tempNormalPrice = priceReader.GetDouble(priceReader.GetOrdinal("NormalPrice"));
            tempSpecialPrice = priceReader.GetDouble(priceReader.GetOrdinal("SpecialPrice"));
            tempWeekDay = priceReader.GetString(priceReader.GetOrdinal("Weekday"));
            //Create Price object
            foundPrice = new Price(tempID, tempNormalPrice, tempSpecialPrice, tempWeekDay);
            return foundPrice;
        }

    }
}
