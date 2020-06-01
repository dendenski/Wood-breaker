﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBall : MonoBehaviour
{
    private ExtraBallManager extraBallManager;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        extraBallManager = FindObjectOfType<ExtraBallManager> ();
        gameManager = FindObjectOfType<GameManager> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball"){
            //add an extra ball to count
            extraBallManager.numberOfExtraBalls++;
            gameManager.bricksInScene.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
