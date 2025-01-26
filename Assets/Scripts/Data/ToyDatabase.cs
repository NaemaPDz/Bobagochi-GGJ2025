using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToyDatabase", menuName = "Game/Toy Database")]
public class ToyDatabase : ScriptableObject
{
    public ToyData[] toyDatas;
}

[System.Serializable]
public class ToyData
{
    public string toyName;
    public int happiness;
    public int cleanliness;
    public int energy;
    public int sweetness;
    public int chewy;
    public Sprite toyIcon;
    public Sprite toyHover;
    public string displayText;
    public int soundIndex;
}
