using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module01_Observer : MonoBehaviour
{
    [SerializeField] private Module01_Button[] buttons = new Module01_Button[16];

    [SerializeField] private Module01_PuzzlePresets puzzlePresets;
    
    void Start()
    {
        InitializeArrays();
    }

    private void OnValidate()
    {
        InitializeArrays();
    }

    private bool CheckSolved()
    {
        foreach (var child in buttons)
        {
            if (child.isOn == false)
                return false;
        }
        return true;
    }

    public void SomethingChanged()
    {
        if (!CheckSolved())
            return;
        
        //unlock next puzzle...
        print("puzzle solved");
    }

    private void InitializeArrays()
    {
        buttons = GetComponentsInChildren<Module01_Button>();
        
        SelectPuzzle();
    }

    private void SelectPuzzle()
    {
        // select a random puzzle
        bool[] puzzle = puzzlePresets.GetRandomPuzzlePreset();


        for (int i = 0; i < buttons.Length && i < puzzle.Length; i++)
            buttons[i].isOn = puzzle[i];        
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(Module01_Observer))]
    public class Module01_ObserverEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Select Puzzle"))
            {
                ((Module01_Observer)target).SelectPuzzle();
            }
            DrawDefaultInspector();
        }
    }
#endif
}
