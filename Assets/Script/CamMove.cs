using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    [Range(0f, 1f)]
    public float t = 0f; // Valeur interpolée entre 0 (pointA) et 1 (pointB)

    public AnimationCurve lerpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool autoLerp = false;
    public float lerpSpeed = 0.5f; // Vitesse à laquelle t augmente automatiquement (0-1 par seconde)

    void Update()
    {
        if (autoLerp)
        {
            t += Time.deltaTime * lerpSpeed;
            t = Mathf.Clamp01(t);
        }

        UpdateCameraTransform();
    }

    void UpdateCameraTransform()
    {
        if (pointA == null || pointB == null) return;

        // Applique la courbe à t
        float curvedT = lerpCurve.Evaluate(t);

        // Lerp position & rotation avec courbe
        transform.position = Vector3.Lerp(pointA.position, pointB.position, curvedT);
        transform.rotation = Quaternion.Slerp(pointA.rotation, pointB.rotation, curvedT);
    }

    // Appel manuel possible si autoLerp désactivé
    public void SetLerp(float targetT)
    {
        t = Mathf.Clamp01(targetT);
        UpdateCameraTransform();
    }

    public void SwapCam()
    {
        Transform oldA = pointA;
        Transform oldB = pointB;

        pointA = oldB;
        pointB = oldA;

        SetLerp(0);
    }
}
