using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldButtonHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const float shieldCooldown = 1f;
    private const float shieldDuration = 2f;

    private Button shieldButton;

    private PlayerShieldHelper PlayerShield => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShieldHelper>();

    public void OnPointerDown(PointerEventData eventData)
    {
        if (shieldButton.interactable)
        {
            PlayerShield.ActivateShield();
            Invoke(nameof(OnShieldDurationEnded), shieldDuration);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke(nameof(OnShieldDurationEnded));

        PlayerShield.DeactivateShield();

        shieldButton.interactable = false;
        Invoke(nameof(EnableButton), shieldCooldown);
    }

    private void Start()
    {
        shieldButton = GetComponent<Button>();
    }

    private void OnShieldDurationEnded()
    {
        PlayerShield.DeactivateShield();
    }

    private void EnableButton()
    {
        shieldButton.interactable = true;
    }
}
