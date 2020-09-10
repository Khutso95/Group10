using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Script_Player_1_Collision : MonoBehaviour
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
        #endregion

        void Update()
        {
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

    

    }
}
