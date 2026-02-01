using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float m_moveSpeed;
    [SerializeField]
    float m_jumpForce;

    Rigidbody m_rigidbody;
    SpriteRenderer m_sprite;
    Animator m_animator;
    Vector2 m_inputDelta;
    int m_floorCount = 0;
    bool m_isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_sprite = GetComponentInChildren<SpriteRenderer>();
        m_animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_animator.GetBool("isGrowing") || m_isDead)
        {
            return;
        }
        Vector3 velocity = m_rigidbody.linearVelocity;
        velocity.x = m_inputDelta.x;
        velocity.z = m_inputDelta.y;
        m_rigidbody.linearVelocity = velocity;
        m_animator.SetBool("isFalling", velocity.y < -0.01f);
    }

    public void OnMove(InputValue input)
    {
        if (m_animator.GetBool("isGrowing") || m_isDead)
        {
            return;
        }
        Vector2 inputVector = input.Get<Vector2>();
        m_inputDelta = inputVector * m_moveSpeed;
        m_animator.SetBool("isWalking", inputVector.sqrMagnitude > 0);
        m_animator.SetBool("isWalkingAway", inputVector.normalized.y > 0.5f);
        m_animator.SetFloat("moveSpeed", inputVector.magnitude);
        if (inputVector.x == 0.0f)
        {
            return;
        }
        m_sprite.flipX = inputVector.x > 0.0f;
    }

    public void OnJump()
    {
        if (m_animator.GetBool("isGrowing") || m_isDead)
        {
            return;
        }
        if (!m_animator.GetBool("isGrounded"))
        {
            return;
        }
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }

    public void OnAttack()
    {
        if (m_animator.GetBool("isGrowing") || m_isDead)
        {
            return;
        }
        m_animator.SetBool("isAttacking", true);
    }

    public void OnAttackEnd()
    {
        m_animator.SetBool("isAttacking", false);
    }

    public void OnGrowEnd()
    {
        m_animator.SetBool("isGrowing", false);
        m_animator.SetBool("hasMask", true);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            m_floorCount++;
            m_animator.SetBool("isGrounded", true);
        }
        else if (collision.collider.CompareTag("PlayerMask"))
        {
            m_animator.SetBool("isGrowing", true);
            m_animator.SetBool("hasMask", true);
            m_rigidbody.linearVelocity = Vector3.zero;
            Destroy(collision.gameObject);
        }
        else if (collision.collider.CompareTag("EnemyFist"))
        {
            m_isDead = true;
            m_rigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            m_floorCount--;
            m_animator.SetBool("isGrounded", m_floorCount > 0);
        }
    }
}
