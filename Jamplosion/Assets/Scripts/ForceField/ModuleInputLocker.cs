using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vfx.ForceField;

public class ModuleInputLocker : MonoBehaviour
{
	public static List<ModuleInputLocker> s_moduleLockers = new List<ModuleInputLocker>();

	[SerializeField] private GameObject forceField;

	private bool isLockedBackingField = false;

	public bool IsLocked
	{
		get => isLockedBackingField;
		private set
		{
			isLockedBackingField = value;
			forceField?.SetActive(value);
		}
	}

	public void SetLocked(bool locked) => IsLocked = locked;

	private void OnEnable() => s_moduleLockers.Add(this);

	private void OnDisable()
	{
		if (s_moduleLockers.Contains(this))
			s_moduleLockers.Remove(this);

		forceField.GetComponent<ForceField_Setter>().isSolved = true;
		forceField.gameObject.SetActive(true);

		UnlockAModuleIfNoneAreUnlocked();
	}

	private static void UnlockAModuleIfNoneAreUnlocked()
	{
		if (IsAnyModuleUnlocked())
			return;

		foreach (var module in s_moduleLockers)
		{
			if (module.gameObject.activeInHierarchy && module.IsLocked)
			{
				module.SetLocked(false);
				return;
			}
		}
	}

	private static bool IsAnyModuleUnlocked()
	{
		foreach (var module in s_moduleLockers)
		{
			if (module.gameObject.activeInHierarchy && !module.IsLocked)
				return true;
		}

		return false;
	}
}

