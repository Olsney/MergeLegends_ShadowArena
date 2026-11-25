using System;
using Code.Services.Tutorial;

namespace Code.StaticData.Tutorial
{
    [Serializable]
    public struct CellStep
    {
        public int CellIndex;
        public TutorialPointerSide PointerSide;
    }
}