using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{
    public class Beta_Script_Raw_Reasource : MonoBehaviour
    {
        [Tooltip("The Time it will take for the Crystal will Despawn")]
        public float LifeTimeDuration;
        [Tooltip("The rate at which the reasource decays by during the use")]
        public float InUseRate;
        [Tooltip("The name of the storage collider tag")]
        public string StorageTag;
        [Tooltip("The name of the deposit collider tag")]
        public string CraftingInputTag;
       

        private float LifeTimeStart;
        private bool FindNearestObject;

        //Varaibles related to the Crafting Bench
        public GameObject[] object_CraftingBench;
        public GameObject closestGO;

        /// Ui Elements to display how long it will be until it degrades
        public Image TimerImage;
        #region ReasourceStates
       
       
        public enum RawReasourceState
        {
            Decaying = 0,
            InUse = 1,
            Stored = 2
        }
        [Tooltip("This is the holder that tracks the state of the reasources")]
        public RawReasourceState _StateHolder;

        public void CheckState()
        {
           // Debug.Log("Checking State");
            switch (_StateHolder)
            {
                case RawReasourceState.Decaying:
                    
                    break;
                case RawReasourceState.InUse:
                    
                    break;
                case RawReasourceState.Stored:
                 
                    break;
               
            }
        }

        #endregion

       
        void Start()
        {
            LifeTimeStart = LifeTimeDuration;
            TimerImage = GetComponentInChildren<Image>();
            object_CraftingBench = GameObject.FindGameObjectsWithTag("CraftingBench");
            CheckForErrors();
            
        }

        void Update()
        {
            RawReasourceDecay();
            CollisionDetection();
            ControlledSearch();
            UpdateUi();
        }

        public void CheckForErrors()
        {
            if (object_CraftingBench == null)
            {
                Debug.LogError("Missing: object_CraftingBench == null");
            }
        }

        public void RawReasourceDecay() //This fucntions as a timer to detroy the reasource
        {
            if (_StateHolder == RawReasourceState.Decaying)
            {
                LifeTimeDuration -= Time.deltaTime;

            }

            if(_StateHolder == RawReasourceState.InUse)
            {
                LifeTimeDuration -= InUseRate * Time.deltaTime;
                SendMessageToCrafter();
            }

            if (LifeTimeDuration <= 0)
            {
                Debug.LogFormat("Destroyed: Script_Crystial_Functions destroyed a gameobject As its LifeTimeDuration reached 0");

                Destroy(gameObject);
            }

        }

        public void CollisionDetection() //This detects in
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);

            }

            if (this.transform.parent != null)
            {
                _StateHolder = RawReasourceState.Decaying;
                return;
            }
            else
            {

                if (hit.transform.tag == StorageTag)
                {
                    _StateHolder = RawReasourceState.Stored;
                }
               

                if (hit.transform.tag == CraftingInputTag)
                {
                    _StateHolder = RawReasourceState.InUse;
                    

                }

                if(hit.transform.tag != StorageTag && hit.transform.tag != CraftingInputTag)
                {
                    _StateHolder = RawReasourceState.Decaying;
                }
            }

        }

        public void UpdateUi()
        {
            float PercentageFill = LifeTimeDuration / LifeTimeStart;

            TimerImage.fillAmount = PercentageFill;
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

        public void ControlledSearch()
        {
            if(this.transform.parent != null)
            {
                FindNearestObject = false;
            }
            
            if (this.transform.parent != null && !FindNearestObject)
            {
                FindClosestCraftingBench();
                FindNearestObject = true;
            }
        }

        public void SendMessageToCrafter()
        {
            string childName = this.transform.GetChild(0).name;
            closestGO.GetComponent<Beta_Script_Crafting>().CraftingRefinedResource(childName);  ///Notifies the GM
        }
    }
}
