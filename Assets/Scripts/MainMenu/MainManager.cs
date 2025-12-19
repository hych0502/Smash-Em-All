using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject InstructionObj;

    public static MainManager m_instance;

    public MainManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = GameObject.FindAnyObjectByType<MainManager>();
            }
            return m_instance;
        }
    }

    private void Start()
    {
        if(instance != this)
        {
            Destroy(this);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void SetInstruction(bool active)
    {
        InstructionObj.SetActive(active);
    }
}
