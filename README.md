
# Bully Algorithm

  
  

## Overview

The Bully Algorithm is a process election algorithm used in computer networks to determine the coordinator among a group of processes. The code implementation in C# demonstrates how the algorithm works, allowing for manual control over options such as shutting down the coordinator, adding new processes, starting an election, and performing distributed computing. The communication between processes is established using a `Dictionary<int, string>` object named sharedMemory. The code features two classes: the `Process` class and the `Coordinator` class. The `Process` class has properties for the process ID, time interval, shared memory, and chunk of data to be processed. The `Coordinator` class contains methods for terminating the coordinator, broadcasting the coordinator, checking if the coordinator is alive, sending messages, and performing distributed computing. The program is run by downloading the .exe file, double-clicking to run, and following the prompts and instructions displayed on the screen.

  ##  How to run the Program

This section provides a step-by-step guide on how to run the Bully Algorithm program, represented by the .exe file.

### Prerequisites

* Windows Operating System

* .NET framework installed

  

### Steps

1. Download the BullyAlgorithmProgram.rar file from the repository.

2. Extract the .exe file from .rar file and Double-click on the .exe file to run it.

3. The program will display a menu with several options:

   * Manually shut down the coordinator.

   * Pause the program and add a new process.

   * Start an election.

   * Start Distributed Computing.

   * See an example of using all of the program's functions without any user interaction.

4. Use the keyboard to input the number of the option you want to select and press Enter.

5. Follow the prompts and instructions displayed on the screen to use the program.

Note: You can exit the program at any time by inputting the number 0 in the menu.
## User Interface and Functionality
The code features a user interface that presents options for interacting with the algorithm. The options include:

* Shutting down the coordinator manually

* Adding a new process to the network

* Starting an election to determine a new coordinator

* Performing distributed computing by dividing a large array into chunks and sending them to the processes for computation. The results are collected and the minimum value among the responses is calculated.

* Demonstrating the use of all of the program's functions without any user interaction.

  

Upon user input, the code implements the appropriate action, such as calling the StartElection method from the ElectionManager class when the user selects to initiate an election or calling the DistributedComputing method from the Coordinator class to perform distributed computing.

  
  

## Communication between processes

In this code, communication between the different processes is established using a Dictionary<int,  string> object named sharedMemory. A Dictionary is a collection of key-value pairs where the key is an int, representing the process ID, and the value is a string, representing the status of the process. The sharedMemory object is used to store the status of all processes in the network and is accessible to all processes in the network.

  

The reason for using a Dictionary was to keep the implementation simple and focused on the concept of the Bully Algorithm. The Dictionary provides a convenient way to store and retrieve information from the shared memory without introducing additional complexities in the code. It also allows for fast lookups, clear and concise storage of information, and can be easily modified to include additional information. The focus was on demonstrating the algorithm rather than providing a comprehensive solution for inter-process communication.

  
  

## Classes Descriptions and Functionalities

### 1. Process Class 
a `Process` class for a Bully Algorithm simulation in a distributed computing environment. The `Process` class has properties for the process ID, time interval, shared memory (a dictionary of process ID to message type), and a chunk of data to be processed. The `Start` method is responsible for coordinating the process' actions in the simulation. It creates a timer that counts down from 2 seconds, and listens for user input to manually terminate the coordinator. If there is no coordinator or the coordinator is not alive, the process initializes an election. If the coordinator is alive and sending messages, the process checks if its ID is higher than the coordinator's, and if so, it terminates the coordinator and starts an election. The `SendChunk` method assigns the received chunk to the process' `chunk` property and calls the `FindMinimumInChunk` method to find the minimum value in the chunk. The `FindMinimumInChunk` method loops through the chunk to find the minimum value and calls the `ReceiveResult` method of the `Coordinator` class to pass the process ID and the minimum value.

### 2. Coordinator Class

The `Coordinator` class implements a coordinator component of the Bully Algorithm for distributed computing. The Bully Algorithm is used for electing a coordinator in a distributed system, where multiple processes can participate.

The class contains methods for:

-   Terminating the coordinator: `TerminateCoordinator` method removes the failed coordinator from the shared memory.
-   Broadcasting the coordinator: `BroadcastCoordinator` method broadcasts the new coordinator id to all the processes in the shared memory with an "OK" status.
-   Checking if the coordinator is alive: `IsCoordinatorAlive` method checks if there is a process with the coordinator status in the shared memory.
-   Sending messages: `SendMessage` method sends messages to the specified process id and receives the response.
-   Distributed computing: `DistributedComputing` method is used to divide the large array into chunks and send them to the processes for computation. The results are received and the minimum value among all the responses is calculated.

The class also has a private method `CreateLargeArray` to initialize the `largeArray` property with random integers, and `DivideArray` method to divide the large array into smaller chunks of equal sizes. The `ReceiveResult` method is used to add the result received from each process to the responses list.

### 3. ElectionManager Class
The class `ElectionManager` contains several methods related to the election process. The `InitElection` method initiates the election by sending an `Election_Request` message to all other processes. It then determines if the process with the highest ID is eligible to be a coordinator, and sends an `Election_Eligible` message if it is. The `StartElection` method starts the actual election and finds the new coordinator. It does this by checking if any process has initiated the election, then finding the process with the highest ID and setting it as the new coordinator. The `FindNewCoordinator` method returns the process with the highest ID in the shared memory, and `CheckIfAnyProcessInitiateElection` returns a boolean indicating whether any process has initiated the election. Finally, the `GetHighestProcessId` method returns the highest process ID in the shared memory.

### 4. MessageManager Class
`MessageType` defines different types of messages that could be exchanged between the processes participating in a Bully Algorithm based election. The message types include: `Election_Eligible`, `OK`, `Coordinator`, `Election_Ineligible`, `Election_WIN`, `Election_Request`, `COORDINATOR_ALIVE`, and `New_Coordinator`.

`MessageManager` class is used to send and receive messages between the processes participating in the election. The class has the following methods:

-   `SendMessage` takes the `senderId` and `message` as inputs and logs a message indicating that the specified process has sent a message of a certain type.
-   `ReceiveMessage` takes the `receiverId`, `senderId`, and `message` as inputs and logs a message indicating that the specified process has received a message of a certain type from another process.
-   `WinningMessage` takes the `receiverId` as an input and logs a message indicating that the process has won the election.
-   `NoMessageReceived` takes the `processId` as an input and logs a message indicating that the process didn't receive a message from the coordinator.

### 5. Program Class
 This Class  allows you to manually shut down the coordinator process and start an election to select a new coordinator.

The code has a while loop that continues until the user inputs 0 to exit the program. The loop displays options for the user to input:

1.  Manually shut down the coordinator.
2.  Pause the program and add a new process.
3.  Start an election.
4.  Start Distributed Computing.
5.  See an example of using all of the program's functions without any user interaction.

If the user inputs 1, the code terminates the coordinator, initiates an election and sets the status of eligible processes to "Election_Eligible" in shared memory.

If the user inputs 2, the code prompts the user to enter the process ID and time interval for the new process and creates a new process instance.

If the user inputs 3, the code calls the StartElection method from the ElectionManager class.

If the user inputs 4, the code calls the DistributedComputing method from the Coordinator class.

If the user inputs 5, the code creates 3 processes, starts all processes, initiates an election, creates a new process higher than coordinator, and initiates Distributed Computing.

## Author

> Ammar Shaalan

 - Email: [ammshaalan@gmail.com](mailto:ammshaalan@gmail.com)
 - LinkedIn: [https:/www.linkedin.com/in/ammarshaalan](https://www.linkedin.com/in/ammarshaalan)
