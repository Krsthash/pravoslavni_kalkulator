using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace православни_калкулатор
{
    public partial class Form1 : Form
    {
        string firstNumber = "";
        string secondNumber = "";
        float operation = 0; // -1 - Result, 0 - No Operation, 1 - Adition, 2 - Subtraction, 3 - Multiplication, 4 - Division
        string[] romanNumerals = { "I", "V", "X", "L", "C", "D", "M", "0" };
        string[] arabicNumbers = { "1", "5", "10", "50", "100", "500", "1000", "0" };
        public Form1()
        {
            InitializeComponent();
        }

        public string RomanToArabic(string text)
        {
            // Example values:
            // CLVII - 157
            // XLVIII - 48
            // MMCMXCIX - 2999
            // CLVII,XLVIII - 157,48
            // XLVIII,MMCMXCIX - 48,2999
            // MMCMXCIX,CLVII - 2999,157

            bool negative = false;
            if (text[0].ToString() == "-")
            {
                negative = true;
                text = text.Substring(1);
            }

            int result = 0;
            int decimalResult = 0;
            bool decimalPoint = false;
            for (int i = 0; i <= text.Length - 1; i++)
            {
                if (text[i].ToString() == ",")
                {
                    decimalPoint = true;
                    continue;
                }
                int index_roman = Array.FindIndex(romanNumerals, x => x.Contains(text[i]));
                if (i + 1 < text.Length)
                {
                    if (text[i + 1].ToString() == ",")
                    {
                        result += int.Parse(arabicNumbers[index_roman]);
                        continue;
                    }
                }
                if (index_roman >= 0)
                {
                    string arabNum = arabicNumbers[index_roman];
                    if (text.Length - 1 > i)
                    {
                        if (int.Parse(arabNum) >= int.Parse(arabicNumbers[Array.FindIndex(romanNumerals, x => x.Contains(text[i + 1]))]))
                        {
                            if (decimalPoint == false)
                            {
                                result += int.Parse(arabNum);
                            }
                            else
                            {
                                decimalResult += int.Parse(arabNum);
                            }

                        } else
                        {
                            if (decimalPoint == false)
                            {
                                result += int.Parse(arabicNumbers[Array.FindIndex(romanNumerals, x => x.Contains(text[i + 1]))]) - int.Parse(arabNum);
                                i++;
                            }
                            else
                            {
                                decimalResult += int.Parse(arabicNumbers[Array.FindIndex(romanNumerals, x => x.Contains(text[i + 1]))]) - int.Parse(arabNum);
                                i++;
                            }
                        }
                    } else
                    {
                        if (decimalPoint == false)
                        {
                            result += int.Parse(arabNum);
                        }
                        else
                        {
                            decimalResult += int.Parse(arabNum);
                        }
                    }

                }

            }
            if (decimalPoint == false)
            {
                if (negative == true)
                {
                    return "-" + result.ToString();
                }
                else
                {
                    return result.ToString();
                }

            }
            if (negative == true)
            {
                return "-" + result.ToString() + "," + decimalResult.ToString();
            }
            return result.ToString() + "," + decimalResult.ToString();
        }

        public string ArabicToRoman(string textO)
        {
            // Example values:
            // CLVII - 157
            // XLVIII - 48
            // MMCMXCIX - 2999
            // CLVII,XLVIII - 157,48
            // XLVIII,MMCMXCIX - 48,2999
            // MMCMXCIX,CLVII - 2999,157

            bool negative = false;
            if (textO[0].ToString() == "-")
            {
                negative = true;
                textO = textO.Substring(1);
            }
            string result = "";
            string decimalResult = "";
            string[] textArray = textO.Split(",");
            string text = textArray[0];
            
            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (int.Parse(text) / int.Parse((Math.Pow(10, i)).ToString()) <= 3)
                {
                    for (int a = 1; a <= int.Parse(text) / int.Parse((Math.Pow(10, i)).ToString()); a++)
                    {
                        result += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    text = (int.Parse(text) - ((int.Parse(text) / int.Parse((Math.Pow(10, i)).ToString())) * int.Parse((Math.Pow(10, i)).ToString()))).ToString(); // int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))]))).ToString();
                }
                else
                {
                    int curr_pos = (text.Length - 1) - i;
                    int desiredNumber = int.Parse(text[curr_pos].ToString()) * int.Parse((Math.Pow(10, i)).ToString());
                    if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber + 1 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        result += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber)
                    {
                        result += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 1 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        result += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 2 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        result += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 3 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        result += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 4 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        result += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 2];
                    }
                    text = (int.Parse(text) - desiredNumber).ToString();
                }

            }
            if(textArray.Length == 1)
            {
                if (negative == true)
                {
                    return "-" + result.ToString();
                }
                return result.ToString();
            }
            text = textArray[1];
            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (int.Parse(text) / int.Parse((Math.Pow(10, i)).ToString()) <= 3)
                {
                    for (int a = 1; a <= int.Parse(text) / int.Parse((Math.Pow(10, i)).ToString()); a++)
                    {
                        decimalResult += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    text = (int.Parse(text) - ((int.Parse(text) / int.Parse((Math.Pow(10, i)).ToString())) * int.Parse((Math.Pow(10, i)).ToString()))).ToString(); // int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))]))).ToString();
                }
                else
                {
                    int curr_pos = (text.Length - 1) - i;
                    int desiredNumber = int.Parse(text[curr_pos].ToString()) * int.Parse((Math.Pow(10, i)).ToString());
                    if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber + 1 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        decimalResult += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber)
                    {
                        decimalResult += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 1 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        decimalResult += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 2 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        decimalResult += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 3 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        decimalResult += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))];
                    }
                    else if (int.Parse(arabicNumbers[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 1]) == desiredNumber - 4 * int.Parse((Math.Pow(10, i)).ToString()))
                    {
                        decimalResult += romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString()))] + romanNumerals[Array.FindIndex(arabicNumbers, x => x.Contains((Math.Pow(10, i)).ToString())) + 2];
                    }
                    text = (int.Parse(text) - desiredNumber).ToString();
                }
            }
            if (negative == true)
            {
                return "-" + result.ToString() + "," + decimalResult.ToString();
            }
            return result.ToString() + "," + decimalResult.ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonI_Click(object sender, EventArgs e)
        {   
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                firstNumber += "I";
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber += "I";
                ResultLabel.Text = secondNumber;
            }
        }

        private void buttonV_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                firstNumber += "V";
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber += "V";
                ResultLabel.Text = secondNumber;
            }
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                firstNumber += "X";
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber += "X";
                ResultLabel.Text = secondNumber;
            }
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                firstNumber += "L";
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber += "L";
                ResultLabel.Text = secondNumber;
            }
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                firstNumber += "C";
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber += "C";
                ResultLabel.Text = secondNumber;
            }
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                firstNumber += "D";
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber += "D";
                ResultLabel.Text = secondNumber;
            }
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                firstNumber += "M";
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber += "M";
                ResultLabel.Text = secondNumber;
            }
        }

        private void equalsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (firstNumber == "" || secondNumber == "")
                {
                    operation = 0;
                    firstNumber = "";
                    secondNumber = "";
                    HistoryLabel.Text = "0";
                    ResultLabel.Text = "0";
                    ResultLabel.Text = "Грешка";
                }
                switch (operation)
                {
                    case 1:
                        HistoryLabel.Text = firstNumber + " + " + secondNumber + " = ";
                        ResultLabel.Text = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) + float.Parse(RomanToArabic(secondNumber))).ToString());
                        operation = -1;
                        firstNumber = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) + float.Parse(RomanToArabic(secondNumber))).ToString());
                        secondNumber = "";
                        break;
                    case 2:
                        HistoryLabel.Text = firstNumber + " - " + secondNumber + " = ";
                        ResultLabel.Text = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) - float.Parse(RomanToArabic(secondNumber))).ToString());
                        operation = -1;
                        firstNumber = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) - float.Parse(RomanToArabic(secondNumber))).ToString());
                        secondNumber = "";
                        break;
                    case 3:
                        HistoryLabel.Text = firstNumber + " * " + secondNumber + " = ";
                        ResultLabel.Text = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) * float.Parse(RomanToArabic(secondNumber))).ToString());
                        operation = -1;
                        firstNumber = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) * float.Parse(RomanToArabic(secondNumber))).ToString());
                        secondNumber = "";
                        break;
                    case 4:
                        HistoryLabel.Text = firstNumber + " / " + secondNumber + " = ";
                        ResultLabel.Text = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) / float.Parse(RomanToArabic(secondNumber))).ToString());
                        operation = -1;
                        firstNumber = ArabicToRoman((float.Parse(RomanToArabic(firstNumber)) / float.Parse(RomanToArabic(secondNumber))).ToString());
                        secondNumber = "";
                        break;
                }
            }
            catch (Exception)
            {
                ResultLabel.Text = "Грешка";
            }
            
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            operation = 1;
            HistoryLabel.Text = firstNumber + " +";
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            operation = 2;
            HistoryLabel.Text = firstNumber + " -";
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            operation = 3;
            HistoryLabel.Text = firstNumber + " *";
        }

        private void divideButton_Click(object sender, EventArgs e)
        {
            operation = 4;
            HistoryLabel.Text = firstNumber + " /";
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            else if (operation == 0)
            {
                if (firstNumber.Length <= 1)
                {
                    firstNumber = "";
                    ResultLabel.Text = "0";
                }
                else
                {
                    firstNumber = firstNumber[0..^1];
                    ResultLabel.Text = firstNumber;
                }
            }
            else
            {
                if (secondNumber.Length <= 1)
                {
                    secondNumber = "";
                    ResultLabel.Text = "0";
                }
                else
                {
                    secondNumber = secondNumber[0..^1];
                    ResultLabel.Text = secondNumber;
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            firstNumber = "";
            secondNumber = "";
            HistoryLabel.Text = "0";
            ResultLabel.Text = "0";
            operation = 0;
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0)
            {
                if(firstNumber == "")
                {
                    firstNumber += "0,";
                    ResultLabel.Text = firstNumber;
                } else
                {
                    firstNumber += ",";
                    ResultLabel.Text = firstNumber;
                }
                
            }
            else
            {
                secondNumber += ",";
                ResultLabel.Text = secondNumber;
            }
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            if (operation == -1)
            {
                firstNumber = "";
                secondNumber = "";
                HistoryLabel.Text = "0";
                ResultLabel.Text = "0";
                operation = 0;
            }
            if (operation == 0 && firstNumber != "0")
            {
                firstNumber += "0";
                ResultLabel.Text = firstNumber;
            }
            else if (secondNumber != "0")
            {
                secondNumber += "0";
                ResultLabel.Text = secondNumber;
            }
        }

        private void sqrtButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (operation == 0)
                {
                    ResultLabel.Text = ArabicToRoman((MathF.Sqrt(float.Parse(RomanToArabic(firstNumber)))).ToString());
                    firstNumber = ArabicToRoman((MathF.Sqrt(float.Parse(RomanToArabic(firstNumber)))).ToString());
                }
                else if (operation == -1)
                {
                    ResultLabel.Text = ArabicToRoman((MathF.Sqrt(float.Parse(RomanToArabic(firstNumber)))).ToString());
                    firstNumber = ArabicToRoman((MathF.Sqrt(float.Parse(RomanToArabic(firstNumber)))).ToString());
                }
                else
                {
                    secondNumber = ArabicToRoman((MathF.Sqrt(float.Parse(RomanToArabic(secondNumber)))).ToString());
                    ResultLabel.Text = secondNumber;
                }
            } catch (Exception)
            {
                ResultLabel.Text = "Грешка";
            }
            
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (operation == 0)
            {
                ResultLabel.Text = ArabicToRoman((MathF.Pow(float.Parse(RomanToArabic(firstNumber)), 2)).ToString());
                firstNumber = ArabicToRoman((MathF.Pow(float.Parse(RomanToArabic(firstNumber)), 2)).ToString());
            }
            else if (operation == -1)
            {
                ResultLabel.Text = ArabicToRoman((MathF.Pow(float.Parse(RomanToArabic(firstNumber)), 2)).ToString());
                firstNumber = ArabicToRoman((MathF.Pow(float.Parse(RomanToArabic(firstNumber)), 2)).ToString());
            }
            else
            {
                secondNumber = ArabicToRoman((MathF.Pow(float.Parse(RomanToArabic(secondNumber)), 2)).ToString());
                ResultLabel.Text = secondNumber;
            }
        }

        private void negateButton_Click(object sender, EventArgs e)
        {
            if (operation == 0)
            {
                firstNumber = ArabicToRoman((float.Parse(RomanToArabic(ResultLabel.Text)) * -1).ToString());
                ResultLabel.Text = firstNumber;
            }
            else if (operation == -1)
            {
                firstNumber = ArabicToRoman((float.Parse(RomanToArabic(ResultLabel.Text)) * -1).ToString());
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber = ArabicToRoman((float.Parse(RomanToArabic(ResultLabel.Text)) * -1).ToString());
                ResultLabel.Text = secondNumber;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (operation == 0)
            {
                firstNumber = ArabicToRoman(MathF.Sqrt((float.Parse(RomanToArabic(ResultLabel.Text)) * float.Parse(RomanToArabic(ResultLabel.Text)))).ToString());
                ResultLabel.Text = firstNumber;
            }
            else if (operation == -1)
            {
                firstNumber = ArabicToRoman(MathF.Sqrt((float.Parse(RomanToArabic(ResultLabel.Text)) * float.Parse(RomanToArabic(ResultLabel.Text)))).ToString());
                ResultLabel.Text = firstNumber;
            }
            else
            {
                secondNumber = ArabicToRoman(MathF.Sqrt((float.Parse(RomanToArabic(ResultLabel.Text)) * float.Parse(RomanToArabic(ResultLabel.Text)))).ToString());
                ResultLabel.Text = secondNumber;
            }
        }
    }
}
