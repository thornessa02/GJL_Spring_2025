using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborPreferences : MonoBehaviour
{
    public string preferredGenre = "Lo-fi";
    public float genreTolerance = 0.5f; // 1 = tolère tout, 0 = très difficile à satisfaire

    public float minPercussionDensity = 0.2f; // % de cases actives minimum
    public float maxPercussionDensity = 0.6f; // % de cases actives maximum
    public bool prefersKickOnBeat = true; // aime le kick sur 1-5-9-13 (temps forts)
    public bool hatesSnareOnEveryBeat = true; // trop agressif

    public int preferredMelodyA = 2;
    public int preferredMelodyB = 1;

    public float preferredBPM = 90f;
    public float bpmTolerance = 10f;

    public MixPreference mix = MixPreference.MelodyDominant;

    public enum MixPreference { Balanced, PercussionDominant, MelodyDominant }


    float ComputeSatisfaction(NeighborPreferences prefs, PlayerMusicData music)
    {
        float score = 1f;

        if (music.genre != prefs.preferredGenre)
            score -= (1f - prefs.genreTolerance);

        float percDensity = music.GetPercussionDensity();
        if (percDensity < prefs.minPercDensity || percDensity > prefs.maxPercDensity)
            score -= 0.2f;

        if (prefs.prefersKickOnBeat && !music.KickIsOnBeat())
            score -= 0.1f;

        if (prefs.hatesSnareSpam && music.SnareTooFrequent())
            score -= 0.15f;

        if (music.melodyA != prefs.preferredMelodyA)
            score -= 0.1f;
        if (music.melodyB != prefs.preferredMelodyB)
            score -= 0.1f;

        if (Mathf.Abs(music.bpm - prefs.preferredBPM) > prefs.bpmTolerance)
            score -= 0.2f;

        if (!music.MixMatches(prefs.mix))
            score -= 0.15f;

        return Mathf.Clamp01(score);
    }
}
