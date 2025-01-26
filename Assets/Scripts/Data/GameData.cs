using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/Game Data")]
public class GameData : ScriptableObject
{
    public float cappedStatsValue;
    public float cappedExpReached;
    public float unwellPercentage;

    public ThresholdRate[] hungerDecayRate;
    public ThresholdRate[] happinessDecayRate;
    public ThresholdRate[] cleanlinessDecayRate;
    public ThresholdRate[] energyDecayRate;
}

[System.Serializable]
public class ThresholdRate
{
    public float percentageThreshold;
    public float ratePerMinute;

    public AffectedStats[] penaltyRate;
    public AffectedAction[] affectedActions;
}

[System.Serializable]
public class AffectedStats
{
    public float affectedRate;
    public StatsType[] valueWasAffect;
}

[System.Serializable]
public class AffectedAction
{
    public AffectedActionType type;
    public ActionType[] actionWasAffect;
}