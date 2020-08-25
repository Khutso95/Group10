using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho.NameSpace
{
    public class Player_PickUp_Script : MonoBehaviour
    {

        #region public fields
        public string bottleTag;

        public float bottlesCollected;
        #endregion
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerStay(Collider other)
        {
            if(other.gameObject.tag == bottleTag && Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(other.gameObject);

                bottlesCollected += 1;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == bottleTag)
            {
                Debug.Log("Player_PickUp_Script : OntriggerEnter Collided with Bottle");
            }
        }
    }
}
