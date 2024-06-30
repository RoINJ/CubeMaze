using UnityEngine;

public class DeadZoneHelper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var shieldHelper = other.GetComponent<PlayerShieldHelper>();
            if (!shieldHelper.IsShieldActive)
            {
                Debug.Log("Player died");
                var explodeHelper = other.GetComponent<PlayerExplodeHelper>();
                explodeHelper.Explode();
            }
        }
    }
}
