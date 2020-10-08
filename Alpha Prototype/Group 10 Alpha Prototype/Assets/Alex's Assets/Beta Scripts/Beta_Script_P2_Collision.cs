using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Beta_Script_P2_Collision : MonoBehaviour
    {
        public GameObject GameManger;
        public AudioClip MetalicSound;
        public AudioClip MetalicSound2;

        void Start()
        {
            GameManger = GameObject.FindGameObjectWithTag("GameController");
        }

        
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            var EnumValue = other.GetComponent<Beta_Script_World_Resource>()._worldResource;
            GameManger.GetComponent<Beta_Script_GameManager>().SpawnResourceP1((int)EnumValue);
            GameManger.GetComponent<Beta_Script_GameManager>().SpawnResourceP2((int)EnumValue, other.transform);
            other.gameObject.SetActive(false);

            AudioManager.Instance.PlayEffects(MetalicSound,0.5f);
            
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            hit.collider.tag = "Road";
            //AudioManager.Instance.PlayEffects(MetalicSound2, 0.05f);
        }
    }
}
