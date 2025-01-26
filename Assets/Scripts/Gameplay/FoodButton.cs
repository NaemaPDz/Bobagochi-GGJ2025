using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public FoodData food { private set; get; }
    
    public void InitFoodButton(FoodData food)
    {
        this.food = food;

        text.SetText(food.foodName);
        image.sprite = food.foodIcon;

        SpriteState spriteState = button.spriteState;
        spriteState.highlightedSprite = food.foodHover;
        button.spriteState = spriteState;
    }

    public void OnButtonClicked()
    {
        GameplayManager.Instance.pet.FeedPet(food);
    }
}
