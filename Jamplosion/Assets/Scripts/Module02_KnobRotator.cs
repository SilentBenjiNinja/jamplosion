using UnityEngine;

public class Module02_KnobRotator : MonoBehaviour
{
    private Vector2 startPos;

    [SerializeField] private float distanceMod = 5f;

    [SerializeField] private int rotationSteps = 10;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        startPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        var delta = (Vector2)Input.mousePosition - startPos;

        float deltaXY = Mathf.Clamp((delta.x + delta.y) / distanceMod, 0, rotationSteps);

        int steps = Mathf.FloorToInt(deltaXY); 
        
        RotateKnob(steps);
    }

    void RotateKnob(int steps)
    {
        var degrees = 270 / rotationSteps * steps;
        transform.localEulerAngles = new Vector3(degrees , 0, 0);
    }
}
