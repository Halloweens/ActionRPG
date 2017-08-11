using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour {

    public OnUsable onUsable;

    public void Use()
    {
        if (onUsable != null)
            onUsable.Invoke(new OnUsableArg());
    }
}

[System.Serializable]
public class OnUsableArg
{
    public OnUsableArg()
    {
    }
}

[System.Serializable]
public class OnUsable : UnityEvent<OnUsableArg> { }
