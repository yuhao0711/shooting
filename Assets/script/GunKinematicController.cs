using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunKinematicController : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // �T�O Rigidbody �M XRGrabInteractable �ե�s�b
        if (rb == null)
        {
            Debug.LogError("Rigidbody �ե󥼧��I");
            return;
        }

        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable �ե󥼧��I");
            return;
        }

        // �q�\����M����ƥ�
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // ��l�Ʈɳ]�m���R�A
        rb.isKinematic = true;
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.isKinematic = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.isKinematic = true;
    }
}
