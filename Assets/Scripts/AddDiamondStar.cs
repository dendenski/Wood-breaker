using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddDiamondStar : MonoBehaviour
{
    // Start is called before the first frame update
    public ScoreManager scoreManager;
    public GameManager gameManager;
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager> ();
        gameManager = FindObjectOfType<GameManager> ();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Ball" || other.gameObject.tag == "Extra Ball"){
            //add an extra ball to count
            scoreManager.AddDiamondStarCount(1);
            gameManager.bricksInScene.Remove(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }


}
