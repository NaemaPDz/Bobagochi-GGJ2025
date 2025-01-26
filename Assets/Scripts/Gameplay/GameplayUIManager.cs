using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] private Image blackScreen;
    [SerializeField] private VideoStreaming video;

    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject foodPanel;
    [SerializeField] private GameObject toyPanel;


    private void Start()
    {
        video.PlayVideo(Scene.Gameplay.ToString());

        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(0f, 1f);

        GameObject foodButton = Resources.Load<GameObject>("Prefabs/FoodButton");
        foreach (FoodData f in Resources.Load<FoodDatabase>("Data/FoodDatabase").foodDatas)
        {
            FoodButton fb = Instantiate(foodButton, foodPanel.transform).GetComponent<FoodButton>();
            fb.InitFoodButton(f);
            fb.transform.SetSiblingIndex(foodPanel.transform.childCount - 2);
        }

        GameObject toyButton = Resources.Load<GameObject>("Prefabs/ToyButton");
        foreach (ToyData t in Resources.Load<ToyDatabase>("Data/ToyDatabase").toyDatas)
        {
            ToyButton tb = Instantiate(toyButton, toyPanel.transform).GetComponent<ToyButton>();
            tb.InitFoodButton(t);
            tb.transform.SetSiblingIndex(toyPanel.transform.childCount - 2);
        }

        ShowActionPanel();
    }

    public void ShowActionPanel()
    {
        actionPanel.SetActive(true);

        foodPanel.SetActive(false);
        toyPanel.SetActive(false);
    }

    public void ShowFoodPanel()
    {
        foodPanel.SetActive(true);

        actionPanel.SetActive(false);
        toyPanel.SetActive(false);
    }

    public void ShowToyPanel()
    {
        toyPanel.SetActive(true);

        actionPanel.SetActive(false);
        foodPanel.SetActive(false);
    }

    public void Endgame()
    {
        blackScreen.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(Scene.EndScene.ToString()));
    }

    public void OnBathButtonClicked()
    {
        GameplayManager.Instance.pet.CleanPet();
    }

    public void OnSleepButtonClicked()
    {
        GameplayManager.Instance.pet.TakePetToSleep();
    }
}


