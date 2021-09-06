using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed,jumpForce,moveInput,checkradius,shoottime;

    public bool isalive,shooting;

    private bool facingRight = true;

    private bool isGrounded;

    public LayerMask whatisground;

    public Animator animator;

    public Transform groundCheck,firepoint;

    public int extraJumps,extraJumpValue,lives;

    private Rigidbody2D rb;

    public GameObject bullet;

    [SerializeField]
    GameObject[] lives_gameobject;

    void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput != 0)
            animator.SetBool("ismoving", true);
        else
            animator.SetBool("ismoving", false);
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (isGrounded)
        {
            animator.SetBool("jumping", false);
            extraJumps = 2;
        }
        else
        {
            animator.SetBool("jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space)&& extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("jumping", true);
            extraJumps--;
        } 
        else if (Input.GetKeyDown(KeyCode.Space)&& extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("jumping", true);
        }

        if (!shooting && Input.GetButton("Fire1"))
            StartCoroutine(shoot());

        if (isalive)
        {
            if (lives > 4)
                lives = 4;
            switch (lives)
            {
                case 4:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(true);
                    lives_gameobject[2].SetActive(true);
                    lives_gameobject[3].SetActive(true);
                    break;
                case 3:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(true);
                    lives_gameobject[2].SetActive(true);
                    lives_gameobject[3].SetActive(false);
                    break;
                case 2:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(true);
                    lives_gameobject[2].SetActive(false);
                    lives_gameobject[3].SetActive(false);
                    break;
                case 1:
                    lives_gameobject[0].SetActive(true);
                    lives_gameobject[1].SetActive(false);
                    lives_gameobject[2].SetActive(false);
                    lives_gameobject[3].SetActive(false);
                    break;
                case 0:
                    lives_gameobject[0].SetActive(false);
                    lives_gameobject[1].SetActive(false);
                    lives_gameobject[2].SetActive(false);
                    lives_gameobject[3].SetActive(false);
                    isalive = false;
                    Debug.LogWarning("Dead");
                    break;
            }
        }
    }

    IEnumerator shoot()
    {
        shooting = true;
        Instantiate(bullet,firepoint);
        animator.SetBool("shooting", true);
        yield return new WaitForSeconds(shoottime);
        animator.SetBool("shooting", false);
        shooting = false;
    }

    public void die()
    {
        lives -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            die();
        if (collision.gameObject.CompareTag("ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isGrounded = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
