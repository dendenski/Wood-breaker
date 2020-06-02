using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    public GameObject optionPanel;
    public Button startButton;
    public Button optionButton;
    public Button quitButton;
    private int soundState;
    private int musicState;

    public GameObject soundToggle;
    public GameObject musicToggle;
    // Start is called before the first frame update
    void Start()
    {
        //soundToggle  = GameObject.Find("Sound Toggle");
        optionPanel.SetActive (false);
        soundState = PlayerPrefs.GetInt ("soundOption");
        musicState = PlayerPrefs.GetInt ("musicOption");
        if(soundState == 0){
            soundToggle.GetComponent<Toggle>().isOn = true;
        }
        else if(soundState == 1){
            soundToggle.GetComponent<Toggle>().isOn = false;
        }
        if(musicState == 0){
            musicToggle.GetComponent<Toggle>().isOn = true;
        }
        else if(musicState == 1){
            musicToggle.GetComponent<Toggle>().isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(soundToggle.GetComponent<Toggle>().isOn == true){
            soundState = 0;
            PlayerPrefs.SetInt ("soundOption", soundState);
            PlayerPrefs.Save();
        }
        else if(soundToggle.GetComponent<Toggle>().isOn == false){
            soundState = 1;
            PlayerPrefs.SetInt ("soundOption", soundState);
            PlayerPrefs.Save();
        }
        if(musicToggle.GetComponent<Toggle>().isOn == true){
            musicState = 0;
            PlayerPrefs.SetInt ("musicOption", musicState);
            PlayerPrefs.Save();
        }
        else if(musicToggle.GetComponent<Toggle>().isOn == false){
            musicState = 1;
            PlayerPrefs.SetInt ("musicOption", musicState);
            PlayerPrefs.Save();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && optionPanel.activeInHierarchy) {
            backButton();
        }
    }

    public void StartButton(){
        SceneManager.LoadScene("MainScene");
    }
    public void Quit(){
        Application.Quit();

    }
    public void OptionButton(){
        optionPanel.SetActive (true);
        startButton.interactable = false;
        optionButton.interactable = false;
        quitButton.interactable = false;

    }
    public void backButton(){
        optionPanel.SetActive (false);
        startButton.interactable = true;
        optionButton.interactable = true;
        quitButton.interactable = true;
    }
    public void mtcChannel()
    {
        Application.OpenURL ("https://www.youtube.com/channel/UCZczqDvepgNqy80gTMGnUXw");
    }
    public void kennyWeb()
    {
        Application.OpenURL ("https://www.kenney.nl/");
    }

}
