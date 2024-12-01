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
    }
}