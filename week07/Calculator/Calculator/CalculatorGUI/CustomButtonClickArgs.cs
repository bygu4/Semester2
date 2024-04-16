// <copyright file="CustomButtonClickArgs.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CalculatorGUI
{
    /// <summary>
    /// Class of event arguments for the click of CustomButton.
    /// </summary>
    public class CustomButtonClickArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomButtonClickArgs"/> class.
        /// </summary>
        /// <param name="value">Value to set for this instance.</param>
        public CustomButtonClickArgs(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value of this instance.
        /// </summary>
        public int Value { get; private set; }
    }
}
