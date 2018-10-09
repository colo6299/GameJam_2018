using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour {

    public bool clicked = false;
    public bool dragged = false;
    public bool dropped = false;
    public bool isKey = false;
    public bool locked = false;
    public static bool fullLock = false;

    private bool last = false;
    private bool lastDrag = false;
    private bool calcLock = false;

    void Update()
    {
        calcLock = fullLock | locked;
        dropped = false;
        DragCheck();
        clicked = false;
    }


    public void Click()
    {
        if (calcLock && !isKey)
        {
            return;
        }
        clicked = true;
    }

    public void LockAll()
    {
        fullLock = true;
    }

    public void UnlockAll()
    {
        fullLock = false;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    void DragCheck()
    {
        if (clicked)
        {
            dragged = true;
        }

        if (!Input.GetMouseButton(0) && dragged)
        {
            dragged = false;
            dropped = true;
        }
    }
}