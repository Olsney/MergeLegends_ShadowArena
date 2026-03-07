using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class GridData
    {
        public int Width;
        public int Height;
        public float Spacing;
        public GameObject GridsRoot;
        public GameObject CellPrefab;
        
        public GridData(int width, int height, float spacing)
        {
            Width = width;
            Height = height;
            Spacing = spacing;
        }
    }
}