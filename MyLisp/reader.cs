using System;
using System.Text.RegularExpressions;

public class Reader {
    //stores tokens
    List <string> tokens;

    //stores position
    int currPos;

    //returns token at currPos and increments
    public string next() {
        currPos++;
        return tokens[currPos - 1];
    }

    //just returns token at currPos
    public string peek() {
        return tokens[currPos];
    }
}

public static List <string> tokenize(string input) {
    //list to hold the tokenized output
    List <string> tokens = new List <string>();
    string regexStr = @"[\s,]*(~@|[\[\]{}()'`~^@]|"(?:\\.|[^\\"])*"?|;.*|[^\s\[\]{}('"`,;)]*)";

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

public static SExpr readAtom(Reader inputReader) {
    string token = inputReader.next();
    if (Regex.IsMatch(token, @"^nil$")) {
        return sexpr.Nil;
    }
    else if (Regex.IsMatch(token, @"^true$")) {
        return sexpr.True;
    }
    else if (Regex.IsMatch(token, @"^false$")) {
        return sexpr.False;
    }
    //else throw an error??
}

public static SExpr readList(Reader inputReader) {
    while (inputReader.peek() != ")") {
        readForm(inputReader);
        //add if it reaches eof before ) then throw an error
    }
    inputReader.next();
    
}

public static SExpr readForm(Reader inputReader) { //need to add mal data type????
    if (inputReader.peek() == "(") {
        readList(inputReader);
    }
    else {
        readAtom(inputReader);
    }

}

public static string readStr(string input) {
    //tokenize input
    List <string> tokenList = tokenize(input);

    //create new reader object with the tokens
    Reader reader = new Reader(tokenList);

    //call readForm with reader instance
    readForm(reader);
}

