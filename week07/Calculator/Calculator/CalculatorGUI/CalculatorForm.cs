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
            this.calculator.Operand_ToFloat();
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
    }
}
