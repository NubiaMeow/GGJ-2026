using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform m_player;
    [SerializeField]
    float m_activationRadius;

    NavMeshAgent m_agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = transform.position - m_player.position;
        float distance = delta.magnitude;
        if (distance < m_activationRadius)
        {
            m_agent.SetDestination(m_player.position);
        }
    }
}
