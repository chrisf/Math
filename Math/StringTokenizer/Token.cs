// http://www.codeproject.com/KB/recipes/stringtokenizer.aspx
/********************************************************
 *	Author: Andrew Deren
 *	Date: July, 2004
 *	http://www.adersoftware.com
 * 
 *	StringTokenizer class. You can use this class in any way you want
 * as long as this header remains in this file.
 * 
 **********************************************************/

using System;

namespace Math
{
	public enum TokenKind
	{
		Unknown,
		Word,
		Number,
		QuotedString,
		WhiteSpace,
		Symbol,
		EOL,
		EOF,
        Operator,
        Function,
        LeftParentheses,
        RightParentheses
	}

	public class Token
	{
		int line;
		int column;
		object value;
		TokenKind kind;

        public Token() { }

		public Token(TokenKind kind, object value, int line, int column)
		{
			this.kind = kind;
			this.value = value;
			this.line = line;
			this.column = column;
		}

		public int Column
		{
			get { return this.column; }
            set { this.column = value; }
		}

		public TokenKind Kind
		{
			get { return this.kind; }
            set { this.kind = value; }
		}

		public int Line
		{
			get { return this.line; }
            set { this.line = value; }
		}

		public object Value
		{
			get { return this.value; }
            set { this.value = value; }
		}
	}

}