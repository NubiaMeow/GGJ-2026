using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    PlayerMovement m_playerMovement;

    void Start()
    {
        m_playerMovement = GetComponentInParent<PlayerMovement>();
    } 

    public void ResetAttack()
    {
        m_playerMovement.OnAttackEnd();
    }
}
