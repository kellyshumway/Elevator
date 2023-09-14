using Elevator;
using System;

namespace ElevatorTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidFloor()
        {
            ElevatorCar elevator = new();
            bool validFloor = (elevator.currentFloor > 0
                            || elevator.currentFloor < elevator.MaxFloor);
            Assert.That(validFloor, Is.True);
        }

        [Test]
        [TestCase (5)]
        public void ValidCall(int calledToFloor)
        {
            KeyValuePair<int, int> calledPair = new(calledToFloor, calledToFloor); 

            var elevator = new ElevatorCar
            {
                ElevatorId = 1,
                currentFloor = 3
            };

            elevator.Called(calledPair);

            bool validCall = (elevator.currentFloor == calledToFloor 
                           && calledToFloor < elevator.MaxFloor);
            Assert.That(validCall, Is.True);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(12)]
        public void InValidCall(int calledToFloor)
        {
            KeyValuePair<int, int> calledPair = new(calledToFloor, 1);

            var elevator = new ElevatorCar
            {
                ElevatorId = 1,
                currentFloor = 3
            };

            elevator.Called(calledPair);

            bool validCall = (elevator.currentFloor == calledToFloor
                           && calledToFloor < elevator.MaxFloor);
            Assert.That(validCall, Is.False);
        }
    }
}