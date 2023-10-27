using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberCode : MonoBehaviour
{
    public GameObject climbObj;
    public PlayerSpawner playerSpawner;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name== "Terrain_0_0-20231017 - 214110")
        {
            GameObject obj = GameObject.Instantiate(climbObj,this.transform.position,this.transform.rotation);
            obj.SetActive(true);
            Destroy(obj,20);

            // 攀爬时禁用plantFeet（使人物avatar的脚可以离开地面）
            // TODO: 后续实现攀爬完成后重置plantFeet状态（phase3）
            if (playerSpawner?.localPlayer) {
                playerSpawner.localPlayer.GetComponent<CharacterIKSetup>().SetFeetPlant(false);
            }
        }
    }
}
