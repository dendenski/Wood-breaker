using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    public Rigidbody2D ball;
    public float constantSpeed = 18;
    public Vector2 tempVelocity;
    // Start is called before the first frame update
    void Start()
    {
        tempVelocity = new Vector2(-5, 10).normalized;
        
        ball.velocity = constantSpeed * tempVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
