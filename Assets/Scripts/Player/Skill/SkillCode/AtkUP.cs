using UnityEngine;

public class AtkUP : Skill
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public override void OnActivate()
    {
        //쿨다운 조절,필수
        GameObject player = GameObject.FindWithTag("Player");
        Weapon weapon = FindAnyObjectByType<Weapon>();
        weapon.damage += 10;
        //base.OnActivate();
    }
}
