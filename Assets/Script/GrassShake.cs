using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassShake : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision .gameObject.CompareTag ("Player"))
        {
        }
            animator.SetTrigger("shake");
    }
}
