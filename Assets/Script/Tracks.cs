using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    [SerializeField] List<Track> tracks;

    private void OnEnable()
    {
        gameObject.GetComponentInParent<TrackReader>().tracks = tracks;
    }
}
