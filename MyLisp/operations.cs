using System;

namespace MyLisp {
    public class Operations {

        public object SENil (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj == null) {
                return true;
            }
            else if (obj is SExpr.SEList nestedList &&  nestedList.Elements.Count == 0) {
                return true;
            }
            else {
                return false;
            }
        }

        public object SESym (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj is SExpr.Atom symAtom && symAtom.Type == SExpr.AtomType.Symbol) {
                return true;
            }
            else {
                return false;
            }
        }

        public object SENum (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj is SExpr.Atom numAtom && numAtom.Type == SExpr.AtomType.Number) {
                return true;
            }
            else {
                return false;
            }
        }

        public object SEListFunc (List <SExpr> inputList) {
            object obj = inputList[0];
            if (obj is SExpr.SEList) {
                return true;
            }
            else {
                return false;
            }
        }

        //public consCell () {

        //}

        //public carVal () {

        //}

        //public cdrVal () {

        //}
    }
}