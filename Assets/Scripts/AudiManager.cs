﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    void Start()
    {
        foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
       //PlaySound("start");
        
    }

    // Update is called once per frame
    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name==name)
            {
                s.source.Play();
            }
           
        }

    }
}
