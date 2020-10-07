using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex.Carvalho
{
    public class Beta_Script_MiniMap_Cam : MonoBehaviour
    {
        public Transform player2;

        private void LateUpdate()
        {
            Vector3 newPos = player2.position;
            newPos.y = transform.position.y;
            transform.position = newPos;

            transform.rotation = Quaternion.Euler(90f, player2.eulerAngles.y, 0f);
        }
    }
}
