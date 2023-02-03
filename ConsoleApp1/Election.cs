
namespace BullyAlgorithm
{
    public static class ElectionManager
    {
        public static void InitElection(int processId, Dictionary<int, string> sharedMemory)
        {
            MessageManager.SendMessage(processId, MessageType.Election_Request.ToString());

            int highestId = GetHighestProcessId(sharedMemory);
            if (highestId > processId)
            {
                MessageManager.ReceiveMessage(processId, highestId, MessageType.Election_Ineligible.ToString());
                MessageManager.SendMessage(processId, MessageType.OK.ToString());
                sharedMemory.Remove(highestId);
                sharedMemory.Add(highestId, MessageType.Election_Eligible.ToString());

            }
            else
            {
                // Elect self as coordinator
                MessageManager.SendMessage(processId, MessageType.Election_Eligible.ToString());
                sharedMemory.Remove(processId);
                sharedMemory.Add(processId, MessageType.Election_Eligible.ToString());
            }

        }
        public static void StartElection(Dictionary<int, string> sharedMemory)
        {
            if (CheckIfAnyProcessInitiateElection(sharedMemory))
            {

                // Start election
                int newCoordinatorId = FindNewCoordinator(sharedMemory);
                sharedMemory.Remove(newCoordinatorId);
                sharedMemory.Add(newCoordinatorId, MessageType.Coordinator.ToString());
                foreach (var item in sharedMemory)
                {
                    if (item.Value == MessageType.Election_Eligible.ToString())
                    {
                        sharedMemory[item.Key] = MessageType.OK.ToString();
                    }
                }
                MessageManager.WinningMessage(newCoordinatorId);
                // Broadcast new Coordinator to all processes
                Coordinator.BroadcastCoordinator(newCoordinatorId, sharedMemory);
            }
            else Console.WriteLine("No process initiated election.");
        }
        private static int FindNewCoordinator(Dictionary<int, string> sharedMemory)
        {
            // Find the process withhe highest id in shared memory

            int maxId = sharedMemory.Keys.First();
            var dict = sharedMemory.Select(x => x.Key).ToList();

            foreach (int id in dict)
            {
                if (id > maxId)
                {
                    maxId = id;
                }
            }
            return maxId;
        }
        private static bool CheckIfAnyProcessInitiateElection(Dictionary<int, string> sharedMemory)
        {
            if (sharedMemory.Where(x => x.Value == MessageType.Election_Eligible.ToString()).Any())
                return true;
            return false;
        }
        private static int GetHighestProcessId(Dictionary<int, string> sharedMemory)
        {
            try
            {
                int highestId = sharedMemory.Select(x => x.Key).Max();
                return highestId;

            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
