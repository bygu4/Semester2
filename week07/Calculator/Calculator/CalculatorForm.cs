namespace CalculatorGUI
{
    using Calculator;

    public partial class CalculatorForm : Form
    {
        private Calculator calculator;

        public CalculatorForm()
        {
            InitializeComponent();
            this.calculator = new Calculator();
            this.Bind();
        }

        private void Bind()
        {
            this.Expression_Box.DataBindings.Add(
                "Text", this.calculator, nameof(Calculator.Expression),
                false, DataSourceUpdateMode.OnPropertyChanged);
            this.Result_Box.DataBindings.Add(
                "Text", this.calculator, nameof(Calculator.Result),
                false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Digit_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.AddDigitToOperand(e.Value);
        }

        private void Operation_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.SetOperation((char)e.Value);
        }

        private void Calculate_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Calculate();
        }

        private void ToPercents_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.OperandToPercents();
        }

        private void Clear_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.Clear();
        }

        private void ClearOperand_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.ClearOperand();
        }

        private void DeleteLastDigit_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.DeleteLastDigit();
        }

        private void ToSquare_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.OperandToSquare();
        }

        private void ToSquareRoot_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.OperandToSquareRoot();
        }

        private void ToInverse_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.OperandToInverse();
        }

        private void ToNegative_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.OperandToNegative();
        }

        private void ToFloat_Button_Click(object sender, CustomButtonClickArgs e)
        {
            this.calculator.OperandToFloat();
        }
    }
}
