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

        //Varaibles related to the Crafting Bench
        public GameObject object_CraftingBench;

        /// Ui Elements to display how long it will be until it degrades
        public Canvas TimerCanvas;
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
                    //Debug.Log("Is Decaing");
                    break;
                case RawReasourceState.InUse:
                    //Debug.Log("Is in Use");
                    break;
                case RawReasourceState.Stored:
                  //  Debug.Log("Is Stored");
                    break;
               
            }
        }

        #endregion

       
        void Start()
        {
            LifeTimeStart = LifeTimeDuration;
            TimerCanvas = GetComponentInChildren<Canvas>();
            TimerImage = GetComponentInChildren<Image>();
            object_CraftingBench = GameObject.FindGameObjectWithTag("GameController");
            CheckForErrors();
        }

        void Update()
        {
            /*
            if(_StateHolder == RawReasourceState.InUse)
            {
                Debug.Log("The Resource is in Use");
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _StateHolder = RawReasourceState.Decaying;
                CheckState();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                _StateHolder = RawReasourceState.InUse;
                CheckState();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                _StateHolder = RawReasourceState.Stored;
                CheckState();
            }
            */

            RawReasourceDecay();
            CollisionDetection();
            UpdateUi();
        }

        public void CheckForErrors()
        {
            if (object_CraftingBench == null)
            {
                Debug.LogError("Missing: object_GameManger == null");
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


                   // string childName = this.transform.GetChild(0).name;
                   //object_GameManager.GetComponent<Script_GM_RM>().CrystalDeposit(childName);   ///Notifies the GM
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
    }
}
