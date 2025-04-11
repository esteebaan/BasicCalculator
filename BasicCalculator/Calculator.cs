using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class Calculator: Form
    {
        private double firstNumber = 0;
        private string currentOperation = "";
        private bool isNewNumber = true;
        private bool operationPending = false;
        public Calculator()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
            
        }

        private void AddNumberToDisplay(string number)
        {
            if (isNewNumber || txtDisplay.Text == "0")
            {
                txtDisplay.Text = number;
                isNewNumber = false;
            }
            else
            {
                txtDisplay.Text += number;
            }
            operationPending = false;
        }

        private void btnOne_Click(object sender, EventArgs e) => AddNumberToDisplay("1");
        private void btnTwo_Click(object sender, EventArgs e) => AddNumberToDisplay("2");
        private void btnThree_Click(object sender, EventArgs e) => AddNumberToDisplay("3");
        private void btnFour_Click(object sender, EventArgs e) => AddNumberToDisplay("4");
        private void btnFive_Click(object sender, EventArgs e) => AddNumberToDisplay("5");
        private void btnSix_Click(object sender, EventArgs e) => AddNumberToDisplay("6");
        private void btnSeven_Click(object sender, EventArgs e) => AddNumberToDisplay("7");
        private void btnEight_Click(object sender, EventArgs e) => AddNumberToDisplay("8");
        private void btnNine_Click(object sender, EventArgs e) => AddNumberToDisplay("9");
        private void btnZero_Click(object sender, EventArgs e) => AddNumberToDisplay("0");
        private void btnPoint_Click(object sender, EventArgs e) => AddNumberToDisplay(".");

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            firstNumber = 0;
            currentOperation = "";
            isNewNumber = true;
            operationPending = false;
        }

        private void btnAdd_Click(object sender, EventArgs e) => SetOperation("+");
        private void btnMinus_Click(object sender, EventArgs e) => SetOperation("-");
        private void btnMultiplied_Click(object sender, EventArgs e) => SetOperation("×");
        private void btnDivided_Click(object sender, EventArgs e) => SetOperation("÷");

        private void SetOperation(string operation)
        {
            if (double.TryParse(txtDisplay.Text, out firstNumber))
            {
                currentOperation = operation;
                isNewNumber = true;
                operationPending = true;
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (!operationPending && !string.IsNullOrEmpty(currentOperation))
            {
                try
                {
                    double secondNumber = double.Parse(txtDisplay.Text);
                    double result = 0;

                    switch (currentOperation)
                    {
                        case "+":
                            result = firstNumber + secondNumber;
                            break;
                        case "-":
                            result = firstNumber - secondNumber;
                            break;
                        case "×":
                            result = firstNumber * secondNumber;
                            break;
                        case "÷":
                            if (secondNumber == 0)
                            {
                                MessageBox.Show("∞!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                btnClear_Click(sender, e);
                                return;
                            }
                            result = firstNumber / secondNumber;
                            break;
                    }

                    txtDisplay.Text = result.ToString();
                    firstNumber = result;
                    isNewNumber = true;
                    operationPending = false;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Entrada numérica inválida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnClear_Click(sender, e);
                }
            }
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Count(c => c == '.') > 1)
            {
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1);
                txtDisplay.SelectionStart = txtDisplay.Text.Length;
            }
        }
    }
}
