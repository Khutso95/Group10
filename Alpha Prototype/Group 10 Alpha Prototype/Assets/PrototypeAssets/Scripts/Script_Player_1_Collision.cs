using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Script_Player_1_Collision : MonoBehaviour
    {
        public bool GrabHold;

        [Tooltip("The name of the Reasource 1 tag")]
        public string R1;

        public float rayMaxDistance;

        public Vector3 offset;
        void Start()
        {

        }


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
                if (GrabHold && hit.collider.tag == R1)
                {
                    hit.collider.transform.parent = transform;
                   /* if (hit.collider.transform == "")
                    {

                    } */
                   
                  
                }
                else
                {
                    hit.collider.transform.parent = null;
                }
            }
        }

    

    }
}
