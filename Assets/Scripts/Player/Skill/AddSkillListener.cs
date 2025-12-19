using UnityEngine;
using UnityEngine.EventSystems;

public class AddSkillListener : MonoBehaviour, IPointerClickHandler
{
    public int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Assign(int skillindex)
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("MouseDown");
        Skill skillinfo = SkillManager.instance.getSelectSkill(index);
        if(skillinfo == null)
        {
            return;
        }
        if (!skillinfo.isActive)
        {
            SkillManager.instance.AddSkill(skillinfo, 0);
        }
        else
        {
            SkillManager.instance.seletedIndex = index;
            UIManager.instance.SetSelectKeyScreen(true);
        }
        UIManager.instance.SetActiveSelectUI(false);
    }
}
