using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();

        if (btn == null)
        {
            return;
        }

        btn.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        if (btn == null)
        {
            return;
        }

        btn.onClick.RemoveListener(PlaySound);
    }

    private void PlaySound()
    {
        SoundManager.Instance?.PlaySoundEffect("btn");
    }
}
