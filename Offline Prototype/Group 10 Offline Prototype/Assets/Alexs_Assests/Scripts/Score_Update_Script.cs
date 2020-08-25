using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Update_Script : MonoBehaviour
{
    [Tooltip("Refernec: The Text Element that will display the score")]
    public Text CurScoreText;


    private void Start()
    {
        if(CurScoreText == null)
        {
            Debug.LogError("Score_Update_Script: CurScoreText is missing a reference");
        }
    }
    void Update()
    {
        float CurScore = Alex.Carvalho.NameSpace.Player_PickUp_Script.bottlesCollected;
        CurScoreText.text = CurScore.ToString();
    }
}
