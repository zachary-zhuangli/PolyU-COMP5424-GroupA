using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseAttack : MonoBehaviour
{

    /// <summary>
    /// How much damage to apply to the Fighting System object
    /// </summary>
    public int Damage = 25;

    /// <summary>
    /// Used to determine velocity of this collider
    /// </summary>
    public Rigidbody ColliderRigidbody;

    /// <summary>
    /// Minimum Amount of force necessary to do damage. Expressed as impulse.magnitude
    /// </summary>
    public float MinImpulse = 0.1f;

    // How much impulse force was applied last onCollision enter
    public float LastDamageImpulse = 0;

    private void Start()
    {
        if (ColliderRigidbody == null)
        {
            ColliderRigidbody = GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!this.isActiveAndEnabled)
        {
            return;
        }

        OnCollisionEvent(collision);
    }

    public virtual void OnCollisionEvent(Collision collision)
    {
        LastDamageImpulse = collision.impulse.magnitude;

        Debug.Log("<><><><>== Attack: " + LastDamageImpulse); 

        if (LastDamageImpulse >= MinImpulse)
        {

            // Can we damage what we hit?
            FightingSystem fs = collision.gameObject.GetComponent<FightingSystem>();
            if (fs)
            {
                Debug.Log("<><><><>== Make Damage!"); 
                // d.DealDamage(Damage, collision.GetContact(0).point, collision.GetContact(0).normal, true, gameObject, collision.gameObject);
                fs.RPC_Attacked(Damage, GetComponent<Transform>().position, (int)LastDamageImpulse);
            }
        }
    }
}
