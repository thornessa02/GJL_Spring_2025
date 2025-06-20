using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TrackVolume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    PhysicalSlider slider;
    [SerializeField] string mixerParameterName = "TrackVolume"; // Le nom exposé dans le mixer
    void Start()
    {
        slider = GetComponent<PhysicalSlider>();
    }

    

    public void OnSliderValueChanged(float sliderValue) // sliderValue entre 0 et 10
    {
        float dbValue = SliderToDecibel_Logarithmic(sliderValue);
        mixer.SetFloat(mixerParameterName, dbValue);
    }

    private float SliderToDecibel(float slider)
    {
        // Valeur du slider : 0 à 10, avec 5 = 0 dB
        // Mapping linéaire : 0 → -80 dB, 10 → +20 dB
        // Donc : 
        //   0–5 → -80 à 0 dB
        //   5–10 → 0 à +20 dB

        if (slider < 5f)
        {
            return Mathf.Lerp(-80f, 0f, slider / 5f);
        }
        else
        {
            return Mathf.Lerp(0f, 20f, (slider - 5f) / 5f);
        }
    }

    private float SliderToDecibel_Logarithmic(float slider)
    {
        // Normaliser le slider entre 0.0 et 1.0
        float normalized = slider / 10f;

        // On évite 0 pour ne pas log(0) → erreur
        float volume = Mathf.Clamp(normalized, 0.0001f, 1f);

        // Convertir en dB (formule standard) : 20 * log10(volume)
        float db = 20f * Mathf.Log10(volume);

        // Clamp final entre -80 et +20 dB
        return Mathf.Clamp(db, -80f, 20f);
    }
}
