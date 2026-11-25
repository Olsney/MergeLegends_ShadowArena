using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "MusicStaticData", menuName = "StaticData/Music")]
    public class MusicStaticData : ScriptableObject
    {
        public List<GameMusicData> GameMusicDatas;
    }
}