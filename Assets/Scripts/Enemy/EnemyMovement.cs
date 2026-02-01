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
    [SerializeField]
    float m_hitDistance;
    [SerializeField]
    SpriteRenderer m_alertSprite;

    NavMeshAgent m_agent;
    Animator m_animator;
    bool m_alert = false;
    float m_alertTime = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = transform.position - m_player.position;
        float distance = delta.magnitude;
        if (distance < m_activationRadius)
        {
            m_agent.SetDestination(m_player.position);
            if (!m_alert)
            {
                m_alert = true;
                m_alertSprite.enabled = true;
            }
            else
            {
                m_alertTime += Time.deltaTime;
            }
            if (m_alertTime > 5.0f)
            {
                m_alertSprite.enabled = false;
            }
        }
        else
        {
            m_alert = false;
            m_alertTime = 0.0f;
            m_alertSprite.enabled = false;
        }

        bool shouldHit = distance < m_hitDistance;
        m_animator.SetBool("isAttacking", shouldHit);
        m_agent.isStopped = shouldHit;
        m_animator.SetFloat("currentSpeed", m_agent.velocity.magnitude / m_agent.speed);
    }


}
