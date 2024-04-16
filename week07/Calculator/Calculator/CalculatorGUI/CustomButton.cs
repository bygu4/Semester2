using System.ComponentModel;

namespace CalculatorGUI
{
    [DefaultEvent("Click")]
    public partial class CustomButton : UserControl
    {
        new public event EventHandler<CustomButtonClickArgs>? Click;

        public CustomButton()
        {
            InitializeComponent();
        }

        public int Value { get; set; }

        public string Label
        {
            get => this.Button.Text;
            set => this.Button.Text = value;
        }

        public Color ButtonColor
        {
            get => this.Button.BackColor;
            set => this.Button.BackColor = value;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            this.Click?.Invoke(this, new CustomButtonClickArgs(this.Value));
        }
    }
}
