// <copyright file="CalculatorForm.Designer.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CalculatorGUI
{
    partial class CalculatorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            InPercents_Button = new CustomButton();
            ClearOperand_Button = new CustomButton();
            Square_Button = new CustomButton();
            SquareRoot_Button = new CustomButton();
            Inverse_Button = new CustomButton();
            Division_Button = new CustomButton();
            Two_Button = new CustomButton();
            Three_Button = new CustomButton();
            Multiplication_Button = new CustomButton();
            Four_Button = new CustomButton();
            Five_Button = new CustomButton();
            Six_Button = new CustomButton();
            Substraction_Button = new CustomButton();
            Seven_Button = new CustomButton();
            Eight_Button = new CustomButton();
            Nine_Button = new CustomButton();
            Addition_Button = new CustomButton();
            ToNegative_Button = new CustomButton();
            Zero_Button = new CustomButton();
            Decimal_Button = new CustomButton();
            Calculate_Button = new CustomButton();
            One_Button = new CustomButton();
            DeleteLastDigit_Button = new CustomButton();
            Clear_Button = new CustomButton();
            tableLayoutPanel2 = new TableLayoutPanel();
            Result_Box = new RichTextBox();
            Expression_Box = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(InPercents_Button, 0, 0);
            tableLayoutPanel1.Controls.Add(ClearOperand_Button, 2, 0);
            tableLayoutPanel1.Controls.Add(Square_Button, 0, 1);
            tableLayoutPanel1.Controls.Add(SquareRoot_Button, 1, 1);
            tableLayoutPanel1.Controls.Add(Inverse_Button, 2, 1);
            tableLayoutPanel1.Controls.Add(Division_Button, 3, 1);
            tableLayoutPanel1.Controls.Add(Two_Button, 1, 2);
            tableLayoutPanel1.Controls.Add(Three_Button, 2, 2);
            tableLayoutPanel1.Controls.Add(Multiplication_Button, 3, 2);
            tableLayoutPanel1.Controls.Add(Four_Button, 0, 3);
            tableLayoutPanel1.Controls.Add(Five_Button, 1, 3);
            tableLayoutPanel1.Controls.Add(Six_Button, 2, 3);
            tableLayoutPanel1.Controls.Add(Substraction_Button, 3, 3);
            tableLayoutPanel1.Controls.Add(Seven_Button, 0, 4);
            tableLayoutPanel1.Controls.Add(Eight_Button, 1, 4);
            tableLayoutPanel1.Controls.Add(Nine_Button, 2, 4);
            tableLayoutPanel1.Controls.Add(Addition_Button, 3, 4);
            tableLayoutPanel1.Controls.Add(ToNegative_Button, 0, 5);
            tableLayoutPanel1.Controls.Add(Zero_Button, 1, 5);
            tableLayoutPanel1.Controls.Add(Decimal_Button, 2, 5);
            tableLayoutPanel1.Controls.Add(Calculate_Button, 3, 5);
            tableLayoutPanel1.Controls.Add(One_Button, 0, 2);
            tableLayoutPanel1.Controls.Add(DeleteLastDigit_Button, 3, 0);
            tableLayoutPanel1.Controls.Add(Clear_Button, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 146);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666641F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(376, 404);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // InPercents_Button
            // 
            InPercents_Button.AutoValidate = AutoValidate.Disable;
            InPercents_Button.BackColor = Color.WhiteSmoke;
            InPercents_Button.ButtonColor = Color.LightGray;
            InPercents_Button.CausesValidation = false;
            InPercents_Button.Dock = DockStyle.Fill;
            InPercents_Button.Label = "%";
            InPercents_Button.Location = new Point(0, 0);
            InPercents_Button.Margin = new Padding(0);
            InPercents_Button.Name = "InPercents_Button";
            InPercents_Button.Size = new Size(94, 67);
            InPercents_Button.TabIndex = 0;
            InPercents_Button.TabStop = false;
            InPercents_Button.Value = 0;
            InPercents_Button.Click += InPercents_Button_Click;
            // 
            // ClearOperand_Button
            // 
            ClearOperand_Button.AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClearOperand_Button.BackColor = Color.WhiteSmoke;
            ClearOperand_Button.ButtonColor = Color.LightGray;
            ClearOperand_Button.CausesValidation = false;
            ClearOperand_Button.Dock = DockStyle.Fill;
            ClearOperand_Button.Label = "CE";
            ClearOperand_Button.Location = new Point(188, 0);
            ClearOperand_Button.Margin = new Padding(0);
            ClearOperand_Button.Name = "ClearOperand_Button";
            ClearOperand_Button.Size = new Size(94, 67);
            ClearOperand_Button.TabIndex = 2;
            ClearOperand_Button.TabStop = false;
            ClearOperand_Button.Value = 0;
            ClearOperand_Button.Click += ClearOperand_Button_Click;
            // 
            // Square_Button
            // 
            Square_Button.AutoValidate = AutoValidate.Disable;
            Square_Button.BackColor = Color.WhiteSmoke;
            Square_Button.ButtonColor = Color.LightGray;
            Square_Button.CausesValidation = false;
            Square_Button.Dock = DockStyle.Fill;
            Square_Button.ImeMode = ImeMode.NoControl;
            Square_Button.Label = "x^2";
            Square_Button.Location = new Point(0, 67);
            Square_Button.Margin = new Padding(0);
            Square_Button.Name = "Square_Button";
            Square_Button.Size = new Size(94, 67);
            Square_Button.TabIndex = 4;
            Square_Button.TabStop = false;
            Square_Button.Value = 0;
            Square_Button.Click += Square_Button_Click;
            // 
            // SquareRoot_Button
            // 
            SquareRoot_Button.AutoValidate = AutoValidate.Disable;
            SquareRoot_Button.BackColor = Color.WhiteSmoke;
            SquareRoot_Button.ButtonColor = Color.LightGray;
            SquareRoot_Button.CausesValidation = false;
            SquareRoot_Button.Dock = DockStyle.Fill;
            SquareRoot_Button.Label = "√x";
            SquareRoot_Button.Location = new Point(94, 67);
            SquareRoot_Button.Margin = new Padding(0);
            SquareRoot_Button.Name = "SquareRoot_Button";
            SquareRoot_Button.Size = new Size(94, 67);
            SquareRoot_Button.TabIndex = 5;
            SquareRoot_Button.TabStop = false;
            SquareRoot_Button.Value = 0;
            SquareRoot_Button.Click += SquareRoot_Button_Click;
            // 
            // Inverse_Button
            // 
            Inverse_Button.AutoValidate = AutoValidate.Disable;
            Inverse_Button.BackColor = Color.WhiteSmoke;
            Inverse_Button.ButtonColor = Color.LightGray;
            Inverse_Button.CausesValidation = false;
            Inverse_Button.Dock = DockStyle.Fill;
            Inverse_Button.Label = "1/x";
            Inverse_Button.Location = new Point(188, 67);
            Inverse_Button.Margin = new Padding(0);
            Inverse_Button.Name = "Inverse_Button";
            Inverse_Button.Size = new Size(94, 67);
            Inverse_Button.TabIndex = 6;
            Inverse_Button.TabStop = false;
            Inverse_Button.Value = 0;
            Inverse_Button.Click += Inverse_Button_Click;
            // 
            // Division_Button
            // 
            Division_Button.AutoValidate = AutoValidate.Disable;
            Division_Button.BackColor = Color.WhiteSmoke;
            Division_Button.ButtonColor = Color.LightGray;
            Division_Button.CausesValidation = false;
            Division_Button.Dock = DockStyle.Fill;
            Division_Button.Label = "÷";
            Division_Button.Location = new Point(282, 67);
            Division_Button.Margin = new Padding(0);
            Division_Button.Name = "Division_Button";
            Division_Button.Size = new Size(94, 67);
            Division_Button.TabIndex = 7;
            Division_Button.TabStop = false;
            Division_Button.Value = 247;
            Division_Button.Click += SetOperation_Button_Click;
            // 
            // Two_Button
            // 
            Two_Button.AutoValidate = AutoValidate.Disable;
            Two_Button.BackColor = Color.White;
            Two_Button.ButtonColor = Color.White;
            Two_Button.CausesValidation = false;
            Two_Button.Dock = DockStyle.Fill;
            Two_Button.Label = "2";
            Two_Button.Location = new Point(94, 134);
            Two_Button.Margin = new Padding(0);
            Two_Button.Name = "Two_Button";
            Two_Button.Size = new Size(94, 67);
            Two_Button.TabIndex = 9;
            Two_Button.TabStop = false;
            Two_Button.Value = 50;
            Two_Button.Click += AddDigit_Button_Click;
            // 
            // Three_Button
            // 
            Three_Button.AutoValidate = AutoValidate.Disable;
            Three_Button.BackColor = Color.White;
            Three_Button.ButtonColor = Color.White;
            Three_Button.CausesValidation = false;
            Three_Button.Dock = DockStyle.Fill;
            Three_Button.Label = "3";
            Three_Button.Location = new Point(188, 134);
            Three_Button.Margin = new Padding(0);
            Three_Button.Name = "Three_Button";
            Three_Button.Size = new Size(94, 67);
            Three_Button.TabIndex = 10;
            Three_Button.TabStop = false;
            Three_Button.Value = 51;
            Three_Button.Click += AddDigit_Button_Click;
            // 
            // Multiplication_Button
            // 
            Multiplication_Button.AutoValidate = AutoValidate.Disable;
            Multiplication_Button.BackColor = Color.WhiteSmoke;
            Multiplication_Button.ButtonColor = Color.LightGray;
            Multiplication_Button.CausesValidation = false;
            Multiplication_Button.Dock = DockStyle.Fill;
            Multiplication_Button.Label = "×";
            Multiplication_Button.Location = new Point(282, 134);
            Multiplication_Button.Margin = new Padding(0);
            Multiplication_Button.Name = "Multiplication_Button";
            Multiplication_Button.Size = new Size(94, 67);
            Multiplication_Button.TabIndex = 11;
            Multiplication_Button.TabStop = false;
            Multiplication_Button.Value = 215;
            Multiplication_Button.Click += SetOperation_Button_Click;
            // 
            // Four_Button
            // 
            Four_Button.AutoValidate = AutoValidate.Disable;
            Four_Button.BackColor = Color.White;
            Four_Button.ButtonColor = Color.White;
            Four_Button.CausesValidation = false;
            Four_Button.Dock = DockStyle.Fill;
            Four_Button.Label = "4";
            Four_Button.Location = new Point(0, 201);
            Four_Button.Margin = new Padding(0);
            Four_Button.Name = "Four_Button";
            Four_Button.Size = new Size(94, 67);
            Four_Button.TabIndex = 12;
            Four_Button.TabStop = false;
            Four_Button.Value = 52;
            Four_Button.Click += AddDigit_Button_Click;
            // 
            // Five_Button
            // 
            Five_Button.AutoValidate = AutoValidate.Disable;
            Five_Button.BackColor = Color.White;
            Five_Button.ButtonColor = Color.White;
            Five_Button.CausesValidation = false;
            Five_Button.Dock = DockStyle.Fill;
            Five_Button.Label = "5";
            Five_Button.Location = new Point(94, 201);
            Five_Button.Margin = new Padding(0);
            Five_Button.Name = "Five_Button";
            Five_Button.Size = new Size(94, 67);
            Five_Button.TabIndex = 13;
            Five_Button.TabStop = false;
            Five_Button.Value = 53;
            Five_Button.Click += AddDigit_Button_Click;
            // 
            // Six_Button
            // 
            Six_Button.AutoValidate = AutoValidate.Disable;
            Six_Button.BackColor = Color.White;
            Six_Button.ButtonColor = Color.White;
            Six_Button.CausesValidation = false;
            Six_Button.Dock = DockStyle.Fill;
            Six_Button.Label = "6";
            Six_Button.Location = new Point(188, 201);
            Six_Button.Margin = new Padding(0);
            Six_Button.Name = "Six_Button";
            Six_Button.Size = new Size(94, 67);
            Six_Button.TabIndex = 14;
            Six_Button.TabStop = false;
            Six_Button.Value = 54;
            Six_Button.Click += AddDigit_Button_Click;
            // 
            // Substraction_Button
            // 
            Substraction_Button.AutoValidate = AutoValidate.Disable;
            Substraction_Button.BackColor = Color.WhiteSmoke;
            Substraction_Button.ButtonColor = Color.LightGray;
            Substraction_Button.CausesValidation = false;
            Substraction_Button.Dock = DockStyle.Fill;
            Substraction_Button.Label = "-";
            Substraction_Button.Location = new Point(282, 201);
            Substraction_Button.Margin = new Padding(0);
            Substraction_Button.Name = "Substraction_Button";
            Substraction_Button.Size = new Size(94, 67);
            Substraction_Button.TabIndex = 15;
            Substraction_Button.TabStop = false;
            Substraction_Button.Value = 45;
            Substraction_Button.Click += SetOperation_Button_Click;
            // 
            // Seven_Button
            // 
            Seven_Button.AutoValidate = AutoValidate.Disable;
            Seven_Button.BackColor = Color.White;
            Seven_Button.ButtonColor = Color.White;
            Seven_Button.CausesValidation = false;
            Seven_Button.Dock = DockStyle.Fill;
            Seven_Button.Label = "7";
            Seven_Button.Location = new Point(0, 268);
            Seven_Button.Margin = new Padding(0);
            Seven_Button.Name = "Seven_Button";
            Seven_Button.Size = new Size(94, 67);
            Seven_Button.TabIndex = 16;
            Seven_Button.TabStop = false;
            Seven_Button.Value = 55;
            Seven_Button.Click += AddDigit_Button_Click;
            // 
            // Eight_Button
            // 
            Eight_Button.AutoValidate = AutoValidate.Disable;
            Eight_Button.BackColor = Color.White;
            Eight_Button.ButtonColor = Color.White;
            Eight_Button.CausesValidation = false;
            Eight_Button.Dock = DockStyle.Fill;
            Eight_Button.Label = "8";
            Eight_Button.Location = new Point(94, 268);
            Eight_Button.Margin = new Padding(0);
            Eight_Button.Name = "Eight_Button";
            Eight_Button.Size = new Size(94, 67);
            Eight_Button.TabIndex = 17;
            Eight_Button.TabStop = false;
            Eight_Button.Value = 56;
            Eight_Button.Click += AddDigit_Button_Click;
            // 
            // Nine_Button
            // 
            Nine_Button.AutoValidate = AutoValidate.Disable;
            Nine_Button.BackColor = Color.White;
            Nine_Button.ButtonColor = Color.White;
            Nine_Button.CausesValidation = false;
            Nine_Button.Dock = DockStyle.Fill;
            Nine_Button.Label = "9";
            Nine_Button.Location = new Point(188, 268);
            Nine_Button.Margin = new Padding(0);
            Nine_Button.Name = "Nine_Button";
            Nine_Button.Size = new Size(94, 67);
            Nine_Button.TabIndex = 18;
            Nine_Button.TabStop = false;
            Nine_Button.Value = 57;
            Nine_Button.Click += AddDigit_Button_Click;
            // 
            // Addition_Button
            // 
            Addition_Button.AutoValidate = AutoValidate.Disable;
            Addition_Button.BackColor = Color.WhiteSmoke;
            Addition_Button.ButtonColor = Color.LightGray;
            Addition_Button.CausesValidation = false;
            Addition_Button.Dock = DockStyle.Fill;
            Addition_Button.Label = "+";
            Addition_Button.Location = new Point(282, 268);
            Addition_Button.Margin = new Padding(0);
            Addition_Button.Name = "Addition_Button";
            Addition_Button.Size = new Size(94, 67);
            Addition_Button.TabIndex = 19;
            Addition_Button.TabStop = false;
            Addition_Button.Value = 43;
            Addition_Button.Click += SetOperation_Button_Click;
            // 
            // ToNegative_Button
            // 
            ToNegative_Button.AutoValidate = AutoValidate.Disable;
            ToNegative_Button.BackColor = Color.WhiteSmoke;
            ToNegative_Button.ButtonColor = Color.White;
            ToNegative_Button.CausesValidation = false;
            ToNegative_Button.Dock = DockStyle.Fill;
            ToNegative_Button.Label = "+/-";
            ToNegative_Button.Location = new Point(0, 335);
            ToNegative_Button.Margin = new Padding(0);
            ToNegative_Button.Name = "ToNegative_Button";
            ToNegative_Button.Size = new Size(94, 69);
            ToNegative_Button.TabIndex = 20;
            ToNegative_Button.TabStop = false;
            ToNegative_Button.Value = 0;
            ToNegative_Button.Click += ToNegative_Button_Click;
            // 
            // Zero_Button
            // 
            Zero_Button.AutoValidate = AutoValidate.Disable;
            Zero_Button.BackColor = Color.White;
            Zero_Button.ButtonColor = Color.White;
            Zero_Button.CausesValidation = false;
            Zero_Button.Dock = DockStyle.Fill;
            Zero_Button.Label = "0";
            Zero_Button.Location = new Point(94, 335);
            Zero_Button.Margin = new Padding(0);
            Zero_Button.Name = "Zero_Button";
            Zero_Button.Size = new Size(94, 69);
            Zero_Button.TabIndex = 21;
            Zero_Button.TabStop = false;
            Zero_Button.Value = 48;
            Zero_Button.Click += AddDigit_Button_Click;
            // 
            // Decimal_Button
            // 
            Decimal_Button.AutoValidate = AutoValidate.Disable;
            Decimal_Button.BackColor = Color.White;
            Decimal_Button.ButtonColor = Color.White;
            Decimal_Button.CausesValidation = false;
            Decimal_Button.Dock = DockStyle.Fill;
            Decimal_Button.Label = this.calculator.DecimalSeparator;
            Decimal_Button.Location = new Point(188, 335);
            Decimal_Button.Margin = new Padding(0);
            Decimal_Button.Name = "Decimal_Button";
            Decimal_Button.Size = new Size(94, 69);
            Decimal_Button.TabIndex = 22;
            Decimal_Button.TabStop = false;
            Decimal_Button.Value = 0;
            Decimal_Button.Click += Decimal_Button_Click;
            // 
            // Calculate_Button
            // 
            Calculate_Button.AutoValidate = AutoValidate.Disable;
            Calculate_Button.BackColor = Color.WhiteSmoke;
            Calculate_Button.ButtonColor = Color.Orange;
            Calculate_Button.CausesValidation = false;
            Calculate_Button.Dock = DockStyle.Fill;
            Calculate_Button.Label = "=";
            Calculate_Button.Location = new Point(282, 335);
            Calculate_Button.Margin = new Padding(0);
            Calculate_Button.Name = "Calculate_Button";
            Calculate_Button.Size = new Size(94, 69);
            Calculate_Button.TabIndex = 23;
            Calculate_Button.TabStop = false;
            Calculate_Button.Value = 0;
            Calculate_Button.Click += Calculate_Button_Click;
            // 
            // One_Button
            // 
            One_Button.AutoValidate = AutoValidate.Disable;
            One_Button.BackColor = Color.White;
            One_Button.ButtonColor = Color.White;
            One_Button.CausesValidation = false;
            One_Button.Dock = DockStyle.Fill;
            One_Button.Label = "1";
            One_Button.Location = new Point(0, 134);
            One_Button.Margin = new Padding(0);
            One_Button.Name = "One_Button";
            One_Button.Size = new Size(94, 67);
            One_Button.TabIndex = 8;
            One_Button.TabStop = false;
            One_Button.Value = 49;
            One_Button.Click += AddDigit_Button_Click;
            // 
            // DeleteLastDigit_Button
            // 
            DeleteLastDigit_Button.AutoValidate = AutoValidate.Disable;
            DeleteLastDigit_Button.BackColor = Color.WhiteSmoke;
            DeleteLastDigit_Button.ButtonColor = Color.LightGray;
            DeleteLastDigit_Button.CausesValidation = false;
            DeleteLastDigit_Button.Dock = DockStyle.Fill;
            DeleteLastDigit_Button.Label = "<-";
            DeleteLastDigit_Button.Location = new Point(282, 0);
            DeleteLastDigit_Button.Margin = new Padding(0);
            DeleteLastDigit_Button.Name = "DeleteLastDigit_Button";
            DeleteLastDigit_Button.Size = new Size(94, 67);
            DeleteLastDigit_Button.TabIndex = 3;
            DeleteLastDigit_Button.TabStop = false;
            DeleteLastDigit_Button.Value = 0;
            DeleteLastDigit_Button.Click += DeleteLastDigit_Button_Click;
            // 
            // Clear_Button
            // 
            Clear_Button.AutoValidate = AutoValidate.Disable;
            Clear_Button.BackColor = Color.WhiteSmoke;
            Clear_Button.ButtonColor = Color.LightGray;
            Clear_Button.CausesValidation = false;
            Clear_Button.Dock = DockStyle.Fill;
            Clear_Button.Label = "C";
            Clear_Button.Location = new Point(94, 0);
            Clear_Button.Margin = new Padding(0);
            Clear_Button.Name = "Clear_Button";
            Clear_Button.Size = new Size(94, 67);
            Clear_Button.TabIndex = 1;
            Clear_Button.TabStop = false;
            Clear_Button.Value = 0;
            Clear_Button.Click += Clear_Button_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(Result_Box, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 2);
            tableLayoutPanel2.Controls.Add(Expression_Box, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 74F));
            tableLayoutPanel2.Size = new Size(382, 553);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // Result_Box
            // 
            Result_Box.BackColor = Color.White;
            Result_Box.BorderStyle = BorderStyle.None;
            Result_Box.CausesValidation = false;
            Result_Box.Dock = DockStyle.Fill;
            Result_Box.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Result_Box.ImeMode = ImeMode.NoControl;
            Result_Box.Location = new Point(1, 56);
            Result_Box.Margin = new Padding(1);
            Result_Box.MaxLength = 16;
            Result_Box.Multiline = false;
            Result_Box.Name = "Result_Box";
            Result_Box.ReadOnly = true;
            Result_Box.RightToLeft = RightToLeft.No;
            Result_Box.Size = new Size(380, 86);
            Result_Box.TabIndex = 3;
            Result_Box.Text = "";
            // 
            // Expression_Box
            // 
            Expression_Box.BackColor = Color.LightGray;
            Expression_Box.BorderStyle = BorderStyle.None;
            Expression_Box.CausesValidation = false;
            Expression_Box.Dock = DockStyle.Fill;
            Expression_Box.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Expression_Box.Location = new Point(1, 1);
            Expression_Box.Margin = new Padding(1);
            Expression_Box.MaxLength = 32;
            Expression_Box.Multiline = false;
            Expression_Box.Name = "Expression_Box";
            Expression_Box.ReadOnly = true;
            Expression_Box.Size = new Size(380, 53);
            Expression_Box.TabIndex = 2;
            Expression_Box.Text = "";
            // 
            // CalculatorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            CausesValidation = false;
            ClientSize = new Size(382, 553);
            Controls.Add(tableLayoutPanel2);
            KeyPreview = true;
            MaximumSize = new Size(700, 800);
            MinimumSize = new Size(400, 600);
            Name = "CalculatorForm";
            Text = "Calculator";
            KeyDown += CalculatorForm_KeyDown;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private CustomButton InPercents_Button;
        private CustomButton Clear_Button;
        private CustomButton ClearOperand_Button;
        private CustomButton DeleteLastDigit_Button;
        private CustomButton Square_Button;
        private CustomButton SquareRoot_Button;
        private CustomButton Inverse_Button;
        private CustomButton Division_Button;
        private CustomButton Two_Button;
        private CustomButton Three_Button;
        private CustomButton Multiplication_Button;
        private CustomButton Four_Button;
        private CustomButton Five_Button;
        private CustomButton Six_Button;
        private CustomButton Substraction_Button;
        private CustomButton Seven_Button;
        private CustomButton Eight_Button;
        private CustomButton Nine_Button;
        private CustomButton Addition_Button;
        private CustomButton ToNegative_Button;
        private CustomButton Zero_Button;
        private CustomButton Decimal_Button;
        private CustomButton Calculate_Button;
        private CustomButton One_Button;
        private RichTextBox Expression_Box;
        private RichTextBox Result_Box;
    }
}
