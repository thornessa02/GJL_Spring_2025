using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSlider : MonoBehaviour
{
    [SerializeField] bool horizontal;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] TrackReader reader;
    [SerializeField] TrackVolume volume;
    bool isSelected;
    Vector2 MouseDelta;
    public float value;
    private void OnMouseDown()
    {
        isSelected=true;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }

        MouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (isSelected)
        {
            if (horizontal)
            {
                transform.localPosition -= new Vector3(MouseDelta.x, 0, 0);
                if (transform.localPosition.x < maxDistance) transform.localPosition = new Vector3(maxDistance, transform.localPosition.y, transform.localPosition.z);
                if (transform.localPosition.x > minDistance) transform.localPosition = new Vector3(minDistance, transform.localPosition.y, transform.localPosition.z);

                value = Remap(transform.localPosition.x, -12f, 15f, 240f, 60f)-20;
                reader.OnSliderValueChanged(value);
            }
            else
            {
                gameObject.transform.localPosition -= new Vector3(0, 0, MouseDelta.y);
                if (transform.localPosition.z < maxDistance) transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y, maxDistance);
                if (transform.localPosition.z > minDistance) transform.localPosition = new Vector3(transform.localPosition.x,  transform.localPosition.y, minDistance);

                value = Remap(transform.localPosition.z, 6f, -2f, 10f, 0f) +15;
                volume.OnSliderValueChanged(value);
            }   
        }
    }

    float Remap(float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        return ((value - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
    }
}
