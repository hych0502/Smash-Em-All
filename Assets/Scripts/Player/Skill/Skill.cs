using UnityEngine;

public class Skill : MonoBehaviour
{
    public bool isActive = true;
    public Sprite IconSp;

    public string SkillName;
    [TextArea]
    public string SkillInstruction;

    public float cooltime = 5.0f;
    
    public virtual void OnActivate()
    {

    }
    public Sprite getIcon()
    {
        return IconSp;
    }
}
