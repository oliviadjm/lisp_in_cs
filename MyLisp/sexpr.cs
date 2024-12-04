using System;

namespace MyLisp {
    //data structure for s expressions - meant to be in sprint 1 but i left it to sprint 2 accidentally
    public abstract class SExpr {
        public virtual SExpr copy() {
            return (SExpr)this.MemberwiseClone();
        }
        
        //list of possible types an atom could be
        public enum AtomType {
            Symbol, Number, String, Constant, Operation,
        }

        public class Atom : SExpr {
            public string Value { get; }
            public AtomType Type { get; }

            //public Atom copy() { return this; }

            public Atom(string value, AtomType type) {
                Value = value;
                Type = type;
            }

            public override string ToString() => Value; //for debugging
        }

        //public class Number : Atom {

        //}

        //public class Symbol : Atom {

        //}

        //public class SEString : Atom {

        //}

        //for sprint 2: add nil, true/false
        static public Atom Nil = new Atom("nil", AtomType.Constant);
        static public Atom True = new Atom("true", AtomType.Constant);
        static public Atom False = new Atom("false", AtomType.Constant);

        public class SEList : SExpr {
            public List <SExpr> Elements { get; }

            public SEList(List <SExpr> elements) {
                Elements = elements;
            }

            public override string ToString() {
                //print the list as "(elem1 elem2 ...)"
                return $"({string.Join(" ", Elements)})";
            }
        }

        public class SEFunction : SExpr {
            public Func<List<SExpr>, SExpr> Function { get; }

            public SEFunction(Func<List<SExpr>, SExpr> function) {
                Function = function;
            }

            public SExpr Invoke(List<SExpr> args) {
                return Function(args);
            }

            public override string ToString() => "<function>";
        }

        public class Quote : SExpr {
            public SExpr Expression { get; }

            public Quote(SExpr expression) {
                Expression = expression;
            }

            public override string ToString() => "'" + Expression.ToString();
        }

        //function for mapping sexprs to bool equivalent
        public static bool ToBool(SExpr expr) {
            if (expr == SExpr.Nil) return false; // `nil` is false
            //if (expr is SExpr.Atom atom && atom.Type == SExpr.AtomType.Constant && atom.Value == "false") return false; // `false` is false
            if (expr is SExpr.Atom atom && atom.Value == "false") return false;
            return true; //everything else is true
        }


    }
}