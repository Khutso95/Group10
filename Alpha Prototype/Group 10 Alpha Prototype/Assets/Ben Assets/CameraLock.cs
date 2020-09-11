using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform player;
    private float heading = 0;
    public float tilt = 30;
    public float camDist = 8.5f;
    public float playerHeight = 1f;


    // Update is called once per frame
    void LateUpdate()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        tilt += Input.GetAxis("Mouse Y") * Time.deltaTime * 180;

        tilt = Mathf.Clamp(tilt, 0, 50);

        transform.rotation = Quaternion.Euler(tilt, heading, 0);

        transform.position = player.position - transform.forward * camDist + Vector3.up*playerHeight;
    }
}
