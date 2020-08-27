using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho.NameSpace
{
    public class Balance_Movement_Script : MonoBehaviour
    {

        #region public field Speed
        public float OverallSpeed;
        public float SpeedDecreaseRate;
        public float SpeedIncreaseRate;
        #endregion

        #region public field Direction
        public float RotSpeed;
        public float RotSpeedBase;
        public float RotIncreaseRate;
        #endregion

        #region private field direction
        CharacterController playerController;
        public static bool leftStepTaken = true;
        public static bool rightStepTaken = true;

        public static bool RotLeft = false;
        public static bool RotRight = false;
        #endregion

        #region private field Balance
        public static float Balance;
        public float testBalance;

        public float maxSpeed;
        public float maxRotation;
        #endregion

        #region Movement Methods

        void Start()
        {
            playerController = GetComponent<CharacterController>();
            if(playerController == null)
            {
                Debug.LogError("Balance_Movement_Script : playerController returned Null, missing Character controller");
            }
        }


        void Update()
        {
            SpeedInput();
            DirectionInput();
            BalancePercentage();
           
        }

        public void SpeedInput()
        {
            if (rightStepTaken)
            {
                if (Input.GetKeyDown(KeyCode.Q) && !Input.GetKey(KeyCode.E))
                {
                    leftStepTaken = true;
                    rightStepTaken = false;
                    OverallSpeed = OverallSpeed + SpeedIncreaseRate;
                }
            }

            if (leftStepTaken)
            {
                if (Input.GetKeyDown(KeyCode.E) && !Input.GetKey(KeyCode.Q))
                {
                    leftStepTaken = false;
                    rightStepTaken = true;
                    OverallSpeed = OverallSpeed + SpeedIncreaseRate;
                }
            }

            OverallSpeed -= SpeedDecreaseRate * Time.deltaTime;
            if(OverallSpeed <= 0) { OverallSpeed = 0;}
          
            
            Vector3 _zMovement = transform.forward;

            playerController.Move(_zMovement * OverallSpeed * Time.deltaTime);
        }

        public void DirectionInput()
        {

            float _lean = Input.GetAxis("Horizontal");

            if(_lean != 0)
            {
                RotSpeed = RotSpeed + RotIncreaseRate * Time.deltaTime;


                if (_lean > 0)
                {
                    RotLeft = false;
                    RotRight = true;
                }
                else if(_lean < 0)
                {
                    RotLeft = true;
                    RotRight = false;
                }
            }
            else
            {
                RotSpeed = RotSpeedBase;
                RotLeft = false;
                RotRight = false;
            }




            transform.Rotate(0f, _lean * RotSpeed * Time.deltaTime, 0f);
        }
        #endregion

        #region Balance Methods

        public void BalancePercentage()
        {
            Balance = (((OverallSpeed / maxSpeed) * 100) + ((RotSpeed / maxRotation) * 100)) / 2;
            testBalance = Balance;
        }
        #endregion
    }
}
