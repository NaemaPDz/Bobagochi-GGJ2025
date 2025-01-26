using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodDatabase", menuName = "Game/Food Database")]
public class FoodDatabase : ScriptableObject
{
    public FoodData[] foodDatas;
}

[System.Serializable]
public class FoodData
{
    public string foodName;
    public int happiness;
    public int energy;
    public int sweetness;
    public int chewy;
    public Sprite foodIcon;
    public Sprite foodHover;
}
