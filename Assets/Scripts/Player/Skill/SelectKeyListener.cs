using UnityEngine;
using UnityEngine.EventSystems;

public class SelectKeyListener : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Key Select Down");
        UIManager.instance.SetSelectKeyScreen(false);
        SkillManager.instance.AddActiveSkillFromListener(index, SkillManager.instance.seletedIndex);
    }
}
