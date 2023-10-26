using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

// TODO: 联机时非host客户端音乐同步
public class MonsterDeathSound : MonoBehaviour
{
	public AudioClip deathSound; // 怪物的死亡声音剪辑
	private AudioSource audioSource; // 用于播放声音的AudioSource组件
	private EmeraldAISystem emeraldAI; // Emerald AI系统的引用

	void Start()
	{
		// 获取或创建AudioSource组件
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}

		// 设置AudioSource的属性
		// audioSource.clip = deathSound;
		audioSource.playOnAwake = false;

		// 获取EmeraldAISystem的引用，并设置死亡事件的监听器
		emeraldAI = GetComponent<EmeraldAISystem>();
		if (emeraldAI != null)
		{
			emeraldAI.DeathEvent.AddListener(OnDeath);
		}
		else
		{
			Debug.LogError("EmeraldAISystem component not found on the monster.");
		}
	}

	// 当怪物死亡时调用此方法
	void OnDeath()
	{
		// 播放死亡声音
		if (audioSource != null && deathSound != null)
		{
			audioSource.Stop();
			audioSource.clip = deathSound;
			audioSource.PlayOneShot(deathSound);
		}
		else
		{
			Debug.LogError("AudioSource or deathSound is missing.");
		}
	}
}
