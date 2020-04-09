// Decompiled with JetBrains decompiler
// Type: Evaluation.WordForm
// Assembly: Evaluation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0956D022-9152-48AE-80E7-68B5F526757D
// Assembly location: \\Mac\Home\Desktop\标注程序\Evaluation.exe

using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Evaluation
{
  public class WordForm : Form
  {
    private Form1 parrent;
    private IContainer components;
    private RichTextBox richTextBox1;
    private Label label1;
    private Label label2;
    private Label label3;

    public WordForm(Form1 parrent_form)
    {
      this.InitializeComponent();
      this.parrent = parrent_form;
      if (this.parrent.config.word_num <= 0)
        return;
      string str = "";
      for (int index = 0; index < this.parrent.config.word_num; ++index)
        str = str + this.parrent.config.words[index, 0] + "," + this.parrent.config.words[index, 1] + "\n";
      this.richTextBox1.Text = str;
    }

    private void WordForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      string str = "";
      for (int index = 0; index < this.parrent.config.word_num; ++index)
        str = str + this.parrent.config.words[index, 0] + "," + this.parrent.config.words[index, 1] + "\n";
      string text = this.richTextBox1.Text;
      if (!(text != str))
        return;
      DialogResult dialogResult = MessageBox.Show((IWin32Window) this, "是否对修改进行保存？", "保存", MessageBoxButtons.YesNoCancel);
      string[,] strArray1 = new string[1000, 2];
      switch (dialogResult)
      {
        case DialogResult.Cancel:
          e.Cancel = true;
          break;
        case DialogResult.Yes:
          int num1 = 0;
          string[] strArray2 = text.Split('\n');
          for (int index = 0; index < strArray2.Length; ++index)
          {
            if (!(strArray2[index].Trim() == ""))
            {
              string[] strArray3 = Regex.Split(strArray2[index], "[,，]");
              if (strArray3.Length == 2)
              {
                strArray1[index, 0] = strArray3[0].Trim();
                strArray1[index, 1] = strArray3[1].Trim();
                ++num1;
              }
              else
              {
                int num2 = (int) MessageBox.Show(strArray2[index] + " 格式有误！");
                e.Cancel = true;
                return;
              }
            }
          }
          for (int index = 0; index < num1; ++index)
          {
            this.parrent.config.words[index, 0] = strArray1[index, 0];
            this.parrent.config.words[index, 1] = strArray1[index, 1];
          }
          int num3 = (int) MessageBox.Show("成功导入" + (object) num1 + "对词组");
          this.parrent.config.word_num = num1;
          this.parrent.config.record_config();
          break;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.richTextBox1 = new RichTextBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.SuspendLayout();
      this.richTextBox1.Location = new Point(-1, -1);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new Size(493, 343);
      this.richTextBox1.TabIndex = 0;
      this.richTextBox1.Text = "";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(4, 357);
      this.label1.Name = "label1";
      this.label1.Size = new Size(458, 18);
      this.label1.TabIndex = 1;
      this.label1.Text = "说明：每一行为一对反义词，两次之间用逗号分隔，例如";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("楷体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label2.Location = new Point(177, 387);
      this.label2.Name = "label2";
      this.label2.Size = new Size(125, 18);
      this.label2.TabIndex = 2;
      this.label2.Text = "负面的,正面的";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("楷体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.label3.Location = new Point(177, 413);
      this.label3.Name = "label3";
      this.label3.Size = new Size(125, 18);
      this.label3.TabIndex = 3;
      this.label3.Text = "乏味的,迷人的";
      this.AutoScaleDimensions = new SizeF(9f, 18f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(491, 440);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.richTextBox1);
      this.Name = nameof (WordForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (WordForm);
      this.FormClosing += new FormClosingEventHandler(this.WordForm_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
