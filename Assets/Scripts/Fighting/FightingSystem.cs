using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingSystem : MonoBehaviour
{
    static double DEFAULT_HP = 100;
    static double DEFAULT_ATTACK = 20;
    static double DEFAULT_DEFENSE = 10;

    [SerializeField]
    public double hp = FightingSystem.DEFAULT_HP;
    [SerializeField]
    public double attack = FightingSystem.DEFAULT_ATTACK;
    [SerializeField]
    public double defense = FightingSystem.DEFAULT_DEFENSE;
    protected bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 被攻击
    public void Attacked(double damage)
    {
        double realDamage = damage - defense;
        if (realDamage < 0)
        {
            realDamage = 0;
        }

        takeDamage(realDamage);
    }

    public void takeDamage(double damage)
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
}
