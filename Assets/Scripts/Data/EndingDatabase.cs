using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "EndingDatabase", menuName = "Game/Ending Database")]
public class EndingDatabase : ScriptableObject
{
    public EndingData[] endingDatas;

    public VideoClip ending2Video;
}

[System.Serializable]
public class EndingData
{
    public PetType type;
    public VideoClip video;
}