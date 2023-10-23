using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusicManager : MonoBehaviour
{
	public static CombatMusicManager Instance; // 全局访问点

	public AudioSource combatMusic; // 战斗音乐
	private int activeMonsterCount = 0; // 活动的怪物数量

	void Awake()
	{
		// 确保只有一个CombatMusicManager实例
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // 使实例在加载新场景时不被销毁
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// 调用此方法开始音乐
	public void StartCombatMusic()
	{
		activeMonsterCount++;
		if (!combatMusic.isPlaying)
		{
			combatMusic.Play();
		}
	}

	// 调用此方法停止音乐
	public void StopCombatMusic()
	{
		activeMonsterCount--;
		if (activeMonsterCount <= 0 && combatMusic.isPlaying)
		{
			combatMusic.Stop();
			activeMonsterCount = 0; // 重置计数器
		}
	}
}
