using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveMusicPoint : MonoBehaviour
{
    public object LocalCopyOfData { get; private set; }
    public AudioSource musicSource;
    public AudioClip[] clips;

    private void Start()
    {
        musicSource.time = (Random.Range(0f, musicSource.clip.length));
       // InvokeRepeating("SaveData", 0f, 10f);
      //  LoadData();
      //too lazy to work with this more tonight. Exhausted as shit
    }

    private void Update()
    {
        if (musicSource.time >= musicSource.clip.length)
        {
            int i = 0;
            foreach (AudioClip clip in clips)
            {
                i++;
                if (musicSource.clip = clip)
                {
                    if(i++ > clips.Length)
                    {
                        musicSource.clip = clips[0];
                        return;
                    }
                    musicSource.clip = clips[i++];
                }
            }
        }
    }
}
