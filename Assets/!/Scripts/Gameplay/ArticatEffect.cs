using System.Collections.Generic;
using UnityEngine;

public class ArticatEffect : MonoBehaviour
{
    Artifact _artifact;

    private void Awake()
    {
        _artifact = GetComponentInParent<Artifact>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Cat cat = other.GetComponent<Cat>();

        if (cat != null)
            _artifact.AddCat(cat);
    }

    private void OnTriggerExit(Collider other)
    {
        Cat cat = other.GetComponent<Cat>();
        
        if (cat != null)
            _artifact.RemoveCat(cat);
    }

    
}
