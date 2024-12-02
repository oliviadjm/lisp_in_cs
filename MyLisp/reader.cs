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

            public string prevPeek() {
                return tokens[currPos - 1];
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
            //Regex regex = new Regex(regexStr);
            //MatchCollection matches = regex.Matches(input); //match input string regex's to ones from the list
            //foreach (Match match in matches) {
                //string token = match.Value;
                //token = token.Trim(); //get rid of the whitespace
                //tokens.Add(token);
            //} 
            //ADD ERROR HANDLING?? null tokens etc
            Regex regex = new Regex(regexStr);
            foreach (Match match in regex.Matches(input)) {
                string token = match.Groups[1].Value;
                if ((token != null) && !(token == "") && !(token[0] == ';')) {
                    //Console.WriteLine("match: ^" + match.Groups[1] + "$");
                    tokens.Add(token);
                }
            }
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
                return new SExpr.Atom(inputReader.prevPeek(), SExpr.AtomType.Number);
            }
            else if (Regex.IsMatch(token, @"^[a-zA-Z_\+\-\*/=!<>&|]+[a-zA-Z0-9_\+\-\*/=!<>&|]*$")) {
                return new SExpr.Atom(inputReader.prevPeek(), SExpr.AtomType.Symbol);
            }
            //else if (Regex.IsMatch(token, @"^\"(\\.|[^\"])*\"$"))) {
                //return new SExpr.Atom(inputReader.tokens[currPos], AtomType.String);
            //}
            //else if () {
                //return new SExpr.Atom(inputReader.tokens[currPos], AtomType.Operation);
            //}
            else if (token == "'") { //for quoted lists for cons
                SExpr quotedExpression = readForm(inputReader);
                return new SExpr.Quote(quotedExpression);
            }
            //else throw an error??
            else {
                throw new Exception($"Unrecognized token: {token}");
            }
        }

        public static SExpr readList(Reader inputReader) {
            List<SExpr> tokenVals = new();
            inputReader.next(); //consume opening paren
            while (inputReader.peek() != ")") {
                //add if it reaches eof before ) then throw an error
                if (inputReader.peek() == null) {
                    throw new Exception("Unexpected EOF while reading list. Missing closing ')'");
                }
                tokenVals.Add(readForm(inputReader));
            }
            inputReader.next(); //consume closing paren

            return new SExpr.SEList(tokenVals);
            
        }

        public static SExpr readForm(Reader inputReader) { //added sexpr datatype
            if (inputReader.peek() == "(") {
                return readList(inputReader);
            }
            else {
                return readAtom(inputReader);
            }

        }

        //FIXEd return values - this doesnt return anything bc return vals dont make sense, think more
        public static SExpr readStr(string input) {
            //tokenize input
            List <string> tokenList = tokenize(input);

            //create new reader object with the tokens
            Reader reader = new Reader(tokenList);

            //call readForm with reader instance
            return readForm(reader);

        }
    }
}