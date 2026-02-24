using System;
using Code.UI.View;

namespace Code.Logic.Fight
{
    public interface IStartFightView : IView
    {
        event Action FightClicked;
    }
}