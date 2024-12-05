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

        public static SExpr consCell(List <SExpr> args) {
            if (args.Count != 2) {
                throw new Exception("cons expects exactly two arguments.");
            }

            var first = args[0];
            var second = args[1];

            //ensure the second argument is a list
            if (second is SExpr.SEList secondList) {
                //prepend the first argument to the second list
                var newElements = new List<SExpr> { first };
                newElements.AddRange(secondList.Elements);
                return new SExpr.SEList(newElements);
            }

            //if the second argument is not a list, treat it as the tail
            return new SExpr.SEList(new List<SExpr> { first, second });
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
        public static SExpr SEEqu (List <SExpr> input) {
            if (input.Count != 2) {
                throw new Exception("eq expects exactly two arguments");
            }

            var expr1 = input[0];
            var expr2 = input[1];

            //check if both arguments are atoms
            if (expr1 is SExpr.Atom atom1 && expr2 is SExpr.Atom atom2) {
                //compare their values
                if (atom1.Value == atom2.Value) {
                    return SExpr.True;
                }
                else {
                    return SExpr.False;
                }
            }

            //if either argument is not an atom, return false
            return SExpr.False;
        }



        //not
        public static SExpr SENot (List <SExpr> input) {
            if (input.Count != 1) {
                throw new Exception("not expects exactly one argument");
            }

            var arg = input[0];

            //return true if the argument is nil or false, otherwise return false
            if (arg == SExpr.Nil || arg == SExpr.False) {
                return SExpr.True;
            }
            else {
                return SExpr.False;
            }
        }

        //and
        /*public static SExpr SEAnd (List <SExpr> input) {
            if (input[0] is SExpr.Atom atomAnd1 && atomAnd1 == SExpr.Nil) {
                return SExpr.False;
            }
            else {
                if (input[1] is SExpr.Atom atomAnd2 && atomAnd2 == SExpr.Nil) {
                    return SExpr.False;
                }
                else {
                    return SExpr.True;
                }
            }
        }
        */


        //or
        /*public static SExpr SEOr (List <SExpr> input) {
            if (input[0] is SExpr.Atom atomOr1 && atomOr1 != SExpr.Nil) {
                return SExpr.True;
            }
            else {
                if (input[1] is SExpr.Atom atomOr2 && atomOr2 != SExpr.Nil) {
                    return SExpr.True;
                }
                else {
                    return SExpr.False;
                }
            }
        }
        */
        public static SExpr SEAnd (List <SExpr> input) {
            foreach (var expr in input) {
                if (expr is SExpr.Atom atomAnd && SExpr.ToBool(atomAnd) == false) {
                    return SExpr.False; 
                }
            }
            return SExpr.True;
        }

        public static SExpr SEOr (List <SExpr> input) {
            foreach (var expr in input) {
                if (expr is SExpr.Atom atomOr && SExpr.ToBool(atomOr) == true) {
                    return expr; //return first non-nil value
                }
            }
            return SExpr.False; //return false if all are nil
        }


        //if



        //cond




    }
}