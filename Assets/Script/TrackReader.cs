using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackReader : MonoBehaviour
{
    
    public List<Track> tracks;
    [SerializeField] Slider bpmSlider;
    private void Start()
    {
        foreach (Track track in tracks)
        {
            track.bpm = bpmSlider.value;
        }
    }
    public void PlayTrackButton()
    {
        foreach (Track track in tracks)
        {
            track.PlayTrack();
        }
    }

    public void StopTrackButton()
    {
        foreach (Track track in tracks)
        {
            track.StopTrack();
        }
    }

    public void OnSliderValueChanged(float sliderValue) // sliderValue entre 0 et 10
    {
        foreach (Track track in tracks)
        {
            track.bpm = sliderValue; ;
            track.BPMChange();
        }
        
    }
}
