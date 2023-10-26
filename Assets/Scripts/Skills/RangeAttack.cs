using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RangeAttack : NetworkBehaviour
{
    public float range = 10.0f;
    public int Damage = 25;
    public float delay = 1.0f;
    private float curTick = 0;
    private bool hasMadeDamage = false;

    // Update is called once per frame
    void Update()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        if (hasMadeDamage)
        {
            return;
        }

        curTick += Time.deltaTime;
        if (curTick >= delay)
        {
            hasMadeDamage = true;
            MakeDamage();
        }
    }

    void MakeDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Respawn"))
            {
                FightingSystem fs = collider.gameObject.GetComponent<FightingSystem>();
                if (fs)
                {
                    fs.RPC_Attacked(Damage, GetComponent<Transform>().position);
                }
            }
        }
    }
}
