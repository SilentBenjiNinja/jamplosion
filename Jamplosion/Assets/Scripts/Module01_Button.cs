using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Renderer))]
public class Module01_Button : MonoBehaviour
{
    private Renderer rend;
    private Module01_Observer observer;

    public bool isOn = false;

    [SerializeField] private Color hoverColor = new Color(.5f, .5f, .25f);

    [SerializeField] private Module01_Button[] neighbors;


    void Start()
    {
        observer = GetComponentInParent<Module01_Observer>();

        rend = GetComponent<Renderer>();
        ColorCheck();
    }

    void OnMouseEnter()
    {
        Color sensitiveEmission = rend.material.color + hoverColor;
        rend.material.SetColor("_EmissionColor", sensitiveEmission);
    }

    private void OnMouseDown()
    {
        isOn = !isOn;

        ColorCheck();

        if (neighbors != null)
        {
            foreach (var button in neighbors)
            {
                button.isOn = !button.isOn;
                button.ColorCheck();
            }
        }

        Color sensitiveEmission = rend.material.color + hoverColor;
        rend.material.SetColor("_EmissionColor", sensitiveEmission);

        observer.SomethingChanged();
    }

    void OnMouseExit()
    {
        rend.material.SetColor("_EmissionColor", Color.gray);
    }

    void ColorCheck()
    {
        if (isOn) rend.material.color = Color.green;
        else rend.material.color = Color.red;
        //rend.material.SetColor("_EmissionColor", Color.gray);
    }

    void OnMouseOver()
    {
    }

    private void OnMouseUp()
    {
    }
}
