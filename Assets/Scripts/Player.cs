using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private float nextFireTime;
    public float fireRate = 0.05f;
    private Vector3 direction;

    public float gravityScale; 

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal * speed, 0f, vertical * speed);

        if (this.transform.position.y > 2.5f && nextFireTime < Time.time)
        {
            
            direction.y = direction.y + (Physics.gravity.y * gravityScale);
            controller.Move(direction  * Time.deltaTime);
            nextFireTime = Time.time + fireRate;

        }

        if (direction.magnitude >= 0.1f) 
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // gives us angle that kernel should face during movement

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            
        }
    }

}
