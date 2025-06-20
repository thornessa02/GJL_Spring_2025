using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class TrackReader : MonoBehaviour
{
    
    public List<Track> tracks;
    [SerializeField] PhysicalSlider bpmSlider;
    public MusicGenre selectedKit = MusicGenre.LoFi;
    public int selectedFX = 1;
    public int selectedMelody = 1;
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

    bool[] GetKicksInTrack(int index)
    {
        bool[] kicks = new bool[16];
        for (int i = 0; i < 16; i++)
        {
            kicks[i] = tracks[index].trackbeats[i].toggle;
        }
        return kicks;
    }

    public void ChangeSekectedMelo(int i)
    {
        selectedMelody = i;
    }
    public void ChangeSelectedFX(int i)
    {
        selectedMelody = i;
    }

    public PlayerMusicData musicData = new PlayerMusicData();
    public AudioMixer mixer;
    public void UploadButton()
    {
        musicData.genre = selectedKit;
        musicData.bpm = bpmSlider.value;

        musicData.kickSteps = GetKicksInTrack(0);
        musicData.snareSteps = GetKicksInTrack(1);
        musicData.hatSteps = GetKicksInTrack(2);

        musicData.melodyVariationA = selectedFX;
        musicData.melodyVariationB = selectedMelody;

        mixer.GetFloat("Track1volume", out float volume1);
        musicData.volumeKick = volume1;
        mixer.GetFloat("Track2volume", out float volume2);
        musicData.volumeSnare = volume2;
        mixer.GetFloat("Track3volume", out float volume3);
        musicData.volumeHat = volume3;
        mixer.GetFloat("Track4volume", out float volume4);
        musicData.volumeMelodyA = volume4;
        mixer.GetFloat("Track5volume", out float volume5);
        musicData.volumeMelodyB = volume5;
    }


    #region PlayerMusicData
    public enum MusicGenre
    {
        LoFi,
        Chiptune,
        Techno,
        Jazz,
        Funk
    }

    public enum MixPreference
    {
        Balanced,
        PercussionDominant,
        MelodyDominant
    }

    public class PlayerMusicData
    {
        // --- Choix principaux ---
        public MusicGenre genre;
        public float bpm;

        // --- Pistes de percussions (3 pistes × 16 steps = grille rythmique)
        // true = note active, false = vide
        public bool[] kickSteps = new bool[16];
        public bool[] snareSteps = new bool[16];
        public bool[] hatSteps = new bool[16];

        // --- Boucles mélodiques (variations 0, 1, ou 2)
        public int melodyVariationA;
        public int melodyVariationB;

        // --- Volume de chaque piste (0–1 ou 0–10, à adapter)
        public float volumeKick;
        public float volumeSnare;
        public float volumeHat;
        public float volumeMelodyA;
        public float volumeMelodyB;

        // ---------------------------------------------------
        // 🔧 Fonctions utilitaires
        // ---------------------------------------------------

        public float GetPercussionDensity()
        {
            int active = 0;
            foreach (bool b in kickSteps) if (b) active++;
            foreach (bool b in snareSteps) if (b) active++;
            foreach (bool b in hatSteps) if (b) active++;
            return active / 48f; // 3 × 16 = 48 total steps
        }

        public bool KickIsOnBeat()
        {
            // Check si le kick est présent sur les temps 1, 5, 9, 13
            return kickSteps[0] || kickSteps[4] || kickSteps[8] || kickSteps[12];
        }

        public bool SnareTooFrequent()
        {
            int count = 0;
            foreach (bool b in snareSteps) if (b) count++;
            return count > 8; // 8 sur 16 → très dense
        }

        public MixPreference GetMix()
        {
            float percVol = volumeKick + volumeSnare + volumeHat;
            float melVol = volumeMelodyA + volumeMelodyB;

            if (Mathf.Abs(percVol - melVol) < 0.2f) return MixPreference.Balanced;
            else if (percVol > melVol) return MixPreference.PercussionDominant;
            else return MixPreference.MelodyDominant;
        }

        public bool MixMatches(MixPreference target)
        {
            return GetMix() == target;
        }
    }
    #endregion
}


