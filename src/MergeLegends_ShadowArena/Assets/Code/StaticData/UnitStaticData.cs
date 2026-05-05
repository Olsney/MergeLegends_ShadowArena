using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/Unit")]
    public class UnitStaticData : ScriptableObject
    {
        public UnitAttackTypeId UnitAttackTypeId;
        public List<UnitStatsByLevelIndexSetup> UnitStatsByLevelIndexSetups;
        public GeneralData GeneralData;
    }
}