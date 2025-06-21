using UnityEngine;
using UnityEditor;
using System.IO;

public class NeighborGenerator
{
    [MenuItem("Tools/Generate Neighbors")]
    public static void GenerateNeighbors()
    {
        string folderPath = "Assets/GeneratedNeighbors";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        CreateNeighbor("MmeLavande", TrackReader.MusicGenre.Jazz, 0.2f, 0.1f, 0.4f, true, true, 0, 1, 85f, 10f, TrackReader.MixPreference.MelodyDominant, folderPath);
        CreateNeighbor("Kevin", TrackReader.MusicGenre.Techno, 0.1f, 0.6f, 1f, false, false, 2, 2, 135f, 15f, TrackReader.MixPreference.PercussionDominant, folderPath);
        CreateNeighbor("Leo", TrackReader.MusicGenre.LoFi, 0.3f, 0.2f, 0.5f, true, false, 1, 2, 75f, 10f, TrackReader.MixPreference.Balanced, folderPath);
        CreateNeighbor("Alex", TrackReader.MusicGenre.Chiptune, 0.15f, 0.3f, 0.7f, false, false, 2, 0, 120f, 20f, TrackReader.MixPreference.MelodyDominant, folderPath);
        CreateNeighbor("Nora", TrackReader.MusicGenre.Funk, 0.4f, 0.4f, 0.8f, true, false, 1, 1, 105f, 15f, TrackReader.MixPreference.Balanced, folderPath);
        CreateNeighbor("Henri", TrackReader.MusicGenre.Jazz, 0.6f, 0f, 0.3f, true, true, 0, 0, 60f, 5f, TrackReader.MixPreference.MelodyDominant, folderPath);
        CreateNeighbor("Sacha", TrackReader.MusicGenre.Chiptune, 0.5f, 0.7f, 1f, false, false, 1, 2, 150f, 20f, TrackReader.MixPreference.PercussionDominant, folderPath);
        CreateNeighbor("Claire", TrackReader.MusicGenre.LoFi, 0.2f, 0.1f, 0.4f, true, true, 0, 0, 65f, 5f, TrackReader.MixPreference.MelodyDominant, folderPath);
        CreateNeighbor("Diego", TrackReader.MusicGenre.Funk, 0.5f, 0.4f, 0.7f, true, false, 1, 0, 95f, 10f, TrackReader.MixPreference.Balanced, folderPath);
        CreateNeighbor("Jade", TrackReader.MusicGenre.Techno, 0.05f, 0.6f, 0.9f, true, true, 2, 2, 128f, 5f, TrackReader.MixPreference.PercussionDominant, folderPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("✅ 10 neighbors generated in " + folderPath);
    }

    private static void CreateNeighbor(string name, TrackReader.MusicGenre genre, float tolerance,
        float minDensity, float maxDensity, bool kickOnBeat, bool hateSnare,
        int melodyA, int melodyB, float bpm, float bpmTolerance, TrackReader.MixPreference mix,
        string folderPath)
    {
        NeighborPreferences neighbor = ScriptableObject.CreateInstance<NeighborPreferences>();

        neighbor.name = name;
        neighbor.preferredGenre = genre;
        neighbor.genreTolerance = tolerance;
        neighbor.minPercussionDensity = minDensity;
        neighbor.maxPercussionDensity = maxDensity;
        neighbor.prefersKickOnBeat = kickOnBeat;
        neighbor.hatesSnareOnEveryBeat = hateSnare;
        neighbor.preferredMelodyA = melodyA;
        neighbor.preferredMelodyB = melodyB;
        neighbor.preferredBPM = bpm;
        neighbor.bpmTolerance = bpmTolerance;
        neighbor.mix = mix;

        string assetPath = $"{folderPath}/{name}.asset";
        AssetDatabase.CreateAsset(neighbor, assetPath);
    }
}
