using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{
    public AudioSource intro;
    public AudioSource song;

    // Start is called before the first frame update
    void Start()
    {
        intro.playOnAwake = true;
        intro.loop = false;
        song.Stop();
        song.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!intro.isPlaying && !song.isPlaying)
            song.Play();
        if (intro.isPlaying)
            song.Stop();
    }
}
