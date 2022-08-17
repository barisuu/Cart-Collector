using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Player
{
    Rigidbody rb;
    private float _heading;

    public FloatingJoystick floatingJoystick;

    [SerializeField] public float moveSpeed = 8;
    [SerializeField] float turnRate;
    [SerializeField] float turnRateNoTouch;

    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int isMoving = Animator.StringToHash("isMoving");

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed=PlayerPrefs.GetFloat("MoveSpeed");
        floatingJoystick = FindObjectOfType<FloatingJoystick>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.right * floatingJoystick.Horizontal +
        Vector3.forward * floatingJoystick.Vertical;
        if (!Input.GetMouseButton(0)) direction = direction.normalized / 1000;
        if (direction.magnitude != 0)
        {
            Animator.SetBool(isMoving, true);
        }
        else if (direction.magnitude == 0)
        {
            Animator.SetBool(isMoving, false);
        }
        Animator.SetFloat(MoveSpeed, direction.magnitude);
        if (Mathf.Abs(floatingJoystick.Vertical) > 0.001f ||
            Mathf.Abs(floatingJoystick.Horizontal) > 0.001f)
        {
            _heading = Mathf.Atan2(direction.x, direction.z);
            Quaternion target = Quaternion.Euler(0f, _heading * Mathf.Rad2Deg, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnRate);
            rb.velocity = direction * moveSpeed;            
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, _heading * Mathf.Rad2Deg , 0) , Time.deltaTime * turnRateNoTouch);
            rb.velocity = Vector3.zero;
        }
    }
}