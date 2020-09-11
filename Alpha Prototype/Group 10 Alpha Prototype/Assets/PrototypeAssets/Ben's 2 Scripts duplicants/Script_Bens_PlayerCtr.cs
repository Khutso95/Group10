using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bens_PlayerCtr : MonoBehaviour
{
    // OBJECTS
    CharacterController _mover;

    //GRAVITY
    public bool _grounded = false;

    //New Variables
    public float _playerSpeed;
    public float _playerTurnSpeed;

    public float _hoverHight;
    public float _hoverMin;
    public float _hoverMax;
    public float _gravity;
    public float _liftSpeed;

    public Vector3 _velocityDown;
    public Vector3 _velocityUp;
    public Vector3 _RayCastOffset;

    public GameObject GM;

    private void Start()
    {
        _mover = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (GM.GetComponent<Alex.Carvalho.Script_GM_RM>().P2CanMove)
        {
            PlayerMovement();
        }
        
        CalculateGround();
    }

    public void PlayerMovement()
    {
        float _vertical = Input.GetAxisRaw("Player 2 Vertical");
        Vector3 _zMovement = transform.forward * _vertical;
        _mover.Move(_zMovement * _playerSpeed * Time.deltaTime);

        float _horizontal = Input.GetAxisRaw("Player 2 Horizontal");
        transform.Rotate(0f, _horizontal * _playerTurnSpeed * Time.deltaTime, 0f);

        if(_vertical != 0 || _horizontal != 0)
        {
            GM.GetComponent<Alex.Carvalho.Script_GM_RM>().isMoving();
        }

        
    }

    

    void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + _RayCastOffset, Vector3.down, out hit, _hoverHight))
        {
            _velocityDown.y = 0f;
            Debug.DrawRay(transform.position + _RayCastOffset, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
            _grounded = true;
            if(hit.distance <= _hoverMin)
            {
                _velocityUp.y += _liftSpeed * Time.deltaTime;
                _mover.Move(_velocityUp * Time.deltaTime);
            }
            
        }
        else
        {
            _grounded = false;
            _velocityUp.y = 0f;
            _velocityDown.y += _gravity * Time.deltaTime;
            _mover.Move(_velocityDown * Time.deltaTime);        
            
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Clone"))
        {
            string child = other.transform.GetChild(0).name;
            GM.GetComponent<Alex.Carvalho.Script_GM_RM>().SpawnReasources(child);
            Destroy(other.gameObject);
        }

    }
}
