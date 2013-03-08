using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Math.Exceptions;

namespace Math
{
    public class Calculator
    {
        public Operator[] operators;
        public Function[] functions;

        Stack<Token> ops;
        Queue output;
        StringTokenizer tokenizer;
        CalculatorMode mode;

        public enum CalculatorMode
        {
            Degrees,
            Radians
        }

        public Calculator(CalculatorMode mode)
        {
            this.mode = mode;

            operators = new Operator[]
            {
                new Operator('^', 4, Operator.Associativity.Right, (args) => 
                {
                    return System.Math.Pow(args[0], args[1]);
                }),
                new Operator('%', 3, Operator.Associativity.Left, (args) =>
                {
                    return args[0] % args[1];
                }),
                new Operator('*', 3, Operator.Associativity.Left, (args) =>
                {
                    return args[0] * args[1];
                }),
                new Operator('/', 3, Operator.Associativity.Left, (args) =>
                {
                    return args[0] / args[1];
                }),
                new Operator('+', 2, Operator.Associativity.Left, (args) =>
                {
                    return args[0] + args[1];
                }),
                new Operator('-', 2, Operator.Associativity.Left, (args) =>
                {
                    return args[0] - args[1];
                })
            };

            functions = new Function[]
            {
                new Function("abs", 1, (args) =>
                {
                    return System.Math.Abs(args[0]);
                }),
                new Function("pi", 0, (args) =>
                {
                    return System.Math.PI;
                }),
                new Function("e", 0, (args) =>
                {
                    return System.Math.E;
                }),
                // the inverse trig are checked first, 
                // otherwise cos^-1 is found as (function)cos (operator)& (number)-1
                // when (function)cos^-1 is expected
                new Function("cos^-1", 1, (args) =>
                {
                    return System.Math.Acos(args[0]);
                }),
                new Function("sin^-1", 1, (args) =>
                {
                    return System.Math.Asin(args[0]);
                }),
                new Function("tan^-1", 1, (args) =>
                {
                    return System.Math.Atan(args[0]);
                }),
                new Function("cot^-1", 1, (args) =>
                {
                    return 1 / System.Math.Atan(args[0]);
                }),
                new Function("sec^-1", 1, (args) =>
                {
                    return 1 / System.Math.Acos(args[0]);
                }),
                new Function("csc^-1", 1, (args) =>
                {
                    return 1 / System.Math.Asin(args[0]);
                }),
                new Function("cos", 1, (args) =>
                {
                    return System.Math.Cos(args[0]);
                }),
                new Function("sin", 1, (args) =>
                {
                    return System.Math.Sin(args[0]);
                }),
                new Function("tan", 1, (args) =>
                {
                    return System.Math.Tan(args[0]);
                }),
                new Function("sec", 1, (args) =>
                {
                    return 1 / System.Math.Cos(args[0]);
                }),
                new Function("csc", 1, (args) =>
                {
                    return 1 / System.Math.Sin(args[0]);
                }),
                new Function("cot", 1, (args) =>
                {
                    return 1 / System.Math.Tan(args[0]);
                }),
                new Function("sqrt", 1, (args) =>
                {
                    return System.Math.Sqrt(args[0]);
                }),
                new Function("min", 2, (args) =>
                {
                    return System.Math.Min(args[0], args[1]);
                }),
                new Function("max", 2, (args) =>
                {
                    return System.Math.Max(args[0], args[1]);
                }),
                new Function("ceil", 1, (args) =>
                {
                    return System.Math.Ceiling(args[0]);
                }),
                new Function("jeff", 0, (args) =>
                    {
                        return 6969;
                    }),
                new Function("floor", 1, (args) =>
                {
                    return System.Math.Floor(args[0]);
                }),
                new Function("round", 1, (args) =>
                {
                    return System.Math.Round(args[0]);
                })/*,
                new Function("june", 0, (args) =>
                {
                    return 80;
                }),
                new Function("renzo", 0, (args) =>
                {
                    return 9;
                })*/
            };
        }

        public void SetMode(CalculatorMode mode)
        {
            this.mode = mode;
        }

        private double DegreeToRadian(double angle)
        {
            return System.Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / System.Math.PI);
        }

        // async solve?
        public double Solve(string expression)
        {
            ops = new Stack<Token>();
            output = new Queue();

            // Tokenize the input
            tokenizer = new StringTokenizer(expression, true);
            tokenizer.SymbolChars = new char[] { ',' };
            tokenizer.Operators = operators;
            tokenizer.Functions = functions;

            InfixToRPN();

            return EvaluateRPN();
        }

        private void InfixToRPN()
        {
            Token token = tokenizer.Next();
            Token prevToken = null;

            while (token.Kind != TokenKind.EOF)
            {
                if (token.Kind == TokenKind.Number)
                {
                    //output.Enqueue(double.Parse((string)token.Value));
                    output.Enqueue(token);
                }
                else if (token.Kind == TokenKind.Function)
                {
                    ops.Push(token);
                }
                else if (token.Kind == TokenKind.Symbol)
                {
                    // function argument seperator
                    if (token.Value.ToString() == ",")
                    {
                        while (ops.Peek().Kind != TokenKind.LeftParentheses)
                        {
                            if (ops.Count == 0)
                            {
                                // separator misplaced or parentheses mismatched
                                throw new MismatchedParenthesisException();
                            }

                            output.Enqueue(ops.Pop());
                        }
                    }
                }
                else if (token.Kind == TokenKind.Operator)
                {
                    evalOperator(token);
                }
                else if (token.Kind == TokenKind.LeftParentheses)
                {
                    if (prevToken != null && prevToken.Kind != TokenKind.Operator && prevToken.Kind != TokenKind.Function)
                        evalOperator(new Token(TokenKind.Operator, operators[2], 0, 0));
                    ops.Push(new Token(TokenKind.LeftParentheses, new Operator('(', 0, Operator.Associativity.None), 0, 0));
                    //ops.Push(new Operator('(', 0, Operator.Associativity.None));
                }
                else if (token.Kind == TokenKind.RightParentheses)
                {
                    Token op;
                    do
                    {
                        op = ops.Pop();
                        if (op.Kind != TokenKind.LeftParentheses)
                            output.Enqueue(op);


                        /* if (token.Kind == TokenKind.EOF && )
                         {
                             throw new MismatchedParenthesisException();
                         }*/
                    } while (op.Kind != TokenKind.LeftParentheses);
                }

                prevToken = token;
                token = tokenizer.Next();
            }

            // since there are no more tokens, move onto the stack

            // while there are still operator tokens in the stack
            while (ops.Count > 0)
            {
                Operator nextOp = ((Operator)ops.Peek().Value);
                if (nextOp.symbol == '(' || nextOp.symbol == ')')
                {
                    throw new MismatchedParenthesisException();
                }
                else
                {
                    output.Enqueue(ops.Pop());
                }
            }
        }

        void evalOperator(Token token)
        {
            Operator op = (Operator)token.Value;
            while (ops.Count > 0
                && (
                    (op.assoc == Operator.Associativity.Left && op.precedence <= ((Operator)ops.Peek().Value).precedence)
                    ||
                    (op.assoc == Operator.Associativity.Right && op.precedence < ((Operator)ops.Peek().Value).precedence)
                )
            )
            {
                output.Enqueue(ops.Pop());
            }

            ops.Push(token);
        }

        private double EvaluateRPN()
        {
            Stack<Token> evaluate = new Stack<Token>();
            do
            {
                Token next = (Token)output.Peek();
                if (next.Kind == TokenKind.Number)
                {
                    evaluate.Push((Token)output.Dequeue());
                }
                else
                {
                    // it's an operator (or function)
                    Operator op = (Operator)next.Value;

                    // if there are fewer than the number of arguments on the stack
                    if (evaluate.Count < op.numArgs)
                    {
                        // not enough values supplied
                        throw new Exception("not enough values supplied");
                    }
                    else
                    {
                        double[] args = new double[op.numArgs];

                        for (int i = op.numArgs - 1; i >= 0; i--)
                        {
                            Token t = ((Token)evaluate.Pop());
                            Console.WriteLine(t);
                            args[i] = double.Parse(t.Value.ToString());
                        }

                        output.Dequeue();

                        Token result = new Token(TokenKind.Number, op.PerformCalculation(args), 0, 0);
                        evaluate.Push(result);
                    }
                }
            } while (output.Count > 0);

            if (evaluate.Count == 1)
            {
                // this is the answer!
                return double.Parse(evaluate.Pop().Value.ToString());
            }
            else
            {
                // too many values were input
                throw new Exception("too many values supplied");
            }
        }
    }
}
