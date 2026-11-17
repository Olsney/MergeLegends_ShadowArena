using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class BossAttackSoundData
    {
        public AudioData clubSwingAudio;
        [Range(0.1f, 3f)] public float clubSwingPitch;
        
        public AudioData groundSlamAudio;
        [Range(0.1f, 3f)] public float groundSlamPitch;
    }
}