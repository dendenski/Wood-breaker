using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLife : MonoBehaviour
{
    public float lifetime;
    private float lifetimeSeconds;
    private AudioSource audioState;
    private int soundState;
    // Start is called before the first frame update
    void Start()
    {
        audioState = this.GetComponent<AudioSource>();
        lifetimeSeconds = lifetime;
        soundState = PlayerPrefs.GetInt ("soundOption");
        if(soundState == 0){
            audioState.volume = 1.0f;
        }
        else if(soundState == 1){
            audioState.volume = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(soundState == 0){
            audioState.volume = 1.0f;
        }
        else if(soundState == 1){
            audioState.volume = 0.0f;
        }
        lifetimeSeconds -= Time.deltaTime;
        if(lifetimeSeconds <=0){
            this.gameObject.SetActive(false);
        }
    }
}
