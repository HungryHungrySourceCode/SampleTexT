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
    public MusicTime time;

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

    public void SaveData()
    {
        try
        {
            if (!Directory.Exists("Saves"))
                Directory.CreateDirectory("Saves");
        }
        catch
        {
            Directory.CreateDirectory("Saves");
        }
        
        


        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");


        MusicTime _time = this.GetComponent<MusicTime>();
        MusicTime.instance = _time;
        LocalCopyOfData = MusicTime.instance;
        MusicTime.instance.time = Time.timeSinceLevelLoad;
        Debug.Log(MusicTime.instance.time);
        formatter.Serialize(saveFile, LocalCopyOfData);
        Debug.Log(LocalCopyOfData);
        saveFile.Close();
    }

    public void LoadData()
    {
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);

        LocalCopyOfData = (MusicTime)formatter.Deserialize(saveFile);
        time = (MusicTime)LocalCopyOfData;
        time.source = musicSource;
        musicSource.time = time.time;
        saveFile.Close();
    }
}
