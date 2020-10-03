using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bens_Shoot : MonoBehaviour
{

    public Transform _Cannon;          // the barrel obj.
    public Rigidbody _Projectile;      // the prefab 

    public GameObject GameManager;


    public void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameController");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && GameManager.GetComponent<Alex.Carvalho.Beta_Script_GameManager>().CanShoot)
        {
            GameManager.GetComponent<Alex.Carvalho.Beta_Script_GameManager>().DecreaseAmmo();
            Rigidbody fireBullet;
            fireBullet = Instantiate(_Projectile, _Cannon.position, _Cannon.rotation) as Rigidbody;
            fireBullet.AddForce(_Cannon.forward * 2500);
        }
    }
}
