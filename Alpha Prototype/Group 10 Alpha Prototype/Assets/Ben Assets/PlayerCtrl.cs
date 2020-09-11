using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // OBJECTS
    public Transform _cam;
    CharacterController _mover;

    // CAMERA
    Vector3 _camF;
    Vector3 _camR;

    // INPUT
    Vector2 input;

    // PHYSICS
    Vector3 _intent;
    Vector3 _velocity;
    Vector3 _velocityXZ;
    public float speed = 5;
    public float accel = 11;
    public float turnSpeed = 5;
    public float turnSpeedLow = 7;
    public float turnSpeedHigh = 20;
 
    //GRAVITY
    public float grav = 10f;
    bool _grounded = false;

    private void Start()
    {
        _mover = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        DoInput();
        CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        DoJump();

        _mover.Move(_velocity * Time.deltaTime);
    }

    void DoInput()
    {
        input = new Vector2(Input.GetAxis("Player 2 Horizontal"), Input.GetAxis("Player 2 Vertical"));
        input = Vector2.ClampMagnitude(input, 1);
    }

    void CalculateCamera()
    {
        _camF = _cam.forward;
        _camR = _cam.right;

        _camF.y = 0;
        _camR.y = 0;
        _camF =_camF.normalized;
        _camR = _camR.normalized;
    }

    void CalculateGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 0.2f))
        {
           _grounded = true;
        }
        else
            _grounded = false;
    }

    void DoMove()
    {
        _intent = _camF * input.y + _camR * input.x;

        float tS = _velocity.magnitude/5;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, tS);
        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(_intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        _velocityXZ = _velocity;
        _velocityXZ.y = 0;
        _velocityXZ = Vector3.Lerp(_velocityXZ, transform.forward * input.magnitude * speed, accel * Time.deltaTime);
        _velocity = new Vector3(_velocityXZ.x, _velocity.y, _velocityXZ.z);

        _mover.Move(_velocity * Time.deltaTime);
    }

    void DoGravity()
    {
        if (_grounded)
            _velocity.y = -0.5f;
        else
        _velocity.y -= grav * Time.deltaTime;
        _velocity.y = Mathf.Clamp(_velocity.y, -10, 10);
    }

    void DoJump()
    {
        if (_grounded)
        {
            if (Input.GetButtonDown("Jump"))
                _velocity.y = 8f;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Clone"))
        {
            Destroy(other.gameObject);
        }

    }

}
