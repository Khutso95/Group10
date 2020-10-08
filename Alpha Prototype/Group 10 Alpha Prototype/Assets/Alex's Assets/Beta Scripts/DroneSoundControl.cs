using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSoundControl : MonoBehaviour
{
    public AudioClip music1;
    public AudioClip music2;
    public AudioSource source;
    public GameObject SoundHolder;
    public GameObject turnSoundHolder;
    public GameObject GameManager;
    public float timer;
    public float timer2;

    bool startCount = false;
    bool startCountAgain = false;

    CharacterController _mover;
    void Start()
    {

        GameManager = GameObject.FindGameObjectWithTag("GameController");
        
        AudioManager.Instance.PlayMusic(music1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.GetComponent<Alex.Carvalho.Beta_Script_GameManager>().CanMove)
        {
            //**********************************//
            //Adjust Pitch
            if ((Input.GetButtonDown("Vertical")))
            {

                SoundHolder.SetActive(true);
                startCountAgain = false;
                startCount = true;

            }
            if (startCount == true)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;
                }
                if (timer > 6f & timer < 8f)
                {
                    source.pitch = 0.7f;
                }
                if (timer > 4f & timer < 6f)
                {
                    source.pitch = 0.7f;
                }
                if (timer > 2f & timer < 4f)
                {
                    source.pitch = 0.8f;
                }
                if (timer > 2f & timer < 4f)
                {
                    source.pitch = 0.9f;
                }
                if (timer <= 0)
                {
                    source.pitch = 1f;
                    startCount = false;
                    timer = 5f;

                }
            }
            Debug.Log(timer);
            //**********************************//

            //***********************************//
            //decelerate//
            if (Input.GetButtonUp("Vertical"))
            {
                startCountAgain = true;
                startCount = false;

            }

            if (startCountAgain == true)
            {
                timer2 -= Time.deltaTime;

                if (timer2 <= 2 & timer2 > 1)
                    source.pitch = 0.8f;
                if (timer2 < 1f)
                    source.pitch = 0.5f;
                if (timer2 <= 0)
                {
                    timer2 = 0f;
                    SoundHolder.SetActive(false);
                    startCountAgain = false;
                    timer2 = 2f;
                }
            }
            //turn left and right//
            if ((Input.GetButtonDown("Horizontal")))
            {
                turnSoundHolder.SetActive(true);
            }
            if ((Input.GetButtonUp("Horizontal")))
            {
                turnSoundHolder.SetActive(false);
            }
        }

     }
 

  
}
