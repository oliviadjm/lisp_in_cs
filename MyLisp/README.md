
[//]: # (This document is best viewed on GitHub: https://github.com/oliviadjm/lisp_in_cs)

# Intro
lisp_in_cs is a C# implementation of Lisp. Several online resources, primarily Crafting Interpreters, Make a Lisp, codeproject.com, geeksforgeeks and also (minorly) various others were used as a guide. 


### Table of Contents
1. [How to Use](#How to Use)
2. [Building](#building)

## How to Use
Without an argument, lisp_in_cs operates a REPL loop that gives the user a command line prompt. This will run until the user enters Ctrl+C. When run with a lisp source file, Program.cs will attempt to execute it. 

## Building
To build and run this project, please install .NET 6.0 SDK from the [.NET download website](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

Then clone my repo
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
to run with my test file. You can also replace test_cases.lisp with a lisp test file of your choosing. My implementation of lisp may or may not subscribe to certain lisp syntaxes - check my test file for the syntax I used regarding single & double quotes (primarily, but also other things).
