using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickUpAndInspect : MonoBehaviour
{
    #region Camera Rotation
    public float mouseToYawPitchRatio = 0.25f;

    public Transform tfPitch;
    public Transform tfYaw;

    Vector2 prevMousePos;
    Vector2 yawPitchVector;

    void RotationFromYawPitch(Vector2 yawPitch)
    {
        float pitch = Mathf.Clamp(yawPitch.x, -90, 90);
        float yaw = yawPitch.y % 360;

        tfPitch.localEulerAngles = new Vector3(pitch, 0, 0);
        tfYaw.localEulerAngles = new Vector3(0, yaw, 0);
    }

    void DragRotation()
    {
        if (Input.GetMouseButton((int)MouseButton.RightMouse))
        {
            Vector2 playWindowSize = new Vector2(
                Screen.width / 2,
                Screen.height / 2
            );
            Vector2 mousePos = Input.mousePosition;
            mousePos -= playWindowSize;
            if (prevMousePos != mousePos)
            {
                Vector2 relativeMouseDelta = (mousePos - prevMousePos) * mouseToYawPitchRatio;
                Vector2 flippedDelta = new Vector2(
                    relativeMouseDelta.y,
                    -relativeMouseDelta.x
                );
                yawPitchVector += flippedDelta;
            }
            prevMousePos = mousePos;
            RotationFromYawPitch(yawPitchVector);
        }
    }
    #endregion

    private void Awake()
    {
        tfCam = Camera.main.transform;
    }

    void Update()
    {
        FetchTestInputs();

        if (!isLocked)
        {
            DragRotation();
        }
    }

    #region Module Lock
    public bool isLocked = false;

    public float freeCamZPos = -5;
    public float lockCamZPos = -3;
    public float lerpSpeed = 0.5f;
    
    public float lerpBias = 0.01f;

    Transform tfCam;

    bool isZooming = false;

    [SerializeField]
    public List<Vector2> moduleRotations;

    void LockRotation(int positionIndex)
    {
        isLocked = true;

        StartCoroutine(ZoomToTarget(positionIndex));
    }

    void UnlockRotation()
    {
        isLocked = false;

        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomOut()
    {
        while (tfCam.position.z > freeCamZPos + lerpBias)
        {
            Vector3 currentPos = tfCam.position;
            Vector3 targetPos = new Vector3(
                tfCam.position.x,
                tfCam.position.y,
                freeCamZPos
            );
            tfCam.position = Vector3.Lerp(currentPos, targetPos, lerpSpeed);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("fully zoomed out");
    }

    IEnumerator ZoomToTarget(int positionIndex)
    {
        float lerpValue = 0;
        while (lerpValue < 1 - lerpBias)
        {
            lerpValue = Mathf.Lerp(lerpValue, 1, lerpSpeed);
            Vector3 targetPos = new Vector3(
                tfCam.position.x,
                tfCam.position.y,
                lockCamZPos
            );
            tfCam.position = Vector3.Lerp(tfCam.position, targetPos, lerpSpeed);
            
            Vector2 currentRot = new Vector2(
                tfPitch.localEulerAngles.x,
                tfYaw.localEulerAngles.y
            );
            Vector2 targetRot = moduleRotations[positionIndex];

            Vector2 lerpRot = Vector2.Lerp(currentRot, targetRot, lerpSpeed);
            RotationFromYawPitch(lerpRot);
            
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("fully zoomed in");
    }

    void FetchTestInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LockRotation(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LockRotation(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LockRotation(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LockRotation(3);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            UnlockRotation();
        }
    }
    #endregion
}
