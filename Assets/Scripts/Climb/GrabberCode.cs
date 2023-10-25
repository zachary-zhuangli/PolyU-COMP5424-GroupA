using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberCode : MonoBehaviour
{
    public GameObject climbObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name== "Terrain_0_0-20231017 - 214110")
        {
            GameObject obj = GameObject.Instantiate(climbObj,this.transform.position,this.transform.rotation);
            obj.SetActive(true);
            Destroy(obj,20);
        }
    }
}
