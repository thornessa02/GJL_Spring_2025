using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSlider : MonoBehaviour
{
    [SerializeField] bool horizontal;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] TrackReader reader;
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
                transform.position += new Vector3(MouseDelta.x, 0, 0);
                if (transform.position.x < maxDistance) transform.position = new Vector3(maxDistance, transform.position.y, transform.position.z);
                if (transform.position.x > minDistance) transform.position = new Vector3(minDistance, transform.position.y, transform.position.z);

                value = Remap(transform.position.x, 12f, -15f, 240f, 60f)-20;
                reader.OnSliderValueChanged(value);
            }
            else
            {
                gameObject.transform.position += new Vector3(0, 0, MouseDelta.y);
                if (transform.position.z < maxDistance) transform.position = new Vector3(transform.position.x,transform.position.y, maxDistance);
                if (transform.position.z > minDistance) transform.position = new Vector3(transform.position.x,  transform.position.y, minDistance);
            }   
        }
    }

    float Remap(float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        return ((value - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin;
    }
}
