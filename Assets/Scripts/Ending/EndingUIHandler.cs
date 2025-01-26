using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class EndingUIHandler : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private VideoPlayer player;
    [SerializeField] private VideoStreaming video;

    private FinalPetData petData;

    private void Start()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0f, 1f);

        GameData g = Resources.Load<GameData>("Data/GameData");

        player.loopPointReached += EndVideo;

        SoundManager.Instance?.StopBackgroundMusic();

        if (GameplayManager.Instance)
        {
            petData = GameplayManager.Instance.GetFinalPetData();
            Destroy(GameplayManager.Instance);

            if ((petData.happiness / g.cappedStatsValue) * 100f < 30f &&
                (petData.energy / g.cappedStatsValue) * 100f < 50f)
            {
                video.PlayVideo(Scene.EndScene.ToString() + "2");
            }
            else
            {
                PetType p = petData.petType;
                video.PlayVideo(Scene.EndScene.ToString() + p.ToString());
            }
        }
        else
        {
            int index = Random.Range(0, 4);

            if (index == 0)
            {
                video.PlayVideo(Scene.EndScene.ToString() + "2");
            }
            else
            {
                video.PlayVideo(Scene.EndScene.ToString() + ((PetType)index).ToString());
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
