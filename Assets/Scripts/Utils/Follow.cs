using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotate = this.transform.GetChild(0).localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position;
        this.transform.rotation = target.rotation;
}
}
