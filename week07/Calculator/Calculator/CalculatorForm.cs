namespace CalculatorGUI
{
    using Calculator;

    public partial class CalculatorForm : Form
    {
        private Calculator calculator;

        public CalculatorForm()
        {
            this.calculator = new Calculator();
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, ButtonEventArgs e)
        {
            this.calculator.AddDigit(e.Value);
        }

        private void OperationButton_Click(object sender, ButtonEventArgs e)
        {
            this.calculator.SetOperation((char)e.Value);
        }
    }
}
