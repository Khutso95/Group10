using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{
    public class Beta_Script_GameManager : MonoBehaviour
    {
        #region Spawning Prefabs Variables

         #region For P1ayer 1
        //The Raw Resource prefabs
        public enum ResourceType
        {
            ofType1 = 0,
            ofType2 = 1,
            ofType3 = 2
        }

        public GameObject Raw_Res_Type_1;
        public GameObject Raw_Res_Type_2;
        public GameObject Raw_Res_Type_3;
        public Transform P1SpawnLocation;
        #endregion

        #region For P1ayer 2
        #endregion
        #endregion

        #region Resource Variables
        //The resources affected 
        public float Type_1_Res;  //Fuel
        public float Type_2_Res;  //Ammo
        public float Type_3_Res;
        //The resources Max
        public float Type_1_Res_Max;
        public float Type_2_Res_Max;
        public float Type_3_Res_Max;
        //The resources Decrease Rate
        public float Type_1_Dec_Rate;
        public float Type_2_Dec_Rate;
        public float Type_3_Dec_Rate;
        //The Resources Increase Rate
        public float Type_1_Inc_Rate;
        public float Type_2_Inc_Rate;
        public float Type_3_Inc_Rate;
        #endregion

        #region Ui Elements
        public Image P2_Fuel_Bar;
        public Image P2_Ammo_Bar;
        #endregion

     
        void Start()
        {

        }

 
        void Update()
        {
            CapResources();
            UpdateP2UI();
        }

        #region Resource Methods
        public void CapResources()
        {
            if(Type_1_Res >= Type_1_Res_Max)
            {
                Type_1_Res = Type_1_Res_Max;
            }

            if(Type_2_Res >= Type_2_Res_Max)
            {
                Type_2_Res = Type_2_Res_Max;
            }

        }

        public void IncreaseFuel()
        {
            Type_1_Res += Type_1_Inc_Rate * Time.deltaTime;
        }

        public void IncreaseAmmo()
        {
            Type_2_Res += Type_2_Inc_Rate * Time.deltaTime;
        }

        public void DecreaseFuel()
        {
            Type_1_Res -= Type_1_Dec_Rate * Time.deltaTime;
        }

        public void DecreaseAmmo()
        {
            Type_2_Res -= Type_2_Dec_Rate * Time.deltaTime;
        }

        public void PowerUp()
        {
            Debug.Log("Power Up");
        }
        #endregion

        #region SpawningResources


        #region Player 1 Enviroment
        public void SpawnResourceP1(int WorldResourceInt)
        {
            if (WorldResourceInt == (int)ResourceType.ofType1)
            {
                Instantiate(Raw_Res_Type_1, P1SpawnLocation.position, Quaternion.identity);
            }
            else if (WorldResourceInt == (int)ResourceType.ofType2)
            {
                Instantiate(Raw_Res_Type_2, P1SpawnLocation.position, Quaternion.identity);
            }
            else if (WorldResourceInt == (int)ResourceType.ofType3)
            {
                Instantiate(Raw_Res_Type_3, P1SpawnLocation.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Spawning Resouce did not find a match for the resource type");
            }
        }
        #endregion

        #region Player 2 Enviroment
        public void SpawnResourceP2(int WorldResourceInt)
        {
            
        }

        #endregion
        #endregion

        #region Ui Methods
        public void UpdateP2UI()
        {
            P2_Fuel_Bar.fillAmount = Type_1_Res / Type_1_Res_Max;
            P2_Ammo_Bar.fillAmount = Type_2_Res / Type_2_Res_Max;
        }
        #endregion
    }
}
