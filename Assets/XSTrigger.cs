using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XSTrigger : MonoBehaviour
{
    private bool isOpen;
    public GameObject xsUI;
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
        if (other.name == "Grabber" && !isOpen)
        {
            isOpen = true;
            xsUI.SetActive(true);
        }
    }
}
