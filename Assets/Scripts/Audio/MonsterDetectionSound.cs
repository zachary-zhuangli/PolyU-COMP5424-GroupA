using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

// TODO: 联机时非host客户端音乐同步
public class MonsterDetectionSound : MonoBehaviour
{
	public AudioClip detectionSound;
	public bool disableEmbienceMusic = true;

	private AudioSource audioSource;
	private EmeraldAISystem emeraldAI;

	public static bool isPlaying = false;

	private GameObject ambienceMusicGO;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}

		// 设置AudioSource的属性
		// audioSource.clip = detectionSound;
		audioSource.playOnAwake = false;

		// 获取EmeraldAISystem的引用，并设置死亡事件的监听器
		emeraldAI = GetComponent<EmeraldAISystem>();
		if (emeraldAI != null)
		{
			emeraldAI.OnStartCombatEvent.AddListener(OnDetection);
			emeraldAI.DeathEvent.AddListener(OnDeath);
		}
		else
		{
			Debug.LogError("EmeraldAISystem component not found on the monster.");
		}

		ambienceMusicGO = GameObject.Find("AmbienceMusic");
	}

	void OnDetection()
	{
		if (MonsterDetectionSound.isPlaying)
		{
			return;
		}

		if (audioSource != null && detectionSound != null)
		{
			MonsterDetectionSound.isPlaying = true;
			audioSource.Stop();
			audioSource.clip = detectionSound;
			audioSource.PlayOneShot(detectionSound);

			if (disableEmbienceMusic)
			{
				ambienceMusicGO.SetActive(false);
			}
		}
		else
		{
			Debug.LogError("AudioSource or DetectionSound is missing.");
		}
	}

	void OnDeath()
	{
		MonsterDetectionSound.isPlaying = false;
		if (disableEmbienceMusic)
		{
			ambienceMusicGO.SetActive(true);
		}
	}
}
