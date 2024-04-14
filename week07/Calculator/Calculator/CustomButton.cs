namespace CalculatorGUI
{
    internal class CustomButton : Button
    {
        new public event EventHandler<ButtonEventArgs> Click;

        public CustomButton(int digit)
            : base()
        {
            this.Digit = digit;
            this.Click += delegate { };
        }

        public int Digit { get; private set; }
    }
}
