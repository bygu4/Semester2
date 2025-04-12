// Copyright (c) Alexander Bugaev 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace CalculatorGUI;

using Calculator;

/// <summary>
/// The main form of the application.
/// </summary>
public partial class CalculatorForm : Form
{
    private Calculator calculator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorForm"/> class.
    /// </summary>
    public CalculatorForm()
    {
        this.calculator = new Calculator();
        this.InitializeComponent();
        this.Bind();
        this.Select();
    }

    private void Bind()
    {
        this.Expression_Box.DataBindings.Add(
            "Text",
            this.calculator,
            nameof(Calculator.Expression),
            false,
            DataSourceUpdateMode.OnPropertyChanged);

        this.Result_Box.DataBindings.Add(
            "Text",
            this.calculator,
            nameof(Calculator.Result),
            false,
            DataSourceUpdateMode.OnPropertyChanged);
    }

    private void CalculatorForm_ButtonClick(object sender, CustomButtonClickArgs e)
        => CalculatorCommands.Execute(this.calculator, e.Value);

    private void CalculatorForm_KeyDown(object sender, KeyEventArgs e)
        => CalculatorKeys.ProcessKeyDown(this.calculator, e);
}
