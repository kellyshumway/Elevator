
using ElevatorApp;

const int elevators = 4;

Console.WriteLine("Welcome to my elevator bank");

Queue<KeyValuePair<int, int>> callQueue = new();
callQueue.Enqueue(new(4, 6));
callQueue.Enqueue(new(3, 1));
callQueue.Enqueue(new(8, 10));
callQueue.Enqueue(new(7, 2));
callQueue.Enqueue(new(2, 8));
callQueue.Enqueue(new(9, 3));

List<Elevator> elevatorBank = new();
for (int i = 0; i < elevators; i++)
{
    elevatorBank.Add(new Elevator(i));
}

int t = 0;
List<Thread> elevatorThreads = new();
foreach (Elevator elevator in elevatorBank) 
{
    Thread elevatorThread = new(elevator.Waiting)
    {
        Name = "Elevator_" + (t++ + 1).ToString()
    };
    elevatorThreads.Add(elevatorThread);
}

while (callQueue.Count > 0)
{
    KeyValuePair<int, int> call = callQueue.Dequeue();

    // find 1st available elevator
    int elevatorId = 0;

    while (elevatorBank[elevatorId++].elevatorState != Elevator.ElevatorState.Ready) 
    { 
        if (elevatorId == elevators) 
            elevatorId = 0; 
    }

    // send call to elevator
    Thread elevatorThread = new(() => elevatorBank[elevatorId - 1].Called(call));
    elevatorThread.Start();
    Thread.Sleep(1000);
}



