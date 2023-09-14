using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public class ElevatorCar
    {
        public int ElevatorId = 0;
        public int MaxFloor = 10;
        public enum ElevatorState { Ready, GoingUp, GoingDown, DoorOpen };
        public ElevatorState elevatorState = ElevatorState.Ready;
        public int currentFloor = 1;

        public ElevatorCar(int elevatorId = 0)
        {
            ElevatorId = elevatorId;
        }

        public void Waiting()
        {
            Console.WriteLine("Elevator {0} is ready.", ElevatorId+1);
        }

        public void Called(KeyValuePair<int, int> callData)
        {
            Console.WriteLine("Elevator {0} has been called to floor {1}.", ElevatorId+1, callData.Key);
            if (callData.Key > currentFloor)
            {
                elevatorState = ElevatorState.GoingUp;
                GoingUp(callData);
            }
            else if (callData.Key < currentFloor)
            {
                elevatorState = ElevatorState.GoingDown;
                GoingDown(callData);
            }
            else
            {
                elevatorState = ElevatorState.DoorOpen;
                Console.WriteLine("Elevator {0} has arrived at floor {1}, door is open.", ElevatorId + 1, currentFloor);
                MoveOccupant(callData.Value);
            }
        }

        public void GoingUp(KeyValuePair<int, int> callData)
        {
            if (callData.Value <= MaxFloor)
            {
                elevatorState = ElevatorState.GoingUp;
                while (currentFloor < callData.Key)
                {
                    if (currentFloor != callData.Key)
                        Console.WriteLine("Elevator {0} is going up, passing floor {1}.", ElevatorId + 1, currentFloor++);
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Elevator {0} has arrived at floor {1}, door is open.", ElevatorId + 1, currentFloor);
                elevatorState = ElevatorState.DoorOpen;
                MoveOccupant(callData.Value);
            }
            else
            {
                elevatorState = ElevatorState.Ready;
                Console.WriteLine("Elevator {0} called to invalid floor {1}, elevator is ready.", ElevatorId + 1, callData.Value);
            }
        }

        public void GoingDown(KeyValuePair<int, int> callData)
        {
            if (callData.Value > 0)
            {
                elevatorState = ElevatorState.GoingDown;

                while (currentFloor > callData.Key)
                {
                    if(currentFloor != callData.Key)
                        Console.WriteLine("Elevator {0} is going down, passing floor {1}.", ElevatorId + 1, currentFloor--);
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Elevator {0} has arrived at floor {1}, door is open.", ElevatorId + 1, currentFloor);
                elevatorState = ElevatorState.DoorOpen;
                MoveOccupant(callData.Value);
            }
            else
            {
                elevatorState = ElevatorState.Ready;
                Console.WriteLine("Elevator {0} called to invalid floor {1}, elevator is ready.", ElevatorId + 1, callData.Value);
            }
        }

        public void MoveOccupant(int requestedFloor)
        {
            Thread.Sleep(3000);
            Console.WriteLine("Elevator {0} is loaded and the door has closed on floor {1}.", ElevatorId + 1, currentFloor);

            if (requestedFloor < currentFloor)
            {
                elevatorState = ElevatorState.GoingDown;
                MovingDown(requestedFloor);
            }
            else if (requestedFloor > currentFloor)
            {
                elevatorState = ElevatorState.GoingUp;
                MovingUp(requestedFloor);
            }
            else
            {
                Console.WriteLine("Request from {0} to {1} is for current floor, elevator is now ready.", currentFloor, requestedFloor);
                elevatorState = ElevatorState.Ready;
            }

        }

        public void MovingUp(int requestedFloor)
        {
            Console.WriteLine("Elevator {0} transporting up from floor {1} to floor {2}.", ElevatorId + 1, currentFloor, requestedFloor);

            while (currentFloor < requestedFloor)
            {
                if(currentFloor != requestedFloor) 
                    Console.WriteLine("Elevator {0} is moving occupants up, passing floor {1} on the way to {2}.", ElevatorId + 1, currentFloor++, requestedFloor);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Elevator {0} has arrived at floor {1} and is unloading.", ElevatorId + 1, currentFloor);
            elevatorState = ElevatorState.Ready;
            Thread.Sleep(500);
            Console.WriteLine("Elevator {0} door is closed and is now ready on floor {1}.", ElevatorId + 1, currentFloor);
        }

        public void MovingDown(int requestedFloor)
        {
            Console.WriteLine("Elevator {0} transporting down from floor {1} to floor {2}.", ElevatorId + 1, currentFloor, requestedFloor);

            while (currentFloor > requestedFloor)
            {
                if (currentFloor != requestedFloor)
                    Console.WriteLine("Elevator {0} is moving occupants down, passing floor {1} on the way to {2}.", ElevatorId + 1, currentFloor--, requestedFloor);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Elevator {0} has arrived at floor {1} and is unloading.", ElevatorId + 1, currentFloor);
            elevatorState = ElevatorState.Ready;
            Thread.Sleep(500);
            Console.WriteLine("Elevator {0} door is closed and is now ready on floor {1}.", ElevatorId + 1, currentFloor);
        }
    }
}
