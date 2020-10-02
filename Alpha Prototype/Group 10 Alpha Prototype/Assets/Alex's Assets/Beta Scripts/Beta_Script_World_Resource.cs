using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{

    public class Beta_Script_World_Resource : MonoBehaviour
    {
        public enum WorldResourceType
        {
            OfType1 = 0,
            OfType2 = 1,
            OfType3 = 2
        }
        [Tooltip("This varaiable indiactes the type of resource the Game manger will recognise and spawn")]
        public WorldResourceType _worldResource;
    }
}
