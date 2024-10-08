﻿// Copyright (c) 2024
//
// Use of this source code is governed by an MIT license
// that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

namespace CalculatorGUI;

partial class CustomButton
{
    /// <summary> 
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором компонентов

    /// <summary> 
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
        Button = new Button();
        SuspendLayout();
        // 
        // Button
        // 
        Button.BackColor = Color.White;
        Button.CausesValidation = false;
        Button.Dock = DockStyle.Fill;
        Button.FlatAppearance.BorderColor = Color.DimGray;
        Button.FlatStyle = FlatStyle.Flat;
        Button.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
        Button.Location = new Point(0, 0);
        Button.Margin = new Padding(0);
        Button.Name = "Button";
        Button.Size = new Size(150, 150);
        Button.TabIndex = 0;
        Button.TabStop = false;
        Button.Text = "button1";
        Button.UseVisualStyleBackColor = false;
        Button.Click += Button_Click;
        // 
        // CustomButton
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnablePreventFocusChange;
        BackColor = Color.White;
        CausesValidation = false;
        Controls.Add(Button);
        Margin = new Padding(0);
        Name = "CustomButton";
        ResumeLayout(false);
    }

    #endregion

    private Button Button;
}
