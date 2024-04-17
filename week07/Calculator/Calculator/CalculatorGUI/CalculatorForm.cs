// <copyright file="CalculatorForm.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CalculatorGUI
{
    using Calculator;
    using Operations;

    /// <summary>
    /// The main form of the application.
    /// </summary>
    public partial class CalculatorForm : Form
    {
        private Calculator calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorForm"/> class.
        /// </summary>
        public CalculatorForm()
        {
            this.InitializeComponent();
            this.calculator = new Calculator();
            this.Bind();
        }

        private void Bind()
        {
            this.Expression_Box.DataBindings.Add(
                "Text",
                this.calculator,
                nameof(Calculator.Expression),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.Result_Box.DataBindings.Add(
                "Text",
                this.calculator,
                nameof(Calculator.Result),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Clear_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Clear();
        }

        private void Calculate_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Calculate();
        }

        private void SetOperation_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.SetOperationBySign((char)e.Value);
        }

        private void ClearOperand_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_Clear();
        }

        private void AddDigit_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_AddDigit((char)e.Value);
        }

        private void DeleteLastDigit_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_DeleteLastDigit();
        }

        private void ToNegative_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_ToNegative();
        }

        private void ToFloat_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_Decimal();
        }

        private void InPercents_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_InPercents();
        }

        private void ToSquare_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_Square();
        }

        private void ToSquareRoot_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_SquareRoot();
        }

        private void ToInverse_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_Inverse();
        }

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            char keyChar = CalculatorKeys.GetChar(e);
            if (char.IsDigit(keyChar))
            {
                this.calculator.Operand_AddDigit(keyChar);
            }
            else if (Enum.IsDefined(typeof(Operations.Signs), (int)keyChar))
            {
                this.calculator.SetOperationBySign(keyChar);
            }

            switch (keyChar)
            {
                case CalculatorKeys.Enter:
                    this.calculator.Calculate();
                    break;
                case CalculatorKeys.Clear:
                    this.calculator.Clear();
                    break;
                case CalculatorKeys.Delete:
                    this.calculator.Operand_Clear();
                    break;
                case CalculatorKeys.Back:
                    this.calculator.Operand_DeleteLastDigit();
                    break;
                case CalculatorKeys.Comma:
                    this.calculator.Operand_Decimal();
                    break;
                case CalculatorKeys.Percent:
                    this.calculator.Operand_InPercents();
                    break;
            }
        }
    }
}
