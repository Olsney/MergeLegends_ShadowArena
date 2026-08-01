using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "SoundStaticData", menuName = "StaticData/Sound")]
    public class SoundStaticData : ScriptableObject
    {
        public List<GameSoundData> GameSoundDatas;
    }
}