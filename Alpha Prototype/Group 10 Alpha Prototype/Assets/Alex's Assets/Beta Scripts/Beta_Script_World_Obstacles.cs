using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{

    public class Beta_Script_World_Obstacles : MonoBehaviour
    {
        public enum ChallengeType
        {
            Not = 0,
            Obstacle = 1,
            Enemy = 2
        }
        public ChallengeType _challengeType;

        public float Health;
        public float moveIntervals;
        public float moveSpeed;

        public Vector3 StartPos;
        public Vector3 TargetPos;

        public GameObject gun;
        
        void Start()
        {
            gun = GameObject.Find("Gun Script Holder");
            moveIntervals = 5f;
            StartPos = transform.localPosition;
            TargetPos = transform.localPosition;
        }

       
        void Update()
        {
            if(_challengeType == ChallengeType.Enemy)
            {
                EnemeyMovement();
            }

            if(Health <=0)
            {
                Destroy(gameObject);
            }
        }

        public void EnemeyMovement()
        {
            moveIntervals -= Time.deltaTime;
            if(moveIntervals <= 0)
            {
               
                float newZPos = Random.Range(StartPos.z - 5f, StartPos.z + 5f);
                float newXPos = Random.Range(StartPos.x - 5f, StartPos.x + 5f);
                TargetPos = new Vector3(newXPos, transform.localPosition.y, newZPos);
                /*
                Vector3 targetDirection = TargetPos - transform.position;
                float angle = Vector3.Angle(targetDirection, transform.forward);
                
                transform.rotation = Quaternion.LookRotation(new Vector3(0, angle, 0)); */
                
                moveIntervals = 5f;

            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPos, moveSpeed);
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_challengeType == ChallengeType.Enemy)
            {
                if (other.gameObject.tag == "Projectile")
                {
                    float Damage = gun.GetComponent<Script_Bens_Shoot>()._bulletDamage;
                    Health -= Damage;
                }
            }
          
        }

       
    }
}
