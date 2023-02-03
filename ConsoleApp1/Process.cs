using Timer = System.Timers.Timer;

namespace BullyAlgorithm
{
    public class Process
    {
        private int processId;
        private int interval;
        private Dictionary<int, string> sharedMemory;
        private int[] chunk;

        public Process(int processId, int interval, Dictionary<int, string> sharedMemory)
        {
            this.processId = processId;
            this.interval = interval;
            this.sharedMemory = sharedMemory;

        }

        public int ProcessId { get { return processId; } }
        public void Start()
        {
            // Variable to keep track of the time interval
            int timeInterval = 2;

            // Create a timer with interval of 1000ms
            Timer timer = new Timer(1000);
            timer.Elapsed += (sender, e) => {
                timeInterval--;
            };
            timer.Start();

            while (timeInterval > 0)
            {

                if (Console.KeyAvailable)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {

                        // Check if there is a coordinator
                        if (Coordinator.Id != 0)
                        {
                            Coordinator.TerminateCoordinator(Coordinator.Id, sharedMemory);
                            Console.WriteLine("Coordinator has been terminated manually");

                            // Add the process to the shared memory as an eligible process for election
                            sharedMemory.Add(processId, MessageType.Election_Eligible.ToString());
                            break;
                        }
                        else
                            Console.WriteLine("There is currently no coordinator.");

                    }
                    else
                    {
                        timer.Stop();
                        break;
                    }
                }
            }

            // Add the process to the shared memory as an OK process
            if (!sharedMemory.ContainsKey(processId))
                sharedMemory.Add(processId, MessageType.OK.ToString());


            // Determine whether the coordinator is alive and whether the coordinator is sending it on time.
            if (Coordinator.IsCoordinatorAlive(sharedMemory) && Coordinator.SendMessage(processId, interval, sharedMemory))
            {
                // Check if the current process has a higher ID than the coordinator
                if (processId > Coordinator.Id)
                {
                    // Set the message type of all processes to "OK"
                    foreach (var process in sharedMemory)
                    {
                        if (process.Value == MessageType.Coordinator.ToString())
                        {
                            sharedMemory[process.Key] = MessageType.OK.ToString();
                        }
                    }
                    // Initialize the election and start it
                    Console.WriteLine("Iam higher than Coordinator. I will initializing election.");
                    ElectionManager.InitElection(processId, sharedMemory);
                    ElectionManager.StartElection(sharedMemory);

                }
            }       
            else
            {
                // Check if there is already a coordinator present in the shared memory
                if (Coordinator.Id != 0)
                {
                    MessageManager.NoMessageReceived(ProcessId);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Coordinator failed, terminating and initializing election.");
                    Console.ResetColor();
                    // Terminate the failed coordinator and initialize the election process
                    Coordinator.TerminateCoordinator(Coordinator.Id, sharedMemory);
                    ElectionManager.InitElection(processId, sharedMemory);
                }
                else
                {
                    // If there is no coordinator or election message present in the shared memory
                    // print a message indicating that the program is starting for the first time
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    if (!sharedMemory.Where(x => x.Value == MessageType.Election_Eligible.ToString()).Any())
                        Console.WriteLine("Program first start, initializing election.");
                    else
                        Console.WriteLine("There is no coordinator");
                    Console.ResetColor();

                    ElectionManager.InitElection(processId, sharedMemory);
                }
            }
        }

        public void SendChunk(int[] chunk)
        {
            // Assign the received chunk to the Chunk property
            this.chunk = chunk;
            // Find the minimum value in the chunk
            FindMinimumInChunk();
        }
        private void FindMinimumInChunk()
        {
            // Initialize minimumValue with the maximum possible integer value
            int minimumValue = int.MaxValue;
            for (int i = 0; i < chunk.Length; i++)
            {
                // If the current value is less than the minimumValue, update minimumValue
                if (chunk[i] < minimumValue)
                {
                    minimumValue = chunk[i];
                }
            }
            // Call the ReceiveResult method of the Coordinator class and pass the process Id and the minimum value
            Coordinator.ReceiveResult(processId, minimumValue);
        }
    }
}
