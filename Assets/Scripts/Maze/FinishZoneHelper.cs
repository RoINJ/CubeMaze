using UnityEngine;

public class FinishZoneHelper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var ps = other.gameObject.GetComponentInChildren<ParticleSystem>();
            if (!ps.isPlaying)
            {
                ps.Play();
            }

            Invoke(nameof(ReloadLevel), 1f);
        }
    }

    private void ReloadLevel()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ReloadLevel();
    }
}
