using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho.NameSpace
{
    public class GameManger_Script : MonoBehaviour
    {

        #region Ui Elements
        public Image LeftImage;
        public Image RightImage;

        public Image Rot_LeftImage;
        public Image Rot_RightImage;

        public Image Fill_Bar;
        #endregion


        #region Public Methods

        public void UpdateWalkUIElement()
        {
            bool leftStep = Alex.Carvalho.NameSpace.Balance_Movement_Script.leftStepTaken;
            bool rightStep = Alex.Carvalho.NameSpace.Balance_Movement_Script.rightStepTaken;
            if (rightStep)
            {
                LeftImage.enabled = true;
                RightImage.enabled = false;
            }
            else
            {
                LeftImage.enabled = false;
                RightImage.enabled = true;
            }

            bool turnLeft = Alex.Carvalho.NameSpace.Balance_Movement_Script.RotLeft;
            bool turnRight = Alex.Carvalho.NameSpace.Balance_Movement_Script.RotRight;

            if (turnRight)
            {
                Rot_RightImage.enabled = true;
                Rot_LeftImage.enabled = false;
            }
            else if(turnLeft)
            {
                Rot_RightImage.enabled = false;
                Rot_LeftImage.enabled = true;
            }
            else
            {
                Rot_RightImage.enabled = false;
                Rot_LeftImage.enabled = false;
            }

            float balancePercentange = Alex.Carvalho.NameSpace.Balance_Movement_Script.Balance;
            Fill_Bar.fillAmount = balancePercentange / 100;
        }
        #endregion

        // Update is called once per frame
        void Update()
        {
            UpdateWalkUIElement();
            
        }
    }
}
