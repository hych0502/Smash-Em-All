using UnityEngine;
using Ilumisoft.HealthSystem;

public class Heal : Skill
{
    public float healAmount = 40.0f;
    public override void OnActivate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Health health = player.GetComponent<Health>();
        health.AddHealth(healAmount);
        //base.OnActivate();
    }
}
