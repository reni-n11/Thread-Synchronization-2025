# Thread-Synchronization-2025
A simple C# program I created in university for synchronization of two threads, using Mutex and ManualResetEvent. The main thread reads an inputted string, while the latter thread prints it until the command "end". 
# Features
1. The user is prompted to enter the first input.
2. Two threads are created - thMain (calls the Read method and handles the user input) and thCaused (calls the Print method, printing the input).
3. The main thread waits for the user to input a string. It uses Mutex to update the shared input with the caused thread.
4. The caused thread prints the string every 2 seconds through Mutex.
5. If "end" is inputted, the caused thread is paused.
