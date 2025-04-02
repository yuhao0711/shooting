using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunPickupTrigger : MonoBehaviour
{
    public GameObject realGun; // �n�ͦ��αҥΪ��u�j

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
            realGun.SetActive(true); // �ҥίu����g�����j
            Destroy(gameObject);     // �R����W�j
        }
    }
}

