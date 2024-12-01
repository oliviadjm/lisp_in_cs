using System;
using System.Collections.Generic;


namespace MyLisp {
    public static class Printer {
        //function to return the string representation of sexpr
        public static string prStr(SExpr sx) {
            switch (sx) {
                case SExpr.Atom atom:
                    return atom.Value; //directly return the value of the atom

                case SExpr.SEList list:
                    //for list, recursively call prStr on each element and join them with spaces
                    return $"({string.Join(" ", list.Elements.Select(prStr))})";

                default:
                    throw new ArgumentException("Unknown type to print");
            }
        }
    }
}