using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SwordAttackAudio : NetworkBehaviour
{
	public AudioSource attackAudioSource; // 拖动你的AudioSource组件到这里
	public AudioClip shieldClip;          // 剑与盾碰撞的声音
	public AudioClip monsterClip;         // 剑与怪物碰撞的声音

	private float lastShieldHitTime = -0.3f;   // 记录上一次与盾碰撞的时间
	private float cooldownTime = 0.3f;        // 与盾碰撞的冷却时间

	// 当开始发生碰撞时，Unity会自动调用此函数
	void OnCollisionEnter(Collision collision)
	{
		OnCollisionEvent(collision.gameObject);
	}

	void OnTriggerEnter(Collider collider)
	{
		OnCollisionEvent(collider.gameObject);
	}

	void OnCollisionEvent(GameObject gameObject)
	{
		// 检查碰撞的对象是否是盾
		if (gameObject.CompareTag("Shield"))
		{
			if (Time.time - lastShieldHitTime >= cooldownTime)
			{
				attackAudioSource.clip = shieldClip;
				attackAudioSource.Play();
				lastShieldHitTime = Time.time; // 更新上一次与盾碰撞的时间
			}
		}
		// 检查碰撞的对象是否是怪物
		else if (gameObject.CompareTag("Respawn"))
		{
			attackAudioSource.clip = monsterClip;
			attackAudioSource.Play();
		}

		// if (!Object.HasStateAuthority) {
        //     // TODO：其它玩家发出的武器攻击声音，可以减小音量
        // }
	}
}