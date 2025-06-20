using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Track : MonoBehaviour
{
    public List<PhysicalButton> trackbeats;
    [SerializeField] bool loop;
    [SerializeField] Material highlight;
    [SerializeField] Material notHighlight;
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
        StopTrack();
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

        for (int i = 0; i < trackbeats.Count; i++)
        {
            if (i == 0) trackbeats[i].ChangeColor(2, notHighlight);
            else trackbeats[i].ChangeColor(0, notHighlight);
        }
    }
    IEnumerator PlayTrackKicks()
    {
        if (!loop)
        {
            for (int i = 0; i < trackbeats.Count; i++)
            {
                if(i==0) trackbeats[i].ChangeColor(2, highlight);
                else trackbeats[i].ChangeColor(0, highlight);

                if (trackbeats[i].toggle)
                {
                    sound.Play();
                }
                yield return new WaitForSeconds((1/(bpm/60))/4);

                if (i == 0) trackbeats[i].ChangeColor(2, notHighlight);
                else trackbeats[i].ChangeColor(0, notHighlight);
            }
            yield return null;
            StartCoroutine(PlayTrackKicks());
        }
        
    }
}
