using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPlayerMenu : MonoBehaviour
{
    // Start is called before the first frame update    
    public AudioSource srcAmbient;

    public AudioClip sfxAmbient;
    void Start()
    {
        srcAmbient.clip = sfxAmbient;
        srcAmbient.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
