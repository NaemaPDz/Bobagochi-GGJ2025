using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance {  private set; get; }

    public GameplayUIManager gameplayUIManager { private set; get; }
    public Pet pet { private set; get; }

    private FinalPetData finalPetData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        pet = FindFirstObjectByType<Pet>();
        gameplayUIManager = FindFirstObjectByType<GameplayUIManager>();
    }

    public void PetEvolution()
    {
        if (pet.state == PetGrowState.Adult)
        {
            SoundManager.Instance.PlaySoundEffect("lv3");
            EndingGame();
            return;
        }

        float sum = pet.sweetness - pet.chewy;
        PetType newType = PetType.Base;

        if (sum > 1)
        {
            newType = PetType.Honey;
        }
        else if (sum < -1)
        {
            newType = PetType.BrownSugar;
        }
        else
        {
            newType = PetType.GreenTea;
        }

        int stateIndex = (int)pet.state;
        stateIndex++;
        PetGrowState newState = (PetGrowState)stateIndex;
        pet.Evolve(newState, newType);
    }

    public void EndingGame()
    {
        finalPetData.petType = pet.type;
        finalPetData.sweetness = pet.sweetness;
        finalPetData.chewy = pet.chewy;
        finalPetData.happiness = pet.happiness;
        finalPetData.energy = pet.energy;

        gameplayUIManager.Endgame();
    }

    public FinalPetData GetFinalPetData()
    {
        return finalPetData;
    }
}


