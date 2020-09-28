using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Beta_Script_P1_Collision : MonoBehaviour
    {
        #region public feilds
        public bool GrabHold;
        public bool Holding;

        [Tooltip("The name of the Reasource tag")]
        public string Reasource;

        [Tooltip("The name of the Cell tag")]
        public string ContainerCell;

        public float rayMaxDistance;

        public Vector3 offset;

        //Scene Transitions
        public GameObject Camera;
        public string AmmoRoom;
        public string CenterRoom;
        public string EngineRoom;
        public string UpgradeRoom;
       
        #endregion

        void Update()
        {
            CheckRoom();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GrabHold = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GrabHold = false;
            }

            Vector3 RayStartingPoint = transform.position - offset;
            RaycastHit hit;
            if (Physics.Raycast(RayStartingPoint, transform.TransformDirection(Vector3.forward), out hit, rayMaxDistance))
            {
                Debug.DrawRay(RayStartingPoint, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

                if (GrabHold && hit.collider.tag == Reasource || GrabHold && hit.collider.tag == ContainerCell)
                {
                    if (!Holding)
                    {
                        hit.collider.transform.parent = transform;
                        Holding = true;
                    }
                }
                else
                {
                    Holding = false;
                    hit.collider.transform.parent = null;
                }
            }
        }

        public void CheckRoom()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
               
                if (hit.collider.name == AmmoRoom)
                {
                    Camera.GetComponent<Beta_Script_Camera>().UpdateEnum(0);
                }
                else if(hit.collider.name == CenterRoom )
                {
                    Camera.GetComponent<Beta_Script_Camera>().UpdateEnum(1);
                }
                else if (hit.collider.name == EngineRoom)
                {
                    Camera.GetComponent<Beta_Script_Camera>().UpdateEnum(2);
                }
                else if (hit.collider.name == UpgradeRoom)
                {
                    Camera.GetComponent<Beta_Script_Camera>().UpdateEnum(3);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
