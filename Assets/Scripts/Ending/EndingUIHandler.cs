using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Linq;
using DG.Tweening;

public class EndingUIHandler : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private VideoPlayer player;

    private FinalPetData petData;

    private void Start()
    {
        blackScreen.gameObject.SetActive(true);

        GameData g = Resources.Load<GameData>("Data/GameData");
        EndingDatabase e = Resources.Load<EndingDatabase>("Data/EndingDatabase");

        player.loopPointReached += EndVideo;

        SoundManager.Instance?.StopBackgroundMusic();

        if (GameplayManager.Instance)
        {
            petData = GameplayManager.Instance.GetFinalPetData();
            Destroy(GameplayManager.Instance);

            if ((petData.happiness / g.cappedStatsValue) * 100f < 30f &&
                (petData.energy / g.cappedStatsValue) * 100f < 50f)
            {
                player.clip = e.ending2Video;
            }
            else
            {
                PetType p = petData.petType;
                
                player.clip = e.endingDatas.FirstOrDefault(i => i.type == p).video;
            }
        }
        else
        {
            int index = UnityEngine.Random.Range(0, e.endingDatas.Length);

            if (index == e.endingDatas.Length - 1)
            {
                player.clip = e.ending2Video;
            }
            else
            {
                player.clip = e.endingDatas[index].video;
            }
        }

        player.Play();
    }

    private void OnDisable()
    {
        player.loopPointReached -= EndVideo;
    }

    private void EndVideo(VideoPlayer source)
    {
        OnExitButtonClicked();
    }

    public void OnExitButtonClicked()
    {
        blackScreen.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(Scene.Home.ToString()));
    }
}
