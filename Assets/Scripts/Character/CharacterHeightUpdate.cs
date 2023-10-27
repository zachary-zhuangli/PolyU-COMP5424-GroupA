using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using RootMotion.FinalIK;


public class CharacterHeightUpdate : MonoBehaviour
{
    public float offset = 1.55f;
    private Transform headTargetTransform;

    void Update()
    {
        if (headTargetTransform == null)
        {
            headTargetTransform = GetComponent<VRIK>().solver.spine.headTarget;
        }
        else
        {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x,
                headTargetTransform.position.y - offset,
                gameObject.transform.position.z
            );
        }
    }
}
