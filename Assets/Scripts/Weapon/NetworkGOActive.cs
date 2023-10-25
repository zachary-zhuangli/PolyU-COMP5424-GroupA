using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkGOActive : NetworkBehaviour
{
    [Networked(OnChanged = nameof(ActiveChanged))]
    public bool active { get; set; } = true;

    private static void ActiveChanged(Changed<NetworkGOActive> changed)
    {
        changed.Behaviour.gameObject.SetActive(changed.Behaviour.active);
    }
}
