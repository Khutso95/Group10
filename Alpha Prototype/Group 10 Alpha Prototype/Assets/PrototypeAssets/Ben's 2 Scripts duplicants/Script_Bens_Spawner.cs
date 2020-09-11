using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bens_Spawner : MonoBehaviour
{
    public Transform _resourceSpawnPoint;
    public Rigidbody _resourcePrefab;

    public Transform _enemySpawnPoint;
    public Rigidbody _enemyUnit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player 2")
        {
            Rigidbody _rb;
            _rb = Instantiate(_resourcePrefab, _resourceSpawnPoint.position, _resourceSpawnPoint.rotation);


            Rigidbody _enemy;
            _enemy = Instantiate(_enemyUnit, _enemySpawnPoint.position, _enemySpawnPoint.rotation);

            Destroy(gameObject);
        }


    }
}
