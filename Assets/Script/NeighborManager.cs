using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborManager : MonoBehaviour
{
    public List<NeighborPreferences> neighborsArchetypes;
    public List<GameObject> actualNeighbors;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
