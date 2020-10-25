using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LockDispatcher : MonoBehaviour
{
	void Start() => LockSomePuzzles();

	private void LockSomePuzzles()
	{
		for (int i = 0; i < ModuleInputLocker.s_moduleLockers.Count - 2; i++)
			ModuleInputLocker.s_moduleLockers[i].SetLocked(true);
	}
}
