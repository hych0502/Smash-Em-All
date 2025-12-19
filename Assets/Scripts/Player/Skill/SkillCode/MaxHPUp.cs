using UnityEngine;
using Ilumisoft.HealthSystem;

public class MaxHPUp : Skill
{
    public float HPAmount = 20.0f;
    public override void OnActivate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Health health = player.GetComponent<Health>();
        health.MaxHealth += HPAmount;
        health.AddHealth(HPAmount);
        //base.OnActivate();
    }
}
