using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBase : MonoBehaviour
{
	public GameManager gameManager;

	[SerializeField] private ModuleInputLocker locker;
	public bool IsLocked => null != locker && locker.IsLocked;
    public PickUpAndInspect camLock;

    public int slotIndex = 0;

    protected void ModuleSolved()
    {
	    locker.enabled = false;
        gameManager.FinishModule(slotIndex);
        camLock.UnlockRotation();
    }
}
