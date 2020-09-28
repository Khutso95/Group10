using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex.Carvalho
{
    public class Beta_Script_Crafting : MonoBehaviour
    {
        #region Variables realated to the identity of the crafter
        public enum CraftingType
        {
            Ammo = 0,
            Fuel = 1,
            Upgrade = 2
        }

        public CraftingType _craftingType;

        [Tooltip("The prefab of the Raw Reasource for the type you want the crafter to recieve")]
        public GameObject RawReasource;
        [Tooltip("The prefab of the Refined Reasource for the type you want the crafter to produce")]
        public GameObject RefinedReasource;
        #endregion

        #region variables realted to crafting
        [Tooltip("The amount of time raw reasources needs to be fed into the crafter to make a refined resource")]
        public float _craftingTime;
        [Tooltip("The amount a new reasource has been crafted")]
        public float _craftingAmount;
        [Tooltip("The rate at which the crafting takes place at")]
        public float _craftingRate;
        [Tooltip("The position of the output of the crafter")]
        public Transform CraftingOuputPos;
        [Tooltip("The name of the child object of the raw reasource designed for this space")]
        public string RawReasourceType;
        #endregion

        #region Variables for the Ui
        public Image CompletionBar;
        #endregion

        private void Start()
        {
            CompletionBar = GetComponentInChildren<Image>();
        }

        private void Update()
        {
            UpdateUI();
        }

        #region Crafting the reasource method          

        public void CraftingRefinedResource(string childName)
        {
            if(childName == RawReasourceType)
            {
                //Increasing the crafting Time
                _craftingAmount += _craftingRate * Time.deltaTime;
               
                //Spawn the reasource
                if (_craftingAmount >= _craftingTime)
                {
                    Instantiate(RefinedReasource, CraftingOuputPos.position, Quaternion.identity);
                    //Reset the crafting time
                    _craftingAmount = 0;
                }
            }
           
        }
        #endregion

        #region Update the UI Region

        public void UpdateUI()
        {
            CompletionBar.fillAmount = _craftingAmount / _craftingTime;
        }
        #endregion
    }
}
