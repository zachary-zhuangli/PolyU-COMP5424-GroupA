using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Fusion;

public class SwordSkills : NetworkBehaviour
{
    public NetworkPrefabRef[] effects;
    private Transform headTargetTransform;
    private NetworkObject lastSkillNO;
    // Start is called before the first frame update
    void Start()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        headTargetTransform = GameObject.Find("HeadTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) || InputBridge.Instance.AButtonDown)
        {
            ReleaseASkill();
        }
        else if (Input.GetKeyDown(KeyCode.F) || InputBridge.Instance.BButtonDown)
        {
            ReleaseBSkill();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) || InputBridge.Instance.RightGripDown) {
            ReleaseGribSkill();
        }
    }

    void ReleaseGribSkill()
    {
        DestroySkillGO();
        lastSkillNO = Runner.Spawn(effects[0],
            gameObject.transform.position - Vector3.up * 1.5f,
            headTargetTransform.rotation, Runner.LocalPlayer);
        lastSkillNO.gameObject.transform.localScale = new Vector3(2, 0.8f, 2);
    }

    void ReleaseASkill()
    {
        DestroySkillGO();
        lastSkillNO = Runner.Spawn(effects[1],
            gameObject.transform.position - Vector3.up * 0.5f,
            headTargetTransform.rotation, Runner.LocalPlayer);
        lastSkillNO.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    void ReleaseBSkill()
    {
        DestroySkillGO();
        lastSkillNO = Runner.Spawn(effects[2],
            gameObject.transform.position - Vector3.up * 1.5f,
            headTargetTransform.rotation, Runner.LocalPlayer);
        lastSkillNO.gameObject.transform.localScale = new Vector3(2, 2, 2);
    }

    void DestroySkillGO()
    {
        if (lastSkillNO != null)
        {
            Runner.Despawn(lastSkillNO);
            lastSkillNO = null;
        }
    }
}
