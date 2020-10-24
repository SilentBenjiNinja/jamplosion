using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField_Setter : MonoBehaviour
{
    private string offset = "Vector1_AEA6E811";
    private string emission = "Color_FE400732";
    private string fresnelPower = "Vector1_E4D88293";
    private string scrollPower = "Vector1_4D3EB88F";
    private string pattern = "Texture2D_7E12B800";

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        _renderer.material.SetFloat(offset, 1f + 0.15f * Mathf.Sin(Time.time / 8f));
    }
    
    // private void SetColor() => _renderer.material.SetColor()
}