using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8;
    public float MaxSpeed = 8;
    public bool IsDashing = false;
    public float turnSpeed = 1080;
    public bool paused;

    private Rigidbody rb;
    private Vector3 input;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            Move();
        }
    }
    void GatherInput() { input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); }

    void Look()
    {
        if (input != Vector3.zero)
        {
            var relative = (transform.position + input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    void Move()
    {
        if (IsDashing) { return; }

        Vector3 moveDir = transform.forward * input.magnitude * speed;

        Vector3 newVelocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        Vector3 horizontal = new Vector3(newVelocity.x, 0, newVelocity.z);

        if (horizontal.magnitude > MaxSpeed)
        {
            horizontal = horizontal.normalized * MaxSpeed;
        }

        rb.velocity = new Vector3(horizontal.x, newVelocity.y, horizontal.z);
    }



}
