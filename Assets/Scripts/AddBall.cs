using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBall : MonoBehaviour
{
    private ExtraBallManager extraBallManager;
    public GameManager gameManager;
    void Start()
    {
        extraBallManager = FindObjectOfType<ExtraBallManager> ();
        gameManager = FindObjectOfType<GameManager> ();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball"){
            extraBallManager.numberOfExtraBalls++;
            gameManager.bricksInScene.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
