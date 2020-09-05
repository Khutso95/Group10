using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{

    public class Script_P1_Reasource_Entity : MonoBehaviour
    {
        public float TimeLimit;
        public float StartTime;
        public float MaxTime;

        public bool ResType1 = false;
        public bool ResType2 = false;
        public bool ResType3 = false;

        public bool IsOnSlot;
        public bool IsOnRefill;

        public string DrainReasourceSlot;
        public string FillReasourceSlot;

        public Material BaseMat;
        public Material DeadMat;

        public Image TimeBar;
        public Canvas TimeCanvas;

        void Start()
        {
            //Section to determine what type of Resource it is
            string child = this.transform.GetChild(0).name;  //Gets the name of the child highest in the hierachy should be the object "ofTypeX"
            if(child == "OfType1") { ResType1 = true; }else if(child == "OfType2") { ResType2 = true; }else if(child == "OfType3") { ResType3 = true; } else
            {
                Debug.LogError("Script_P1_Reasource_Entity Child object not OfTypeX");
            }

            TimeLimit = StartTime;
            TimeCanvas = GetComponentInChildren<Canvas>();
            TimeBar = GetComponentInChildren<Image>();
         
        }
        
        void CheckForErrors()
        {
            if (TimeBar == null) { Debug.LogError("Script_P1_Reasource_Entity: Missing TimeBar reference"); }
            if (BaseMat == null) { Debug.LogError("Script_P1_Reasource_Entity: Missing BaseMat reference"); }
            if (DeadMat == null) { Debug.LogError("Script_P1_Reasource_Entity: Missing DeadMat reference"); }
            if (DrainReasourceSlot == " ") { Debug.LogError("Script_P1_Reasource_Entity: ReasourceSlot feild is empty"); }
          
        }
        void Update()
        {
            DetectOnPoint();
            ReasourceManagement();
        }


        public void DetectOnPoint()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
                
            }

            if(this.transform.parent != null ) //checks if it is has a parent = being moved by the player
            {
                IsOnSlot = false;
                
            }
            else
            {
                if(hit.transform.tag == DrainReasourceSlot)
                {
                    IsOnSlot = true;
                }
                else
                {
                    IsOnSlot = false;
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
            if (TimeLimit >= MaxTime)
            {
                TimeLimit = MaxTime;
            }

            if (IsOnSlot)
            {
                TimeLimit -= Time.deltaTime;

                float ImageFill = TimeLimit / MaxTime;
                TimeBar.fillAmount = ImageFill;
                if (TimeLimit <= 0)
                {
                    ObjectDied();
                }
            }
            else if (IsOnRefill)
            {
                TimeLimit += Time.deltaTime;
                float ImageFill = TimeLimit / MaxTime;
                TimeBar.fillAmount = ImageFill;
                if(TimeLimit >= 0)
                {
                    ObjectLives();
                }
            }
            {
                return;
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
