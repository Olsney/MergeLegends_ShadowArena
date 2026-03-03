using System;
using UnityEngine;

namespace Code.Logic.PlayArea.GridConfig
{
    [Serializable]
    public class GridConfig
    {
        public int Width;
        public int Height;
        public float Spacing;
        public GameObject GridsRoot;
        public GameObject CellPrefab;
        
        public GridConfig(int width, int height, float spacing)
        {
            Width = width;
            Height = height;
            Spacing = spacing;
        }
    }
}