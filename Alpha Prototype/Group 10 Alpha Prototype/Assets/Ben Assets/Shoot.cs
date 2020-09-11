using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform _Cannon;          // the barrel obj.
    public Rigidbody _Projectile;      // the prefab 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))   
        {
            Rigidbody fireBullet;    
            fireBullet = Instantiate(_Projectile, _Cannon.position, _Cannon.rotation) as Rigidbody;                                                                                       
            fireBullet.AddForce(_Cannon.forward * 2500);    
        }                                             
    }
}
