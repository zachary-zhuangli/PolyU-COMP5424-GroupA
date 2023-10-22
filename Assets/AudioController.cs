using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MusicController : MonoBehaviour
{
	public AudioSource combatMusic; // 指向AudioSource组件的引用，该组件播放战斗音乐

	// 在Unity编辑器中设置或在Start/Awake方法中通过代码获取
	void Start()
	{
		// 如果combatMusic未设置，则尝试从同一游戏对象中获取
		if (combatMusic == null)
		{
			combatMusic = GetComponent<AudioSource>();
		}
	}

	// 调用此方法播放音乐
	public void PlayCombatMusic()
	{
		if (combatMusic != null && !combatMusic.isPlaying)
		{
			combatMusic.Play();
		}
		else
		{
			Debug.LogWarning("combatMusic is null or already playing.");
		}
	}

	// 调用此方法停止音乐
	public void StopCombatMusic()
	{
		if (combatMusic != null && combatMusic.isPlaying)
		{
			combatMusic.Stop();
		}
		else
		{
			Debug.LogWarning("combatMusic is null or already stopped.");
		}
	}
}//在这个脚本中，combatMusic 是一个 AudioSource 组件的引用，它应该设置为播放战斗音乐的音频源。您可以在 Unity 编辑器中将 AudioSource 组件拖放到此引用上，或者如果 AudioSource 和此脚本在同一个游戏对象上，它将在 Start 方法中自动获取。

//PlayCombatMusic 和 StopCombatMusic 方法可以从您的其他脚本中调用，以开始和停止音乐播放。确保您的 AudioSource 有一个音频剪辑设置为播放，并且 AudioSource 不是静音的，音量已经调整到合适的水平。