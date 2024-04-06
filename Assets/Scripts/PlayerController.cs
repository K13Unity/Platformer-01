using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpSpeed = 7;

    private bool canDoubleJump = false;


    public GameObject WinnerText;

    private Rigidbody2D body;
    private Animator animator;

    private bool isLeftMovement;

    private bool isRunning;
    private bool isJumping;

    private bool isGameOvered;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (horizontalMovement != 0)
        {
            isRunning = true;


            body.position += new Vector2(horizontalMovement * speed * Time.deltaTime, 0);

            if ((horizontalMovement > 0 && isLeftMovement) || (horizontalMovement < 0 && !isLeftMovement))
            {
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                isLeftMovement = !isLeftMovement;
                Debug.Log("isRunning: " + isRunning);
            }
        }
        else
        {
            isRunning= false;
            Debug.Log("isRunning: " + isRunning);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                isJumping = true;
                body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                canDoubleJump = true; 
                Debug.Log("isJumping: " + isJumping);
            }
            else if (canDoubleJump) 
            {
                isJumping = true;
                body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                canDoubleJump = false; 
                Debug.Log("Double Jump!");
            }
        }

        SwitchAnimation();
    }
    private void SwitchAnimation()
    {
        if (isJumping)
        {
            if(body.velocity.y > 0)
            {
                animator.SetTrigger("ToUp");
            }
            else
            {
                animator.SetTrigger("ToDown");
            }
            
        }
        else if (isRunning)
        {
            animator.SetTrigger("ToRun");
        }
        else
        {
            animator.SetTrigger("ToIdle");
        }
    }

    private bool isGrounded()
    {
        var raycast = Physics2D.Raycast(transform.position - transform.localScale / 2, Vector2.down, 0.1f);
        return raycast.collider != null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping= false;

        if(collision.gameObject.tag == "Enemy")
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameWin();
    }

    private void GameOver()
    {
        if (isGameOvered)
        {
            return;
        }

        isGameOvered = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameWin()
    {
        WinnerText.SetActive(true);
        Destroy(gameObject, 3);
    }

    private void OnDestroy()
    {
        GameOver();
    }

}
