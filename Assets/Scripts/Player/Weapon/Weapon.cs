using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 25;
    public enum State
    {
        Ready,
        Doing
    }

    public State state { get; protected set; }

    public virtual void Attack()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
