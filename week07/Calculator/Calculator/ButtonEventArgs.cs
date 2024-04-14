﻿namespace CalculatorGUI
{
    internal class ButtonEventArgs : EventArgs
    {
        public ButtonEventArgs(int value)
        {
            this.Value = value;
        }

        public int Value { get; private set; }
    }
}
