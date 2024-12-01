using System;
using System.Text.RegularExpressions;

namespace MyLisp {
    
    public class Reader {
            //stores tokens
            List <string> tokens;

            //stores position
            int currPos;

            //constructor
            public Reader(List<string> tokens) {
                this.tokens = tokens;
                this.currPos = 0;
            }

            //returns token at currPos and increments
            public string next() {
                currPos++;
                return tokens[currPos - 1];
            }

            //just returns token at currPos
            public string peek() {
                return tokens[currPos];
            }
        
        

        public static List <string> tokenize(string input) {
            //list to hold the tokenized output
            List <string> tokens = new List <string>();
            string regexStr = @"[\s ,]*(~@|[\[\]{}()'`~@]|""(?:[\\].|[^\\""])*""?|;.*|[^\s \[\]{}()'""`~@,;]*)";

            //from before i knew about pattern matching lol
            /*for (int i = 5; i < input.Length(); i++) {
                if (input[i] == "~" && input[i+1] ==  "@") {

                }
                else if ()
            }
            */
            Regex regex = new Regex(regexStr);
            MatchCollection matches = regex.Matches(input); //match input string regex's to ones from the list
            foreach (Match match in matches) {
                string token = match.Value;
                token = token.Trim(); //get rid of the whitespace
                tokens.Add(token);
            } 
            //ADD ERROR HANDLING?? null tokens etc
            
            return tokens;

        }

        //function for mapping sexprs to bool equivalent
        public static bool ToBool(SExpr expr) {
            if (expr == SExpr.Nil) return false; // `nil` is false
            if (expr is SExpr.Atom atom && atom.Type == SExpr.AtomType.Constant && atom.Value == "false") return false; // `false` is false
            return true; //everything else is true
        }


        public static SExpr readAtom(Reader inputReader) {
            string token = inputReader.next();
            if (Regex.IsMatch(token, @"^nil$")) {
                return SExpr.Nil;
            }
            else if (Regex.IsMatch(token, @"^true$")) {
                return SExpr.True;
            }
            else if (Regex.IsMatch(token, @"^false$")) {
                return SExpr.False;
            }
            else if (Regex.IsMatch(token, @"^-?\d+(\.\d+)?$")) {
                return new SExpr.Atom(inputReader.peek(), SExpr.AtomType.Number);
            }
            else if (Regex.IsMatch(token, @"^[a-zA-Z_\+\-\*/=!<>&|]+[a-zA-Z0-9_\+\-\*/=!<>&|]*$")) {
                return new SExpr.Atom(inputReader.peek(), SExpr.AtomType.Symbol);
            }
            //else if (Regex.IsMatch(token, @"^\"(\\.|[^\"])*\"$"))) {
                //return new SExpr.Atom(inputReader.tokens[currPos], AtomType.String);
            //}
            //else if () {
                //return new SExpr.Atom(inputReader.tokens[currPos], AtomType.Operation);
            //}
            //else throw an error??
            else {
                throw new Exception($"Unrecognized token: {token}");
            }
        }

        public static SExpr readList(Reader inputReader) {
            List<SExpr> tokenVals = new();
            while (inputReader.peek() != ")") {
                readForm(inputReader);
                //add if it reaches eof before ) then throw an error
            }
            inputReader.next();

            return new SExpr.SEList(tokenVals);
            
        }

        public static SExpr readForm(Reader inputReader) { //need to add mal data type????
            if (inputReader.peek() == "(") {
                return readList(inputReader);
            }
            else {
                return readAtom(inputReader);
            }

        }

        //FIXME return values - this doesnt return anything bc return vals dont make sense, think more
        public static string readStr(string input) {
            //tokenize input
            List <string> tokenList = tokenize(input);

            //create new reader object with the tokens
            Reader reader = new Reader(tokenList);

            //call readForm with reader instance
            readForm(reader);
        }

    }
}