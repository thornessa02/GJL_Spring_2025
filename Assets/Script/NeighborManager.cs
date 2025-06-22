using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborManager : MonoBehaviour
{
    public List<NeighborPreferences> neighborsArchetypes;
    public List<GameObject> actualNeighbors;
    public TrackReader.PlayerMusicData musicData;

    void Start()
    {
        foreach (GameObject neighbor in actualNeighbors)
        {
            int randomIndex = Random.Range(0, neighborsArchetypes.Count - 1);
            NeighborPreferences archetypes = neighborsArchetypes[randomIndex];
            //add archetype
            neighborsArchetypes.RemoveAt(randomIndex);
        }
    }

    int ComputeScore()
    {
        float score = 0;
        foreach (GameObject neighbor in actualNeighbors)
        {
            score += neighbor.GetComponent<NeighborPreferences>().ComputeSatisfaction(neighbor.GetComponent<NeighborPreferences>(),musicData);
        }

        score = score / actualNeighbors.Count;
        score *= 100;

        return Mathf.RoundToInt(score);
    }

    public ScoreAnimator score;
    public void DisplayScore()
    {

        score.AddScore(ComputeScore());
    }

    public void Retry()
    {

    }
}
