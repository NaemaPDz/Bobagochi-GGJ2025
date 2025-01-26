using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDatabase", menuName = "Game/Sound Database")]
public class SoundDatabase : ScriptableObject
{
    public SoundData[] SoundDatas;

    public AudioClip bgm;
}

[System.Serializable]
public class SoundData
{
    public string soundName;
    public AudioClip soundClip;
}
