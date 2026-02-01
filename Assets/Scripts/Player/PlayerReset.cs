using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReset : MonoBehaviour
{
    bool m_dying = false;
    float m_timeout = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_dying)
        {
            m_timeout += Time.deltaTime;
        }
        if (m_timeout >= 3.0f)
        {
            Debug.Log("Bad luck!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Floor"))
        {
            m_dying = true;
            Debug.Log("Ouch");
        }
    }
}
