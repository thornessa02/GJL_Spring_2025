using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Track : MonoBehaviour
{
    [SerializeField] List<Toggle> trackbeats;
    [SerializeField] bool loop;
    AudioSource sound;
    [HideInInspector] public float bpm;
    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    public void BPMChange()
    {
        if (loop) ;
        sound.pitch = bpm / 120;
    }
    public void PlayTrack()
    {
        if (!loop) StartCoroutine(PlayTrackKicks());
        else
        {
            sound.Play();
            sound.pitch = bpm / 120;
        }
    }
    public void StopTrack()
    {
        if (!loop) StopAllCoroutines();
        else sound.Stop();
    }
    IEnumerator PlayTrackKicks()
    {
        if (!loop)
        {
            for (int i = 0; i < trackbeats.Count; i++)
            {
                if (trackbeats[i].isOn)
                {
                    sound.Play();
                }
                yield return new WaitForSeconds(1/(bpm/60));
            }
            yield return null;
            StartCoroutine(PlayTrackKicks());
        }
        
    }
}
