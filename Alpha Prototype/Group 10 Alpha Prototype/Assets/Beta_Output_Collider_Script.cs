using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{

    public class Beta_Output_Collider_Script : MonoBehaviour
    {
        //Varaibles related to the Crafting Bench
        public GameObject[] object_CraftingBench;
        public GameObject closestGO;

        public bool occupied;
        public string ReasourceName;

        
        void Start()
        {
            object_CraftingBench = GameObject.FindGameObjectsWithTag("CraftingBench");
            FindClosestCraftingBench();
        }

       
        void Update()
        {
            
        }

        public GameObject FindClosestCraftingBench()
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject CraftingBench in object_CraftingBench)
            {
                Vector3 diff = CraftingBench.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = CraftingBench;
                    distance = curDistance;
                }
            }
            closestGO = closest;
            return closest;
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == ReasourceName)
            {
                occupied = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == ReasourceName)
            {
                occupied = true;
            }
        }

    }
}
