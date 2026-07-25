using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text stackCountText;
    public void Set(Sprite sprite, int StackCount)
    {
        image.sprite = sprite;
        stackCountText.text = StackCount.ToString();
    }


}
