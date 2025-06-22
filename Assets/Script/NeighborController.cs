using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborController : MonoBehaviour
{
    public NeighborPreferences preferences;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ChooseAnim(TrackReader.PlayerMusicData musicData)
    {
        float score = preferences.ComputeSatisfaction(preferences,musicData);

        if (score <= 0.35)
        {
            anim.SetTrigger("Angry");
        }
        else if(score >= 0.60)
        {
            anim.SetTrigger("Happy");
        }
        else
        {
            anim.SetTrigger("Neutral");
        }
    }
}
