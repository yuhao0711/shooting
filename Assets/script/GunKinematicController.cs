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

        // 確保 Rigidbody 和 XRGrabInteractable 組件存在
        if (rb == null)
        {
            Debug.LogError("Rigidbody 組件未找到！");
            return;
        }

        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable 組件未找到！");
            return;
        }

        // 訂閱抓取和釋放事件
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // 初始化時設置為靜態
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
