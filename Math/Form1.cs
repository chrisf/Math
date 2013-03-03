// http://en.wikipedia.org/wiki/Reverse_Polish_notation
// http://en.wikipedia.org/wiki/Shunting-yard_algorithm
// http://www.codeproject.com/KB/recipes/stringtokenizer.aspx

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Math.Exceptions;

namespace Math
{
    public partial class Form1 : Form
    {
        Calculator calc;
        bool keydown;

        public Form1()
        {
            InitializeComponent();
            calc = new Calculator();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            double answer = calc.Solve(textBox1.Text);
            label1.Text = answer.ToString();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '=')
                {
                    e.Handled = true;
                    double result = calc.Solve(GetExpression());

                    richTextBox1.AppendText("= "+result.ToString());
                }
            }
            catch { }
        }

        string GetExpression()
        {
            int currentLine = richTextBox1.GetLineFromCharIndex(richTextBox1.GetFirstCharIndexOfCurrentLine());
            return GetExpression(richTextBox1.Lines[currentLine]);
        }
        
        string GetExpression(string str)
        {
            if (str.Contains("="))
            {
                return str.Substring(str.LastIndexOf('=') + 1);
            }

            return str;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            textBox1.Select();
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str;
            string nl = Environment.NewLine;
            str = "Supported operators: " + nl;
            for(int i = 0; i < calc.operators.Length; i++)
            {
                str += calc.operators[i].symbol;

                if (i % 2 == 1 || i == calc.operators.Length - 1)
                    str += nl;
                else
                    str += "\t";
            }
            str += "Supported functions:" + nl;
            for (int i = 0; i < calc.functions.Length; i++)
            {
                str += calc.functions[i].funcName + "(";
                for (int j = 0; j < calc.functions[i].numArgs; j++)
                {
                    switch (j)
                    {
                        case 0:
                            str += "a";
                            break;
                        case 1:
                            str += "b";
                            break;
                        case 2:
                            str += "c";
                            break;
                        case 3:
                            str += "d";
                            break;
                        case 4:
                            str += "e";
                            break;
                        case 5:
                            str += "f";
                            break;
                        default:
                            str += "n";
                            break;
                    }

                    if (j < calc.functions[i].numArgs - 1)
                        str += ", ";
                }

                str += ")";

                if (i % 2 == 1 || i == calc.operators.Length - 1)
                    str += nl;
                else
                    str += "\t";

            }
            MessageBox.Show(str);
        }
    }    
}
