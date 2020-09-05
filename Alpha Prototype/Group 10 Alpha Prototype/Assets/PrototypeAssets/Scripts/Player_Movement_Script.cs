using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Player_Movement_Script : MonoBehaviour
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
            float _vertical = Input.GetAxisRaw("Player 2 Vertical");

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
            
            float _horizontal = Input.GetAxisRaw("Player 2 Horizontal");
            
            if(_horizontal != 0)
            {
                _turnSpeed += _turnSpeedRate * Time.deltaTime;
                if(_turnSpeed > _turnSpeedMax)
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
    }

}
