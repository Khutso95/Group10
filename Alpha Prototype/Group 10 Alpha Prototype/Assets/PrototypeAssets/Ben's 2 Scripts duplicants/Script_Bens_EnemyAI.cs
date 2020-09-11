using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Script_Bens_EnemyAI : MonoBehaviour
{

    Transform tr_Player;
    public float f_RotSpeed = 4f, f_MoveSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        tr_Player = GameObject.FindGameObjectWithTag("Player 2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(tr_Player.position - transform.position), f_RotSpeed * Time.deltaTime);
        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.name == "Projectile(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
