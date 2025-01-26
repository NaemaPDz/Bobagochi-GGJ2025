using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(fileName = "PetDatabase", menuName = "Game/Pet Database")]
public class PetDatabase : ScriptableObject
{
    public PetData[] petDatas;
}

[System.Serializable]
public class PetData
{
    public PetGrowState petGrowState;
    public PetType petType;

    public SkeletonDataAsset skeletonDataAsset;
}
