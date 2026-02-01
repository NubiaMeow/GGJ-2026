using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    Transform m_target;
    [SerializeField]
    float m_smoothTime;

    Vector3 m_velocity;
    Vector3 m_relativePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_relativePosition = transform.position - m_target.position;
        m_relativePosition.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = m_relativePosition;
        target.x += m_target.position.x;
        target.z += m_target.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref m_velocity, m_smoothTime);
    }
}
