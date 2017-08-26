using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        CalculatorEngine ku = new CalculatorEngine();
        private bool hasDot;
        private bool have2;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand, secondOperand;
        private string operate;

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;

        }

        public MainForm()
        {
            InitializeComponent();

            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                lblDisplay.Text = "Error";
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }
        private void btnMemory_function(object sender, EventArgs e)
        {
            int Mtemp;
            string command = ((Button)sender).Text;
            switch(command)
            {
                case "M+":
                    comboBox1.Items[comboBox1.SelectedIndex] = ((Convert.ToDouble(comboBox1.SelectedItem)) + Convert.ToDouble(lblDisplay.Text));
                    break;
                case "M-":
                    comboBox1.Items[comboBox1.SelectedIndex] = ((Convert.ToDouble(comboBox1.SelectedItem)) - Convert.ToDouble(lblDisplay.Text));
                    break;
                case "MS":
                    comboBox1.Items.Add(lblDisplay.Text);
                    comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                    break;
                case "MR":
                    if (comboBox1.Items.Count > 0)
                        lblDisplay.Text = comboBox1.SelectedItem.ToString();
                    break;
                case "MC":
                    comboBox1.Items.Clear();
                    break;
                case "MD":
                    if(comboBox1.Items.Count > 0)
                    {
                        Mtemp = comboBox1.SelectedIndex;
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                        Mtemp--;
                        if (Mtemp < 0)
                        {
                            Mtemp = 0;
                        }
                        if (comboBox1.Items.Count > 0)
                            comboBox1.SelectedIndex = Mtemp;
                    }
                    break;
            }
            if(comboBox1.Items.Count>0)
            {
                btnMplus.Enabled = true;
                btnMminus.Enabled = true;
                btnMclear.Enabled = true;
                btnMdelete.Enabled = true;
                btnMrestore.Enabled = true;
                comboBox1.Enabled = true;
            }else
            {
                btnMplus.Enabled = false;
                btnMminus.Enabled = false;
                btnMclear.Enabled = false;
                btnMdelete.Enabled = false;
                btnMrestore.Enabled = false;
                comboBox1.Enabled = false;
            }
        }
        private void btnMore_function(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string click = ((Button)sender).Text;

            switch(click)
            {
                case "%":
                    secondOperand = (Convert.ToDouble(firstOperand) * ((Convert.ToDouble(lblDisplay.Text) / 100))).ToString();
                    lblDisplay.Text = secondOperand;
                    if (lblDisplay.Text.Length > 8)
                    {
                        lblDisplay.Text = "Error";
                    }
                    break;
                case "√":
                    lblDisplay.Text = Math.Sqrt(Convert.ToDouble(lblDisplay.Text)).ToString();
                    if (lblDisplay.Text.Length > 8)
                    {
                        lblDisplay.Text = "Error";
                    }
                    break;
                case "1/X":
                    lblDisplay.Text = string.Format("{0:0.############}", (1 / Convert.ToDouble(lblDisplay.Text)));
                    if (lblDisplay.Text.Length > 8)
                    {
                        lblDisplay.Text = "Error";
                    }
                    break;

            }
        }
        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (have2)
            {
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                secondOperand = lblDisplay.Text;
                string result = ku.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
                have2 = false;
            }
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            operate = ((Button)sender).Text;
            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    have2 = true;
                    break;
            }
            isAllowBack = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = ku.calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            have2 = false;
            isAfterEqual = true;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }
    }
}
