using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class FightingSystem : MonoBehaviour
{
    static int DEFAULT_MAX_HP = 100;
    static int DEFAULT_DEFENSE = 0;
    // static int RAGDOLL_FORCE = 400;

    [SerializeField]
    public int hp = FightingSystem.DEFAULT_MAX_HP;
    [SerializeField]
    public int maxHp = FightingSystem.DEFAULT_MAX_HP;
    [SerializeField]
    public int defense = FightingSystem.DEFAULT_DEFENSE;
    [SerializeField]
    public bool isDead = false;

    // 是否是AI控制（momster）；
    // 对于AI控制的对象，生命系统数据需要与AI系统同步
    [SerializeField]
    public bool poweredByAI = true;

    // Start is called before the first frame update
    void Start()
    {
        if (poweredByAI)
        {
            SyncHealthDataToAI();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 被攻击
    public void Attacked(int damage, Transform target, int ragdollForce = 400)
    {
        int damageAfterDefense = damage - defense;
        if (damageAfterDefense < 0)
        {
            damageAfterDefense = 0;
        }

        if (this.poweredByAI)
        {
            // 对于AI类对象，需要基于LDB的方式计算伤害量，并将生命数据在AI系统和本组件中同步
            AIAtacked(damageAfterDefense, target, ragdollForce);
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
        }
    }

    private void SyncHealthDataToAI()
    {
        if (GetComponent<EmeraldAIEventsManager>())
        {
            GetComponent<EmeraldAIEventsManager>().UpdateHealth(this.maxHp, this.hp);
        }
    }
}
