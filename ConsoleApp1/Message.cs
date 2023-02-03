using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullyAlgorithm
{
    public enum MessageType
    {
        Election_Eligible,
        OK,
        Coordinator,
        Election_Ineligible,
        Election_WIN,
        Election_Request,
        COORDINATOR_ALIVE,
        New_Coordinator
    }
    public static class MessageManager
    {
        public static void SendMessage(int senderId, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.Now}] Process {senderId} sent message: {message}");
            Console.ResetColor();
        }

        public static void ReceiveMessage(int receiverId, int senderId, string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now}] [Process {receiverId}] received a message of type {message} from process {senderId}");
            Console.ResetColor();
        }

        public static void WinningMessage(int receiverId)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[{DateTime.Now}] [Process {receiverId}] received a message: You won in the elections.");
            Console.ResetColor();
        }
        public static void NoMessageReceived(int processId)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now}] Process {processId} didn't receive a message from Coordinator.");
            Console.ResetColor();
        }

    }

}
