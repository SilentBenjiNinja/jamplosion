using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

	public static class MyLockDispatcher
	{
		public static void LockSomePuzzles()
		{
			for (int i = 0; i < ModuleInputLocker.s_moduleLockers.Count - 2; i++)
				ModuleInputLocker.s_moduleLockers[i].SetLocked(true);
		}
	}
