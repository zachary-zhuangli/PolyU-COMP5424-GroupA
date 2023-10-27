using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Fusion;

public class SwordVibration : NetworkBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		if (!Object.HasStateAuthority) {
            return;
        }

		// 检查碰撞的对象是否是带有"Respawn"标签的对象
		if (collision.gameObject.CompareTag("Respawn"))
		{
			// 当与带有"Respawn"标签的对象发生碰撞时，触发手柄震动
			RightHandHapticImpulse();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!Object.HasStateAuthority) {
            return;
        }

		if (collider.gameObject.CompareTag("Respawn"))
		{
			RightHandHapticImpulse();
		}
	}

    void RightHandHapticImpulse()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        device.SendHapticImpulse(0, 1.0f, 0.3f);
    }

}