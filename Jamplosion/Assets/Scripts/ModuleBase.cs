using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBase : MonoBehaviour
{
    public GameManager gameManager;

    public PickUpAndInspect camLock;

    public int slotIndex = 0;

    protected void ModuleSolved()
    {
        gameManager.FinishModule(slotIndex);
        camLock.UnlockRotation();
    }
}
