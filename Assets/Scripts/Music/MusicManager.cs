using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip newMusic; //Pick an audio track to play.

    AudioClip oldMusic; 

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length == 1)
        {
            var go = GameObject.Find("Music Player"); //Finds the game object called Game Music, if it goes by a different name, change this.
                                                      //go.GetComponent<AudioClip>().Play(); //Plays the audio.
            oldMusic = go.GetComponent<AudioSource>().clip;
            //Debug.Log(oldMusic.name);
            //Debug.Log(newMusic.name);

            if (oldMusic != newMusic)
            {
                Debug.Log("I'm here");
                go.GetComponent<AudioSource>().clip = newMusic;
                go.GetComponent<AudioSource>().Play();
            }
        }
        else
            Awake();
    }

}
