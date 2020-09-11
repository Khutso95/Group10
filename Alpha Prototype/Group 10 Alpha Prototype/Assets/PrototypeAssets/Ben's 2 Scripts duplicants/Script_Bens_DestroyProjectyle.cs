using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bens_DestroyProjectyle : MonoBehaviour
{
    public float _rotationSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        Destroy(gameObject, 1.8f);
    }
}
