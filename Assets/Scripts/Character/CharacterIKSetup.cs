using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using RootMotion.FinalIK;
using UnityEngine;

// 本地玩家的Avatar IK target直接使用本地的XR设备数据
// 网络玩家的Avatar IK target使用通过网络同步过来的XR设备数据
public class CharacterIKSetup : NetworkBehaviour
{
    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            Debug.Log("set target for player VRIK");
            GetComponent<VRIK>().solver.spine.headTarget = GameObject.Find("HeadTarget").transform;
            GetComponent<VRIK>().solver.leftArm.target = GameObject.Find("LeftHandTarget").transform;
            GetComponent<VRIK>().solver.rightArm.target = GameObject.Find("RightHandTarget").transform;
        }
        else
        {
            var netPlayer = GetComponent<NetworkObject>().StateAuthority;

            // 获取对应玩家的XR设备位姿数据同步对象
            var sync = FindXRSyncObjectFromPlayerId(netPlayer.PlayerId);
            if (sync)
            {
                Debug.Log("set target for network player VRIK");
                GetComponent<VRIK>().solver.spine.headTarget = sync.transform.Find("PlayerHeadTarget");
                GetComponent<VRIK>().solver.leftArm.target = sync.transform.Find("PlayerLeftHandTarget");
                GetComponent<VRIK>().solver.rightArm.target = sync.transform.Find("PlayerRightHandTarget");
            }
            else
            {
                Debug.LogError("can not find XRTargetTransformSync for network player: " + netPlayer.PlayerId);
            }
        }
    }

    private GameObject FindXRSyncObjectFromPlayerId(int playerId)
    {
        return GameObject
            .FindGameObjectsWithTag("GameController")
            .ToList<GameObject>()
            .Find(
                sync => sync.GetComponent<XRTargetTransformSync>() && sync.GetComponent<NetworkObject>().StateAuthority.PlayerId == playerId
            );
    }
}
