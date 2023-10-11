using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// 玩家角色Avatar 和 玩家XR设备位姿数据同步组件 的生成器
// 本地和网络玩家角色Avatar的身体姿态 都将通过对应XR设备位姿数据在本地基于IK实时运算
// 网络玩家的手指姿态 将直接通过对应Avatar上手部骨骼的位姿数据同步实现
public class PlayerSpawner : NetworkBehaviour
{
    public NetworkPrefabRef PlayerPrefab = NetworkPrefabRef.Empty;
    public NetworkPrefabRef XRPosPrefab = NetworkPrefabRef.Empty; 

    public override void Spawned()
    {
        if (PlayerPrefab == NetworkPrefabRef.Empty)
        {
            Debug.LogError("Player prefab is empty");
            return;
        }
        if (XRPosPrefab == NetworkPrefabRef.Empty)
        {
            Debug.LogError("XR Pos prefab is empty");
            return;
        }

        Debug.Log("Player and XRTarget spawn: " + Runner.LocalPlayer);
        Runner.Spawn(XRPosPrefab, new Vector3(0, 0, 0), Quaternion.identity, Runner.LocalPlayer);
        Runner.Spawn(PlayerPrefab, new Vector3(0, -1, 0), Quaternion.identity, Runner.LocalPlayer);
    }
}
