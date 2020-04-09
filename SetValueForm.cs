// Decompiled with JetBrains decompiler
// Type: Evaluation.SetValueForm
// Assembly: Evaluation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0956D022-9152-48AE-80E7-68B5F526757D
// Assembly location: \\Mac\Home\Desktop\标注程序\Evaluation.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Evaluation
{
  public class SetValueForm : Form
  {
    private Form1 parrent;
    private IContainer components;
    private RadioButton radioButton5;
    private RadioButton radioButton4;
    private RadioButton radioButton3;
    private RadioButton radioButton2;
    private RadioButton radioButton1;

    public SetValueForm(Form1 parrent_form)
    {
      this.InitializeComponent();
      this.parrent = parrent_form;
      if (this.parrent.config.range_index == 1)
        this.radioButton1.Checked = true;
      if (this.parrent.config.range_index == 2)
        this.radioButton2.Checked = true;
      if (this.parrent.config.range_index == 3)
        this.radioButton3.Checked = true;
      if (this.parrent.config.range_index == 4)
        this.radioButton4.Checked = true;
      if (this.parrent.config.range_index != 5)
        return;
      this.radioButton5.Checked = true;
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      if (this.radioButton2.Checked)
        this.parrent.config.range_index = 2;
      this.parrent.config.record_config();
    }

    private void radioButton3_CheckedChanged(object sender, EventArgs e)
    {
      if (this.radioButton3.Checked)
        this.parrent.config.range_index = 3;
      this.parrent.config.record_config();
    }

    private void radioButton4_CheckedChanged(object sender, EventArgs e)
    {
      if (this.radioButton4.Checked)
        this.parrent.config.range_index = 4;
      this.parrent.config.record_config();
    }

    private void radioButton5_CheckedChanged(object sender, EventArgs e)
    {
      if (this.radioButton5.Checked)
        this.parrent.config.range_index = 5;
      this.parrent.config.record_config();
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      if (this.radioButton1.Checked)
        this.parrent.config.range_index = 1;
      this.parrent.config.record_config();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radioButton5 = new RadioButton();
      this.radioButton4 = new RadioButton();
      this.radioButton3 = new RadioButton();
      this.radioButton2 = new RadioButton();
      this.radioButton1 = new RadioButton();
      this.SuspendLayout();
      this.radioButton5.AutoSize = true;
      this.radioButton5.Location = new Point(31, 157);
      this.radioButton5.Name = "radioButton5";
      this.radioButton5.Size = new Size(186, 22);
      this.radioButton5.TabIndex = 9;
      this.radioButton5.TabStop = true;
      this.radioButton5.Text = "10级量表（1，10）";
      this.radioButton5.UseVisualStyleBackColor = true;
      this.radioButton5.CheckedChanged += new EventHandler(this.radioButton5_CheckedChanged);
      this.radioButton4.AutoSize = true;
      this.radioButton4.Location = new Point(31, 124);
      this.radioButton4.Name = "radioButton4";
      this.radioButton4.Size = new Size(168, 22);
      this.radioButton4.TabIndex = 8;
      this.radioButton4.TabStop = true;
      this.radioButton4.Text = "7级量表（1，7）";
      this.radioButton4.UseVisualStyleBackColor = true;
      this.radioButton4.CheckedChanged += new EventHandler(this.radioButton4_CheckedChanged);
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new Point(31, 91);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(177, 22);
      this.radioButton3.TabIndex = 7;
      this.radioButton3.TabStop = true;
      this.radioButton3.Text = "7级量表（-3，3）";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(31, 58);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(168, 22);
      this.radioButton2.TabIndex = 6;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "5级量表（1，5）";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new Point(31, 26);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(177, 22);
      this.radioButton1.TabIndex = 5;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "5级量表（-2，2）";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
      this.AutoScaleDimensions = new SizeF(9f, 18f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(309, 210);
      this.Controls.Add((Control) this.radioButton5);
      this.Controls.Add((Control) this.radioButton4);
      this.Controls.Add((Control) this.radioButton3);
      this.Controls.Add((Control) this.radioButton2);
      this.Controls.Add((Control) this.radioButton1);
      this.Name = nameof (SetValueForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "设置量表";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
