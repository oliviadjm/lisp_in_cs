using System;
using System.IO;

namespace MyLisp {
    class MyLisp {
        static string read(string input){
            return Reader.readStr(input);
        }

        static string eval(string input) {
            return input;
        }

        static string print(string input) {
            return input;
        }

        static string repl(string input) {
            return print(eval(read(input)));
        }

        static void Main(string[] args) {
            while(true) {
                try {
                    Console.Write("user> ");
                    string input = Console.ReadLine();
                    Console.WriteLine(repl(input));
                }
                catch(IOException e) {
                    Console.WriteLine("IOException");
                    break;
                }
                    
            }
        }
    }

}