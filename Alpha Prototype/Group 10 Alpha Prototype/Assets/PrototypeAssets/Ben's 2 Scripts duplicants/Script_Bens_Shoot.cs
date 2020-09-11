using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bens_Shoot : MonoBehaviour
{

    public Transform _Cannon;          // the barrel obj.
    public Rigidbody _Projectile;      // the prefab 

    public GameObject GM;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && GM.GetComponent<Alex.Carvalho.Script_GM_RM>().P2CanShoot)
        {
            GM.GetComponent<Alex.Carvalho.Script_GM_RM>().isShooting();
            Rigidbody fireBullet;
            fireBullet = Instantiate(_Projectile, _Cannon.position, _Cannon.rotation) as Rigidbody;
            fireBullet.AddForce(_Cannon.forward * 2500);
        }
    }
}
