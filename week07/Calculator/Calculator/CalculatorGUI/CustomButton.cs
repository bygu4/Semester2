// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace CalculatorGUI;

using System.ComponentModel;

/// <summary>
/// Button with specific char value.
/// </summary>
[DefaultEvent("Click")]
public partial class CustomButton : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomButton"/> class.
    /// </summary>
    public CustomButton() => this.InitializeComponent();

    /// <summary>
    /// Handlers that are invoked when the button is clicked.
    /// </summary>
    public new event EventHandler<CustomButtonClickArgs>? Click;

    /// <summary>
    /// Gets or sets the value of the button.
    /// </summary>
    public char Value { get; set; }

    /// <summary>
    /// Gets or sets the label on the button.
    /// </summary>
    public string Label
    {
        get => this.Button.Text;
        set => this.Button.Text = value;
    }

    /// <summary>
    /// Gets or sets background color of the button.
    /// </summary>
    public Color ButtonColor
    {
        get => this.Button.BackColor;
        set => this.Button.BackColor = value;
    }

    private void Button_Click(object sender, EventArgs e)
    {
        this.Click?.Invoke(this, new CustomButtonClickArgs(this.Value));
        this.ActiveControl = null;
    }
}
