using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[System.Serializable]
public class MonsterSpawnInfo
{
    public NetworkPrefabRef monsterPrefab;
    public GameObject spwanPoint;
    public bool needSpawnMonster;
}

// TODO: 增加怪物复活逻辑
public class MonsterSpawner : NetworkBehaviour
{
    [SerializeField] public List<MonsterSpawnInfo> monsterSpawnInfo = new List<MonsterSpawnInfo>();
    // private List<NetworkObject> monsters = new List<NetworkObject>();

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        monsterSpawnInfo.ForEach(spawnInfo =>
        {
            if (spawnInfo.needSpawnMonster)
            {
                spawnInfo.needSpawnMonster = false;
                Debug.Log("SpawnMonster");
                SpawnMonster(spawnInfo);
            }
        });
    }

    private void SpawnMonster(MonsterSpawnInfo spawnInfo)
    {
        if (spawnInfo.monsterPrefab == null)
        {
            Debug.LogError("Monster prefab is empty");
            return;
        }

        var monster = Runner.Spawn(spawnInfo.monsterPrefab, spawnInfo.spwanPoint.transform.position, Quaternion.identity, Runner.LocalPlayer);
        // monsters.Add(monster);
    }
}
