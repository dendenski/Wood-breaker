using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    private int currentScore;
    private int highScore;
    public int diamondStarCount;
    public Text diamondStarCountText;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        
        diamondStarCount = PlayerPrefs.GetInt ("diamondStarCount");
        highScore = PlayerPrefs.GetInt ("highscore");
        if((highScore) < 10){
            highScoreText.text = "0000" + (highScore);
        }
        else if((highScore) < 100){
            highScoreText.text = "000" + (highScore);
        }
        else if((highScore) < 1000){
            highScoreText.text = "00" + (highScore);
        }
        else if((highScore) < 10000){
            highScoreText.text = "00" + (highScore);
        }
        else{
            scoreText.text = "" + (highScore);
        }
        diamondStarCountText.text = "x " + diamondStarCount;
    }
    public void ScoreIncrease(){
        currentScore++;
        if((currentScore) < 10){
            scoreText.text = "0000" + (currentScore);
        }
        else if((currentScore) < 100){
            scoreText.text = "000" + (currentScore);
        }
        else if((currentScore) < 1000){
            scoreText.text = "00" + (currentScore);
        }
        else if((currentScore) < 10000){
            scoreText.text = "0" + (currentScore);
        }
        else{
            scoreText.text = "" + (currentScore);
        }
    }
    public void SetHighScore(){
        if(currentScore > highScore){
            PlayerPrefs.SetInt ("highscore", currentScore);
            PlayerPrefs.Save();
        }
    }

    public void AddDiamondStarCount(int amount){
        diamondStarCount += amount;
        PlayerPrefs.SetInt ("diamondStarCount", diamondStarCount);
        PlayerPrefs.Save();
        diamondStarCount = PlayerPrefs.GetInt ("diamondStarCount");
        diamondStarCountText.text = "x " + diamondStarCount;
    }

    public bool SubtractDiamondStarCount(int amount){
        if((diamondStarCount - amount) >= 0){
            diamondStarCount -= amount;
            PlayerPrefs.SetInt ("diamondStarCount", diamondStarCount);
            PlayerPrefs.Save();
            diamondStarCount = PlayerPrefs.GetInt ("diamondStarCount");
            diamondStarCountText.text = "x " + diamondStarCount;
            return true;
        }else{
            return false;
        }
    }
}
