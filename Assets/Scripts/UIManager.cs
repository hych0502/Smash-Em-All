using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Text waveText;

    public GameObject gameoverUI;

    public Image[] SkillImgs;

    public Image[] CooldownFills;

    public Image[] selectskillimgs;
    public Text[] selectskillnames;
    public Text[] selectskilltexts;

    public GameObject selectUI;

    public GameObject GrayScreen;

    public GameObject SelectKey;
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindAnyObjectByType<UIManager>();
            return m_instance;
        }
    }
    private static UIManager m_instance;
    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
    }
    public void UpdateWaveText(int waves, int count) {
        waveText.text = "Wave: " + waves + "\nEnemy Left: " + count;
    }

    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetSkillImage(int index, Sprite sp)
    {
        SkillImgs[index].sprite = sp;
    }
    public void SetActiveSelectUI(bool active)
    {
        selectUI.SetActive(active);
    }
    public void SetSelectUI(int index, Sprite icon, string skillname, string skillins)
    {
        selectskillimgs[index].sprite = icon;
        selectskillnames[index].text = skillname;
        selectskilltexts[index].text = skillins;
    }
    public void SetCooldownImage(int index, float fill)
    {
        //Debug.Log(fill);
        CooldownFills[index].fillAmount = fill;
    }
    public void SetGrayScreen(bool set)
    {
        GrayScreen.SetActive(set);
    }
    public void SetSelectKeyScreen(bool active)
    {
        SelectKey.SetActive(active);
    }
}
