using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddDiamondStar : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameManager gameManager;
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager> ();
        gameManager = FindObjectOfType<GameManager> ();
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball"){
            scoreManager.AddDiamondStarCount(1);
            gameManager.bricksInScene.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
