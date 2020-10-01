using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{

    public class Beta_Output_Collider_Script : MonoBehaviour
    {
        [Tooltip("The Crafting Bench Object that corresponds to this object")]
        public GameObject CraftingBench;
        [Tooltip("The name of the refined resource tag")]
        public string ReasourceName;
        [Tooltip("Bool detects if a refined resource is on top of the output")]
        public bool _canSpawn;

        public void DetectBlock()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1))
            {
                if(hit.transform.tag == ReasourceName)
                {
                    _canSpawn = false;
                }
                else
                {
                    _canSpawn = true;
                }
            }
            else
            {
                _canSpawn = true;
            }
        }

    }
}
