using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour {
    public Animator anim;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float prevY;
    private float curMult = 1;
    private bool falling;
    private void Start()
    {
        prevY = transform.position.y;
    }
    bool falltriggered;
    void Update()
    {
        
        //

        
        CharacterController controller = GetComponent<CharacterController>();
        
        if (!controller.isGrounded)
        {
            if (prevY > transform.position.y && falltriggered)
            {
                falling = true;
                falltriggered = false;
                Debug.Log("falling");
            }
            else
            {
                falling = false;
            }



        }
        Debug.DrawRay(transform.position, Vector3.down * 20, Color.green);
        if (Physics.Raycast(transform.position, Vector3.down, 20) | controller.isGrounded)
        {

            falling = false;
            falltriggered = true;
        }
        anim.SetBool("Falling", falling);
        prevY = transform.position.y;


        anim.SetBool("OnGround", controller.isGrounded);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if (controller.isGrounded)
        {
            falltriggered = false;
            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Run"))
            {
                moveDirection *= curMult;
                curMult += 1.5f * Time.deltaTime;
                
                
                if (curMult >= 2)
                {
                    curMult = 2;
                }
            }

            else {
                moveDirection *= curMult;
                curMult -= 1.5f * Time.deltaTime;
                if (curMult <= 1) {
                    curMult = 1;
                }
            }
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                anim.SetTrigger("Jump");
            }
            
        }
        else {
            Vector3 temp = moveDirection;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            moveDirection.y = temp.y;
        }
        anim.SetFloat("Speedx", h * speed * curMult);
        anim.SetFloat("Speedy", v * speed * curMult);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        
    }
}
