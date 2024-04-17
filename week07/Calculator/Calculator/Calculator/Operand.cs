﻿// <copyright file="Operand.cs" company="SPBU">
// Copyright (c) Alexander Bugaev 2024. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Calculator;

using Operations;
using System.ComponentModel;

/// <summary>
/// Class representing an operand of the expression.
/// </summary>
public class Operand : INotifyPropertyChanged
{
    /// <summary>
    /// Default Representation value of the Operand.
    /// </summary>
    public const string Default = "0";

    private string representation;
    private float value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Operand"/> class.
    /// </summary>
    public Operand()
    {
        this.representation = Operand.Default;
    }

    /// <summary>
    /// Handlers that are invoked after setting of Operand properties.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets current string representation of the Operand.
    /// </summary>
    public string Representation
    {
        get
        {
            return this.representation;
        }

        private set
        {
            this.representation = value;
            this.NotifyPropertyChanged(nameof(this.Representation));
        }
    }

    /// <summary>
    /// Gets current value of the Operand.
    /// </summary>
    public float Value
    {
        get
        {
            return this.value;
        }

        private set
        {
            this.value = value;
            this.NotifyPropertyChanged(nameof(this.Value));
        }
    }

    /// <summary>
    /// Set Operand to default values.
    /// </summary>
    public void SetToDefault()
    {
        this.SetByRepresentation(Operand.Default);
    }

    /// <summary>
    /// Add digit to the representation of the Operand.
    /// </summary>
    /// <param name="digit">Digit to add.</param>
    /// <exception cref="ArgumentException">Argument not a digit.</exception>
    public void AddDigit(char digit)
    {
        if (!char.IsDigit(digit))
        {
            throw new ArgumentException("Argument was not a digit");
        }

        if (this.representation == Operand.Default)
        {
            this.SetByRepresentation($"{digit}");
        }
        else if (this.representation == $"-{Operand.Default}")
        {
            this.SetByRepresentation($"-{digit}");
        }
        else
        {
            this.SetByRepresentation($"{this.Representation}{digit}");
        }
    }

    /// <summary>
    /// Delete the last digit from the representation of the Operand.
    /// </summary>
    public void DeleteLastDigit()
    {
        if ((this.Value > 0 && this.Representation.Length == 1) ||
            this.representation == $"-{Operand.Default}")
        {
            this.SetToDefault();
        }
        else if (this.Value < 0 && this.Representation.Length == 2)
        {
            this.SetByRepresentation($"-{Operand.Default}");
        }
        else
        {
            this.SetByRepresentation(this.Representation[0..^1]);
        }
    }

    /// <summary>
    /// Add decimal point to the representation of the Operand.
    /// </summary>
    public void Decimal()
    {
        this.SetByRepresentation($"{this.Representation},");
    }

    /// <summary>
    /// Convert Operand value to the negative one.
    /// </summary>
    public void ToNegative()
    {
        if (this.Representation.StartsWith('-'))
        {
            this.Representation = this.Representation[1..];
        }
        else
        {
            this.Representation = $"-{this.Representation}";
        }

        this.Value = -this.Value;
    }

    /// <summary>
    /// Convert Operand value to percents.
    /// </summary>
    public void InPercents()
    {
        this.ApplyUnaryOperation(Operations.InPercents);
    }

    /// <summary>
    /// Raise Operand value to the power of two.
    /// </summary>
    public void Square()
    {
        this.ApplyUnaryOperation(Operations.Square);
    }

    /// <summary>
    /// Set Operand value as its square root.
    /// </summary>
    public void SquareRoot()
    {
        this.ApplyUnaryOperation(Operations.SquareRoot);
    }

    /// <summary>
    /// Convert Operand value to the inverse one.
    /// </summary>
    public void Inverse()
    {
        this.ApplyUnaryOperation(Operations.Inverse);
    }

    /// <summary>
    /// Set Operand properties based on given value.
    /// </summary>
    /// <param name="value">Value to set.</param>
    public void SetByValue(float value)
    {
        if (float.IsNaN(value))
        {
            this.Representation = "Undefined";
        }
        else
        {
            this.Representation = $"{value}";
        }

        this.Value = value;
    }

    private void SetByRepresentation(string representation)
    {
        if (float.TryParse(representation, out float value))
        {
            this.Representation = representation;
            this.Value = value;
        }
    }

    private void ApplyUnaryOperation(Operation operation)
    {
        this.Representation = operation.GetExpression(this.Representation, string.Empty);
        this.Value = operation.GetResult(this.Value, 0);
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
