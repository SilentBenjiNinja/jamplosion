using System;
using UnityEngine;

namespace Vfx.KnobRiddle
{
    [RequireComponent(typeof(MeshRenderer))]
    public class KnobRiddle_Setter : MonoBehaviour
    {
#pragma warning disable CS0414
#pragma warning disable CS0649

        private string input1 = "Vector1_8FC680A2";
        private string input2 = "Color_FE400732";
        private string input3 = "Vector1_E4D88293";
        private string speed = "Vector1_51541A47";

        private MeshRenderer _renderer;

#pragma warning restore CS0141
#pragma warning restore CS0649

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void SetInput1(float value) => _renderer.material.SetFloat(input1, value);
        public void SetInput2(float value) => _renderer.material.SetFloat(input2, value);
        public void SetInput3(float value) => _renderer.material.SetFloat(input3, value);
    }
}