using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponData", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class MeleeWeaponData : ScriptableObject
{
    public float damage = 25;
    public float timeBetSwing = 0.5f;
    public float scaleX = 1.0f;
    public float scaleY = 1.0f;
    public Sprite sprite;
    public Animation anim;
}
