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
    }

    void ReleaseASkill()
    {
        DestroySkillGO();
        lastSkillNO = Runner.Spawn(effects[0],
            gameObject.transform.position - Vector3.up * 1f,
            headTargetTransform.rotation, Runner.LocalPlayer);
        lastSkillNO.gameObject.transform.localScale = new Vector3(2, 1, 2);
    }

    void ReleaseBSkill()
    {
        DestroySkillGO();
        lastSkillNO = Runner.Spawn(effects[1],
            gameObject.transform.position - Vector3.up * 1f,
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
