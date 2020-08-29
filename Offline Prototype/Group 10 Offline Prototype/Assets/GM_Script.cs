using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_Script : MonoBehaviour
{
    public float InsidePlayerFuel;
    public float OutsidePlayerFuel;

    public float pickUpYellow;
    public float pickUpGreen;

    public Image LeftUI;
    public Image RightUI;

    public float decreaseRate;
    public float increaseRate;

    // Update is called once per frame
    void Update()
    {
        LeftUI.fillAmount = InsidePlayerFuel / 100;
        RightUI.fillAmount = OutsidePlayerFuel / 100;

        InsidePlayerFuel -= decreaseRate * Time.deltaTime;
        OutsidePlayerFuel -= decreaseRate * Time.deltaTime;
    }

    public void IncreaseLeft()
    {
        InsidePlayerFuel += increaseRate;

    }

    public void IncreaseRight()
    {
        OutsidePlayerFuel += increaseRate;
       
    }
}
