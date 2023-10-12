using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// TODO: 增加怪物复活逻辑
public class MonsterSpawner : NetworkBehaviour
{
    [SerializeField] public NetworkPrefabRef MonsterPrefab = NetworkPrefabRef.Empty;
    public GameObject spwanPoint;

    public bool needSpawnMonster = true;

    // private List<NetworkObject> monsters = new List<NetworkObject>();

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
        {
            needSpawnMonster = false;
            return;
        }

        if (needSpawnMonster)
        {
            Debug.Log("SpawnMonster");
            SpawnMonster();
            needSpawnMonster = false;
        }
    }

    private void SpawnMonster()
    {
        if (MonsterPrefab == NetworkPrefabRef.Empty)
        {
            Debug.LogError("Monster prefab is empty");
            return;
        }

        var monster = Runner.Spawn(MonsterPrefab, spwanPoint.transform.position, Quaternion.identity, Runner.LocalPlayer);
        // monsters.Add(monster);
    }
}
