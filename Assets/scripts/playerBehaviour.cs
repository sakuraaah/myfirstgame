using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;
	public float speed = 12f;
	public Animator anim;
	private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask whatIsGround;
	public int extraJumps;
	public bool canMove;

	public int health;
	public int numberOfLifes =5;

	public Image[] lives;

	public Sprite fullLife;
	public Sprite emptyLife;
	public Sprite bonusLife;
	public Sprite noLife;

	void Start()
    {
		health = 3;
        rb = GetComponent<Rigidbody2D>();
		anim=GetComponent<Animator>();
		canMove = true;
    }

    void Update()
	{
		 if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && canMove)
		 {
			 jump();
			 extraJumps--;
		 } 
		 
		 if (Input.GetAxis ("Horizontal") != 0 && canMove)
		 {
			 Flip();
		 }
		 
		 if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		 {
		 	anim.SetBool("isRunning", true);
		 } 
		 else
		 {
		 	anim.SetBool("isRunning", false);
		 }		

		 if (Input.GetKey(KeyCode.Space) && canMove)
		 {
		 	anim.SetBool("isJumping", true);
		 }	
		 else 
		 {
			if (isGrounded == true)
			{
				extraJumps = 1;
				anim.SetBool("isJumping", false);
			}	
		 }
		 
	 	 if (health<=0 || rb.transform.position.y<=-15)
		 {
            for (int i = 0; i < lives.Length; i++)
            {
				if (i<=2)
                {
					lives[i].sprite = emptyLife;
				}
				else
                {
					lives[i].sprite = noLife;
				}
			}
			anim.SetBool("isDead", true);
			canMove = false;
			Invoke("ReloadLevel", 2);
		 }	
		 
    }
	
	void FixedUpdate()
	{
		if (canMove)
		{
			rb.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed, rb.velocity.y);
		}
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }
	
	/*void OnGUI()
	{
		GUI.Box (new Rect (0,0,100,30), "Life: " + health + "hp");
	}*/	
	
	void jump()
	{
		rb.AddForce (Vector2.up * 23f, ForceMode2D.Impulse);
	}	
	
	void Flip()
	{
		if (Input.GetAxis ("Horizontal") > 0)
			transform.localRotation = Quaternion.Euler (0,0,0);
		
		if (Input.GetAxis ("Horizontal") < 0)
			transform.localRotation = Quaternion.Euler (0,180,0);
	}	
	
	void ReloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy")
		{
			if (health <= 3)
            {
				health -= 1;
				lives[health].sprite = emptyLife;
			} 
			else if (health >= 4)
            {
				health -= 1;
				lives[health].sprite = noLife;
			}
			
		}	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Heal")
		{
			if (health == numberOfLifes)
            {
				Destroy(col.gameObject);
			}
			else if (health >= 3)
            {
				lives[health].sprite = bonusLife;
				health += 1;
				Destroy(col.gameObject);
			}
            else if (health <= 2)
			{
				lives[health].sprite = fullLife;
				health += 1;
				Destroy(col.gameObject);
			}
		}	
		
		if (col.gameObject.tag == "Finish")
		{	
			if (SceneManager.GetActiveScene().buildIndex == 5) {
				SceneManager.LoadScene("Menu");	
			} else{	
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);	
			}
		}	
	}
}