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
        public Vector3 P1SpawnOffSett;
        #endregion

        #region For P1ayer 2
        public int Type1ListSize;
        public List<GameObject> Type1ActiveResource = new List<GameObject>();
        public int Type2ListSize;
        public List<GameObject> Type2ActiveResource = new List<GameObject>();
        public int Type3ListSize;
        public List<GameObject> Type3ActiveResource = new List<GameObject>();
        private List<GameObject> TempDisabledObject = new List<GameObject>();


        #endregion
        #endregion

        #region Resource Variables
        //The resources affected 
        public float Type_1_Res;  //Fuel
        public float Type_2_Res;  //Ammo
        public float Type_3_Res;  //Health
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
        //Variables relating to the P2 Actions
        public bool CanShoot;
        public bool CanMove;
        #endregion

        #region Upgrade Variables
        public bool UpgradedSpeed;
        public bool UpgradedDamage;
        private float _ResetTimer;
        #endregion

        #region Ui Elements
        public Image P2_Fuel_Bar;
        public Image P2_Ammo_Bar;
        public Image Health_Bar;
        #endregion

     
        void Start()
        {
            _ResetTimer = 1f;
        }

 
        void Update()
        {
            CapResources();
            UpdateP2UI();
            CheckResources();
            CheckUpgradeSense();
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

            if(Type_3_Res >= Type_3_Res_Max)
            {
                Type_3_Res = Type_3_Res_Max;
            }
            if(Type_1_Res <= 0)
            {
                Type_1_Res = 0;
            }

            if(Type_2_Res <= 0)
            {
                Type_2_Res = 0;
            }
            
            if(Type_3_Res <= 0)
            {
                Type_3_Res = 0;
            }
        }

        public void CheckResources()
        {
            if(Type_1_Res <= 0)
            {
                CanMove = false;
            }
            else
            {
                CanMove = true;
            }

            if(Type_2_Res <= 0)
            {
                CanShoot = false;
            }
            else
            {
                CanShoot = true;
            }

        }

        public void CheckUpgradeSense()
        {
            _ResetTimer -= Time.deltaTime;
            if(_ResetTimer <= 0)
            {
                UpgradedSpeed = false;
                UpgradedDamage = false;
                _ResetTimer = 1f;
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
            Type_2_Res -= Type_2_Dec_Rate;
        }

        public void DecreaseHealth(int EnumValue)
        {
           
            if(EnumValue == 1)
            {
                Type_3_Res -= Type_3_Dec_Rate;
            }
            else if(EnumValue == 2)
            {
                Type_3_Res -= Type_3_Dec_Rate * 2;
            }
            else
            {
                Debug.Log("DecreaseHealth EnumValue was not 1 or 2");
            }
        }

        public void PowerUp(int upgradeType)
        {

            if(upgradeType == 1)
            {
                UpgradedSpeed = true;
               
            }
            else if (upgradeType == 2)
            {
                UpgradedDamage = true;
               
            }
            else if (upgradeType == 3)
            {
                Type_3_Res += Type_3_Inc_Rate * Time.deltaTime;
            }
            
        }
        #endregion

        #region SpawningResources
        #region Player 1 Enviroment
        public void SpawnResourceP1(int WorldResourceInt)
        {
            if (WorldResourceInt == (int)ResourceType.ofType1)
            {
                Instantiate(Raw_Res_Type_1, P1SpawnLocation.position + P1SpawnOffSett, Quaternion.identity);
            }
            else if (WorldResourceInt == (int)ResourceType.ofType2)
            {
                Instantiate(Raw_Res_Type_2, P1SpawnLocation.position + P1SpawnOffSett, Quaternion.identity);
            }
            else if (WorldResourceInt == (int)ResourceType.ofType3)
            {
                Instantiate(Raw_Res_Type_3, P1SpawnLocation.position + P1SpawnOffSett, Quaternion.identity);
            }
            else
            {
                Debug.Log("Spawning Resouce did not find a match for the resource type");
            }
        }
        #endregion

        #region Player 2 Enviroment

        public void UpdateTypeLists()
        {
            int TempListLength = TempDisabledObject.Count;
            int TempRandomNum = Random.Range(0, TempListLength); 
            TempDisabledObject[TempRandomNum].SetActive(true);
            TempDisabledObject.Clear();
        }

        public void SpawnResourceP2(int WorldResourceInt, Transform _transform)
        {
            if(WorldResourceInt == (int)ResourceType.ofType1)
            {                
                for (int i = 0; i < Type1ListSize; i++)
                {
                    if(Type1ActiveResource[i].transform.position != _transform.position)
                    {
                        if(Type1ActiveResource[i].activeInHierarchy == false)
                        {
                            TempDisabledObject.Add(Type1ActiveResource[i]);
                        }
                        
                    }

                    if(i == Type1ListSize - 1)
                    {
                        
                        UpdateTypeLists();
                    }
                }
            }
            else if(WorldResourceInt == (int)ResourceType.ofType2)
            {
                 for (int i = 0; i < Type2ListSize; i++)
                {
                    if(Type2ActiveResource[i].transform.position != _transform.position)
                    {
                        if(Type2ActiveResource[i].activeInHierarchy == false)
                        {
                            TempDisabledObject.Add(Type2ActiveResource[i]);
                        }
                        
                    }

                    if(i == Type2ListSize - 1)
                    {
                        
                        UpdateTypeLists();
                    }
                }
            }
            else if(WorldResourceInt == (int)ResourceType.ofType3)
            {
                for (int i = 0; i < Type3ListSize; i++)
                {
                    if (Type3ActiveResource[i].transform.position != _transform.position)
                    {
                        if (Type3ActiveResource[i].activeInHierarchy == false)
                        {
                            TempDisabledObject.Add(Type3ActiveResource[i]);
                        }

                    }

                    if (i == Type3ListSize - 1)
                    {

                        UpdateTypeLists();
                    }
                }
            }
        }


        #endregion
        #endregion

        #region Ui Methods
        public void UpdateP2UI()
        {
            P2_Fuel_Bar.fillAmount = Type_1_Res / Type_1_Res_Max;
            P2_Ammo_Bar.fillAmount = Type_2_Res / Type_2_Res_Max;
            Health_Bar.fillAmount = Type_3_Res / Type_3_Res_Max;
        }
        #endregion
    }
}
