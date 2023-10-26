using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Fusion;

public class SwordSkills : NetworkBehaviour
{
    public NetworkPrefabRef[] effects;
    private NetworkObject lastSkillNO;
    // Start is called before the first frame update
    void Start()
    {

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
            ReleaseSkill(0);
        }
    }

    void ReleaseSkill(int skillIndex)
    {
        DestroySkillGO();
        lastSkillNO = Runner.Spawn(effects[skillIndex],
            gameObject.transform.position - Vector3.up * 1f,
            Quaternion.Euler(new Vector3(0, gameObject.transform.rotation.y, 0)), Runner.LocalPlayer);
        lastSkillNO.gameObject.transform.localScale = new Vector3(2, 1, 2);
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
