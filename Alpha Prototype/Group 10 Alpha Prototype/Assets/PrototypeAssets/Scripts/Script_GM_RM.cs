using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{ 
    public class Script_GM_RM : MonoBehaviour
    {
        #region player 1 variables
        public string InputName;
        public string DepositName;

        public GameObject Cell_1;
        public float Cell_1_Amount;
        public GameObject Cell_2;
        public float Cell_2_Amount;
        public GameObject Cell_3;
        public float Cell_3_Amount;

        private float tempTimer = 0f;

        #endregion

        #region player 2 variables
        public bool P2CanShoot;
        public bool P2CanMove;
        #endregion

        #region gameManager Variables;
        public float ResType_1;       //Movement
        public float ResType_1_Max;
        public float ResType_1_DecreaseRate;
        public float ResType_2;      
        public float ResType_2_Max;
        public float ResType_3;       //Shooting
        public float ResType_3_Max;

        public Image Res_1_Image;
        public Image Res_2_Image;
        public Image Res_3_Image;

        public GameObject ResType_1_Prefab;
        public GameObject ResType_2_Prefab;
        public GameObject ResType_3_Prefab;
        public GameObject ResType_1_Spawn;
        public GameObject ResType_2_Spawn;
        public GameObject ResType_3_Spawn;
        #endregion

        void Update()
        {
            CheckToRefill();
            UpdateReasourcesFromP1();
            UIupdate();
            CheckP2Actions();
            GeneralManagement();
        }
    
        #region  P1-Methods

        public void UpdateReasourcesFromP1()//Updates the value in the GM about what the vlalue in the Cell is
        {
            Cell_1_Amount = Cell_1.GetComponent<Script_P1_Reasource_Entity>().ReasourceAmount;
            Cell_2_Amount = Cell_2.GetComponent<Script_P1_Reasource_Entity>().ReasourceAmount;
            Cell_3_Amount = Cell_3.GetComponent<Script_P1_Reasource_Entity>().ReasourceAmount;
        }

        public void CheckToRefill()
        {
            //This section resets the GM text feild to check if both pieces are in place to refil
            tempTimer -= Time.deltaTime;
            if (tempTimer <= 0)
            {
                tempTimer = 0.5f;
                InputName = "";
                DepositName = "";
            }

            //this section makes a comparison of the two child object names it has recieved. if they match then that means the reasource matches and can fill up the cell 
            if (InputName != "" && DepositName != "")
            {
                if (DepositName == InputName)
                {
                    RefillCell();
                }
            }
        }
     
        public void CrystalDeposit(string name)
        {
           
            DepositName = name;

        }
        public void CrystalInput(string name)
        {
            
            InputName = name;

        }
        #endregion

        #region P1-ReasourceManagement
        public void RefillCell() //This gets the the specific cell it needs to refill and tella that one to refill
        {
            if (InputName == "OfType1")
            {
                Cell_1.GetComponent<Script_P1_Reasource_Entity>().RefilCell();
            }
            else if(InputName == "OfType2")
            {
                Cell_2.GetComponent<Script_P1_Reasource_Entity>().RefilCell();
            }
            else if(InputName == "OfType3")
            {
                Cell_3.GetComponent<Script_P1_Reasource_Entity>().RefilCell();
            }
            else
            {
                Debug.LogError("Script_GM_RM: RefillCell - did not find any InputName matching given paramaters");
            }
        }
        #endregion

        #region GM-Methods
        public void IncreaseLocalRes(string name)
        {
            if (name == "OfType1")
            {
                ResType_1 += Time.deltaTime;
            }
            else if (name == "OfType2")
            {
                ResType_2 += Time.deltaTime;
            }
            else if (name == "OfType3")
            {
                ResType_3 += Time.deltaTime;
            }
            else
            {
                Debug.LogError("Script_GM_RM: IncreaseLocalRes - did not find any Childname matching given paramaters");
            }
        }

        public void SpawnReasources(string name)
        {
            if (name == "OfType1")
            {
                Instantiate(ResType_1_Prefab, ResType_1_Spawn.transform.position, Quaternion.identity);
            }
            else if (name == "OfType2")
            {
                Instantiate(ResType_2_Prefab, ResType_2_Spawn.transform.position, Quaternion.identity);
            }
            else if (name == "OfType3")
            {
                Instantiate(ResType_3_Prefab, ResType_3_Spawn.transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Script_GM_RM: SpawnReasources - did not find any Childname matching given paramaters");
            }
        }

        public void GeneralManagement()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public void UIupdate()
        {
            if(ResType_1 >= ResType_1_Max)
            {
                ResType_1 = ResType_1_Max;
            }

            if(ResType_2 >= ResType_2_Max)
            {
                ResType_2 = ResType_2_Max;
            }

            if(ResType_3 >= ResType_3_Max)
            {
                ResType_3 = ResType_3_Max;
            }



            float fill_1 = ResType_1 / ResType_1_Max;
            float fill_2 = ResType_2 / ResType_2_Max;
            float fill_3 = ResType_3 / ResType_3_Max;
            Res_1_Image.fillAmount = fill_1;
            Res_2_Image.fillAmount = fill_2;
            Res_3_Image.fillAmount = fill_3;
        }
        #endregion

        #region P2-Methods
        public void CheckP2Actions()
        {
            if(ResType_3 <= 0)
            {
                P2CanShoot = false;
            }
            else
            {
                P2CanShoot = true;
            }

            if(ResType_1 <= 0)
            {
                P2CanMove = false;
               
            }
            else
            {
                P2CanMove = true;
            }
        }
        public void isShooting()
        {
            ResType_3 = ResType_3 - 0.5f;
        }

        public void isMoving()
        {
            ResType_1 -= ResType_1_DecreaseRate * Time.deltaTime;
        }
        #endregion
    }
}
