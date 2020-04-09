// Decompiled with JetBrains decompiler
// Type: Evaluation.Form1
// Assembly: Evaluation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0956D022-9152-48AE-80E7-68B5F526757D
// Assembly location: \\Mac\Home\Desktop\标注程序\Evaluation.exe

using MaterialSkin;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Sunisoft.IrisSkin;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Evaluation
{
  public class Form1 : Form
  {
    public string background = "background.png";
    public string default_skin = "Skins\\MacOS.ssk";
    public Config config;
    public int[,] scores;
    public ArrayList skins;
    public Random rd;
    private IContainer components;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem 文件ToolStripMenuItem;
    private PictureBox pictureBox1;
    private TrackBar trackBar1;
    private Label left_label;
    private Label right_label;
    private Label negative_label;
    private Label positive_label;
    private MaterialContextMenuStrip materialContextMenuStrip1;
    private ToolStripMenuItem 开始任务ToolStripMenuItem;
    private ToolStripMenuItem 导出数据ToolStripMenuItem;
    private ToolStripMenuItem 退出程序ToolStripMenuItem;
    private ToolStripMenuItem 设置ToolStripMenuItem;
    private ToolStripMenuItem 导入图片ToolStripMenuItem;
    private ToolStripMenuItem 导入词语ToolStripMenuItem;
    private OpenFileDialog openImageDialog;
    private ToolStripMenuItem 重置任务ToolStripMenuItem;
    private BackgroundWorker backgroundWorker1;
    private Button next_button;
    private Button last_button;
    private ToolStripMenuItem 设置分数范围ToolStripMenuItem;
    private SkinEngine skinEngine1;
    private SaveFileDialog saveFileDialog1;
    private Label cursor_label;
    private ToolStripMenuItem 皮肤ToolStripMenuItem;
    private ToolStripMenuItem 一键换肤ToolStripMenuItem;
    private ToolStripMenuItem 恢复默认ToolStripMenuItem;
    private Label progress_label;

    public Form1()
    {
      this.InitializeComponent();
      this.skinEngine1.SkinFile = this.default_skin;
      this.skins = new ArrayList();
      this.rd = new Random();
      foreach (FileSystemInfo file in new DirectoryInfo("Skins").GetFiles("*"))
        this.skins.Add((object) file.FullName);
      this.config = new Config();
      if (File.Exists("config.json"))
      {
        StreamReader streamReader = new StreamReader("config.json");
        this.config = JsonConvert.DeserializeObject<Config>(streamReader.ReadToEnd());
        streamReader.Close();
      }
      else
        this.config.record_config();
      if (this.config.current_pos >= 0 && this.config.picture_num > 0 && this.config.word_num > 0)
      {
        this.scores = new int[this.config.picture_num, this.config.word_num];
        if (File.Exists("scores.txt"))
          this.read_scores();
        else
          this.set_score(this.config.range_index);
      }
      this.refresh_form();
    }

    public void set_score(int index)
    {
      int num;
      switch (index)
      {
        case 1:
          num = -2;
          break;
        case 2:
          num = 1;
          break;
        case 3:
          num = -3;
          break;
        case 4:
          num = 1;
          break;
        case 5:
          num = 1;
          break;
        default:
          num = 0;
          break;
      }
      for (int index1 = 0; index1 < this.config.picture_num; ++index1)
      {
        for (int index2 = 0; index2 < this.config.word_num; ++index2)
          this.scores[index1, index2] = num;
      }
    }

    public void draw_trackbar()
    {
      this.left_label.Text = this.trackBar1.Minimum.ToString();
      this.right_label.Text = this.trackBar1.Maximum.ToString();
      this.cursor_label.Text = "(" + this.trackBar1.Value.ToString() + ")";
    }

    public void read_scores()
    {
      StreamReader streamReader = new StreamReader("scores.txt");
      int index1 = 0;
      while (true)
      {
        string str = streamReader.ReadLine();
        if (str != null && !(str == ""))
        {
          string[] strArray = str.Split(',');
          for (int index2 = 0; index2 < strArray.Length; ++index2)
            this.scores[index1, index2] = int.Parse(strArray[index2]);
          ++index1;
        }
        else
          break;
      }
      streamReader.Close();
    }

    public void write_scores()
    {
      StreamWriter streamWriter = new StreamWriter("scores.txt");
      string str = "";
      for (int index1 = 0; index1 < this.config.picture_num; ++index1)
      {
        for (int index2 = 0; index2 < this.config.word_num; ++index2)
          str = index2 != this.config.word_num - 1 ? str + this.scores[index1, index2].ToString() + "," : str + this.scores[index1, index2].ToString() + "\n";
      }
      streamWriter.Write(str);
      streamWriter.Close();
    }

    public void refresh_form()
    {
      if (this.config.range_index == 1)
      {
        this.trackBar1.Minimum = -2;
        this.trackBar1.Maximum = 2;
        this.trackBar1.Value = -2;
      }
      if (this.config.range_index == 2)
      {
        this.trackBar1.Minimum = 1;
        this.trackBar1.Maximum = 5;
        this.trackBar1.Value = 1;
      }
      if (this.config.range_index == 3)
      {
        this.trackBar1.Minimum = -3;
        this.trackBar1.Maximum = 3;
        this.trackBar1.Value = -3;
      }
      if (this.config.range_index == 4)
      {
        this.trackBar1.Minimum = 1;
        this.trackBar1.Maximum = 7;
        this.trackBar1.Value = 1;
      }
      if (this.config.range_index == 5)
      {
        this.trackBar1.Minimum = 1;
        this.trackBar1.Maximum = 10;
        this.trackBar1.Value = 1;
      }
      if (this.config.current_pos == -1)
      {
        this.pictureBox1.Image = Image.FromFile(this.background);
        this.last_button.Enabled = false;
        this.next_button.Enabled = false;
        this.negative_label.Text = "";
        this.positive_label.Text = "";
        this.progress_label.Text = "进度：0/0";
      }
      else
      {
        int index1 = this.config.current_pos / this.config.word_num;
        int index2 = this.config.current_pos % this.config.word_num;
        this.pictureBox1.Image = Image.FromFile(this.config.pictures[index1]);
        this.negative_label.Text = this.config.words[index2, 0];
        this.positive_label.Text = this.config.words[index2, 1];
        this.last_button.Enabled = true;
        this.next_button.Enabled = true;
        if (this.config.current_pos == 0)
          this.last_button.Enabled = false;
        if (this.config.current_pos == this.config.picture_num * this.config.word_num - 1)
          this.next_button.Enabled = false;
        this.trackBar1.Value = this.scores[index1, index2];
        this.progress_label.Text = "进度：" + (this.config.current_pos + 1).ToString() + "/" + (object) (this.config.word_num * this.config.picture_num);
      }
      this.draw_trackbar();
    }

    private void 导入图片ToolStripMenuItem_Click(object sender, EventArgs e)
    {
            if (this.config.current_pos != -1)
            {
                MessageBox.Show("请先终止当前任务！");
            }
            else
            {
                OpenFileDialog openImageDialog = new OpenFileDialog();
                openImageDialog.Multiselect = true;
                openImageDialog.Filter = "图片文件(*.jpg,*.bmp,*.png, *.jpeg)|*.jpg;*.gif;*.bmp;*.png,*.jpeg";
                if (openImageDialog.ShowDialog() == DialogResult.OK)
                {

                    string[] fileNames = openImageDialog.FileNames;
                    this.config.init_picture(fileNames);
                    MessageBox.Show("图片导入成功，共" + fileNames.Length.ToString() + "张");
                }
            }
     }

    private void 导入词语ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.config.current_pos != -1)
      {
        int num = (int) MessageBox.Show("请先终止当前任务！");
      }
      else
      {
        WordForm wordForm = new WordForm(this);
        wordForm.Owner = (Form) this;
        wordForm.Show();
      }
    }

    private void 开始任务ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.config.current_pos >= 0)
      {
        int num1 = (int) MessageBox.Show("任务已在进行中！");
      }
      else if (this.config.picture_num == 0)
      {
        int num2 = (int) MessageBox.Show("请先导入图片！");
      }
      else if (this.config.word_num == 0)
      {
        int num3 = (int) MessageBox.Show("请先导入词语！");
      }
      else
      {
        this.config.current_pos = 0;
        this.scores = new int[this.config.picture_num, this.config.word_num];
        this.set_score(this.config.range_index);
        this.refresh_form();
      }
    }

    private void 重置任务ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("将清除所有已标注的图片，是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      this.config.current_pos = -1;
      this.config.picture_num = 0;
      this.config.word_num = 0;
      this.config.record_config();
      this.scores = (int[,]) null;
      if (File.Exists("scores.txt"))
        File.Delete("scores.txt");
      this.refresh_form();
    }

    private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Environment.Exit(0);
    }

    private void next_button_Click(object sender, EventArgs e)
    {
      this.scores[this.config.current_pos / this.config.word_num, this.config.current_pos % this.config.word_num] = this.trackBar1.Value;
      ++this.config.current_pos;
      this.refresh_form();
      this.write_scores();
      this.config.record_config();
    }

    private void last_button_Click(object sender, EventArgs e)
    {
      this.scores[this.config.current_pos / this.config.word_num, this.config.current_pos % this.config.word_num] = this.trackBar1.Value;
      --this.config.current_pos;
      this.refresh_form();
      this.write_scores();
      this.config.record_config();
    }

    private void 导出数据ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.saveFileDialog1.Title = "导出标注数据";
      this.saveFileDialog1.Filter = "csv文件|*.csv";
      this.saveFileDialog1.FilterIndex = 1;
      this.saveFileDialog1.RestoreDirectory = true;
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      StreamWriter streamWriter = new StreamWriter(this.saveFileDialog1.FileName.ToString(), false, Encoding.Default);
      string str1 = " ";
      for (int index = 0; index < this.config.word_num; ++index)
        str1 = str1 + "," + this.config.words[index, 0] + "_" + this.config.words[index, 1];
      string str2 = str1 + "\n";
      for (int index1 = 0; index1 < this.config.picture_num; ++index1)
      {
        string str3 = str2 + Path.GetFileName(this.config.pictures[index1]);
        for (int index2 = 0; index2 < this.config.word_num; ++index2)
          str3 = str3 + "," + this.scores[index1, index2].ToString();
        str2 = str3 + "\n";
      }
      streamWriter.Write(str2);
      streamWriter.Close();
      int num = (int) MessageBox.Show("导出成功！");
    }

    private void 设置分数范围ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.config.current_pos != -1)
      {
        int num1 = (int) MessageBox.Show("请先终止当前任务！");
      }
      else
      {
        SetValueForm setValueForm = new SetValueForm(this);
        setValueForm.Owner = (Form) this;
        int num2 = (int) setValueForm.ShowDialog();
        this.refresh_form();
      }
    }

    private void trackBar1_MouseDown(object sender, MouseEventArgs e)
    {
      this.trackBar1.Value = Convert.ToInt32((double) e.X / (double) this.trackBar1.Width * (double) (this.trackBar1.Maximum - this.trackBar1.Minimum)) + this.trackBar1.Minimum;
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e)
    {
      this.cursor_label.Text = "(" + this.trackBar1.Value.ToString() + ")";
    }

    private void 一键换肤ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.skinEngine1.SkinFile = (string) this.skins[this.rd.Next(this.skins.Count)];
    }

    private void 恢复默认ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.skinEngine1.SkinFile = this.default_skin;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.menuStrip1 = new MenuStrip();
      this.文件ToolStripMenuItem = new ToolStripMenuItem();
      this.开始任务ToolStripMenuItem = new ToolStripMenuItem();
      this.重置任务ToolStripMenuItem = new ToolStripMenuItem();
      this.导出数据ToolStripMenuItem = new ToolStripMenuItem();
      this.退出程序ToolStripMenuItem = new ToolStripMenuItem();
      this.设置ToolStripMenuItem = new ToolStripMenuItem();
      this.导入图片ToolStripMenuItem = new ToolStripMenuItem();
      this.导入词语ToolStripMenuItem = new ToolStripMenuItem();
      this.设置分数范围ToolStripMenuItem = new ToolStripMenuItem();
      this.pictureBox1 = new PictureBox();
      this.trackBar1 = new TrackBar();
      this.left_label = new Label();
      this.right_label = new Label();
      this.negative_label = new Label();
      this.positive_label = new Label();
      this.materialContextMenuStrip1 = new MaterialContextMenuStrip();
      this.openImageDialog = new OpenFileDialog();
      this.backgroundWorker1 = new BackgroundWorker();
      this.next_button = new Button();
      this.last_button = new Button();
      this.skinEngine1 = new SkinEngine();
      this.saveFileDialog1 = new SaveFileDialog();
      this.cursor_label = new Label();
      this.皮肤ToolStripMenuItem = new ToolStripMenuItem();
      this.一键换肤ToolStripMenuItem = new ToolStripMenuItem();
      this.恢复默认ToolStripMenuItem = new ToolStripMenuItem();
      this.progress_label = new Label();
      this.menuStrip1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.trackBar1.BeginInit();
      this.SuspendLayout();
      this.menuStrip1.BackColor = SystemColors.ButtonHighlight;
      this.menuStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.文件ToolStripMenuItem,
        (ToolStripItem) this.设置ToolStripMenuItem,
        (ToolStripItem) this.皮肤ToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(916, 32);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      this.文件ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.开始任务ToolStripMenuItem,
        (ToolStripItem) this.重置任务ToolStripMenuItem,
        (ToolStripItem) this.导出数据ToolStripMenuItem,
        (ToolStripItem) this.退出程序ToolStripMenuItem
      });
      this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
      this.文件ToolStripMenuItem.Size = new Size(58, 28);
      this.文件ToolStripMenuItem.Text = "文件";
      this.开始任务ToolStripMenuItem.Name = "开始任务ToolStripMenuItem";
      this.开始任务ToolStripMenuItem.Size = new Size(152, 28);
      this.开始任务ToolStripMenuItem.Text = "开始任务";
      this.开始任务ToolStripMenuItem.Click += new EventHandler(this.开始任务ToolStripMenuItem_Click);
      this.重置任务ToolStripMenuItem.Name = "重置任务ToolStripMenuItem";
      this.重置任务ToolStripMenuItem.Size = new Size(152, 28);
      this.重置任务ToolStripMenuItem.Text = "清除任务";
      this.重置任务ToolStripMenuItem.Click += new EventHandler(this.重置任务ToolStripMenuItem_Click);
      this.导出数据ToolStripMenuItem.Name = "导出数据ToolStripMenuItem";
      this.导出数据ToolStripMenuItem.Size = new Size(152, 28);
      this.导出数据ToolStripMenuItem.Text = "导出数据";
      this.导出数据ToolStripMenuItem.Click += new EventHandler(this.导出数据ToolStripMenuItem_Click);
      this.退出程序ToolStripMenuItem.Name = "退出程序ToolStripMenuItem";
      this.退出程序ToolStripMenuItem.Size = new Size(152, 28);
      this.退出程序ToolStripMenuItem.Text = "退出程序";
      this.退出程序ToolStripMenuItem.Click += new EventHandler(this.退出程序ToolStripMenuItem_Click);
      this.设置ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.导入图片ToolStripMenuItem,
        (ToolStripItem) this.导入词语ToolStripMenuItem,
        (ToolStripItem) this.设置分数范围ToolStripMenuItem
      });
      this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
      this.设置ToolStripMenuItem.Size = new Size(58, 28);
      this.设置ToolStripMenuItem.Text = "设置";
      this.导入图片ToolStripMenuItem.Name = "导入图片ToolStripMenuItem";
      this.导入图片ToolStripMenuItem.Size = new Size(152, 28);
      this.导入图片ToolStripMenuItem.Text = "导入图片";
      this.导入图片ToolStripMenuItem.Click += new EventHandler(this.导入图片ToolStripMenuItem_Click);
      this.导入词语ToolStripMenuItem.Name = "导入词语ToolStripMenuItem";
      this.导入词语ToolStripMenuItem.Size = new Size(152, 28);
      this.导入词语ToolStripMenuItem.Text = "导入词语";
      this.导入词语ToolStripMenuItem.Click += new EventHandler(this.导入词语ToolStripMenuItem_Click);
      this.设置分数范围ToolStripMenuItem.Name = "设置分数范围ToolStripMenuItem";
      this.设置分数范围ToolStripMenuItem.Size = new Size(152, 28);
      this.设置分数范围ToolStripMenuItem.Text = "设置量表";
      this.设置分数范围ToolStripMenuItem.Click += new EventHandler(this.设置分数范围ToolStripMenuItem_Click);
      this.pictureBox1.Location = new Point(12, 52);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(892, 606);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.Controls.Add((Control) this.progress_label);
      this.pictureBox1.TabStop = false;
      this.trackBar1.BackColor = SystemColors.Control;
      this.trackBar1.Location = new Point(230, 709);
      this.trackBar1.Maximum = 2;
      this.trackBar1.Minimum = -2;
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new Size(451, 69);
      this.trackBar1.TabIndex = 2;
      this.trackBar1.TickStyle = TickStyle.TopLeft;
      this.trackBar1.ValueChanged += new EventHandler(this.trackBar1_ValueChanged);
      this.trackBar1.MouseDown += new MouseEventHandler(this.trackBar1_MouseDown);
      this.left_label.AutoSize = true;
      this.left_label.Location = new Point(240, 695);
      this.left_label.Name = "left_label";
      this.left_label.Size = new Size(26, 18);
      this.left_label.TabIndex = 3;
      this.left_label.Text = "-2";
      this.right_label.AutoSize = true;
      this.right_label.Location = new Point(653, 695);
      this.right_label.Name = "right_label";
      this.right_label.Size = new Size(17, 18);
      this.right_label.TabIndex = 7;
      this.right_label.Text = "2";
      this.negative_label.AutoSize = true;
      this.negative_label.Font = new Font("黑体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.negative_label.Location = new Point(138, 726);
      this.negative_label.Name = "negative_label";
      this.negative_label.Size = new Size(62, 18);
      this.negative_label.TabIndex = 8;
      this.negative_label.Text = "粗糙的";
      this.positive_label.AutoSize = true;
      this.positive_label.Font = new Font("黑体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 134);
      this.positive_label.Location = new Point(719, 722);
      this.positive_label.Name = "positive_label";
      this.positive_label.Size = new Size(62, 18);
      this.positive_label.TabIndex = 9;
      this.positive_label.Text = "精致的";
      this.materialContextMenuStrip1.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.materialContextMenuStrip1.Depth = 0;
      this.materialContextMenuStrip1.MouseState = MouseState.HOVER;
      this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
      this.materialContextMenuStrip1.Size = new Size(61, 4);
      this.openImageDialog.FileName = "openFileDialog1";
      this.openImageDialog.Filter = "(*.jpg,*.png,*.jpeg,*.bmp,*.gif)|*.jgp;*.JPG;*.png;*.PNG;*.jpeg;*.JPEG;*.bmp;*.BMP;*.gif|All files(*.*)|*.*";
      this.openImageDialog.Multiselect = true;
      this.openImageDialog.Title = "打开图片";
      this.next_button.BackgroundImageLayout = ImageLayout.Stretch;
      this.next_button.Font = new Font("微软雅黑", 10.5f, FontStyle.Bold, GraphicsUnit.Point, (byte) 134);
      this.next_button.Location = new Point(823, 698);
      this.next_button.Name = "next_button";
      this.next_button.Size = new Size(59, 59);
      this.next_button.TabIndex = 10;
      this.next_button.Text = "-->";
      this.next_button.UseVisualStyleBackColor = true;
      this.next_button.Click += new EventHandler(this.next_button_Click);
      this.last_button.BackgroundImageLayout = ImageLayout.Stretch;
      this.last_button.Font = new Font("微软雅黑", 10.5f, FontStyle.Bold, GraphicsUnit.Point, (byte) 134);
      this.last_button.Location = new Point(45, 700);
      this.last_button.Name = "last_button";
      this.last_button.Size = new Size(59, 59);
      this.last_button.TabIndex = 11;
      this.last_button.Text = "<--";
      this.last_button.UseVisualStyleBackColor = true;
      this.last_button.Click += new EventHandler(this.last_button_Click);
      this.skinEngine1.__DrawButtonFocusRectangle = true;
      this.skinEngine1.DisabledButtonTextColor = Color.Gray;
      this.skinEngine1.DisabledMenuFontColor = SystemColors.GrayText;
      this.skinEngine1.InactiveCaptionColor = SystemColors.InactiveCaptionText;
      this.skinEngine1.SerialNumber = "";
      this.skinEngine1.SkinFile = (string) null;
      this.cursor_label.AutoSize = true;
      this.cursor_label.Font = new Font("微软雅黑", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 134);
      this.cursor_label.Location = new Point(433, 675);
      this.cursor_label.Name = "cursor_label";
      this.cursor_label.Size = new Size(47, 31);
      this.cursor_label.TabIndex = 12;
      this.cursor_label.Text = "(0)";
      this.皮肤ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.一键换肤ToolStripMenuItem,
        (ToolStripItem) this.恢复默认ToolStripMenuItem
      });
      this.皮肤ToolStripMenuItem.Name = "皮肤ToolStripMenuItem";
      this.皮肤ToolStripMenuItem.Size = new Size(58, 28);
      this.皮肤ToolStripMenuItem.Text = "皮肤";
      this.一键换肤ToolStripMenuItem.Name = "一键换肤ToolStripMenuItem";
      this.一键换肤ToolStripMenuItem.Size = new Size(152, 28);
      this.一键换肤ToolStripMenuItem.Text = "一键换肤";
      this.一键换肤ToolStripMenuItem.Click += new EventHandler(this.一键换肤ToolStripMenuItem_Click);
      this.恢复默认ToolStripMenuItem.Name = "恢复默认ToolStripMenuItem";
      this.恢复默认ToolStripMenuItem.Size = new Size(152, 28);
      this.恢复默认ToolStripMenuItem.Text = "恢复默认";
      this.恢复默认ToolStripMenuItem.Click += new EventHandler(this.恢复默认ToolStripMenuItem_Click);
      this.progress_label.AutoSize = true;
      this.progress_label.Location = new Point(786, 67);
      this.progress_label.Name = "progress_label";
      this.progress_label.Size = new Size(62, 18);
      this.progress_label.TabIndex = 13;
      this.progress_label.Text = "label1";
      this.progress_label.Parent = (Control) this.pictureBox1;
      this.progress_label.BackColor = Color.Transparent;
      this.AutoScaleDimensions = new SizeF(9f, 18f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(916, 791);
      this.Controls.Add((Control) this.progress_label);
      this.Controls.Add((Control) this.cursor_label);
      this.Controls.Add((Control) this.last_button);
      this.Controls.Add((Control) this.next_button);
      this.Controls.Add((Control) this.positive_label);
      this.Controls.Add((Control) this.negative_label);
      this.Controls.Add((Control) this.right_label);
      this.Controls.Add((Control) this.left_label);
      this.Controls.Add((Control) this.trackBar1);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = nameof (Form1);
      this.Text = "语义评价";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.trackBar1.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
