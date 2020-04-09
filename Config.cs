// Decompiled with JetBrains decompiler
// Type: Evaluation.Config
// Assembly: Evaluation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0956D022-9152-48AE-80E7-68B5F526757D
// Assembly location: \\Mac\Home\Desktop\标注程序\Evaluation.exe

using Newtonsoft.Json;
using System.IO;

namespace Evaluation
{
  public class Config
  {
    public int current_pos;
    public string[] pictures;
    public string[,] words;
    public int picture_num;
    public int word_num;
    public int range_index;

    public Config()
    {
      this.current_pos = -1;
      this.picture_num = 0;
      this.word_num = 0;
      this.range_index = 1;
      this.pictures = new string[1000];
      this.words = new string[1000, 2];
    }

    public void reset()
    {
      this.current_pos = -1;
      this.picture_num = -1;
      this.word_num = -1;
    }

    public void init_picture(string[] var_pictures)
    {
      this.picture_num = var_pictures.Length;
      for (int index = 0; index < this.picture_num; ++index)
        this.pictures[index] = var_pictures[index];
    }

    public void init_picture(string[][] var_words)
    {
      this.word_num = var_words.Length;
      for (int index1 = 0; index1 < this.word_num; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
          this.words[index1, index2] = var_words[index1][index2];
      }
    }

    public void record_config()
    {
      string str = JsonConvert.SerializeObject((object) this);
      StreamWriter streamWriter = new StreamWriter("config.json");
      streamWriter.Write(str);
      streamWriter.Close();
    }
  }
}
