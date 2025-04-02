using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunPickupTrigger : MonoBehaviour
{
    public GameObject realGun; // 要生成或啟用的真槍

    private void OnEnable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(OnGrab);
    }

    private void OnDisable()
    {
        GetComponent<XRGrabInteractable>().selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (realGun != null)
        {
            realGun.SetActive(true); // 啟用真正能射擊的槍
            Destroy(gameObject);     // 刪除桌上槍
        }
    }
}

