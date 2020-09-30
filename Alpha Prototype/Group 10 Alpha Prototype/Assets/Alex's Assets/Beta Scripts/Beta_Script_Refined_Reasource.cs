using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Beta_Script_Refined_Reasource : MonoBehaviour
    {
        #region ReasourceStates
        public enum RefinedReasourceState
        {
            
            Idle = 0,
            InUse = 1,
            OnOutput = 2

        }
        [Tooltip("This is the holder that tracks the state of the reasources")]
        public RefinedReasourceState _StateHolder;

        public void CheckState()
        {
            // Debug.Log("Checking State");
            switch (_StateHolder)
            {
                case RefinedReasourceState.Idle:

                    break;
                case RefinedReasourceState.InUse:

                    break;
                case RefinedReasourceState.OnOutput:

                    break;

            }
        }

        #endregion

        #region collision Variables
        [Tooltip("The name of the output collider tag")]
        public string CraftingOutputTag;
        [Tooltip("The name of the respective resource input tag that the raw resource would be used in")]
        public string InUseTag;
        #endregion

        #region Communication Variables
        //Varaibles related to the Crafting Bench
        public GameObject[] object_CraftingBench;
        public GameObject closestGO;

        //Varables related to the Use of the Refined Resource
        #endregion

        #region Ui Variables
        #endregion

        #region Collision Methods
        public void CollisionDetection() //This detects in
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5))
            {
                
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);

            }

            if (this.transform.parent != null)
            {
                _StateHolder = RefinedReasourceState.Idle;
                return;
            }
            else
            {

                if (hit.transform.tag == CraftingOutputTag)
                {
                    FindClosestCraftingBench();
                    _StateHolder = RefinedReasourceState.OnOutput;
                   
                }


                if (hit.transform.tag == InUseTag)
                {
                    _StateHolder = RefinedReasourceState.InUse;
                    

                }

                if (hit.transform.tag != CraftingOutputTag && hit.transform.tag != InUseTag)
                {
                    _StateHolder = RefinedReasourceState.Idle;
                }
            }

        }

        public void FixRotation()
        {
            //Code that fixes the rotation back to the nearest 90 degree angle
            if (transform.rotation.y != 0 && transform.parent == null)
            {
                var CurAngle = transform.eulerAngles;
                CurAngle.y = Mathf.Round(CurAngle.y / 90) * 90;
                transform.eulerAngles = CurAngle;
            }
        }
        #endregion

        #region Communication


        public GameObject FindClosestCraftingBench() //Finds the closest crafting bench, this is used to notify it if it is still on the spawning location
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

        public void SendMessageToCrafter()
        {

            
        }

        #endregion
        // Start is called before the first frame update
        void Start()
        {
            object_CraftingBench = GameObject.FindGameObjectsWithTag("CraftingBench");
        }

        // Update is called once per frame
        void Update()
        {
            FixRotation();
            SendMessageToCrafter();
            CollisionDetection();
        }
    }
}
