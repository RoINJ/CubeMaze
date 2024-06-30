using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldButtonHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const float shieldCooldown = 1f;
    private const float shieldDuration = 2f;

    private Button shieldButton;

    [SerializeField]
    private Image cooldownImage;

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
        StartCoroutine(Cooldown(shieldCooldown));
    }

    private void Start()
    {
        shieldButton = GetComponent<Button>();
    }

    private void OnShieldDurationEnded()
    {
        PlayerShield.DeactivateShield();
    }

    private IEnumerator Cooldown(float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            var fillAmount = Mathf.Lerp(1, 0, timer / duration);
            cooldownImage.fillAmount = fillAmount;
            yield return null;
        }

        cooldownImage.fillAmount = 0;
        shieldButton.interactable = true;
    }
}
