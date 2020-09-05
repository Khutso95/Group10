using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Player_1_Reasource_Point : MonoBehaviour
{
    public Canvas ReasourceCanvas;
    public Image ReasourceBar;

    public float LocalAmount;
    public float LocalMaxAmount;
    public float LocalPercentageAmount;

    public bool hasReasource1;
    public bool hasReasource2;
    public bool hasReasource3;


    public string BaseResName;
    public string Res1Name;
    
    void Start()
    {
        
        
    }

    void Update()
    {
        decreaseRate();
        
    }

    void decreaseRate()
    {

    }

   
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.transform.parent != null)
        {
            Debug.Log("Null");
            return;

        }
        else
        {
            if(other.gameObject.transform.tag == BaseResName)
            {
                ReasourceCanvas.enabled = true;
                Debug.Log("Hit");
                
                    
            }
            else
            {
                Debug.LogError("Missing: Script_Player_1_Reasource_Point  OnTriggerStay, other.tag != BaseResName");
                Debug.Log(other.gameObject.transform.tag);

            }
        }
    }
}
