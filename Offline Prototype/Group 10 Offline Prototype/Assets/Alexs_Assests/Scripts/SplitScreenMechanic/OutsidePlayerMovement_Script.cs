using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho.SplitScreen
{

    public class OutsidePlayerMovement_Script : MonoBehaviour
    {

        #region Public Fields
        [Tooltip("Reference the character controller on the player here")]
        public CharacterController PlayerController;

        [Tooltip("This is the speed the player will run at")]
        public float _playerSpeed;
        [Tooltip("The base run speed the player is defaulted to")]
        public float _playerSpeedBase;
        [Tooltip("The rate at which the run speed with increase by over time")]
        public float _playerSpeedRate;
        [Tooltip("The maximun speed")]
        public float _playerSpeedMax;


        [Tooltip("This is the speed the player will turn at")]
        public float _turnSpeed;
        [Tooltip("The base turn speed the player is defaulted to")]
        public float _turnSpeedBase;
        [Tooltip("The rate at which the turn speed with increase by over time")]
        public float _turnSpeedRate;
        [Tooltip("The maximun turn speed the player can move at")]
        public float _turnSpeedMax;

        public bool isOutsidePlayer;
        public float _vertical;
        public float _horizontal;
        public GameObject GM;
        #endregion


        #region Movement Methods
        /// <summary>
        /// FixedUpdate to run the movement Controls
        /// </summary>
        void FixedUpdate()
        {
            DirectionControls();
            SpeedControls();
        }
        /// <summary>
        /// 1. Get the Vertical axis
        /// 2.Check if there is an input - if so slowly increase the value to create a ramp up of speed
        /// 3. if no input set it back down to the base
        /// 4.Move the player forward or backwards
        /// </summary>
        public void SpeedControls()
        {
            if (isOutsidePlayer)
            {
                _vertical = Input.GetAxisRaw("Vertical_OP");
            }
            else
            {
                _vertical = Input.GetAxisRaw("Vertical_IP");
            }
            

            if (_vertical != 0)
            {
                _playerSpeed += _playerSpeedRate * Time.deltaTime;
                if (_playerSpeed > _playerSpeedMax)
                {
                    _playerSpeed = _playerSpeedMax;
                }
            }
            else
            {
                _playerSpeed = _playerSpeedBase;
            }
            Vector3 _zMovement = transform.forward * _vertical;

            PlayerController.Move(_zMovement * _playerSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 1. Get the Horizontal axis
        /// 2.Check if there is an input - if so slowly increase the value to create a ramp up of speed
        /// 3. if no input set it back down to the base
        /// 4.Rotate the player in the desired directions
        /// </summary>
        public void DirectionControls()
        {
            if (isOutsidePlayer)
            {
                _horizontal = Input.GetAxisRaw("Horizontal_OP");
            }
            else
            {
                _horizontal = Input.GetAxisRaw("Horizontal_IP");
            }

            

            if (_horizontal != 0)
            {
                _turnSpeed += _turnSpeedRate * Time.deltaTime;
                if (_turnSpeed > _turnSpeedMax)
                {
                    _turnSpeed = _turnSpeedMax;
                }

            }
            else
            {

                _turnSpeed = _turnSpeedBase;
            }

            transform.Rotate(0f, _horizontal * _turnSpeed * Time.deltaTime, 0f);


        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Deliver")
            {
                GM.GetComponent<GM_Script>().IncreaseLeft();
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "Bottle")
            {
                GM.GetComponent<GM_Script>().IncreaseRight();
                Destroy(other.gameObject);
            }
        }
        
    }

}
