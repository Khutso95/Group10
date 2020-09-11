using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{

    public class Script_Crystal_Functions : MonoBehaviour
    {
        [Tooltip("The Time it will take for the Crystal will Despawn")]
        public float LifeTimeDuration;
        [Tooltip("bool to check if the crystal is stored")]
        public bool isStored;
        [Tooltip("The name of the storage collider tag")]
        public string StorageTag;
        [Tooltip("The name of the deposit collider tag")]
        public string DepositTag;

        private float LifeTimeStart;

        //Varaibles related to the GM
        public GameObject object_GameManager;

        /// Ui Elements to display how long it will be until it degrades
        public Canvas TimerCanvas;
        public Image TimerImage;

        void Start()
        {
            LifeTimeStart = LifeTimeDuration;
            TimerCanvas = GetComponentInChildren<Canvas>();
            TimerImage = GetComponentInChildren<Image>();
            object_GameManager = GameObject.FindGameObjectWithTag("GameController");

            CheckForErrors();
        }

        void Update()
        {
            CrystalDegrade();
            CollisionDetection();
            UpdateUi();
        }

        public void CheckForErrors()
        {
            if (object_GameManager == null)
            {
                Debug.LogError("Missing: object_GameManger == null");
            }
        }
        public void CrystalDegrade()
        {
            if (!isStored)
            {
                LifeTimeDuration -= Time.deltaTime;

            }

            if(LifeTimeDuration <= 0)
            {
                Debug.LogFormat("Destroyed: Script_Crystial_Functions destroyed a gameobject As its LifeTimeDuration reached 0");
  
                Destroy(gameObject);
            }
           
        }

        public void CollisionDetection()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
                
            }

            if(this.transform.parent != null)
            {
                return;
            }
            else
            {

                if (hit.transform.tag == StorageTag)
                {
                    isStored = true;
                    //TimerCanvas.enabled = false;

                }
                else
                {
                    isStored = false;
                   // TimerCanvas.enabled = true;
                }

                if(hit.transform.tag == DepositTag)
                {
                    string childName = this.transform.GetChild(0).name;
                    object_GameManager.GetComponent<Script_GM_RM>().CrystalDeposit(childName);   ///Notifies the GM
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
