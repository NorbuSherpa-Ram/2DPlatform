using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        //ShakeCamera();
    }
    public void ShakeCamera()
    {
        animator.SetTrigger("shake");
    }
}
