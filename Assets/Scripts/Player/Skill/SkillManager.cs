using UnityEngine;


public class SkillManager : MonoBehaviour
{
    public static SkillManager m_instance = null;

    private Skill[] skills = new Skill[2];

    private Skill[] selectskills = new Skill[3];

    private float[] Skillcooldowns = new float[2];

    public GameObject[] skillPrefabs;

    private bool IsSelecting = false;
    public bool isSelecting {
        get
        {
            return IsSelecting;
        }
    }

    public int seletedIndex { get; set;}
    
    public static SkillManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = GameObject.FindAnyObjectByType<SkillManager>();
            }
            return m_instance;
        }
    }

    [HideInInspector]
    public AudioSource skillsound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance != this)
        {
            Destroy(this);
        }
        skillsound = GetComponent<AudioSource>();
        //Skill skillexample = skillPrefabs[2].GetComponent<Skill>();
        //AddSkill(skillexample, 0);
    }

    private void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (skills[i] == null)
                continue;
            UIManager.instance.SetCooldownImage(i, Skillcooldowns[i] / skills[i].cooltime);
            Skillcooldowns[i] -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    public void AddSkill(Skill skill, int index)
    {
        if (skill.isActive)
        {
            AssignActiveSkill(skill, index);
        }
        else
        {
            skill.OnActivate();
        }
        CloseSkillScreen();
    }
    void AssignActiveSkill(Skill skill, int index)
    {
        Debug.Log(index);
        skills[index] = skill;
        UIManager.instance.SetSkillImage(index, skill.IconSp);
    }

    public void ActiveSkill(int index)
    {
        if(Skillcooldowns[index] > 0.0f)
        {
            return;
        }
        skills[index].OnActivate();
        Skillcooldowns[index] = skills[index].cooltime;
    }
    public void SetSkillScreen()
    {
        GameManager GM = GameManager.GM;
        if (!GM.isPause)
        {
            GM.TogglePause();
        }
        for (int i = 0; i < 3; i++)
        {
            selectskills[i] = skillPrefabs[Random.Range(0, skillPrefabs.Length)].GetComponent<Skill>();
            UIManager.instance.SetSelectUI(i, selectskills[i].IconSp, selectskills[i].SkillName, selectskills[i].SkillInstruction);
        }
        UIManager.instance.SetActiveSelectUI(true);
        IsSelecting = true;
    }
    public void AddActiveSkillFromListener(int skillindex, int selectindex)
    {
        if(selectskills[selectindex] == null)
        {
            return;
        }
        AddSkill(selectskills[selectindex], skillindex);
    }

    public void CloseSkillScreen()
    {
        GameManager GM = GameManager.GM;
        if (GM.isPause)
        {
            GM.TogglePause();
        }
        UIManager.instance.SetActiveSelectUI(false);
        IsSelecting = false;
    }

    public Skill getSelectSkill(int index)
    {
        return selectskills[index];
    }
}
