using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBase : MonoBehaviour
{
	public GameManager gameManager;

	public int slotIndex = 0;

	[SerializeField] private ModuleInputLocker locker;
	public bool IsLocked => null != locker && locker.IsLocked;
}
