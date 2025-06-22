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

    public float ComputeSatisfaction(NeighborPreferences prefs, TrackReader.PlayerMusicData music)
    {
        float score = 1f;

        if (music.genre != prefs.preferredGenre)
            score -= (1f - prefs.genreTolerance);

        float percDensity = music.GetPercussionDensity();
        if (percDensity < prefs.minPercussionDensity || percDensity > prefs.maxPercussionDensity)
            score -= 0.2f;

        if (prefs.prefersKickOnBeat && !music.KickIsOnBeat())
            score -= 0.1f;

        if (prefs.hatesSnareOnEveryBeat && music.SnareTooFrequent())
            score -= 0.15f;

        if (music.melodyVariationA != prefs.preferredMelodyA)
            score -= 0.1f;
        if (music.melodyVariationB != prefs.preferredMelodyB)
            score -= 0.1f;

        if (Mathf.Abs(music.bpm - prefs.preferredBPM) > prefs.bpmTolerance)
            score -= 0.2f;

        if (!music.MixMatches(prefs.mix))
            score -= 0.15f;

        return Mathf.Clamp01(score);
    }
}
