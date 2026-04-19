using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    Renderer _renderer;
    ArticatEffect _effect;
    PlayerController _playerController;
    Material _material;

    [SerializeField] float _playerDetectionRadius = 5f;
    [SerializeField] AudioClip _alertClip;

    bool _isAngry = false;
    List<Cat> _affectedCats = new List<Cat>();

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _effect = GetComponentInChildren<ArticatEffect>();
        _material = _renderer.material;
    }

    private void Start()
    {
        _playerController = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        if(_renderer.isVisible && Vector3.Distance(_playerController.transform.position, transform.position) <= _playerDetectionRadius)
        {
            if(!_isAngry)
                AudioSource.PlayClipAtPoint(_alertClip, transform.position);

            _isAngry = true;
            MakeCatsAngry();
            _material.color = Color.red;
            _material.SetColor("_EmissionColor", Color.red);

        }
        else
        {
            _isAngry = false;
            MakeCatsPassive();
            _material.color = Color.blue;
            _material.SetColor("_EmissionColor", Color.blue);
        }
    }

    public void AddCat(Cat cat)
    {
        Debug.Log($"Adding cat {cat.name} to artifact influence.");

        if (!_affectedCats.Contains(cat))
        {
            _affectedCats.Add(cat);
            cat.EnterArtifactInfluence(gameObject.transform);
            if (_isAngry)
                cat.BecomeAggressive();
        }
    }

    public void RemoveCat(Cat cat)
    {
        Debug.Log($"Removing cat {cat.name} from artifact influence.");

        if (_affectedCats.Contains(cat))
        {
            _affectedCats.Remove(cat);
            cat.ExitArtifactInfluence();
            cat.BecomePassive();
        }
    }

    
    public void MakeCatsAngry()
    {
        foreach (var cat in _affectedCats)
        {
            cat.BecomeAggressive();
        }
    }

    public void MakeCatsPassive()
    {
        foreach (var cat in _affectedCats)
        {
            cat.BecomePassive();
        }
    }

    public void FreeTheCats()
    {
        foreach (var cat in _affectedCats)
        {
            cat.BecomePassive();
        }
        _affectedCats.Clear();
        MakeCatsAngry();
        Destroy(this);
    }

}
