using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WeaponBaseAttack : NetworkBehaviour
{

    /// <summary>
    /// How much damage to apply to the Fighting System object
    /// </summary>
    public int Damage = 25;

    /// <summary>
    /// Used to determine velocity of this collider
    /// </summary>
    // public Rigidbody ColliderRigidbody;

    /// <summary>
    /// Minimum Amount of force necessary to do damage. Expressed as impulse.magnitude
    /// </summary>
    public float MinImpulse = 0.1f;

    // How much impulse force was applied last onCollision enter
    public float LastDamageImpulse = 0;

    private void Start()
    {
        // if (ColliderRigidbody == null)
        // {
        //     ColliderRigidbody = GetComponent<Rigidbody>();
        // }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 非本地玩家不做伤害的碰撞检测，伤害统一由对应本地玩家发起
        if (!Object.HasStateAuthority) {
            return;
        }

        if (!this.isActiveAndEnabled || !collision.gameObject.CompareTag("Respawn"))
        {
            return;
        }

        OnCollisionEvent(collision);
    }

    private void OnTriggerEnter(Collider collider)
    {
        // 非本地玩家不做伤害的碰撞检测，伤害统一由对应本地玩家发起
        if (!Object.HasStateAuthority) {
            return;
        }

        if (!this.isActiveAndEnabled || !collider.gameObject.CompareTag("Respawn"))
        {
            return;
        }

        FightingSystem fs = collider.GetComponent<FightingSystem>();
        if (fs)
        {
            fs.RPC_Attacked(Damage, GetComponent<Transform>().position, (int)LastDamageImpulse);
        }
    }

    public virtual void OnCollisionEvent(Collision collision)
    {
        LastDamageImpulse = collision.impulse.magnitude;

        if (LastDamageImpulse >= MinImpulse)
        {

            // Can we damage what we hit?
            FightingSystem fs = collision.gameObject.GetComponent<FightingSystem>();
            if (fs)
            {
                // d.DealDamage(Damage, collision.GetContact(0).point, collision.GetContact(0).normal, true, gameObject, collision.gameObject);
                fs.RPC_Attacked(Damage, GetComponent<Transform>().position, (int)LastDamageImpulse);
            }
        }
    }
}
