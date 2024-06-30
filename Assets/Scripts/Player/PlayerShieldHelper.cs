using UnityEngine;

public class PlayerShieldHelper : MonoBehaviour
{
    private const string DefaultPlayerColor = "#FFFF00";
    private const string ShieldedPlayerColor = "#ADFF2F";

    public bool IsShieldActive { get; private set; }

    private Renderer playerRenderer;

    public void ActivateShield()
    {
        IsShieldActive = true;

        ColorUtility.TryParseHtmlString(ShieldedPlayerColor, out var color);
        playerRenderer.material.color = color;
    }

    public void DeactivateShield()
    {
        IsShieldActive = false;

        ColorUtility.TryParseHtmlString(DefaultPlayerColor, out var color);
        playerRenderer.material.color = color;
    }

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }
}
