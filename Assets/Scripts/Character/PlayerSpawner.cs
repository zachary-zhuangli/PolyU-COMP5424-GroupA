using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using BNG;

// 玩家角色Avatar 和 玩家XR设备位姿数据同步组件 的生成器
// 本地和网络玩家角色Avatar的身体姿态 都将通过对应XR设备位姿数据在本地基于IK实时运算
// 网络玩家的手指姿态 将直接通过对应Avatar上手部骨骼的位姿数据同步实现
public class PlayerSpawner : NetworkBehaviour
{
    public NetworkPrefabRef PlayerPrefab = NetworkPrefabRef.Empty;
    public float AvatarHeight = 1.55f;
    public NetworkPrefabRef XRPosPrefab = NetworkPrefabRef.Empty;
    public GameObject BNGPlayerControllerGO;

    public NetworkObject localPlayer { get; private set; }

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

        Runner.Spawn(XRPosPrefab, new Vector3(0, 0, 0), Quaternion.identity, Runner.LocalPlayer);
        Transform VRHeadTransform = GameObject.Find("HeadTarget").transform;
        this.localPlayer = Runner.Spawn(PlayerPrefab, VRHeadTransform.position - Vector3.up * 1.5f, VRHeadTransform.rotation, Runner.LocalPlayer);
        HideLocalPlayer(this.localPlayer);
        SetCharacterHeight();
    }

    private void HideLocalPlayer(NetworkObject playerGO)
    {
        playerGO.transform.gameObject.layer = LayerMask.NameToLayer("Hide");

        foreach (Transform tran in playerGO.GetComponentsInChildren<Transform>())
        {
            tran.gameObject.layer = LayerMask.NameToLayer("Hide");
        }
    }

    private void SetCharacterHeight()
    {
        CharacterController characterController = BNGPlayerControllerGO.GetComponent<CharacterController>();
        BNGPlayerController bngPlayerController = BNGPlayerControllerGO.GetComponent<BNGPlayerController>();
        if (characterController && bngPlayerController)
        {
            float currentHeight = characterController.height;
            float offset = AvatarHeight - currentHeight;
            bngPlayerController.CharacterControllerYOffset = offset;
        }
        else
        {
            Debug.LogError("CharacterController is null when setup CharacterHeight");
        }
    }
}
