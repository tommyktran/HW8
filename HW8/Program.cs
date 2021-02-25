using System;
using System.Collections.Generic;
using System.Linq;

namespace HW8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> infixList = InfixListFromUser();
            List<string> postfixList = PostfixFromInfix(infixList);
            //DisplayList("Postfix Queue:", postfixList);
            Console.Write("Infix list: ");
            foreach (string token in infixList)
            {
                Console.Write(token + " ");
            }
            Console.WriteLine();
            Console.Write("Postfix list: ");
            foreach (string token in postfixList)
            {
                Console.Write(token + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"The value of the expression is {ValueOfPostfixList(postfixList)}.");
        }

        // Prompt the user for an expression, and return a list of tokens from the user input.
        static List<string> InfixListFromUser()
        {
            /*
             * Your code goes here ...
             */
            Console.Write("Enter a mathematical expression: ");
            string expression = Console.ReadLine();
            var infixList = TokensFromString(expression);
            return infixList;

            //string expression = "3 + 6 ^ 2 / ((4 - 2) * 2)";
            //var infixList = TokensFromString(expression);

            //return infixList;
        }

        // Change the list of tokens from infix to postfix format.
        static List<string> PostfixFromInfix(List<string> infixList)
        {
            var opStack = new Stack<string>();
            var postfixList = new List<string>();
            foreach (string token in infixList)
            {
                /*
                 * Your code goes here ...
                 * 
                 * If the token is a number then
                 *      add the token to postfixList
                 * else
                 * {
                 *      while the last op in opStack should be executed before the cur op
                 *      {
                 *          pop the last op in opStack
                 *          add it to postfixList unless it's "("
                 *      }
                 *      add the cur op to postfixList unless it's ")"
                 * }
                 */
                
                if (new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" }.Contains(token)) {
                    postfixList.Add(token);
                } else
                {
                    //foreach (string value in postfixList)
                    //{
                    //    Console.Write(value + " ");
                    //}
                    //Console.WriteLine();
                    //foreach (string value in opStack)
                    //{
                    //    Console.Write(value + " ");
                    //}
                    //Console.WriteLine();
                    //Console.WriteLine("--------------");
                    while (PrevOpShouldBeExecutedBeforeCurOp(opStack, token) || (token == ")" && opStack.Peek() != "("))
                    {
                        if (opStack.Peek() != "(")
                        {
                            postfixList.Add(opStack.Pop());
                        } else
                        {
                            opStack.Pop();
                            opStack.Pop();
                        }
                    }
                    if (token != ")")
                    {
                        opStack.Push(token);
                    } else if (token == ")" && opStack.Peek() == "(")
                    {
                        opStack.Pop();
                    }
                    
                    
                }
            }

            // Pop each op from opStack and add it to postfixList.
            /*
             * Your code goes here ...
             * 
             * while opStack is not empty
             * {
             *      pop an op from opStack and add it to postfixList
             * }
             *
             */
            while (opStack.Count != 0)
            {
                postfixList.Add(opStack.Pop());
            }
            return postfixList;
        }

        // Return whether the previous op should be executed before the current op.
        static bool PrevOpShouldBeExecutedBeforeCurOp(Stack<string> opStack, string curOp)
        {
            /*
             * Your code goes here ...
             */
            var orderOfOperations = new List<string> { "+", "-", "*", "/", "^", ")", "(" };
            var orderOfOperations2 = new List<string> { "-", "+", "/", "*", "^", ")", "(" };

            var opStack2 = new Stack<string>(opStack.Reverse());
            
            if (curOp == "(")
            {
                return false;
            }
            if (opStack.Contains("("))
            {
                var opArray = opStack2.ToArray();
                int parenthesesIndex = Array.IndexOf(opArray, "(");

                opArray = new string[parenthesesIndex];
                for (int i = 0; i < parenthesesIndex; i++ )
                {
                    opArray[i] = opStack2.Pop();
                }

                opStack2.Clear();
                foreach (string token in opArray)
                {
                    opStack2.Push(token);
                }
            }
            if (opStack2.Count == 0)
            {
                return false;
            }
            if (orderOfOperations.IndexOf(curOp) <= orderOfOperations.IndexOf(opStack2.Peek()))
            {
                return true;
            } else if (orderOfOperations2.IndexOf(curOp) <= orderOfOperations2.IndexOf(opStack2.Peek()))
            {
                return true;
            } else
            {
                return false;
            }
        }


        //// Return the calculated value of the expression given by the postfix list.
        static double ValueOfPostfixList(List<string> postfixList)
        {
            /*
             * Your code goes here ...
             */
            var operators = new List<string> { "+", "-", "*", "/", "^" };
            while (postfixList.Count > 1)
            {
                for (int i = 0; i <= postfixList.Count - 1; i++)
                {
                    if (operators.Contains(postfixList[i]))
                    {
                        postfixList[i] = Convert.ToString(OpResult(Double.Parse(postfixList[i-1]), Double.Parse(postfixList[i - 2]), postfixList[i]));
                        postfixList.RemoveAt(i - 1);
                        postfixList.RemoveAt(i - 2);
                        i = 0;
                    }
                }
            }
            return Double.Parse(postfixList[0]);
        }

        //// Return the result of an operation or "" if the operation is invalid.
        static double OpResult(double num1, double num2, string op)
        {
            /*
             * Your code goes here ...
             */
            

            switch (op)
            {
                case "+":
                    return num2 + num1;
                case "-":
                    return num2 - num1;
                case "*":
                    return num2 * num1;
                case "/":
                    return num2 / num1;
                case "^":
                    return Math.Pow(num2, num1);
                default:
                    return 0.0;
            }
        }


        static List<string> TokensFromString(string expression)
        {
            var tokens = new List<string> { };
            string currentToken = "";
            for (int i = 0; i <= expression.Length - 1; i++)
            {
                if (new List<int> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' }.Contains(expression[i]))
                {

                    currentToken += expression[i];
                    if (i == expression.Length - 1)
                    {
                        if (currentToken != "")
                        {
                            tokens.Add(currentToken);
                        }
                        currentToken = "";
                    }
                }
                else if (expression[i] != ' ')
                {
                    if (currentToken != "")
                    {
                        tokens.Add(currentToken);
                    }
                    tokens.Add(expression[i].ToString());
                    currentToken = "";
                }
            }
            return tokens;
        }
        static void OutputTokens(List<string> tokens)
        {
            foreach (string token in tokens)
            {
                Console.WriteLine(token);
            }
        }

    }
}