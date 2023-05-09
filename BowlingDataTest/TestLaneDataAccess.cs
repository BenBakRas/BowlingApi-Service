﻿using BowlingData.DatabaseLayer;
using ShModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BowlingDataTest
{
    public class TestLaneDataAccess
    {
        private readonly ITestOutputHelper _extraOutput;

        readonly private ILaneAccess _laneAccess;

        readonly string _connectionString = "Server=localhost; Integrated Security=true; Database=BowlingTest";

        public TestLaneDataAccess(ITestOutputHelper output)
        {
            _extraOutput = output;
            _laneAccess = new LaneDatabaseAccess(_connectionString);
        }
        [Fact]
        public void TestGetAllLanes()
        {
            // Arrange

            // Act
            List<Lane> readLanes = _laneAccess.GetAllLanes();
            bool lanesWereRead = (readLanes.Count > 0);
            // Print additional output
            _extraOutput.WriteLine("Number of Lanes: " + readLanes.Count);

            // Assert
            Assert.True(lanesWereRead);
        }
        [Fact]
        public void TestCreateLane()
        {
            // Arrange
            Lane lane = new Lane(5); // Create a new Lane object

            // Act
            int insertedId = _laneAccess.CreateLane(lane);

            // Assert
            Assert.True(insertedId > 0);
        }

        [Fact]
        public void TestGetLaneById()
        {
            // Arrange
            Lane lane = new Lane(6); // Create a new Lane object
            int insertedId = _laneAccess.CreateLane(lane); // Insert the Lane into the database

            // Act
            Lane retrievedLane = _laneAccess.GetLaneById(insertedId);

            // Assert
            Assert.NotNull(retrievedLane);
            Assert.Equal(insertedId, retrievedLane.Id);
            Assert.Equal(lane.LaneNumber, retrievedLane.LaneNumber);
        }

        [Fact]
        public void TestDeleteLaneById()
        {
            // Arrange
            Lane lane = new Lane(5); // Create a new Lane object
            int insertedId = _laneAccess.CreateLane(lane); // Insert the Lane into the database

            // Act
            bool isDeleted = _laneAccess.DeleteLaneById(insertedId);

            // Assert
            Assert.True(isDeleted);
        }
    }
}
