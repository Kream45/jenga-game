using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{

    public bool enabled = true;
    public bool isSpecial = false;
    public Material normalMaterial;
    public Material specialMaterial;

    void Start()
    {
        SetMaterial();
        enabled = true;
    }


    private void OnValidate()
    {
        SetMaterial();
    }

    void SetMaterial()
    {
            GetComponent<Renderer>().material = 
                isSpecial ? specialMaterial : normalMaterial;
    }

    private void OnMouseDrag()
    {
        if (FindObjectOfType<gameController>().isPlaying == false) return;
        var cameraPosition = FindObjectOfType<Camera>().transform.position;
        var direction = (cameraPosition - transform.position).normalized;
      
        GetComponent<Rigidbody>().AddForce(direction * 100);
    }
}
