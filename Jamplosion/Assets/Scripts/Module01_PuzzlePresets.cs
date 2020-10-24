using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Module01_PuzzlePresets : ScriptableObject
{
    //[SerializeField] public bool[][] puzzles;
    public Puzzle[] puzzles;

    public bool[] GetRandomPuzzlePreset()
    {
        int i = UnityEngine.Random.Range(0, puzzles.Length);

        return puzzles[i].preset;
    }

    [Serializable]
    public class Puzzle
    {
        public bool[] preset;
    }

    private void OnValidate()
    {
        foreach (var puzzle in puzzles)
        {
            if (puzzle.preset != null && puzzle.preset.Length != 16)
            {
                puzzle.preset = new bool[16];
            }
        }        
    }
}