using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SwordVibration : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		// 检查碰撞的对象是否是带有"Respawn"标签的对象
		if (collision.gameObject.CompareTag("Respawn"))
		{
			// 当与带有"Respawn"标签的对象发生碰撞时，触发手柄震动
			InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
			device.SendHapticImpulse(0, 1.0f, 0.3f);
		}
	}

}