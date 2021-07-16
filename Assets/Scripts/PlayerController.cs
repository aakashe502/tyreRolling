using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;


    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;//0;left 1:middle 2:right
    public float laneDistance = 3;//the distance between two lanes
    public float jumpForce;
    public float Gravity = -20;
    public Animator animator;
    public float maxspeed;
    


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        if (forwardSpeed < maxspeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }

       


        animator.SetBool("isGameStarted", true);
      

        direction.z = forwardSpeed;
     
        animator.SetBool("isGrounded", controller.isGrounded);
        if (controller.isGrounded)
        {
            direction.y =-1;
        if(Input.GetKey(KeyCode.Space)||SwipeManager.swipeUp){
            Jump();
        }
        }
        else{
            direction.y+=Gravity*Time.deltaTime;
        }

        if (SwipeManager.swipeDown||Input.GetKeyDown("s"))
        {
            StartCoroutine(slide());
        }


       if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d")||SwipeManager.swipeRight)
       {
           desiredLane++;
           if(desiredLane==3)
           desiredLane=2;
       }
       if(Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown("a")||SwipeManager.swipeLeft)
       {
           desiredLane--;
           if(desiredLane==-1)
           desiredLane=0;
       }

        if (Input.GetKeyDown("p"))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 3.0f;
           else if(Time.timeScale == 3.0f)
            {
                Time.timeScale = 1.0f;
            }
        }

            Vector3 targetPosition =transform.position.z*transform.forward +transform.position.y*transform.up;
       if(desiredLane==0)
       {
          targetPosition+=Vector3.left*laneDistance;
       }
       else if(desiredLane==2)
       {
          targetPosition+=Vector3.right*laneDistance;
       }
           //transform.position = Vector3.Lerp(transform.position,targetPosition,500*Time.deltaTime);
           //controller.center=controller.center;

           if(transform.position ==targetPosition)
               return;
           Vector3 diff=targetPosition-transform.position;
           Vector3 movedir=diff.normalized*25*Time.deltaTime;
           if(movedir.sqrMagnitude<diff.sqrMagnitude){
               controller.Move(movedir);
           }
           else{
               controller.Move(diff);
           }

    }

     private void FixedUpdate() {
        if (!PlayerManager.isGameStarted)
            return;

        controller.Move(direction*Time.fixedDeltaTime);
    }

private void Jump(){
    direction.y = jumpForce;

}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameover = true;
            FindObjectOfType<AudiManager>().PlaySound("start");
        }
    }
    private IEnumerator slide()
    {
        animator.SetBool("issliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);
        controller.center = new Vector3(0, 0f, 0);
        controller.height = 2;
        animator.SetBool("issliding", false);
    }

}
