using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class AudioData
    {
        public AudioClip AudioClip;

        [Range(0f, 1f)] 
        public float Volume = 1f;
    }
}