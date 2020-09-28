using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{

    public class Script_P1_Reasource_Entity : MonoBehaviour
    {
        public float ReasourceAmount;
        public float StartingAmount;
        public float MaxAmount;

        public bool ResType1 = false;
        public bool ResType2 = false;
        public bool ResType3 = false;

        public bool IsOnDrain;
        public bool IsOnRefill;

        public string DrainReasourceSlot;
        public string FillReasourceSlot;

        public Material BaseMat;
        public Material DeadMat;

        public Image TimeBar;
        public Canvas TimeCanvas;

        //variables for the GM
        public GameObject object_GameManager;

        void Start()
        {
            //Section to determine what type of Resource it is
            string child = this.transform.GetChild(0).name;  //Gets the name of the child highest in the hierachy should be the object "ofTypeX"
            if(child == "OfType1") { ResType1 = true; }else if(child == "OfType2") { ResType2 = true; }else if(child == "OfType3") { ResType3 = true; } else
            {
                Debug.LogError("Script_P1_Reasource_Entity Child object not OfTypeX");
            }

            ReasourceAmount = StartingAmount;
            TimeCanvas = GetComponentInChildren<Canvas>();
            object_GameManager = GameObject.FindGameObjectWithTag("GameController");

        }
        void Update()
        {
            DetectOnPoint();
            ReasourceManagement();

            //Code that fixes the rotation back to the nearest 90 degree angle
            if(transform.rotation.y != 0 && transform.parent == null)
            {
                var CurAngle = transform.eulerAngles;
                CurAngle.y = Mathf.Round(CurAngle.y / 90) * 90;
                transform.eulerAngles = CurAngle;
            }
        }

        void CheckForErrors()
        {
            if (TimeBar == null) { Debug.LogError("Script_P1_Reasource_Entity: Missing TimeBar reference"); }
            if (BaseMat == null) { Debug.LogError("Script_P1_Reasource_Entity: Missing BaseMat reference"); }
            if (DeadMat == null) { Debug.LogError("Script_P1_Reasource_Entity: Missing DeadMat reference"); }
            if (DrainReasourceSlot == " ") { Debug.LogError("Script_P1_Reasource_Entity: ReasourceSlot feild is empty"); }
          
        }
       

        public void DetectOnPoint()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
                
            }

            if(this.transform.parent != null ) //checks if it is has a parent = being moved by the player
            {
                IsOnDrain = false;
                
            }
            else
            {
                if(hit.transform.tag == DrainReasourceSlot)
                {
                    IsOnDrain = true;
                }
                else
                {
                    IsOnDrain = false;
                }
            }

            if(this.transform.parent != null)
            {
                IsOnRefill = false;

            }
            else
            {
                if(hit.transform.tag == FillReasourceSlot)
                {
                    IsOnRefill = true;
                }
                else
                {
                    IsOnRefill = false;
                }
            }
        }

        public void ReasourceManagement()
        {
            if (ReasourceAmount >= MaxAmount)
            {
                ReasourceAmount = MaxAmount;
            }
            if(ReasourceAmount <= 0)
            {
                ReasourceAmount = 0;
            }

            if (IsOnDrain)
            {
                ReasourceAmount -= Time.deltaTime;
                float ImageFill = ReasourceAmount / MaxAmount;
                TimeBar.fillAmount = ImageFill;

                if (ReasourceAmount > 0)
                {

                    string childName = this.transform.GetChild(0).name;
                    object_GameManager.GetComponent<Script_GM_RM>().IncreaseLocalRes(childName);
                }

                if (ReasourceAmount <= 0)
                {
                    ObjectDied();
                }
            }
            else if (IsOnRefill)
            {
                string childName = this.transform.GetChild(0).name;
                object_GameManager.GetComponent<Script_GM_RM>().CrystalInput(childName);   ///Notifies the GM
            }
            {
                return;
            }

        }

        public void RefilCell()
        {

            ReasourceAmount += Time.deltaTime;
            float ImageFill = ReasourceAmount / MaxAmount;
            TimeBar.fillAmount = ImageFill;
            if (ReasourceAmount > 0)
            {
                ObjectLives();
            }
        }

        public void ObjectDied()
        {
            Material mat = GetComponent<MeshRenderer>().material;
            mat.color = DeadMat.color;            
        }

        public void ObjectLives()
        {
            Material mat = GetComponent<MeshRenderer>().material;
            mat.color = BaseMat.color;
        }
    }
}
