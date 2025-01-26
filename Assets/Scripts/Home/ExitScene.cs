using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class ExitScene : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private VideoStreaming video;

    private void Start()
    {
        video.PlayVideo(Scene.Gameplay.ToString());
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0f, 1f);
    }

    public void OnExitYesButtonClicked()
    {
        blackScreen.DOFade(1f, 1f).OnComplete(() => Application.Quit());
    }

    public void OnExitNoButtonClicked()
    {
        blackScreen.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(Scene.Home.ToString()));
    }
}
