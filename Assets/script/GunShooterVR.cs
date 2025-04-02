using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunShooterVR : MonoBehaviour
{
    public InputActionProperty triggerAction; // 綁定 Trigger 按鈕（XRI RightHand / Select）
    public SimpleShoot simpleShoot;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        triggerAction.action.Enable();
    }

    void OnDisable()
    {
        triggerAction.action.Disable();
    }

    void Update()
    {
        if (grab != null && grab.isSelected)
        {
            if (triggerAction.action.WasPressedThisFrame())
            {
                simpleShoot.Shoot(); // 射擊！💥
            }
        }
    }
}
