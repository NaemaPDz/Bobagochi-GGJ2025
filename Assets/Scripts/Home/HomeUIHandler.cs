using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class HomeUIHandler : MonoBehaviour
{
    [SerializeField] private Image blackScreen;

    private void Start()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0f, 1f);
        SoundManager.Instance?.StopBackgroundMusic();
        SoundManager.Instance?.PlayBackgroundMusic();
    }

    public void OnScreenClicked()
    {
        blackScreen.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(Scene.Gameplay.ToString()));
    }

    public void OnExitButtonClicked()
    {
        blackScreen.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(Scene.ExitScene.ToString()));
    }
}
