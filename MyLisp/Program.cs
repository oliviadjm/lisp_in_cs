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
                    //return env.lookup(atom.Value, env.Env);
                    return env.lookup(atom.Value);
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
                            //handle `else` clause
                            return eval(elseClause.Elements[1], env);
                        }
                    }
                    return SExpr.Nil; //if no conditions are true
                }

                if (list.Elements[0] is SExpr.Atom defineAtom && defineAtom.Value == "define") {
                    if (list.Elements.Count != 4) {
                        throw new Exception("define requires a name, parameter list, and body.");
                    }

                    var fnName = list.Elements[1] as SExpr.Atom;
                    var paramList = list.Elements[2] as SExpr.SEList;
                    var body = list.Elements[3];

                    if (fnName == null || paramList == null) {
                        throw new Exception("Invalid function definition syntax.");
                    }

                    var definedFunction = new SExpr.SEFunction((args) => {
                        if (args.Count != paramList.Elements.Count) {
                            throw new Exception($"Function '{fnName.Value}' expects {paramList.Elements.Count} arguments, but got {args.Count}.");
                        }

                        //create a new environment for the function call
                        var localEnv = new Dictionary<string, SExpr>();

                        //bind arguments to parameters
                        for (int i = 0; i < paramList.Elements.Count; i++) {
                            var paramName = paramList.Elements[i] as SExpr.Atom;
                            if (paramName == null) {
                                throw new Exception("Parameter list must contain only symbols.");
                            }
                            localEnv[paramName.Value] = eval(args[i], env); //evaluate each argument
                        }

                        //var functionEnv = new Environment(localEnv);
                        //push the new environment
                        env.pushEnvironment(localEnv);
                        
                        //maybe instead of pushing it i could set env equal to localEnv?
                        //this would require some sort of constructor method

                        //evaluate the body in the new environment
                        var result = eval(body, env);

                        //pop the environment after evaluation
                        env.popEnvironment();

                        return result;
                    });

                    //define the function in the current environment
                    env.define(fnName.Value, definedFunction);

                    //return the function name as confirmation
                    return new SExpr.Atom(fnName.Value, SExpr.AtomType.Symbol);
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

        static void ProcessFile(string filePath, Environment env) {
            try {
                var lines = File.ReadAllLines(filePath);
                int testNumber = 1;

                foreach (var line in lines) {
                    //skip comments or empty lines
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith(";")) {
                        continue;
                    }

                    //split line into expression and expected output
                    var parts = line.Split(';');
                    var expression = parts[0].Trim();
                    var expectedOutput = parts.Length > 1 ? parts[1].Trim() : "unknown";

                    try {
                        //evaluate the expression
                        string actualOutput = repl(expression, env);

                        //compare actual output with expected output
                        string result = actualOutput == expectedOutput ? "PASS" : $"FAIL (Expected: {expectedOutput}, Got: {actualOutput})";

                        //print test result
                        Console.WriteLine($"Test {testNumber}: {expression} {result}");
                    }
                    catch (Exception e) {
                        Console.WriteLine($"Test {testNumber}: {expression} FAIL (Error: {e.Message})");
                    }

                    testNumber++;
                }
            }
            catch (Exception e) {
                Console.WriteLine($"Failed to process file: {filePath}. {e.Message}");
            }
        }


        static void Main(string[] args) {
            var env = new Environment();
            if (args.Length > 0 && File.Exists(args[0])) {
                //if a file is provided, process the file
                ProcessFile(args[0], env);
            }
            else {
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
}