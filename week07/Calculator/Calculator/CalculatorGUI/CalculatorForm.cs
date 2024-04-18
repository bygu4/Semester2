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
            this.calculator = new Calculator();
            this.InitializeComponent();
            this.Bind();
            this.Select();
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

        private void Calculate_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Calculate();
        }

        private void SetOperation_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.SetBinaryOperation((Operations.Binary)e.Value);
        }

        private void Clear_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Clear();
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

        private void Decimal_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_Decimal();
        }

        private void ToNegative_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_ToNegative();
        }

        private void InPercents_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_InPercents();
        }

        private void Square_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_Square();
        }

        private void SquareRoot_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_SquareRoot();
        }

        private void Inverse_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Operand_Inverse();
        }

        private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        {
            CalculatorKeys.ProcessKeyDown(this.calculator, e);
        }
    }
}
