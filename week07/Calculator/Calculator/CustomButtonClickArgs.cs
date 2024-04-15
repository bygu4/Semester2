namespace CalculatorGUI
{
    public class CustomButtonClickArgs : EventArgs
    {
        public CustomButtonClickArgs(int value)
        {
            this.Value = value;
        }

        public int Value { get; private set; }
    }
}
