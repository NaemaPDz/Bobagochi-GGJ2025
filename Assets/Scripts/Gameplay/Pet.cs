using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public GameData gameData { private set; get; }
    public PetDatabase petData { private set; get; }
    public SkeletonGraphic skeletonGraphic { private set; get; }

    public PetGrowState state { private set; get; } = PetGrowState.Baby;
    public PetType type { private set; get; } = PetType.Base;
    public float exp { private set; get; } = 0f;

    public float hunger { private set; get; } = 10f;
    public float happiness { private set; get; } = 10f;
    public float cleanliness { private set; get; } = 10f;
    public float energy { private set; get; } = 10f;

    public float sweetness { private set; get; } = 0f;
    public float chewy { private set; get; } = 0f;

    private float updateTimer = 0f;
    private bool isAction = false;

    private void Start()
    {
        skeletonGraphic = GetComponent<SkeletonGraphic>();
        gameData = Resources.Load<GameData>("Data/GameData");
        petData = Resources.Load<PetDatabase>("Data/PetDatabase");

        skeletonGraphic.AnimationState.Complete += OnAnimationCompleted;

        hunger = gameData.cappedStatsValue;
        happiness = gameData.cappedStatsValue;
        cleanliness = gameData.cappedStatsValue;
        energy = gameData.cappedStatsValue;
    }

    private void OnDisable()
    {
        skeletonGraphic.AnimationState.Complete -= OnAnimationCompleted;
    }

    private void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer >= 1f)
        {
            updateTimer = 0f;

            hunger = ClampStatsValue(hunger - (GetDecayRatePerSec(StatsType.Hunger, hunger, gameData.hungerDecayRate)));
            happiness = ClampStatsValue(happiness - (GetDecayRatePerSec(StatsType.Happiness, happiness, gameData.happinessDecayRate)));
            cleanliness = ClampStatsValue(cleanliness - (GetDecayRatePerSec(StatsType.Cleanliness, cleanliness, gameData.cleanlinessDecayRate)));
            energy = ClampStatsValue(energy - (GetDecayRatePerSec(StatsType.Energy, energy, gameData.energyDecayRate)));

            if (!isAction)
            {
                ForceAction();
            }
        };
    }

    public void FeedPet(FoodData f)
    {
        if (f == null)
        {
            return;
        }

        if (!isAction)
        {
            isAction = true;
            exp++;
            happiness = ClampStatsValue(happiness + f.happiness);
            energy = ClampStatsValue(energy + f.energy);

            hunger = ClampStatsValue(hunger + 2f);
            sweetness = ClampStatsValue(sweetness + f.sweetness);
            chewy = ClampStatsValue(chewy + f.chewy);

            StartCoroutine(PlaySound("eat" + (Random.Range(0, 2) + 1)));
            PlayAnimaion(GetAnimationPrefix() + AnimationStaticValue.EAT + f.foodName);
        }
    }

    public void PlayWithPet(ToyData t)
    {
        if (t == null)
        {
            return;
        }

        if (!isAction)
        {
            isAction = true;
            exp++;
            happiness = ClampStatsValue(happiness + t.happiness);
            energy = ClampStatsValue(energy + t.energy);

            cleanliness = ClampStatsValue(cleanliness + t.cleanliness);
            sweetness = ClampStatsValue(sweetness + t.sweetness);
            chewy = ClampStatsValue(chewy + t.chewy);

            StartCoroutine(PlaySound("play" + t.soundIndex));
            PlayAnimaion(GetAnimationPrefix() + AnimationStaticValue.PLAY + t.toyName);
        }
    }

    public void CleanPet()
    {
        if (!isAction)
        {
            isAction = true;
            happiness = ClampStatsValue(happiness + 2f);
            cleanliness = ClampStatsValue(gameData.cappedStatsValue);

            StartCoroutine(PlaySound("bath"));
            PlayAnimaion(GetAnimationPrefix() + AnimationStaticValue.BATH);
        }
    }

    public void TakePetToSleep()
    {
        if (!isAction)
        {
            isAction = true;
            happiness = ClampStatsValue(happiness + 2f);
            energy = ClampStatsValue(gameData.cappedStatsValue);

            StartCoroutine(PlaySound("sleep"));
            PlayAnimaion(GetAnimationPrefix() + AnimationStaticValue.SLEEP);
        }
    }

    private string GetAnimationPrefix()
    {
        string prefix = AnimationStaticValue.BOBA;

        if (GetPercentage(hunger) <= gameData.unwellPercentage)
        {
            prefix += AnimationStaticValue.HUNGRY;
        }

        if (GetPercentage(cleanliness) <= gameData.unwellPercentage)
        {
            prefix += AnimationStaticValue.STINKY;
        }

        if (GetPercentage(energy) <= gameData.unwellPercentage)
        {
            prefix += AnimationStaticValue.SLEEPY; ;
        }

        return prefix;
    }

    private void PlayAnimaion(string animationName, bool loop = false)
    { 
        skeletonGraphic.AnimationState.SetAnimation(0, animationName, loop);
    }

    private IEnumerator PlaySound(string name)
    {
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.PlaySoundEffect(name);
    }

    private void OnAnimationCompleted(TrackEntry trackEntry)
    {
        string animationName = GetAnimationPrefix() + AnimationStaticValue.IDLE;

        if (trackEntry.Animation.Name != animationName)
        {
            if (exp >= gameData.cappedExpReached)
            {
                exp = 0;
                GameplayManager.Instance.PetEvolution();
            }

            PlayAnimaion(animationName, true);
            isAction = false;
        }
    }

    private void ForceAction()
    {
        if (GetPercentage(energy) < gameData.energyDecayRate[0].percentageThreshold)
        {
            TakePetToSleep();
        }
    }

    public void Evolve(PetGrowState newState, PetType newType)
    {
        exp = 0;
        state = newState;
        type = newType;

        PetData newPetForm = petData.petDatas.FirstOrDefault(p => p.petGrowState == newState && p.petType == newType);

        if (newPetForm == null)
        {
            return;
        }

        skeletonGraphic.skeletonDataAsset = newPetForm.skeletonDataAsset;
        skeletonGraphic.Initialize(true);

        skeletonGraphic.AnimationState.Complete -= OnAnimationCompleted;
        skeletonGraphic.AnimationState.Complete += OnAnimationCompleted;

        SoundManager.Instance.PlaySoundEffect("lv" + (int)newState);
        PlayAnimaion(GetAnimationPrefix() + AnimationStaticValue.IDLE,true);
    }

    private float ClampStatsValue(float value)
    {
        return Mathf.Clamp(value, 0f, gameData.cappedStatsValue);
    }

    private float GetDecayRatePerSec(StatsType stats, float currentValue, ThresholdRate[] rate)
    {
        float percentage = GetPercentage(currentValue);
        foreach (ThresholdRate t in rate)
        {
            if (percentage <= t.percentageThreshold)
            {
                float decayRate = t.ratePerMinute;

                var affectedStatsSet = new HashSet<StatsType>(t.penaltyRate.SelectMany(p => p.valueWasAffect));
                if (affectedStatsSet.Contains(stats))
                {
                    AffectedStats a = t.penaltyRate.FirstOrDefault(i => i.valueWasAffect.Contains(stats));
                    if (a != null)
                    {
                        decayRate += a.affectedRate / 100f;
                    }
                }

                return decayRate / 60f;
            }
        }

        return 0f;
    }

    private float GetPercentage(float value)
    {
        return (value / gameData.cappedStatsValue) * 100f;
    }
}
