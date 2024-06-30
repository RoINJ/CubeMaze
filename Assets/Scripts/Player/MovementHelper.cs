using UnityEngine;
using UnityEngine.AI;

public class MovementHelper : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;

        Invoke(nameof(MoveToGreenZone), 2f);
    }

    private void MoveToGreenZone()
    {
        var finish = GameObject.FindGameObjectWithTag("Finish").transform;
        navMeshAgent.SetDestination(finish.position);
    }
}
