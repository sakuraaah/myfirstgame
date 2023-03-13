using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
	private Animator anim;
	private bool isGrounded;
	private Transform groundCheck;
	private float checkRadius;
	private LayerMask whatIsGround;

    void Start()
    {
        anim=GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			anim.SetBool("isRunning", true);
		} 
		else
		{
			anim.SetBool("isRunning", false);
		}		

		if (Input.GetKey(KeyCode.Space))
		{
			anim.SetBool("isJumping", true);
		}	
		else if (isGrounded == true)
		{
			anim.SetBool("isJumping", false);
		}
    }
}
