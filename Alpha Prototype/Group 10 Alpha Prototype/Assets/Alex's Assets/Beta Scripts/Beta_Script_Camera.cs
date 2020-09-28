using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Beta_Script_Camera : MonoBehaviour
    {
        public enum CamPos
        {
            AmmoRoom = 0,
            CenterRoom = 1,
            EngineRoom = 2,
            UpgradeRoom = 3
           
        }

        public void UpdateEnum(int EnumInt)
        {
             if(EnumInt == (int)CamPos.AmmoRoom)
            {
                UpdateCameraPos(CamPos.AmmoRoom);
            }

            if (EnumInt == (int)CamPos.CenterRoom)
            {
                UpdateCameraPos(CamPos.CenterRoom);
            }

            if (EnumInt == (int)CamPos.EngineRoom)
            {
                UpdateCameraPos(CamPos.EngineRoom);
            }

            if (EnumInt == (int)CamPos.UpgradeRoom)
            {
                UpdateCameraPos(CamPos.UpgradeRoom);
            }
        }

        //Central Cam: -32, 20.5, -45
        //Upgrade Cam: -11, 20.5, -45
        //Ammo Cam: -53, 20.5, -45
        //Engine Cam: -32, 20.5, -68.8
        public void UpdateCameraPos(CamPos curRoom)
        {

            switch (curRoom)
            {
                case CamPos.AmmoRoom:
                    
                    transform.position = new Vector3(-53f, 20.5f, -45f);
                    break;
                case CamPos.CenterRoom:
                    
                    transform.position = new Vector3(-32f, 20.5f, -45f);
                    break;
                case CamPos.EngineRoom:
                    
                    transform.position = new Vector3(-32f, 20.5f, -68.8f);
                    break;
                case CamPos.UpgradeRoom:
                    
                    transform.position = new Vector3(-11f, 20.5f, -45f);
                    break;
            }
        }
    }
}
