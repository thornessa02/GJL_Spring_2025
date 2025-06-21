using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NeighborPreferences", menuName = "ScriptableObject/NeighborPreferences")]
public class NeighborPreferences : ScriptableObject
{
    public TrackReader.MusicGenre preferredGenre = TrackReader.MusicGenre.LoFi;
    public float genreTolerance = 0.5f; // 1 = tolère tout, 0 = très difficile à satisfaire

    public float minPercussionDensity = 0.2f; // % de cases actives minimum
    public float maxPercussionDensity = 0.6f; // % de cases actives maximum
    public bool prefersKickOnBeat = true; // aime le kick sur 1-5-9-13 (temps forts)
    public bool hatesSnareOnEveryBeat = true; // trop agressif

    public int preferredMelodyA = 2;
    public int preferredMelodyB = 1;

    public float preferredBPM = 90f;
    public float bpmTolerance = 10f;

    public TrackReader.MixPreference mix = TrackReader.MixPreference.MelodyDominant;

    
}
