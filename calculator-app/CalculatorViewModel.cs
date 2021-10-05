using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace calculator_app
{
    class CalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        const string operators = "-+/*()";
        const string operatorsBeforeMinus = "(-*/+";
        private Func<double, double, double>[] _operations = {
            (a1, a2) => a1 - a2,
            (a1, a2) => a1 + a2,
            (a1, a2) => a1 / a2,
            (a1, a2) => a1 * a2
        };

        public DelegateCommand ClickCommand { get; set; }
        private string textBoxText;
        public string TextBoxText
        {
            get { return textBoxText; }
            set { textBoxText = value; OnPropertyChanged(); }
        }

        private string result;
        public string Result
        {
            get { return result; }
            set { result = value; OnPropertyChanged(); }
        }

        public CalculatorViewModel()
        {
            ClickCommand = new DelegateCommand(Eval);
        }

        private void Eval()
        {
            // User will always enter a valid mathematical expression. No input checking e.g. spaces/text is done

            if (string.IsNullOrEmpty(TextBoxText))
            {
                return;
            }

            List<string> argList = SeparateEachArgument(TextBoxText);
            argList = DetectMinus(argList);

            // eval all brackets
            while (argList.IndexOf("(") >= 0)
            {
                int lastOpenBracks = argList.LastIndexOf("(");
                int closeOpenBracks = argList.IndexOf(")", lastOpenBracks);
                List<string> subArgList = new List<string>();
                for (int index = lastOpenBracks + 1; index < closeOpenBracks; index++)
                {
                    subArgList.Add(argList[index]);
                }
                argList.RemoveRange(lastOpenBracks, closeOpenBracks - lastOpenBracks + 1);
                argList.Insert(lastOpenBracks, EvaluateSubstring(subArgList));
            }
            string semiFinalResult = EvaluateSubstring(argList);
            string finalResult = double.Parse(semiFinalResult).ToString("N2");
            Result = finalResult;

        }

        private string EvaluateSubstring(List<string> subArgList)
        {
            //"-(22+(-03*4))/2-7/(2+5)
            //eval all */
            while ((subArgList.IndexOf("*") > 0 || subArgList.IndexOf("/") > 0) && subArgList.Count > 3)
            {
                int mulIndex = subArgList.IndexOf("*") > 0 ? subArgList.IndexOf("*") : int.MaxValue;
                int divIndex = subArgList.IndexOf("/") > 0 ? subArgList.IndexOf("/") : int.MaxValue;
                int index = Math.Min(divIndex, mulIndex);
                List<string> subSubArgList = new List<string>();
                for (int i = -1; i < 2; i++)
                {
                    subSubArgList.Add(subArgList[index + i]);
                }
                subArgList.RemoveRange(index - 1, 3);
                subArgList.Insert(index - 1, EvaluateSubstring(subSubArgList));
            }

            int subIndex = 1;
            double result = double.Parse(subArgList[0]);
            while (subIndex < subArgList.Count)
            {
                string arg = subArgList[subIndex];
                double abc = double.Parse(subArgList[subIndex + 1]);

                result = _operations[operators.IndexOf(arg)](result, abc);

                subIndex += 2;
            }
            return result.ToString();
        }

        // detect all number/operator 
        private List<string> SeparateEachArgument(string input)
        {
            List<string> argList = new List<string>();
            string tempHolder = string.Empty;
            foreach (char c in input)
            {
                if (operators.Contains(c))
                {
                    if (tempHolder.Length > 0)
                    {
                        argList.Add(tempHolder);
                        tempHolder = string.Empty;
                    }
                    argList.Add(c.ToString());
                }
                else
                {
                    tempHolder += c;
                }
            }
            if (tempHolder.Length > 0)
            {
                argList.Add(tempHolder);
            }
            return argList;
        }
        private List<string> DetectMinus(List<string> argList)
        {
            List<string> newArgList = new List<string>();
            for (int i = 0; i < argList.Count; i++)
            {
                if (argList[i] == "-")
                {
                    if ((i > 0 && operatorsBeforeMinus.Contains(argList[i - 1])) || i == 0)
                    {
                        newArgList.Add("-1");
                        newArgList.Add("*");
                        continue;
                    }
                }
                else if (i > 0 && argList[i] == "(" && !operators.Contains(argList[i - 1]))
                {
                    newArgList.Add("*");
                    newArgList.Add("(");
                    continue;
                }
                else if (i < argList.Count - 1 && argList[i] == ")" && !operators.Contains(argList[i + 1]))
                {
                    newArgList.Add(")");
                    newArgList.Add("*");
                    continue;
                }
                newArgList.Add(argList[i]);
            }
            return newArgList;
        }
    }
}
