using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

// 玩家XR设备位姿数据网络同步组件
// 每一个网络tick将本地玩家的XR设备位姿数据（world transform）记录到该组件，并通过Network Transform组件同步给网络各用户
// 每个客户端将拥有所有玩家的XRTargetTransformSync组件对象，用于查询所有玩家的位姿
public class XRTargetTransformSync : NetworkBehaviour
{
    private GameObject XRHeadTarget;
    private GameObject XRLeftHandTarget;
    private GameObject XRRightHandTarget;

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority && XRHeadTarget)
        {
            SyncXRTransform();
        }
    }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            XRHeadTarget = GameObject.Find("HeadTarget");
            XRLeftHandTarget = GameObject.Find("LeftHandTarget");
            XRRightHandTarget = GameObject.Find("RightHandTarget");
        }
    }

    private void SyncXRTransform()
    {
        var headTrans = XRHeadTarget.transform;
        var leftHandTrans = XRLeftHandTarget.transform;
        var rightHandTrans = XRRightHandTarget.transform;
        transform.Find("PlayerHeadTarget").position = headTrans.position;
        transform.Find("PlayerHeadTarget").rotation = headTrans.rotation;
        transform.Find("PlayerLeftHandTarget").position = leftHandTrans.position;
        transform.Find("PlayerLeftHandTarget").rotation = leftHandTrans.rotation;
        transform.Find("PlayerRightHandTarget").position = rightHandTrans.position;
        transform.Find("PlayerRightHandTarget").rotation = rightHandTrans.rotation;
    }
}
