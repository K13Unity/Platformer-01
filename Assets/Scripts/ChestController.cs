using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;

    private bool isOpened;


    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpened && collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("ToOpening");
            isOpened = true;
        }
    }

}    
