using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vfx.ForceField
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ForceField_Setter : MonoBehaviour
    {
#pragma warning disable CS0414
#pragma warning disable CS0649
        [ColorUsage(true, true)] [SerializeField]
        private Color standardColor;

        [ColorUsage(true, true)] [SerializeField]
        private Color alertColor;

        private string offset = "Vector1_AEA6E811";
        private string emission = "Color_FE400732";
        private string fresnelPower = "Vector1_E4D88293";
        private string scrollPower = "Vector1_4D3EB88F";
        private string pattern = "Texture2D_7E12B800";

        private MeshRenderer _renderer;

#pragma warning restore CS0141
#pragma warning restore CS0649

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void Start() => SetStandardColor();

        void Update()
        {
            _renderer.material.SetFloat(offset, 1f + 0.15f * Mathf.Sin(Time.time / 8f));
        }

        private void OnMouseEnter() => SetAlertColor();

        private void OnMouseExit() => SetStandardColor();
        private void SetStandardColor() => _renderer.material.SetColor(emission, standardColor);

        private void SetAlertColor() => _renderer.material.SetColor(emission, alertColor);
    }
}