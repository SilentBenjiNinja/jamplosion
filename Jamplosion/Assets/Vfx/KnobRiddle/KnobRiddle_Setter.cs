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
        private string input2 = "Vector1_B91E0812";
        private string input3 = "Vector1_88DC087B";
        private string speed = "Vector1_51541A47";

        private MeshRenderer _renderer;

        [SerializeField] [Range(0f, 1f)] private float knob1 = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float knob2 = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float knob3 = 0.5f;
        [SerializeField] [Range(1f, 10f)] private float speedValue = 1f;

#pragma warning restore CS0141
#pragma warning restore CS0649

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();

            SetInput1(knob1);
            SetInput2(knob2);
            SetInput3(knob3);
            SetSpeed(speedValue);
        }

        public void SetInput1(float value) => _renderer.material.SetFloat(input1, value);
        public void SetInput2(float value) => _renderer.material.SetFloat(input2, value);
        public void SetInput3(float value) => _renderer.material.SetFloat(input3, value);
        public void SetSpeed(float value) => _renderer.material.SetFloat(speed, value);
    }
}