// Decompiled with JetBrains decompiler
// Type: Evaluation.Properties.Resources
// Assembly: Evaluation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0956D022-9152-48AE-80E7-68B5F526757D
// Assembly location: \\Mac\Home\Desktop\标注程序\Evaluation.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Evaluation.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (Evaluation.Properties.Resources.resourceMan == null)
          Evaluation.Properties.Resources.resourceMan = new ResourceManager("Evaluation.Properties.Resources", typeof (Evaluation.Properties.Resources).Assembly);
        return Evaluation.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Evaluation.Properties.Resources.resourceCulture;
      }
      set
      {
        Evaluation.Properties.Resources.resourceCulture = value;
      }
    }
  }
}
