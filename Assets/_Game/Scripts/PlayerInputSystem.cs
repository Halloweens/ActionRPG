using UnityEngine;
using System.Collections;
using System;

class PlayerInputSystem : InputSystem
{
    public Camera playerCamera = null;

    public override float GetForward()
    {
        return Input.GetAxis("Vertical");
    }

    public override float GetStrafe()
    {
        return Input.GetAxis("Horizontal");
    }

    public override Vector3 GetLookDir()
    {
        if (playerCamera)
            return playerCamera.transform.forward;

        return Vector3.forward;
    }
}
