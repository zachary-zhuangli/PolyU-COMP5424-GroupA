using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;
using Fusion;

public class FightingSystem : NetworkBehaviour
{
    static int DEFAULT_MAX_HP = 100;
    static int DEFAULT_DEFENSE = 0;
    // static int RAGDOLL_FORCE = 400;

    [SerializeField]
    [Networked]
    public int hp { get; set; } = FightingSystem.DEFAULT_MAX_HP;
    [SerializeField]
    [Networked]
    public int maxHp { get; set; } = FightingSystem.DEFAULT_MAX_HP;
    [SerializeField]
    [Networked]
    public int defense { get; set; } = FightingSystem.DEFAULT_DEFENSE;
    [SerializeField]
    [Networked]
    public bool isDead { get; set; } = false;

    // 是否是AI控制（momster）；
    // 对于AI控制的对象，生命系统数据需要与AI系统同步
    [SerializeField]
    public bool poweredByAI = true;

    public override void Spawned()
    {
        if (Object.HasStateAuthority && poweredByAI)
        {
            SyncHealthDataToAI();
        }
    }

    // 被攻击
    // 已遗弃，请使用RPC_Attacked
    public void Attacked(int damage, Transform target, int ragdollForce = 400)
    {
        RPC_Attacked(damage, target.position, ragdollForce);
    }

    // TODO: 传攻击者对象ID，使用真实的Transform组件，代替attackerPos
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Attacked(int damage, Vector3 attackerPos = new Vector3(), int ragdollForce = 400)
    {
        int damageAfterDefense = damage - defense;
        if (damageAfterDefense < 0)
        {
            damageAfterDefense = 0;
        }

        if (this.poweredByAI)
        {
            // 对于AI类对象，需要基于LDB的方式计算伤害量，并将生命数据在AI系统和本组件中同步
            Transform attackerTransform = GetTempTransform("TEMP_AttackerTransform");
            attackerTransform.position = attackerPos;
            AIAtacked(damageAfterDefense, attackerTransform, ragdollForce);
            SyncHealthDataFromAI();
        }
        else
        {
            takeDamage(damageAfterDefense);
        }
    }

    private void AIAtacked(int damage, Transform target, int ragdollForce = 400)
    {
        if (GetComponent<LocationBasedDamageArea>())
        {
            GetComponent<LocationBasedDamageArea>().DamageArea(damage, EmeraldAISystem.TargetType.Player, target, ragdollForce);
        }
        else if (GetComponent<EmeraldAISystem>())
        {
            GetComponent<EmeraldAISystem>().Damage(damage, EmeraldAI.EmeraldAISystem.TargetType.Player, target, ragdollForce);
        }
    }

    public void takeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            hp = 0;
        }
    }

    // 提供给外部系统同步HP数据
    public void setHP(int hp)
    {
        if (hp <= 0)
        {
            this.hp = 0;
            this.isDead = true;
        }
        else if (hp > this.maxHp)
        {
            this.hp = this.maxHp;
        }
        else
        {
            this.hp = hp;
        }
    }

    private void SyncHealthDataFromAI()
    {
        EmeraldAISystem EmeraldComponent = GetComponent<EmeraldAISystem>();
        if (EmeraldComponent)
        {
            this.setHP(EmeraldComponent.CurrentHealth);
            this.isDead = EmeraldComponent.IsDead;
        }
    }

    private void SyncHealthDataToAI()
    {
        if (GetComponent<EmeraldAIEventsManager>())
        {
            GetComponent<EmeraldAIEventsManager>().UpdateHealth(this.maxHp, this.hp);
            GetComponent<EmeraldAISystem>().IsDead = this.isDead;
        }
    }

    public void InitHealthStatusForAI() {
        SyncHealthDataToAI();
    }

    private Transform GetTempTransform(string tempGOName)
    {
        GameObject tempGO = GameObject.Find(tempGOName);
        if (tempGO == null)
        {
            tempGO = new GameObject(tempGOName);
        }
        return tempGO.transform;
    }
}
