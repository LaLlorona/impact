using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public UnityEvent OnLandEvent;
    public CharacterController controller;
   
    public Animator animator;

    public Transform cam;

    public float speed = 6.0f;

    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;

    Vector3 velocity; 
    float turnSmoothVelocity;


    public float jumpHeight = 1.5f;

    public LayerMask groundMask;
    bool isGrounded;
    bool isJumping;

  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(controller.isGrounded);

        if (!controller.isGrounded && velocity.y > 0)
        {
            Debug.Log("Jumping");
        }
        else if (!controller.isGrounded && velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsFalling", true);
            Debug.Log("falling");
        }
        else if (controller.isGrounded)
        {
            isJumping = false;
            animator.SetBool("IsGrounded", true);
            animator.SetBool("IsFalling", false);
            
        }

       



            float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Sqrt(horizontal * horizontal + vertical * vertical));

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 moveDir = Vector3.zero;

        if (direction.magnitude >= 0.1f) //move character
        {
            
     
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);


            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        /*    controller.Move(moveDir.normalized * speed * Time.deltaTime);*/

            
        }

        //jump
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsJumping", true);
            isJumping = true;
            velocity.y =  Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;



        controller.Move(velocity * Time.deltaTime + moveDir.normalized * speed * Time.deltaTime);


        

        if (controller.isGrounded)
        {
            
            velocity.y = -2.0f;
        }



      

        


    }

}
