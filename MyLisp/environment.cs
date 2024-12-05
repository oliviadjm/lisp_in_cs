using System;
using System.Collections.Generic;

namespace MyLisp {

    public class Environment {
        //environment stack
        private Stack<Dictionary<string, SExpr>> EnvStack = new Stack<Dictionary<string, SExpr>>();

        public Environment() {
            //public Dictionary<string, SExpr> Env = 
            EnvStack.Push(new Dictionary<string, SExpr>() {
                {"add", new SExpr.SEFunction(Operations.Addition)},
                {"sub", new SExpr.SEFunction(Operations.Subtract)},
                {"mul", new SExpr.SEFunction(Operations.Multiply)},
                {"div", new SExpr.SEFunction(Operations.Divide)},
                {"mod", new SExpr.SEFunction(Operations.Modulo)},
                {"cons", new SExpr.SEFunction(Operations.consCell)},
                {"car", new SExpr.SEFunction(Operations.carVal)},
                {"cdr", new SExpr.SEFunction(Operations.cdrVal)},
                {"lt", new SExpr.SEFunction(Operations.lessThan)},
                {"gt", new SExpr.SEFunction(Operations.gtrThan)},
                {"lte", new SExpr.SEFunction(Operations.lessThanEqu)},
                {"gte", new SExpr.SEFunction(Operations.gtrThanEqu)},
                {"nil?", new SExpr.SEFunction(Operations.SENil)},
                {"symbol?", new SExpr.SEFunction(Operations.SESym)},
                {"number?", new SExpr.SEFunction(Operations.SENum)},
                {"list?", new SExpr.SEFunction(Operations.SEListFunc)},
                {"eq?", new SExpr.SEFunction(Operations.SEEqu)},
                {"not", new SExpr.SEFunction(Operations.SENot)},
                {"and", new SExpr.SEFunction(Operations.SEAnd)},
                {"or", new SExpr.SEFunction(Operations.SEOr)},
            });

            //EnvStack.pushEnvironment(Env);
        }
       
        //public Environment(Dictionary <string, SExpr> localEnv) {
            //EnvStack.Push(localEnv);
        //}

        //constructor
        public Environment(Environment parentEnv) {
            //copy the parent environment stack
            //EnvStack = new Stack<Dictionary<string, SExpr>>(new Stack<Dictionary<string, SExpr>>(parentEnv.EnvStack));
            //add a new local environment on top of the stack
            //EnvStack.Push(new Dictionary<string, SExpr>());

            EnvStack.Push(new Dictionary <string, SExpr>());
            foreach (var dict in parentEnv.EnvStack) {
                EnvStack.Push(dict); //inherit from parent environment
            }
        }

        
        //public SExpr lookup(string name, Dictionary<string, SExpr> Env) {
        public SExpr lookup(string name) { 
            foreach (var env in EnvStack) {
                if (env.TryGetValue(name, out var value)) {
                    return value;
                }
            }
            throw new Exception($"Symbol '{name}' is not defined.");
        }

        public void define(string name, SExpr value) {
            EnvStack.Peek()[name] = value;
        }

        //push a new environment onto the stack
        public void pushEnvironment (Dictionary <string, SExpr> newEnv) {
            EnvStack.Push(newEnv);
        }

        //pop the top environment from the stack
        public void popEnvironment() {
            if (EnvStack.Count > 1) {
                EnvStack.Pop();
            }
            else {
                throw new Exception("Cannot pop the global environment.");
            }
        }


    }
}