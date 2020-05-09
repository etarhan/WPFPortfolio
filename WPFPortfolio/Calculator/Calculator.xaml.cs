using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace WPFPortfolio.Calculator
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl
    {
        CalculateImput calc = new CalculateImput();
        bool addition = false;
        bool substraction = false;
        bool multiplication = false;
        bool division = false;
        bool calculation = false;
        int deciSeperator = 0;

        CultureInfo provider;

        public Calculator()
        {
            InitializeComponent();
            currentEntry.Text = "";
            savedEntry.Text = "";
            provider = new CultureInfo("en-US");
            provider.NumberFormat.NumberDecimalSeparator = ",";
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            NumberClick(0);
        }

        private void buttonDot_Click(object sender, RoutedEventArgs e)
        {
            deciSeperator++;
            if (deciSeperator == 1)
            {
                if (calculation == true)
                {
                    currentEntry.Text = String.Empty;
                    currentEntry.Text += ",";
                    calculation = false;
                }
                else
                    currentEntry.Text += ",";
            }

        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            EqualsResult();
            deciSeperator = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NumberClick(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NumberClick(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NumberClick(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NumberClick(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NumberClick(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NumberClick(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NumberClick(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NumberClick(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            NumberClick(9);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            currentOperation.Text = "+";
            if (multiplication == true || division == true || substraction == true)
            {
                addition = true;
                multiplication = false;
                division = false;
                substraction = false;
                EqualsResultContinue();
            }
            if (!String.IsNullOrEmpty(currentEntry.Text) && addition == false)
            {
                savedEntry.Text = currentEntry.Text;
                currentEntry.Text = String.Empty;
                addition = true;
            }

            else if (!String.IsNullOrEmpty(currentEntry.Text) && addition == true)
                EqualsResultContinue();

            deciSeperator = 0;
        }
        private void buttonSubstract_Click(object sender, EventArgs e)
        {
            currentOperation.Text = "-";
            if (multiplication == true || division == true || addition == true)
            {
                substraction = true;
                multiplication = false;
                division = false;
                addition = false;
                EqualsResultContinue();
            }
            if (!String.IsNullOrEmpty(currentEntry.Text) && substraction == false)
            {
                savedEntry.Text = currentEntry.Text;
                currentEntry.Text = String.Empty;
                substraction = true;
            }
            else if (!String.IsNullOrEmpty(currentEntry.Text) && substraction == true)
                EqualsResultContinue();

            deciSeperator = 0;
        }
        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            currentOperation.Text = "*";
            if (substraction == true || division == true || addition == true)
            {
                multiplication = true;
                substraction = false;
                division = false;
                addition = false;
                EqualsResultContinue();
            }
            if (!String.IsNullOrEmpty(currentEntry.Text) && multiplication == false)
            {
                savedEntry.Text = currentEntry.Text;
                currentEntry.Text = String.Empty;
                multiplication = true;
            }
            else if (!String.IsNullOrEmpty(currentEntry.Text) && multiplication == true)
                EqualsResultContinue();

            deciSeperator = 0;
        }
        private void buttonDividedBy_Click(object sender, EventArgs e)
        {
            currentOperation.Text = "/";
            if (substraction == true || multiplication == true || addition == true)
            {
                division = true;
                substraction = false;
                multiplication = false;
                addition = false;
                EqualsResultContinue();
            }
            if (!String.IsNullOrEmpty(currentEntry.Text) && division == false)
            {
                savedEntry.Text = currentEntry.Text;
                currentEntry.Text = String.Empty;
                division = true;
            }
            else if (!String.IsNullOrEmpty(currentEntry.Text) && division == true)
                EqualsResultContinue();

            deciSeperator = 0;
        }

        private void buttonBackSpace_Click(object sender, EventArgs e)
        {
            if (currentEntry.Text.Length > 1)
                currentEntry.Text = currentEntry.Text.Remove(currentEntry.Text.Length - 1, 1);
            else
                currentEntry.Text = String.Empty;
        }

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            currentEntry.Text = String.Empty;
            deciSeperator = 0;

        }

        //Helper Methods
        #region HelperMethods
        private void NumberClick(double number)
        {
            if (calculation == true)
            {
                currentEntry.Text = String.Empty;
                currentEntry.Text += number;
                calculation = false;
            }
            else
                currentEntry.Text += number;
        }
        private void EqualsResultContinue()
        {

            if (!String.IsNullOrEmpty(currentEntry.Text))
            {
                if (addition == true)
                {
                    savedEntry.Text = calc.AddCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                    currentEntry.Text = String.Empty;
                    addition = true;
                    calculation = true;
                }
                else if (substraction == true)
                {
                    savedEntry.Text = calc.SubstCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                    currentEntry.Text = String.Empty;
                    substraction = true;
                    calculation = true;
                    
                }
                else if (multiplication == true)
                {
                    savedEntry.Text = calc.MultiCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                    currentEntry.Text = String.Empty;
                    multiplication = true;
                    calculation = true;
                    
                }
                else if (division == true)
                {
                    if (calc.DivByZero != true)
                    {
                        savedEntry.Text = calc.DivCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                        currentEntry.Text = String.Empty;
                        division = true;
                        calculation = true;
                        
                    }
                    else
                    {
                        savedEntry.Text = "Divided by Zero Error!";
                        division = true;
                        calculation = true;
                        currentOperation.Text = "";
                    }
                }
            }
        }
        private void EqualsResult()
        {
            if (!String.IsNullOrEmpty(currentEntry.Text))
            {
                if (addition == true)
                {
                    currentEntry.Text = calc.AddCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                    savedEntry.Text = String.Empty;
                    currentOperation.Text = String.Empty;
                    addition = false;
                    calculation = true;
                }
                else if (substraction == true)
                {
                    currentEntry.Text = calc.SubstCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                    savedEntry.Text = String.Empty;
                    substraction = false;
                    calculation = true;
                    currentOperation.Text = String.Empty;
                }
                else if (multiplication == true)
                {
                    currentEntry.Text = calc.MultiCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                    savedEntry.Text = String.Empty;
                    multiplication = false;
                    calculation = true;
                    currentOperation.Text = String.Empty;
                }
                else if (division == true)
                {
                    currentEntry.Text = calc.DivCalc(Convert.ToDouble(currentEntry.Text, provider), Convert.ToDouble(savedEntry.Text, provider)).ToString();
                        if (calc.DivByZero != true)
                        {
                            savedEntry.Text = String.Empty;
                            division = false;
                            calculation = true;
                            currentOperation.Text = String.Empty;
                        }
                        else
                        {
                            currentEntry.Text = "Divided by Zero Error!";
                            savedEntry.Text = String.Empty;
                            division = false;
                            calculation = true;
                            currentOperation.Text = String.Empty;
                        }
                }
            }
        }
        #endregion

        private void buttonClear_Click(object sender, EventArgs e)
        {
            currentEntry.Text = String.Empty;
            savedEntry.Text = String.Empty;
            currentOperation.Text = String.Empty;
            addition = false;
            substraction = false;
            multiplication = false;
            division = false;
            calculation = false;
            deciSeperator = 0;
        }

    }
}
