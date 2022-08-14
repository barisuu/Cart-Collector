using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    float dragVal;
    private float _heading;

    public FloatingJoystick floatingJoystick;

    [SerializeField] float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        floatingJoystick = FindObjectOfType<FloatingJoystick>();
        rb=GetComponent<Rigidbody>();
        dragVal=rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.right * floatingJoystick.Horizontal +
         Vector3.forward * floatingJoystick.Vertical;
         if (!Input.GetMouseButton(0)) direction = direction.normalized / 1000;
        
        if (Mathf.Abs(floatingJoystick.Vertical) > 0.001f ||
            Mathf.Abs(floatingJoystick.Horizontal) > 0.001f)
        {
            _heading = Mathf.Atan2(direction.x, direction.z);
            transform.rotation = Quaternion.Euler(0f, _heading * Mathf.Rad2Deg, 0);
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, _heading * Mathf.Rad2Deg, 0);
            rb.velocity = Vector3.zero;
        }
        //rb.drag=dragVal;
    }
}
