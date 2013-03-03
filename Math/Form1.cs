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
                    Calculator calc = new Calculator();
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
            string expression = str;

            if (str.Contains("="))
            {
                expression = str.Substring(str.LastIndexOf('=') + 1);
            }

            return expression;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            textBox1.Focus();
            textBox1.SelectAll();
        }
    }    
}
