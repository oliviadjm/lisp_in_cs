using System;
using System.Collections.Generic;

namespace MyLisp {

    public class Environment {

        public Dictionary<string, SExpr> Env = new Dictionary<string, SExpr>() {
            {"add", new SExpr.SEFunction(Operations.Addition)},
            {"sub", new SExpr.SEFunction(Operations.Subtract)},
            {"mul", new SExpr.SEFunction(Operations.Multiply)},
            {"div", new SExpr.SEFunction(Operations.Divide)},
            {"mod", new SExpr.SEFunction(Operations.Modulo)},
            {"cons", new SExpr.SEFunction(Operations.consCell)},
            {"car", new SExpr.SEFunction(Operations.carVal)},
            {"cdr", new SExpr.SEFunction(Operations.cdrVal)},
        };

       
        public SExpr lookup(string name, Dictionary<string, SExpr> Env) {
            if (Env.TryGetValue(name, out var value))
                return value;

            throw new Exception($"Symbol '{name}' is not defined.");
        }


    }
}