using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    private static List<AudioSource> soundSources = new List<AudioSource>();


    private void Start()
    {
        //if(soundSources.Count == 0)
        //{
        soundSources= new List<AudioSource>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("DynamicSound"))
            {
                soundSources.Add(go.GetComponent<AudioSource>());
            }
        //}

    }
    public void TriggerSoundEvent(AnimationEvent sound)
    {
        AudioSource freeSource = soundSources.First(e => !e.isPlaying);
        freeSource.transform.position = transform.position;
        freeSource.clip = clips.First(e => e.name == sound.stringParameter);
        freeSource.Play();
        if(sound.intParameter == 0)
        {
            freeSource.pitch = Random.Range(0.9f, 1.1f);
        }
        else
        {
            freeSource.pitch = 1;
        }
    }

    public void SoundEvent(string name)
    {
        AudioSource freeSource = soundSources.First(e => !e.isPlaying);
        freeSource.transform.position = transform.position;
        freeSource.clip = clips.First(e => e.name == name);
        freeSource.Play();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Step(AnimationEvent ae)
    {
        TriggerSoundEvent(ae);
    }
}
