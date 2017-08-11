using UnityEngine;
using System.Collections;

abstract class InputSystem : MonoBehaviour
{
    public abstract float GetForward();
    public abstract float GetStrafe();
    public abstract Vector3 GetLookDir();
}
