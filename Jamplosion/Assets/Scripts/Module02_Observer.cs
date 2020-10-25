using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vfx.KnobRiddle;

public class Module02_Observer : ModuleBase
{
    private KnobRiddle_Setter shader;

    [SerializeField] private Module02_KnobRotator knob1;
    [SerializeField] private Module02_KnobRotator knob2;
    [SerializeField] private Module02_KnobRotator knob3;

    private void Awake()
    {
        shader = GetComponentInChildren<KnobRiddle_Setter>();

        if (knob1)
        {
            knob1.onValueChanged += SetInput1;
            knob1.onValueChanged += DeactivateIfSolved;
            // knob1.onValueChanged += DebugKnob;
        }

        if (knob2)
        {
            knob2.onValueChanged += SetInput2;
            knob2.onValueChanged += DeactivateIfSolved;
            // knob2.onValueChanged += DebugKnob;
        }

        if (knob3)
        {
            knob3.onValueChanged += SetInput3;
            knob3.onValueChanged += DeactivateIfSolved;
            // knob3.onValueChanged += DebugKnob;
        }
    }

    private void OnDestroy()
    {
        if (!shader)
            return;

        if (knob1)
        {
            knob1.onValueChanged -= SetInput1;
            knob1.onValueChanged -= DeactivateIfSolved;
        }

        if (knob2)
        {
            knob2.onValueChanged -= SetInput2;
            knob2.onValueChanged -= DeactivateIfSolved;
        }

        if (knob3)
        {
            knob3.onValueChanged -= SetInput3;
            knob3.onValueChanged -= DeactivateIfSolved;
        }
    }

    public void SetInput1(float value) => shader.SetInput1(value);
    public void SetInput2(float value) => shader.SetInput2(value);
    public void SetInput3(float value) => shader.SetInput3(value);

    private bool Solved()
    {
        int solution = knob1.realStep;

        if (solution == knob2.realStep && solution == knob3.realStep)
            return true;

        return false;
    }

    public void DeactivateIfSolved(float nonSense)
    {
        if (Solved())
        {
            ModuleSolved();
        }
    }

    public void DebugKnob(float value) => Debug.Log("knob says " + value);
}