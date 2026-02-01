using UnityEngine;
using UnityEngine.InputSystem;

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
        Vector3 velocity = m_rigidbody.linearVelocity;
        velocity.x = m_inputDelta.x;
        velocity.z = m_inputDelta.y;
        m_rigidbody.linearVelocity = velocity;
        m_animator.SetBool("isFalling", velocity.y < -0.01f);
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        m_inputDelta = inputVector * m_moveSpeed;
        m_animator.SetBool("isWalking", inputVector.sqrMagnitude > 0);
        m_animator.SetBool("isWalkingAway", inputVector.normalized.y > 0.5f);
        m_animator.SetFloat("moveSpeed", inputVector.magnitude);
        if (inputVector.x == 0.0f)
        {
            return;
        }
        m_sprite.flipX = inputVector.x < 0.0f;
    }

    public void OnJump()
    {
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }

    public void OnAttack()
    {
        m_animator.SetBool("isAttacking", true);
    }

    public void OnAttackEnd()
    {
        m_animator.SetBool("isAttacking", false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            m_floorCount++;
            m_animator.SetBool("isGrounded", true);
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
