
using System;

namespace BullyAlgorithm
{

    class Program
    {
        static void Main(string[] args)
        {
            //Declaring shared memory as a dictionary that holds the process ID as a key and message type as a value.
            Dictionary<int, string> sharedMemory = new Dictionary<int, string>();

            //Declaring a list to hold processes.
            List<Process> processes = new List<Process>();
            Console.WriteLine("You can manually shut down the coordinator at any time before it sends you a message by entering 1.");

            
           
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Enter 1 to manually terminate the coordinator");
                Console.WriteLine("Enter 2 to pause the program and add a new process");
                Console.WriteLine("Enter 3 to start election");
                Console.WriteLine("Enter 4 to Distributed Computing");
                Console.WriteLine("Enter 5 to see an example of using all of the program's functions without any user interaction");
                Console.WriteLine("Enter 0 to exit the program.");

                string input = Console.ReadLine();
                int numberInput;
                if (Int32.TryParse(input, out numberInput))
                {
                    if (numberInput == 1)
                    {
                        int failedCoordinator = Coordinator.Id;

                        //Checking if there is a coordinator to terminate.
                        if (failedCoordinator != 0)
                        {
                            Coordinator.TerminateCoordinator(failedCoordinator, sharedMemory);
                            Console.WriteLine("Coordinator has been terminated manually");
                            Console.WriteLine("Elections initiated");

                            //Setting the status of eligible processes to 'Election_Eligible' in shared memory.
                            foreach (var item in sharedMemory)
                            {
                                if (item.Value == MessageType.OK.ToString())
                                {
                                    sharedMemory[item.Key] = MessageType.Election_Eligible.ToString();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("there is no coordinator to trminate");

                        }
                        continue;
                    }
                    else if (numberInput == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Please keep in mind that you can terminate the coordinator within 2 seconds of entering the process id and time interval");
                        Console.ResetColor();
                        // Pause program and add a new process
                        Console.WriteLine("Enter the process ID of the new process:");
                        //Reading the process ID from the user.
                        int processId = int.Parse(Console.ReadLine());

                        //Checking if the process ID already exists in the shared memory.
                        if (sharedMemory.ContainsKey(processId))
                        {
                            Console.WriteLine("The process ID must be unique.");
                            continue;
                        }
                        Console.WriteLine("Enter the time interval in milliseconds of the new process:");
                        //Reading the time interval from the user.
                        int interval = int.Parse(Console.ReadLine());

                        //Creating a new process.
                        var p = new Process(processId, interval, sharedMemory);
                        // Adding the newly created process to the processes list.
                        processes.Add(p);
                        p.Start();
                        continue;
                    }
                    else if (numberInput == 3)
                    {
                        // Calling the StartElection method from the ElectionManager class.
                        ElectionManager.StartElection(sharedMemory);
                    }
                    else if (numberInput == 4)
                    {
                        Coordinator.DistributedComputing(processes, sharedMemory);
                    }
                    else if(numberInput == 5)
                    {
                        List<Process> processes1 = new List<Process>();
                        Dictionary<int, string> sharedMemory1 = new Dictionary<int, string>();


                        // Create 3 processes
                        for (int i = 1; i <= 3; i++)
                        {
                            processes1.Add(new Process(i, 100, sharedMemory1));
                        }

                        // Start all processes
                        foreach (Process p in processes1)
                        {
                            p.Start();
                        }
                        var p2 = new Process(25, 100, sharedMemory1);
                        var p3 = new Process(15, 100, sharedMemory1);
                        var p4 = new Process(30, 100, sharedMemory1);
                        processes1.Add(p3);
                        processes1.Add(p2);
                        processes1.Add(p4);
                        p2.Start();
                        p3.Start();
                        Console.WriteLine("Initiating start election execution");
                        ElectionManager.StartElection(sharedMemory1);
                        Console.WriteLine("Now we will create process higher than coordinator");
                        p4.Start();
                        Console.WriteLine("Initiating Distributed Computing execution");
                        Coordinator.DistributedComputing(processes1, sharedMemory1);
                    }
                    else if (numberInput == 0)
                        break;
                    else
                        Console.WriteLine("enter a number from the list");    
                }
            }
        }
    }
}

