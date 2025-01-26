#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameData gameData = (GameData)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Sort Threshold Array"))
        {
            SortThresholdRates(gameData.hungerDecayRate);
            SortThresholdRates(gameData.happinessDecayRate);
            SortThresholdRates(gameData.cleanlinessDecayRate);
            SortThresholdRates(gameData.energyDecayRate);
        }
    }

    private void SortThresholdRates(ThresholdRate[] thresholdRates)
    {
        System.Array.Sort(thresholdRates, (a, b) => a.percentageThreshold.CompareTo(b.percentageThreshold));
        EditorUtility.SetDirty(target);
    }
}
#endif