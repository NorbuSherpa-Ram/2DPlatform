using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : Enemy
{
    public float fireTime;
    private float nextFireTime;

    public Transform firePoint;
    [SerializeField] GameObject fireBall;

    void Start()
    {
     //   InvokeRepeating("Shot", .5f, fireTime);
    }

    void Shot()
    {
      GameObject fire = Instantiate(fireBall, firePoint.position, Quaternion.identity );
        fire.GetComponent<FireBall>().speed =-10;
    }

    //private void Update()
    //{
    //    if(Time.time  > nextFireTime )
    //    {
    //        Shot();
    //        nextFireTime = Time.time  + fireTime;
    //    }
    //}
}
