using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using BNG;

public class WeaponSpawner : NetworkBehaviour
{
    public Weapon weaponManager;
    public NetworkPrefabRef swordPrefab = NetworkPrefabRef.Empty;
    public NetworkPrefabRef bowPrefab = NetworkPrefabRef.Empty;

    private NetworkObject swordNO;
    private NetworkObject bowNO;

    public override void Spawned()
    {
        swordNO = Runner.Spawn(swordPrefab, new Vector3(0, 0, 0), Quaternion.identity, Runner.LocalPlayer);
        bowNO = Runner.Spawn(bowPrefab, new Vector3(0, 0, 0), Quaternion.identity, Runner.LocalPlayer);
        swordNO.gameObject.transform.SetParent(GameObject.Find("SwordContainer").transform);
        bowNO.gameObject.transform.SetParent(GameObject.Find("BowContainer").transform);
        SetWeaponManager();
    }

    private void SetWeaponManager()
    {
        weaponManager.weapons[0] = swordNO.gameObject;
        weaponManager.weapons[1] = bowNO.gameObject;
        weaponManager.grabbableBow = bowNO.GetComponent<Grabbable>();
        weaponManager.grabbableSword = swordNO.GetComponent<Grabbable>();
        weaponManager.swordObj = swordNO.gameObject;
        weaponManager.bowObj = bowNO.gameObject;
        weaponManager.ChangeWeapon(0);
    }
}
