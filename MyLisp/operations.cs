using System;

namespace MyLisp {
    public class Operations {

        public static SExpr SENil (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj == SExpr.Nil) {
                return SExpr.True;
            }
            else if (obj is SExpr.SEList nestedList &&  nestedList.Elements.Count == 0) {
                return SExpr.True;
            }
            else {
                return SExpr.False;
            }
        }

        public static SExpr SESym (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj is SExpr.Atom symAtom && symAtom.Type == SExpr.AtomType.Symbol) {
                return SExpr.True;
            }
            else {
                return SExpr.False;
            }
        }

        public static SExpr SENum (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj is SExpr.Atom numAtom && numAtom.Type == SExpr.AtomType.Number) {
                return SExpr.True;
            }
            else {
                return SExpr.False;
            }
        }

        public static SExpr SEListFunc (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj is SExpr.SEList) {
                return SExpr.True;
            }
            else {
                return SExpr.False;
            }
        }

        public static SExpr consCell (List <SExpr> inputList) {
            SExpr head = inputList[0];
            SExpr tail = inputList[1];

            if (tail is SExpr.Quote quote) { //create a new list with `head` prepended to the elements of `tail`
                if (quote.Expression is SExpr.SEList list) {
                    //prepend `head` to the list
                    var newList = new List<SExpr> { head };
                    newList.AddRange(list.Elements); //add tail
                    return new SExpr.SEList(newList);
                    
                }

                //return quote.Prepend(head).ToList();
            } 
            else { //if tail is not a list, return a dotted pair
                return new SExpr.SEList(new List<SExpr> { 
                    head, tail 
                });
            }

            throw new Exception("probelms");
        }

        public static SExpr carVal (List <SExpr> inputList) {
            if (inputList[0] is SExpr.SEList list && list.Elements.Count > 0) {
                return list.Elements[0];
            } 
            else {
                throw new ArgumentException("car requires a non-empty list");
            }
        }

        public static SExpr cdrVal (List <SExpr> inputList) {
            if (inputList[0] is SExpr.SEList list && list.Elements.Count > 0) {
                List<SExpr> rest = list.Elements.GetRange(1, list.Elements.Count - 1);
                return new SExpr.SEList(rest);
            }
            else {
                throw new ArgumentException("cdr requires a non-empty list");
            }
        }

        public static SExpr Addition (List <SExpr> inputNums) {
            double result = 0;
            foreach (SExpr sx in inputNums) {
                if (sx is SExpr.Atom numAtom && numAtom.Type == SExpr.AtomType.Number) {
                    result = result + double.Parse(numAtom.Value);
                }
                
            }
            //add error handling if not numbers
            return new SExpr.Atom(result.ToString(), SExpr.AtomType.Number);
        }

        public static SExpr Subtract (List <SExpr> inputNums) {
            double result = 0;
            //foreach (SExpr sx in inputNums) {
                //if (sx is SExpr.Atom numAtom && numAtom.Type == SExpr.AtomType.Number) {
                    //result = result - double.Parse(numAtom.Value);
                //}
            //}
            if (inputNums[0] is SExpr.Atom firstAtom && firstAtom.Type == SExpr.AtomType.Number) {
                result = double.Parse(firstAtom.Value); //initialize result to the first number otherwise subtracts from 0 first
            }
            for (int i = 1; i < inputNums.Count; i++) {
                if (inputNums[i] is SExpr.Atom numAtom && numAtom.Type == SExpr.AtomType.Number) {
                    result -= double.Parse(numAtom.Value); //subtract subsequent numbers
                }
                else {
                    throw new ArgumentException("Subtract requires numeric arguments.");
                }
            }
            //add error handling if not numbers
            return new SExpr.Atom(result.ToString(), SExpr.AtomType.Number);
        }

        public static SExpr Multiply (List <SExpr> inputNums) {
            double result = 1;
            foreach (SExpr sx in inputNums) {
                if (sx is SExpr.Atom numAtom && numAtom.Type == SExpr.AtomType.Number) {
                    result = result * double.Parse(numAtom.Value);
                }
            }
            //add error handling if not numbers
            return new SExpr.Atom(result.ToString(), SExpr.AtomType.Number);
        }

        //public SExpr Divide (List <SExpr> inputNums) {
            //double[] result = new double[2];
            //add error handling if not numbers

            //for division only - cant divide by 0
            //if (result[1] == 0) {
                //throw new RuntimeException("Division by zero.");
            //}
            //return result[0]/result[1];
        //}

        public static SExpr Divide (List <SExpr> inputNums) {
            if (inputNums.Count != 2) {
                throw new Exception("'div' expects exactly two arguments.");
            }

            //extract and parse the two numbers
            if (inputNums[0] is SExpr.Atom num1 && num1.Type == SExpr.AtomType.Number &&
                inputNums[1] is SExpr.Atom num2 && num2.Type == SExpr.AtomType.Number) {
                
                double numerator = double.Parse(num1.Value);
                double denominator = double.Parse(num2.Value);

                //check for division by zero
                if (denominator == 0) {
                    throw new Exception("Division by zero.");
                }

                double result = numerator / denominator;
                return new SExpr.Atom(result.ToString(), SExpr.AtomType.Number);
            }

            throw new Exception("Non-number argument passed to 'div'.");
        }

        //mod??
        //public SExpr Modulo (List <SExpr> inputNums) {
            //double[] result = new double[2];
            //add error handling if not numbers

            //for mod only - cant mod by 0
            //if (result[1] == 0) {
                //throw new RuntimeException("Modulo by zero.");
            //}
            //return result[0] % result[1];
        //}

        public static SExpr Modulo (List <SExpr> inputNums) {
            if (inputNums.Count != 2) {
                throw new Exception("'mod' expects exactly two arguments.");
            }

            //extract and parse the two numbers
            if (inputNums[0] is SExpr.Atom num1 && num1.Type == SExpr.AtomType.Number &&
                inputNums[1] is SExpr.Atom num2 && num2.Type == SExpr.AtomType.Number) {
                
                double numerator = double.Parse(num1.Value);
                double denominator = double.Parse(num2.Value);

                //check for division by zero
                if (denominator == 0) {
                    throw new Exception("Mod by zero.");
                }

                double result = numerator % denominator;
                return new SExpr.Atom(result.ToString(), SExpr.AtomType.Number);
            }

            throw new Exception("Non-number argument passed to 'mod'.");
        }

        public static SExpr lessThan (List <SExpr> inputNums) {
            if (inputNums[0] is SExpr.Atom num1 && num1.Type == SExpr.AtomType.Number &&
             inputNums[1] is SExpr.Atom num2 && num2.Type == SExpr.AtomType.Number) {
                if (double.Parse(num1.Value) < double.Parse(num2.Value)) {
                    return SExpr.True;
                }
                else {
                    return SExpr.False;
                }
            }
            throw new Exception("Arguments to lt must be numbers");
        }

        public static SExpr lessThanEqu (List <SExpr> inputNums) {
            if (inputNums[0] is SExpr.Atom num1 && num1.Type == SExpr.AtomType.Number &&
             inputNums[1] is SExpr.Atom num2 && num2.Type == SExpr.AtomType.Number) {
                if (double.Parse(num1.Value) <= double.Parse(num2.Value)) {
                    return SExpr.True;
                }
                else {
                    return SExpr.False;
                }
            }
            throw new Exception("Arguments to lte must be numbers");
        }

        public static SExpr gtrThan (List <SExpr> inputNums) {
            if (inputNums[0] is SExpr.Atom num1 && num1.Type == SExpr.AtomType.Number &&
             inputNums[1] is SExpr.Atom num2 && num2.Type == SExpr.AtomType.Number) {
                if (double.Parse(num1.Value) > double.Parse(num2.Value)) {
                    return SExpr.True;
                }
                else {
                    return SExpr.False;
                }
            }
            throw new Exception("Arguments to gt must be numbers");
        }

        public static SExpr gtrThanEqu (List <SExpr> inputNums) {
            if (inputNums[0] is SExpr.Atom num1 && num1.Type == SExpr.AtomType.Number &&
             inputNums[1] is SExpr.Atom num2 && num2.Type == SExpr.AtomType.Number) {
                if (double.Parse(num1.Value) >= double.Parse(num2.Value)) {
                    return SExpr.True;
                }
                else {
                    return SExpr.False;
                }
            }
            throw new Exception("Arguments to gte must be numbers");
        }

        //eq
        public static SExpr SEEqu (List<SExpr> args) {
            if (args.Count != 2) {
                throw new Exception("eq expects exactly two arguments");
            }

            var expr1 = args[0];
            var expr2 = args[1];

            // Check if both arguments are atoms
            if (expr1 is SExpr.Atom atom1 && expr2 is SExpr.Atom atom2) {
                // Compare their values
                if (atom1.Value == atom2.Value) {
                    return SExpr.True;
                }
                else {
                    return SExpr.False;
                }
            }

            // If either argument is not an atom, return false
            return SExpr.False;
        }



        //not
        public static SExpr SENot(List<SExpr> args) {
            if (args.Count != 1) {
                throw new Exception("not expects exactly one argument");
            }

            var arg = args[0];

            // Return True if the argument is Nil or False, otherwise return False
            if (arg == SExpr.Nil || arg == SExpr.False) {
                return SExpr.True;
            }
            else {
                return SExpr.False;
            }
        }




    }
}