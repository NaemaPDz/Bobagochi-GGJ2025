using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToyButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public ToyData toy { private set; get; }

    public void InitFoodButton(ToyData toy)
    {
        this.toy = toy;

        text.SetText(toy.displayText);
        image.sprite = toy.toyIcon;

        SpriteState spriteState = button.spriteState;
        spriteState.highlightedSprite = toy.toyHover;
        button.spriteState = spriteState;
    }

    public void OnButtonClicked()
    {
        GameplayManager.Instance.pet.PlayWithPet(toy);
    }
}
