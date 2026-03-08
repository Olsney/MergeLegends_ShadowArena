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
    }
}