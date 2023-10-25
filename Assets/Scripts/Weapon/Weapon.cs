using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class Weapon : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject leftHandModel;
    public GameObject rightHandModel;
    public Grabbable grabbableBow;
    public Grabbable grabbableSword;
    public Grabber grabberLeft;
    public Grabber grabberRight;
    public Follow bowFollow;
    public Follow swordFollow;
    public GameObject bowObj;
    public GameObject swordObj;

    private int index = 0; // 0: nothing, 1: sword, 2: bow

    private Vector3 followedSwordPosBackup;
    private Vector3 followedSwordEulerAnglesBackup;

    public void ChangeWeapon(int weaponIndex)
    {
        if (weaponIndex == 1)
        {
            // hold sword
            ReleaseLeftHand();
            ReleaseRightHand();
            DisableSwordFollow();
            EnableBowFollow();
            grabbableSword.enabled = true;
            grabbableBow.enabled = false;

            grabberRight.GrabGrabbable(grabbableSword);

            SetActive(swordObj, true);
            SetActive(bowObj, false);
            SetActive(leftHandModel, true);
        }
        else if (weaponIndex == 2)
        {
            // hold bow
            ReleaseLeftHand();
            ReleaseRightHand();
            EnableSwordFollow();
            DisableBowFollow();
            grabbableSword.enabled = false;
            grabbableBow.enabled = true;

            grabberLeft.GrabGrabbable(grabbableBow);

            SetActive(swordObj, false);
            SetActive(bowObj, true);
            SetActive(leftHandModel, false);
        }
        else
        {
            // hold nothing
            ReleaseLeftHand();
            ReleaseRightHand();
            EnableSwordFollow();
            EnableBowFollow();
            grabbableSword.enabled = false;
            grabbableBow.enabled = false;

            SetActive(swordObj, false);
            SetActive(bowObj, false);
            SetActive(leftHandModel, true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) || InputBridge.Instance.XButtonDown)
        {
            index++;
            index = index % 3;
            ChangeWeapon(index);
        }
    }

    private void ReleaseRightHand()
    {
        grabberRight.TryRelease();
        grabbableSword.transform.SetParent(swordFollow.transform);
    }

    private void ReleaseLeftHand()
    {
        grabberLeft.TryRelease();
        grabbableBow.transform.SetParent(bowFollow.transform);
    }

    private void DisableSwordFollow()
    {
        swordFollow.enabled = false;
        if (followedSwordEulerAnglesBackup == null)
        {
            followedSwordEulerAnglesBackup = new Vector3(swordObj.transform.localEulerAngles.x, swordObj.transform.localEulerAngles.y, swordObj.transform.localEulerAngles.z);
        }
    }

    private void EnableSwordFollow()
    {
        swordFollow.enabled = true;
        swordObj.transform.localPosition = Vector3.zero;
        swordObj.transform.localEulerAngles = followedSwordEulerAnglesBackup;
    }

    private void DisableBowFollow()
    {
        bowFollow.enabled = false;
    }

    private void EnableBowFollow()
    {
        bowFollow.enabled = true;
        bowObj.transform.localPosition = Vector3.zero;
        bowObj.transform.localEulerAngles = Vector3.zero;
    }

    private void SetActive(GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
        if (gameObject.GetComponent<NetworkGOActive>())
        {
            gameObject.GetComponent<NetworkGOActive>().active = active;
        }
    }
}
