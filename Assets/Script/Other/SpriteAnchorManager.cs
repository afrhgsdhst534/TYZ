using UnityEngine;
using System.Collections.Generic;
[ExecuteAlways]
public class SpriteAnchorManager : MonoBehaviour
{
    private List<SpriteAnchor> registeredObjects = new List<SpriteAnchor>();
    private int lastScreenWidth = -1;
    private int lastScreenHeight = -1;
    private void LateUpdate()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            NotifyAll();
        }
    }
    public void Register(SpriteAnchor obj)
    {
        if (!registeredObjects.Contains(obj))
            registeredObjects.Add(obj);
    }
    public void Unregister(SpriteAnchor obj)
    {
        if (registeredObjects.Contains(obj))
            registeredObjects.Remove(obj);
    }
    public void NotifyAll()
    {
        foreach (var obj in registeredObjects)
        {
            if (obj != null && obj.useAutoFit)
                obj.Recalculate();
        }
    }
}