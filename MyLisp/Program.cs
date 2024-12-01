using System;
using System.IO;

namespace MyLisp {
    class MyLisp {
        static SExpr read(string input){
            return Reader.readStr(input);
        }

        static SExpr eval(SExpr input) {
            //if (input.Type == SExpr.AtomType.Symbol) {
                //return input.Value;
            //}
            //else if (input is SEList) {
                //?????
            //}
            //else {
                return input;
            //}
        }

        static string print(SExpr input) {
            return Printer.prStr(input);
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