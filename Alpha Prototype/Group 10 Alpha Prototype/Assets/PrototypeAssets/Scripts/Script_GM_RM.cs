using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        #endregion

        #region gameManager Variables;
        public float ResType_1;
        public float ResType_2;
        public float RestType_3;

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

            //temperary Code
            if (Input.GetKeyDown(KeyCode.P))
            {
                string name = "OfType1";
                SpawnReasources(name);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                string name = "OfType2";
                SpawnReasources(name);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                string name = "OfType3";
                SpawnReasources(name);
            }
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
                RestType_3 += Time.deltaTime;
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
        #endregion
    }
}
