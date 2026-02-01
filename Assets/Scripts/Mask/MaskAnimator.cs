using UnityEngine;

public class MaskAnimator : MonoBehaviour
{
    [SerializeField]
    float m_spinSpeed;

    Vector3 m_offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_offset = transform.position;
        m_spinSpeed *= 360;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        Vector3 position = transform.position;
        position.y = m_offset.y + (Mathf.Sin(Time.time) * 0.25f);
        transform.Rotate(deltaTime * m_spinSpeed * Vector3.up);
        transform.position = position;
    }
}
