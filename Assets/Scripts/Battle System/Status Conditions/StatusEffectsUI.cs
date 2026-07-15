using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectUIPrefab;
    [SerializeField] private Sprite armorSprite,burnSprite;
    private Dictionary<StatusType,StatusEffectUI> statusEffectUIs = new();
    public void UpdateStatusEffectUI(StatusType statusType, int stackCount)
    {
        if(stackCount == 0)
        {
            if (statusEffectUIs.ContainsKey(statusType))
            {
                StatusEffectUI statusEffectUI = statusEffectUIs[statusType];
                statusEffectUIs.Remove(statusType);
                Destroy(statusEffectUI.gameObject);
            }
        }
        else
        {
            if (!statusEffectUIs.ContainsKey(statusType))
            {
                StatusEffectUI statusEffectUI = Instantiate(statusEffectUIPrefab,transform);
                statusEffectUIs.Add(statusType,statusEffectUI);
            }
            Sprite sprite = GetSpriteByType(statusType);
            statusEffectUIs[statusType].Set(sprite, stackCount);
        }
    }

    private Sprite GetSpriteByType(StatusType statusType)
    {
        return statusType switch
        {
            StatusType.ARMOR => armorSprite,
            StatusType.BURN => burnSprite,
            _ => null
        };
    }
}
