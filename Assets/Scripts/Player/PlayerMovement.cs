using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float m_moveSpeed;
    [SerializeField]
    float m_jumpForce;
    [SerializeField]
    Animator m_animator;

    Vector3 m_velocity;
    bool m_grounded = true;
    const float mk_gravity = 9.83f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        Vector3 position = transform.position;
        position += m_velocity * deltaTime;

        if (!m_grounded)
        {
            m_velocity.y -= mk_gravity * deltaTime;

            if (position.y <= 0.0f)
            {
                position.y = 0.0f;
                m_velocity.y = 0.0f;
                m_grounded = true;
            }

            m_animator.SetBool("isFalling", m_velocity.y < 0);
        }

        transform.position = position;

        m_animator.SetBool("isGrounded", m_grounded);
    }

    public void OnMove(InputValue input)
    {
        Vector2 delta = m_moveSpeed  * input.Get<Vector2>();
        m_velocity.x = delta.x;
        m_velocity.z = delta.y;
        float speed = delta.magnitude;
        m_animator.SetBool("isWalking", speed > 0);
        m_animator.SetBool("isWalkingAway", delta.normalized.y > 0.5);
        m_animator.SetFloat("moveSpeed", speed / 5);
    }

    public void OnJump()
    {
        if (!m_grounded)
        {
            return;
        }
        m_velocity.y = m_jumpForce;
        m_grounded = false;
    }
}
