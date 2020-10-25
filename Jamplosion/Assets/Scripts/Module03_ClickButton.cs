using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module03_ClickButton : MonoBehaviour
{  
    public Material lmat;
    public Material nMat;

    private Renderer myR;

    public int startCount = 3;

    public int myNumber;

    public delegate void ClickEV(int number);

    public event ClickEV onClick;

    void Awake()
    {
        myR = GetComponent<Renderer>();
        myR.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        ClickedColor();
    }

    private void OnMouseUp()
    {
        UnClickedColor();
    }

    public void ClickedColor()
    {
        myR.sharedMaterial = nMat;
    }

    public void UnClickedColor()
    {
        myR.sharedMaterial = lmat;
    }
}
