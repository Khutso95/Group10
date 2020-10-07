using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Beta_Script_Refined_Output : MonoBehaviour
    {

        public enum OutputType
        {
            OfType1 = 0,
            OfType2 = 1,
            OfType3 = 2
        }
        public OutputType _outputType;

        public enum UpgradeType
        {
            notUpgrad = 0,
            SpeedUpgrade = 1,
            DamageUpgrade = 2,
            Repair = 3
        }
        public UpgradeType _upgradeType;
    }
}
