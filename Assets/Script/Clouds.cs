using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float speed = 0.5f;
    public float maxXPos;
    public float minXPos;


    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector2.right*speed *Time.deltaTime );
        if(transform.position.x >=maxXPos )
        {
            transform.position = new Vector2(minXPos, transform.position.y);
        }
    }
}
