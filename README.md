
<img width="670" alt="tests" src="https://github.com/user-attachments/assets/b6f0e1f5-657f-4e12-939f-192dd4b239e2">
<img width="384" alt="test_repl_and_sprint6" src="https://github.com/user-attachments/assets/06bea0cc-3c2d-4043-92df-b5c4af30b8a7">


This project was completed by James Montgomery and Olivia Monteiro. The Test for the entire code is shown in the very top of the README right above this message but has also been submitted as a separate image file called "tests" to the submission. Snapshots have been submitted as separate .zip files with the name of each sprint. PLEASE NOTE that we completed sprints 2, 3, 4 CONCURRENTLY so the zip files will be fairly similar. All this information has also been included in the SUBMISSION NOTES!

[//]: # (This document is best viewed on GitHub: https://github.com/oliviadjm/lisp_in_cs)

# Intro
lisp_in_cs is a C# implementation of Lisp. Several online resources, primarily Crafting Interpreters, Make a Lisp, codeproject.com, geeksforgeeks and also (minorly) various others were used as a guide. This project was completed by James Montgomery and Olivia Monteiro.

## How to Use
Without an argument, lisp_in_cs operates a REPL loop that gives the user a command line prompt. This will run until the user enters Ctrl+C. When run with a lisp source file, Program.cs will attempt to execute it. 

## Building
To build and run this project, please install .NET 6.0 SDK from the [.NET download website](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

Then clone the repo
```
git clone https://github.com/oliviadjm/lisp_in_cs.git
```
Navigate to "MyLisp" and run
```
dotnet build
```
Then, either run 
```
dotnet run
```
in order to run the command line REPL loop, or run
```
dotnet run test_cases.lisp
```
to run with the test file. You can also replace test_cases.lisp with a lisp test file of your choosing. Our implementation of lisp may or may not subscribe to certain lisp syntaxes - check the test file for the syntax used regarding single & double quotes (primarily, but also other things).

