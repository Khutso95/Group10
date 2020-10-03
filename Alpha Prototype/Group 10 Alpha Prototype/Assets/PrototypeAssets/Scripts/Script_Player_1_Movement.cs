using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Script_Player_1_Movement : MonoBehaviour
    {
        #region public fields
        [Tooltip("Player gameobject require chracter controller, insert character controller reference")]
        public CharacterController PlayerController;

        public float PlayerSpeed;

        public float RotationSpeed;

        #endregion
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MoveThePlayer();
        }

        public void MoveThePlayer()
        {
            float _P1Horizontal = Input.GetAxisRaw("Player 1 Horizontal");
            float _P1Vertical = Input.GetAxisRaw("Player 1 Vertical");
            Vector3 movement = new Vector3(_P1Horizontal, 0f, _P1Vertical);

            if(movement != Vector3.zero)
            {

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * RotationSpeed);
                
            }
            
           
            PlayerController.Move(movement * PlayerSpeed * Time.deltaTime);     
        }
    }
}
