using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour
{
    public UnityEvent OnClick;


    Animator anim;
    Renderer renderer;
    public bool toggle;

    [SerializeField] Material toggleOn;
    [SerializeField] Material toggleOff;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        anim.SetTrigger("Press");

        if (OnClick.GetPersistentEventCount() > 0)
        {
            OnClick.Invoke();
            return;
        }

        toggle = !toggle;

        Material[] newMats = renderer.materials;

        if (toggle) newMats[1] = toggleOn;
        else newMats[1] = toggleOff;

        renderer.materials = newMats;
    }

    public void ChangeColor(int index, Material newMat)
    {
        Material[] newMats = renderer.materials;

        newMats[index] = newMat;

        renderer.materials = newMats;
    }
}
