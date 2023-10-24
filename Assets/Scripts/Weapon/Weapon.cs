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
    public Grabbable grabbable1;
    public Grabbable grabbable2;
    public Grabber grabber;
    public Grabber grabber2;
    public Follow follow;
    public Follow follow1;
    public Follow follow2;
    public GameObject swordObj;
    public GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        follow.enabled = false;
        follow1.enabled = true;
        follow2.enabled = true;
        grabber.GrabGrabbable(grabbable);
        grabbable1.gameObject.SetActive(false);
        grabbable2.gameObject.SetActive(false);
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
                // rightHand.SetActive(false);

                follow1.enabled = false;
                follow2.enabled = false;
                grabber.GrabGrabbable(grabbable1);
                grabber2.GrabGrabbable(grabbable2);
                grabbable1.enabled = true;
                grabbable2.enabled = true;
            }
            if (index == 2)
            {
                rightHand.SetActive(true);
                swordObj.SetActive(false);

                grabber.TryRelease();
                grabber2.TryRelease();
                follow1.enabled = true;
                follow2.enabled = true;
                grabbable1.enabled = false;
                grabbable2.enabled = false;
                grabbable1.transform.parent = follow1.transform;
                grabbable1.transform.localPosition = Vector3.zero;
                grabbable1.transform.localEulerAngles = follow1.GetComponent<Follow>().rotate;
                grabbable2.transform.parent = follow2.transform;
                grabbable2.transform.localPosition = Vector3.zero;
                grabbable2.transform.localEulerAngles = follow2.GetComponent<Follow>().rotate;

            }
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[index].SetActive(true);
        }
    }
}
