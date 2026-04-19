using System;
using Code.Services.Windows;
using Code.UI.Services.Windows;

namespace Code.StaticData
{
  [Serializable]
  public class WindowData
  {
    public WindowType windowType;
    public WindowBase Template;
  }
}