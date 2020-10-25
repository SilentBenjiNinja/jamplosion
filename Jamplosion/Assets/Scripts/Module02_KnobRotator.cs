using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Module02_KnobRotator : MonoBehaviour
{
    public Action<float> onValueChanged;
    private Module02_Observer observer;

    private Vector2 startPos;
    private int currentStep = 0;

    private int randomizer;
    public int realStep => (currentStep + randomizer) % rotationSteps;
    public float shaderFactor => (float)realStep / (float)rotationSteps;

    [SerializeField] private float distanceMod = 5f;
    [SerializeField] private int rotationSteps = 10;

    private int stepAtDragStart = 0;

    private void Awake()
    {
        observer = GetComponentInParent<Module02_Observer>();
        currentStep = Random.Range(0, rotationSteps);
        randomizer = Random.Range(0, rotationSteps);
    }

    private void Start()
    {
        RotateKnob(currentStep);
        onValueChanged?.Invoke(shaderFactor);
    }

    private void OnMouseDown()
    {
        startPos = Input.mousePosition;
        stepAtDragStart = currentStep;
    }

    private void OnMouseDrag()
    {
        var delta = (Vector2)Input.mousePosition - startPos;

        float traveledDistance = (delta.x + delta.y) / distanceMod;
        int step = Mathf.FloorToInt(traveledDistance);
        step += stepAtDragStart;
        step = Mathf.Clamp(step, 0, rotationSteps);

        RotateKnob(step);

        if (step != currentStep)
        {
            currentStep = step;
            onValueChanged?.Invoke(shaderFactor);
        }
    }

    void RotateKnob(int steps)
    {
        var degrees = 270 / rotationSteps * steps;
        var rotation = new Vector3(degrees, 90, 0);
        transform.localEulerAngles = rotation;
    }
}