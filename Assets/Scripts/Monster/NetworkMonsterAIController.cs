using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;
using Fusion;
using EmeraldAI.Utility;

// Monster的Owner（主客户端）开启AI系统，控制Monster的行为；其他客户端禁用AI系统
public class NetworkMonsterAIController : NetworkBehaviour, IStateAuthorityChanged
{
    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            Debug.Log("MonsterController Spawned, enable EmeraldAI system for " + Runner.LocalPlayer.PlayerId);
            EnableAI();
        }
        else
        {
            Debug.Log("MonsterController Spawned, disable EmeraldAI system");
            DisableAI();
        }
    }

    public void StateAuthorityChanged()
    {
        if (Object.HasStateAuthority)
        {
            Debug.Log("Monster StateAuthority Changed, enable EmeraldAI system for " + Runner.LocalPlayer.PlayerId);
            EnableAI();
        }
        else
        {
            DisableAI();
        }
    }

    private void EnableAI()
    {
        GetComponent<EmeraldAILookAtController>().enabled = true;
        GetComponent<EmeraldAIDetection>().enabled = true;
        GetComponent<EmeraldAIInitializer>().enabled = true;
        GetComponent<EmeraldAIBehaviors>().enabled = true;
        GetComponent<EmeraldAISystem>().enabled = true;
        GetComponent<EmeraldAIEventsManager>().enabled = true;

        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

        if (GetComponent<FightingSystem>())
        {
            try {
                GetComponent<FightingSystem>().InitHealthStatusForAI();
            } catch (System.Exception e) {
                Debug.LogError("InitHealthStatusForAI failed: " + e.Message);
            }
        }
    }

    private void DisableAI()
    {
        GetComponent<EmeraldAIEventsManager>().enabled = false;
        GetComponent<EmeraldAISystem>().enabled = false;
        GetComponent<EmeraldAILookAtController>().enabled = false;
        GetComponent<EmeraldAIDetection>().enabled = false;
        GetComponent<EmeraldAIInitializer>().enabled = false;
        GetComponent<EmeraldAIBehaviors>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
    }
}
