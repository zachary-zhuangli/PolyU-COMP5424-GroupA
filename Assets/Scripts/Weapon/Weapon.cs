using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class Weapon : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    private int index;
    public GameObject obj;
    public Grabbable grabbable;
    public Grabber grabber;
    public Follow follow;
    public GameObject swordObj;
    public GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        follow.enabled = false;
        grabber.GrabGrabbable(grabbable);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.U) || InputBridge.Instance.AButtonDown)
        //{
        //    follow.enabled = false;
        //    grabber.GrabGrabbable(grabbable);
        //}
        //if (Input.GetKeyDown(KeyCode.Y) || InputBridge.Instance.BButtonDown)
        //{
        //    print("111");
        //    //grabbable.Release(Vector3.zero,Vector3.zero);
        //    //grabbable.ResetGrabbing();
        //    grabber.TryRelease();
        //    follow.enabled = true;
        //}
        if (Input.GetKeyDown(KeyCode.T) || InputBridge.Instance.XButtonDown)
        {
            index++;
            if (index == weapons.Count)
            {
                index = 0;
            }
            print(index);
            if (index == 0)
            {
                grabbable.enabled = true;
                follow.enabled = false;
                grabber.GrabGrabbable(grabbable);
            }
            if (index == 1)
            {
                grabber.TryRelease();
                follow.enabled = true;
                grabbable.enabled = false;
                grabbable.transform.parent = follow.transform;
                grabbable.transform.localPosition = Vector3.zero;
                grabbable.transform.localEulerAngles = Vector3.zero;
                swordObj.SetActive(true);
                rightHand.SetActive(false);
            }
            if (index == 2)
            {
                rightHand.SetActive(true);
                swordObj.SetActive(false);
            }
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[index].SetActive(true);
        }
    }
}
