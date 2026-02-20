using System;
using UnityEngine;

namespace Code.Logic.PlayArea.GridSettings
{
    [Serializable]
    public class GridConfig
    {
        [field: SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField] public float Spacing { get; private set; }

        public GridConfig(int width, int height, float spacing)
        {
            Width = width;
            Height = height;
            Spacing = spacing;
        }
    }
}