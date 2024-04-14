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
            tableLayoutPanel2 = new TableLayoutPanel();

            One_Button = new CustomButton(1);
            Two_Button = new CustomButton(2);
            Three_Button = new CustomButton(3);
            Four_Button = new CustomButton(4);
            Five_Button = new CustomButton(5);
            Six_Button = new CustomButton(6);
            Seven_Button = new CustomButton(7);
            Eight_Button = new CustomButton(8);
            Nine_Button = new CustomButton(9);
            Zero_Button = new CustomButton(0);

            Addition_Button = new CustomButton('+');
            Substraction_Button = new CustomButton('-');
            Multiplication_Button = new CustomButton('*');
            Division_Button = new CustomButton('/');

            Calculate_Button = new Button();
            ToFloat_Button = new Button();
            ToNegative_Button = new Button();
            ToInverse_Button = new Button();
            ToSquareRoot_Button = new Button();
            ToSquared_Button = new Button();
            button5 = new Button();
            Clear_Button = new Button();
            ClearOperand_Button = new Button();
            DeleteLastDigit_Button = new Button();

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
            tableLayoutPanel1.Controls.Add(Zero_Button, 1, 5);
            tableLayoutPanel1.Controls.Add(Addition_Button, 3, 4);
            tableLayoutPanel1.Controls.Add(Nine_Button, 2, 4);
            tableLayoutPanel1.Controls.Add(Eight_Button, 1, 4);
            tableLayoutPanel1.Controls.Add(Seven_Button, 0, 4);
            tableLayoutPanel1.Controls.Add(Substraction_Button, 3, 3);
            tableLayoutPanel1.Controls.Add(Six_Button, 2, 3);
            tableLayoutPanel1.Controls.Add(Five_Button, 1, 3);
            tableLayoutPanel1.Controls.Add(Four_Button, 0, 3);
            tableLayoutPanel1.Controls.Add(Multiplication_Button, 3, 2);
            tableLayoutPanel1.Controls.Add(Three_Button, 2, 2);
            tableLayoutPanel1.Controls.Add(Two_Button, 1, 2);
            tableLayoutPanel1.Controls.Add(Division_Button, 3, 1);
            tableLayoutPanel1.Controls.Add(Calculate_Button, 3, 5);
            tableLayoutPanel1.Controls.Add(ToFloat_Button, 2, 5);
            tableLayoutPanel1.Controls.Add(ToNegative_Button, 0, 5);
            tableLayoutPanel1.Controls.Add(ToInverse_Button, 2, 1);
            tableLayoutPanel1.Controls.Add(ToSquareRoot_Button, 1, 1);
            tableLayoutPanel1.Controls.Add(ToSquared_Button, 0, 1);
            tableLayoutPanel1.Controls.Add(button5, 0, 0);
            tableLayoutPanel1.Controls.Add(Clear_Button, 1, 0);
            tableLayoutPanel1.Controls.Add(ClearOperand_Button, 2, 0);
            tableLayoutPanel1.Controls.Add(DeleteLastDigit_Button, 3, 0);
            tableLayoutPanel1.Controls.Add(One_Button, 0, 2);
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
            tableLayoutPanel1.Size = new Size(376, 404);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // Calculate_Button
            // 
            Calculate_Button.BackColor = Color.DarkOrange;
            Calculate_Button.Dock = DockStyle.Fill;
            Calculate_Button.FlatStyle = FlatStyle.Flat;
            Calculate_Button.Font = new Font("Segoe UI", 13.8F);
            Calculate_Button.Location = new Point(283, 336);
            Calculate_Button.Margin = new Padding(1);
            Calculate_Button.Name = "Calculate_Button";
            Calculate_Button.Size = new Size(92, 67);
            Calculate_Button.TabIndex = 19;
            Calculate_Button.Text = "=";
            Calculate_Button.UseVisualStyleBackColor = false;
            // 
            // ToFloat_Button
            // 
            ToFloat_Button.BackColor = Color.Gainsboro;
            ToFloat_Button.Dock = DockStyle.Fill;
            ToFloat_Button.FlatStyle = FlatStyle.Flat;
            ToFloat_Button.Font = new Font("Segoe UI", 13.8F);
            ToFloat_Button.Location = new Point(189, 336);
            ToFloat_Button.Margin = new Padding(1);
            ToFloat_Button.Name = "ToFloat_Button";
            ToFloat_Button.Size = new Size(92, 67);
            ToFloat_Button.TabIndex = 18;
            ToFloat_Button.Text = ",";
            ToFloat_Button.UseVisualStyleBackColor = false;
            // 
            // ToNegative_Button
            // 
            ToNegative_Button.BackColor = Color.Gainsboro;
            ToNegative_Button.Dock = DockStyle.Fill;
            ToNegative_Button.FlatStyle = FlatStyle.Flat;
            ToNegative_Button.Font = new Font("Segoe UI", 13.8F);
            ToNegative_Button.Location = new Point(1, 336);
            ToNegative_Button.Margin = new Padding(1);
            ToNegative_Button.Name = "ToNegative_Button";
            ToNegative_Button.Size = new Size(92, 67);
            ToNegative_Button.TabIndex = 16;
            ToNegative_Button.Text = "+/-";
            ToNegative_Button.UseVisualStyleBackColor = false;
            // 
            // ToInverse_Button
            // 
            ToInverse_Button.BackColor = Color.Silver;
            ToInverse_Button.Dock = DockStyle.Fill;
            ToInverse_Button.FlatStyle = FlatStyle.Flat;
            ToInverse_Button.Font = new Font("Segoe UI", 13.8F);
            ToInverse_Button.Location = new Point(189, 68);
            ToInverse_Button.Margin = new Padding(1);
            ToInverse_Button.Name = "ToInverse_Button";
            ToInverse_Button.Size = new Size(92, 65);
            ToInverse_Button.TabIndex = 2;
            ToInverse_Button.Text = "1/x";
            ToInverse_Button.UseVisualStyleBackColor = false;
            // 
            // ToSquareRoot_Button
            // 
            ToSquareRoot_Button.BackColor = Color.Silver;
            ToSquareRoot_Button.Dock = DockStyle.Fill;
            ToSquareRoot_Button.FlatStyle = FlatStyle.Flat;
            ToSquareRoot_Button.Font = new Font("Segoe UI", 13.8F);
            ToSquareRoot_Button.Location = new Point(95, 68);
            ToSquareRoot_Button.Margin = new Padding(1);
            ToSquareRoot_Button.Name = "ToSquareRoot_Button";
            ToSquareRoot_Button.Size = new Size(92, 65);
            ToSquareRoot_Button.TabIndex = 1;
            ToSquareRoot_Button.Text = "√x";
            ToSquareRoot_Button.UseVisualStyleBackColor = false;
            // 
            // ToSquared_Button
            // 
            ToSquared_Button.BackColor = Color.Silver;
            ToSquared_Button.Dock = DockStyle.Fill;
            ToSquared_Button.FlatStyle = FlatStyle.Flat;
            ToSquared_Button.Font = new Font("Segoe UI", 13.8F);
            ToSquared_Button.Location = new Point(1, 68);
            ToSquared_Button.Margin = new Padding(1);
            ToSquared_Button.Name = "ToSquared_Button";
            ToSquared_Button.Size = new Size(92, 65);
            ToSquared_Button.TabIndex = 0;
            ToSquared_Button.Text = "x^2";
            ToSquared_Button.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = Color.Silver;
            button5.Dock = DockStyle.Fill;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button5.Location = new Point(1, 1);
            button5.Margin = new Padding(1);
            button5.Name = "button5";
            button5.Size = new Size(92, 65);
            button5.TabIndex = 20;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = false;
            // 
            // Clear_Button
            // 
            Clear_Button.BackColor = Color.Silver;
            Clear_Button.Dock = DockStyle.Fill;
            Clear_Button.FlatStyle = FlatStyle.Flat;
            Clear_Button.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Clear_Button.Location = new Point(95, 1);
            Clear_Button.Margin = new Padding(1);
            Clear_Button.Name = "Clear_Button";
            Clear_Button.Size = new Size(92, 65);
            Clear_Button.TabIndex = 21;
            Clear_Button.Text = "C";
            Clear_Button.UseVisualStyleBackColor = false;
            // 
            // ClearOperand_Button
            // 
            ClearOperand_Button.BackColor = Color.Silver;
            ClearOperand_Button.Dock = DockStyle.Fill;
            ClearOperand_Button.FlatStyle = FlatStyle.Flat;
            ClearOperand_Button.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            ClearOperand_Button.Location = new Point(189, 1);
            ClearOperand_Button.Margin = new Padding(1);
            ClearOperand_Button.Name = "ClearOperand_Button";
            ClearOperand_Button.Size = new Size(92, 65);
            ClearOperand_Button.TabIndex = 22;
            ClearOperand_Button.Text = "CE";
            ClearOperand_Button.UseVisualStyleBackColor = false;
            // 
            // DeleteLastDigit_Button
            // 
            DeleteLastDigit_Button.BackColor = Color.Silver;
            DeleteLastDigit_Button.Dock = DockStyle.Fill;
            DeleteLastDigit_Button.FlatStyle = FlatStyle.Flat;
            DeleteLastDigit_Button.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            DeleteLastDigit_Button.Location = new Point(283, 1);
            DeleteLastDigit_Button.Margin = new Padding(1);
            DeleteLastDigit_Button.Name = "DeleteLastDigit_Button";
            DeleteLastDigit_Button.Size = new Size(92, 65);
            DeleteLastDigit_Button.TabIndex = 23;
            DeleteLastDigit_Button.Text = "<-";
            DeleteLastDigit_Button.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 2);
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
            // One_Button
            // 
            One_Button.BackColor = Color.Gainsboro;
            One_Button.Dock = DockStyle.Fill;
            One_Button.FlatStyle = FlatStyle.Flat;
            One_Button.Font = new Font("Segoe UI", 13.8F);
            One_Button.Location = new Point(1, 135);
            One_Button.Margin = new Padding(1);
            One_Button.Name = "One_Button";
            One_Button.Size = new Size(92, 65);
            One_Button.TabIndex = 24;
            One_Button.Text = "1";
            One_Button.UseVisualStyleBackColor = false;
            One_Button.Click += DigitButton_Click;
            // 
            // Division_Button
            // 
            Division_Button.BackColor = Color.Silver;
            Division_Button.Dock = DockStyle.Fill;
            Division_Button.FlatStyle = FlatStyle.Flat;
            Division_Button.Font = new Font("Segoe UI", 13.8F);
            Division_Button.Location = new Point(283, 68);
            Division_Button.Margin = new Padding(1);
            Division_Button.Name = "Division_Button";
            Division_Button.Size = new Size(92, 65);
            Division_Button.TabIndex = 25;
            Division_Button.Text = "/";
            Division_Button.UseVisualStyleBackColor = false;
            // 
            // Two_Button
            // 
            Two_Button.BackColor = Color.Gainsboro;
            Two_Button.Dock = DockStyle.Fill;
            Two_Button.FlatStyle = FlatStyle.Flat;
            Two_Button.Font = new Font("Segoe UI", 13.8F);
            Two_Button.Location = new Point(95, 135);
            Two_Button.Margin = new Padding(1);
            Two_Button.Name = "Two_Button";
            Two_Button.Size = new Size(92, 65);
            Two_Button.TabIndex = 26;
            Two_Button.Text = "2";
            Two_Button.UseVisualStyleBackColor = false;
            Two_Button.Click += DigitButton_Click;
            // 
            // Three_Button
            // 
            Three_Button.BackColor = Color.Gainsboro;
            Three_Button.Dock = DockStyle.Fill;
            Three_Button.FlatStyle = FlatStyle.Flat;
            Three_Button.Font = new Font("Segoe UI", 13.8F);
            Three_Button.Location = new Point(189, 135);
            Three_Button.Margin = new Padding(1);
            Three_Button.Name = "Three_Button";
            Three_Button.Size = new Size(92, 65);
            Three_Button.TabIndex = 27;
            Three_Button.Text = "3";
            Three_Button.UseVisualStyleBackColor = false;
            Three_Button.Click += DigitButton_Click;
            // 
            // Multiplication_Button
            // 
            Multiplication_Button.BackColor = Color.Silver;
            Multiplication_Button.Dock = DockStyle.Fill;
            Multiplication_Button.FlatStyle = FlatStyle.Flat;
            Multiplication_Button.Font = new Font("Segoe UI", 13.8F);
            Multiplication_Button.Location = new Point(283, 135);
            Multiplication_Button.Margin = new Padding(1);
            Multiplication_Button.Name = "Multiplication_Button";
            Multiplication_Button.Size = new Size(92, 65);
            Multiplication_Button.TabIndex = 28;
            Multiplication_Button.Text = "*";
            Multiplication_Button.UseVisualStyleBackColor = false;
            // 
            // Four_Button
            // 
            Four_Button.BackColor = Color.Gainsboro;
            Four_Button.Dock = DockStyle.Fill;
            Four_Button.FlatStyle = FlatStyle.Flat;
            Four_Button.Font = new Font("Segoe UI", 13.8F);
            Four_Button.Location = new Point(1, 202);
            Four_Button.Margin = new Padding(1);
            Four_Button.Name = "Four_Button";
            Four_Button.Size = new Size(92, 65);
            Four_Button.TabIndex = 29;
            Four_Button.Text = "4";
            Four_Button.UseVisualStyleBackColor = false;
            Four_Button.Click += DigitButton_Click;
            // 
            // Five_Button
            // 
            Five_Button.BackColor = Color.Gainsboro;
            Five_Button.Dock = DockStyle.Fill;
            Five_Button.FlatStyle = FlatStyle.Flat;
            Five_Button.Font = new Font("Segoe UI", 13.8F);
            Five_Button.Location = new Point(95, 202);
            Five_Button.Margin = new Padding(1);
            Five_Button.Name = "Five_Button";
            Five_Button.Size = new Size(92, 65);
            Five_Button.TabIndex = 30;
            Five_Button.Text = "5";
            Five_Button.UseVisualStyleBackColor = false;
            Five_Button.Click += DigitButton_Click;
            // 
            // Six_Button
            // 
            Six_Button.BackColor = Color.Gainsboro;
            Six_Button.Dock = DockStyle.Fill;
            Six_Button.FlatStyle = FlatStyle.Flat;
            Six_Button.Font = new Font("Segoe UI", 13.8F);
            Six_Button.Location = new Point(189, 202);
            Six_Button.Margin = new Padding(1);
            Six_Button.Name = "Six_Button";
            Six_Button.Size = new Size(92, 65);
            Six_Button.TabIndex = 31;
            Six_Button.Text = "6";
            Six_Button.UseVisualStyleBackColor = false;
            Six_Button.Click += DigitButton_Click;
            // 
            // Substraction_Button
            // 
            Substraction_Button.BackColor = Color.Silver;
            Substraction_Button.Dock = DockStyle.Fill;
            Substraction_Button.FlatStyle = FlatStyle.Flat;
            Substraction_Button.Font = new Font("Segoe UI", 13.8F);
            Substraction_Button.Location = new Point(283, 202);
            Substraction_Button.Margin = new Padding(1);
            Substraction_Button.Name = "Substraction_Button";
            Substraction_Button.Size = new Size(92, 65);
            Substraction_Button.TabIndex = 32;
            Substraction_Button.Text = "-";
            Substraction_Button.UseVisualStyleBackColor = false;
            // 
            // Seven_Button
            // 
            Seven_Button.BackColor = Color.Gainsboro;
            Seven_Button.Dock = DockStyle.Fill;
            Seven_Button.FlatStyle = FlatStyle.Flat;
            Seven_Button.Font = new Font("Segoe UI", 13.8F);
            Seven_Button.Location = new Point(1, 269);
            Seven_Button.Margin = new Padding(1);
            Seven_Button.Name = "Seven_Button";
            Seven_Button.Size = new Size(92, 65);
            Seven_Button.TabIndex = 33;
            Seven_Button.Text = "7";
            Seven_Button.UseVisualStyleBackColor = false;
            Seven_Button.Click += DigitButton_Click;
            // 
            // Eight_Button
            // 
            Eight_Button.BackColor = Color.Gainsboro;
            Eight_Button.Dock = DockStyle.Fill;
            Eight_Button.FlatStyle = FlatStyle.Flat;
            Eight_Button.Font = new Font("Segoe UI", 13.8F);
            Eight_Button.Location = new Point(95, 269);
            Eight_Button.Margin = new Padding(1);
            Eight_Button.Name = "Eight_Button";
            Eight_Button.Size = new Size(92, 65);
            Eight_Button.TabIndex = 34;
            Eight_Button.Text = "8";
            Eight_Button.UseVisualStyleBackColor = false;
            Eight_Button.Click += DigitButton_Click;
            // 
            // Nine_Button
            // 
            Nine_Button.BackColor = Color.Gainsboro;
            Nine_Button.Dock = DockStyle.Fill;
            Nine_Button.FlatStyle = FlatStyle.Flat;
            Nine_Button.Font = new Font("Segoe UI", 13.8F);
            Nine_Button.Location = new Point(189, 269);
            Nine_Button.Margin = new Padding(1);
            Nine_Button.Name = "Nine_Button";
            Nine_Button.Size = new Size(92, 65);
            Nine_Button.TabIndex = 35;
            Nine_Button.Text = "9";
            Nine_Button.UseVisualStyleBackColor = false;
            Nine_Button.Click += DigitButton_Click;
            // 
            // Addition_Button
            // 
            Addition_Button.BackColor = Color.Silver;
            Addition_Button.Dock = DockStyle.Fill;
            Addition_Button.FlatStyle = FlatStyle.Flat;
            Addition_Button.Font = new Font("Segoe UI", 13.8F);
            Addition_Button.Location = new Point(283, 269);
            Addition_Button.Margin = new Padding(1);
            Addition_Button.Name = "Addition_Button";
            Addition_Button.Size = new Size(92, 65);
            Addition_Button.TabIndex = 36;
            Addition_Button.Text = "+";
            Addition_Button.UseVisualStyleBackColor = false;
            // 
            // Zero_Button
            // 
            Zero_Button.BackColor = Color.Gainsboro;
            Zero_Button.Dock = DockStyle.Fill;
            Zero_Button.FlatStyle = FlatStyle.Flat;
            Zero_Button.Font = new Font("Segoe UI", 13.8F);
            Zero_Button.Location = new Point(95, 336);
            Zero_Button.Margin = new Padding(1);
            Zero_Button.Name = "Zero_Button";
            Zero_Button.Size = new Size(92, 67);
            Zero_Button.TabIndex = 37;
            Zero_Button.Text = "0";
            Zero_Button.UseVisualStyleBackColor = false;
            // 
            // CalculatorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 553);
            Controls.Add(tableLayoutPanel2);
            MaximumSize = new Size(700, 800);
            MinimumSize = new Size(400, 600);
            Name = "CalculatorForm";
            Text = "Calculator";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;

        private CustomButton One_Button;
        private CustomButton Two_Button;
        private CustomButton Three_Button;
        private CustomButton Four_Button;
        private CustomButton Five_Button;
        private CustomButton Six_Button;
        private CustomButton Seven_Button;
        private CustomButton Eight_Button;
        private CustomButton Nine_Button;
        private CustomButton Zero_Button;

        private CustomButton Addition_Button;
        private CustomButton Substraction_Button;
        private CustomButton Multiplication_Button;
        private CustomButton Division_Button;


        private Button Calculate_Button;
        private Button button5;
        private Button Clear_Button;
        private Button ClearOperand_Button;
        private Button DeleteLastDigit_Button;
        private Button ToSquared_Button;
        private Button ToSquareRoot_Button;
        private Button ToInverse_Button;
        private Button ToNegative_Button;
        private Button ToFloat_Button;
    }
}
