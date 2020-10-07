using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{
    public class Beta_Script_Refined_Reasource : MonoBehaviour
    {
        #region ReasourceStates
        public enum RefinedReasourceState
        {
            Idle = 0,
            InUse = 1,
            OnOutput = 2,
            Empty = 3
        }
        public enum RefinedResourceType
        {
            Type_1 = 0,
            Type_2 = 1,
            Type_3 = 2
        }
        [Tooltip("This is the holder that tracks the state of the reasources")]
        public RefinedReasourceState _StateHolder;
        [Tooltip("This is the type of resource this Gameobject is associated with")]
        public RefinedResourceType _ResourceType;     
        #endregion

        #region collision Variables
        [Tooltip("The name of the output collider tag")]
        public string CraftingOutputTag;
        [Tooltip("The name of the respective resource input tag that the raw resource would be used in")]
        public string InUseTag;
        [Tooltip("The name of the tag to destroy the resource")]
        public string DestroyTag;
        #endregion

        #region Communication Variables
        //Varaibles related to the Crafting Bench
        [Tooltip("These are all the GameObject Dedicated as crafitng bench in the scene")]
        public GameObject[] object_CraftingBench;
        [Tooltip("This is the closest crafting bench currently")]
        public GameObject closestCB;
        [Tooltip("Reference the Game Manager Gameobject here")]
        public GameObject GameManager;
        #endregion

        #region Resources Variables
        [Tooltip("The Current Amount of Resource Inside the refined Resource")]
        public float ResourceAmount;
        [Tooltip("The Maximun Amount of Resource the refined resource hold")]
        public float ResourceMax;
        [Tooltip("The Rate at which the resource is used up")]
        public float ResourceUseRate;

        #endregion

        #region Ui Variables
        public Image UiImage;
        public Material DeadMat;
        #endregion

        void Start()
        {
            object_CraftingBench = GameObject.FindGameObjectsWithTag("CraftingBench");
            GameManager = GameObject.FindGameObjectWithTag("GameController");
            UiImage = GetComponentInChildren<Image>();
            ResourceAmount = ResourceMax;
        }


        void Update()
        {
            FixRotation();
            CollisionDetection();
            CommunicateWithGameManager();
            UpdateUI();
            ManageResource();
        }

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
                if(_StateHolder != RefinedReasourceState.Empty)
                {
                    _StateHolder = RefinedReasourceState.Idle;
                }
               
                return;
            }
            else
            {
                if(_StateHolder != RefinedReasourceState.Empty)
                {
                    if (hit.transform.tag == CraftingOutputTag)
                    {
                        FindClosestCraftingBench();
                        _StateHolder = RefinedReasourceState.OnOutput;

                    }


                    if (hit.transform.tag == InUseTag)
                    {
                        var temp = hit.transform.GetComponent<Beta_Script_Refined_Output>()._outputType;
                        if ((int)temp == (int)_ResourceType)
                        {
                            _StateHolder = RefinedReasourceState.InUse;
                        }
                    }

                    if (hit.transform.tag != CraftingOutputTag && hit.transform.tag != InUseTag)
                    {
                        _StateHolder = RefinedReasourceState.Idle;
                    }
                }
                
                if(hit.transform.tag == DestroyTag)
                {
                   
                    Destroy(gameObject);
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
            closestCB = closest;
            return closest;
        }

        public void CommunicateWithGameManager()
        {
            if(_StateHolder == RefinedReasourceState.InUse)
            {
                switch (_ResourceType)
                {
                    case RefinedResourceType.Type_1:  //fuel
                        GameManager.GetComponent<Beta_Script_GameManager>().IncreaseFuel();
                        break;
                    case RefinedResourceType.Type_2:  //Ammo
                        GameManager.GetComponent<Beta_Script_GameManager>().IncreaseAmmo();
                        break;
                    case RefinedResourceType.Type_3:  //Upgrade 
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5))
                        {
                            var UpgradeType = hit.transform.gameObject.GetComponent<Beta_Script_Refined_Output>()._upgradeType;
                            GameManager.GetComponent<Beta_Script_GameManager>().PowerUp((int)UpgradeType);
                        }
                          
                        break;

                }
            }
        }
        #endregion

        #region Resource Methods
        public void ManageResource()
        {
            if(_StateHolder == RefinedReasourceState.InUse)
            {
                ResourceAmount -= ResourceUseRate * Time.deltaTime;
            }

            if(ResourceAmount <= 0)
            {
                _StateHolder = RefinedReasourceState.Empty;
                Material mat = GetComponent<MeshRenderer>().material;
                mat.color = DeadMat.color;
            }
        }

        #endregion

        #region Ui MEthods
        public void UpdateUI()
        {
            UiImage.fillAmount = ResourceAmount / ResourceMax;
        }
        #endregion
    }
}
