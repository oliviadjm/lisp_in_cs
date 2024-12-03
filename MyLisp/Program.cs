using System;
using System.IO;

namespace MyLisp {
    class MyLisp {
        static SExpr read(string input){
            return Reader.readStr(input);
        }

        static SExpr eval(SExpr input, Environment env) {
            if (input is SExpr.Atom atom) { //if input is an atom
                //return input.Value;
                if (atom.Type == SExpr.AtomType.Symbol) {
                    return env.lookup(atom.Value, env.Env);
                }

                //if its just a literal then return the literal
                return input;
            }
            if (input is SExpr.SEList list) {
                if (list.Elements.Count == 0) {
                    throw new Exception("Cannot evaluate an empty list");
                }

                if (list.Elements[0] is SExpr.Atom firstAtom && firstAtom.Value == "if") {
                    if (list.Elements.Count < 3 || list.Elements.Count > 4) {
                        throw new Exception("if requires three or four arguments: (if condition then-expr [else-expr])");
                    }

                    var condition = eval(list.Elements[1], env);

                    if (condition == SExpr.Nil || condition == SExpr.False) {
                        //evaluate else branch or return nil if absent
                        return list.Elements.Count == 4 ? eval(list.Elements[3], env) : SExpr.Nil;
                    }
                    else {
                        //evaluate then branch
                        return eval(list.Elements[2], env);
                    }
                }

                if (list.Elements[0] is SExpr.Atom condAtom && condAtom.Value == "cond") {
                    foreach (var clause in list.Elements.Skip(1)) {
                        if (clause is SExpr.SEList pair && pair.Elements.Count == 2) {
                            var condition = eval(pair.Elements[0], env);
                            if (condition != SExpr.Nil && condition != SExpr.False) {
                                return eval(pair.Elements[1], env);
                            }
                        } else if (clause is SExpr.SEList elseClause && elseClause.Elements.Count == 1 &&
                                elseClause.Elements[0] is SExpr.Atom elseAtom && elseAtom.Value == "else") {
                            // Handle `else` clause
                            return eval(elseClause.Elements[1], env);
                        }
                    }
                    return SExpr.Nil; // If no conditions are true
                }

                //evaluate the first element (the operator or function)
                var op = eval(list.Elements[0], env);
                //if (op is not SExpr.SEFunction function) {
                    //throw new Exception($"Expected a function but got: {op}");
                //}

                if (op is SExpr.SEFunction function) {
                    //var args = list.Elements.Skip(1).Select(arg => eval(arg, env)).ToList();
                    var args = list.Elements.Skip(1).ToList();
                    return function.Invoke(args); // Invoke the function with arguments
                }

                //evaluate the arguments
                //var args = list.Elements.Skip(1).Select(arg => eval(arg, env)).ToList();

                //invoke the function
                //return function.Invoke(args);
                throw new Exception($"Expected a function but got: {op}");
            }
            if (input is SExpr.Quote quote) {
                return quote.Expression;
            }
            else {
                //return input;
                throw new Exception($"Unknown expression type: {input}");
            }
        }

        static string print(SExpr input) {
            return Printer.prStr(input);
        }

        static string repl(string input, Environment env) {
            return print(eval(read(input), env));
        }

        static void Main(string[] args) {
            var env = new Environment();
            while(true) {
                try {
                    Console.Write("user> ");
                    string input = Console.ReadLine();
                    Console.WriteLine(repl(input, env));
                }
                catch(IOException e) {
                    Console.WriteLine("IOException");
                    break;
                }
                    
            }
        }
    }

}