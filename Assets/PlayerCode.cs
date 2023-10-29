using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCode : MonoBehaviour
{
    public Image blood;
    public PlayerSpawner playerSpawner;
    // Start is called before the first frame update
    void Start()
    {
        //blood.fillAmount = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerSpawner.localPlayer)
        {
            FightingSystem fs = playerSpawner.localPlayer.GetComponent<FightingSystem>();
            int hp = fs.hp;    // 当前血量
            int maxHp = fs.maxHp;    // 最大血量
            float temphp = hp / maxHp;
            blood.fillAmount = temphp;
        }
        else
        {
            // 当前还没有初始化玩家
            //blood.gameObject.SetActive(false);
        }
    }
}
