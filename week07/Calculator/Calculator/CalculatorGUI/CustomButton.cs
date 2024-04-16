// <copyright file="CustomButton.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CalculatorGUI
{
    using System.ComponentModel;

    /// <summary>
    /// Button with specific value.
    /// </summary>
    [DefaultEvent("Click")]
    public partial class CustomButton : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomButton"/> class.
        /// </summary>
        public CustomButton()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handlers that are invoked when the button is clicked.
        /// </summary>
        public new event EventHandler<CustomButtonClickArgs>? Click;

        /// <summary>
        /// Gets or sets the value of the button.
        /// </summary>
        public int Value { get; set; }

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
        }
    }
}
