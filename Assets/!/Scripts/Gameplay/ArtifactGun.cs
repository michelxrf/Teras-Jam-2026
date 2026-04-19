using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArtifactGun : MonoBehaviour
{
    [SerializeField] GameObject _gun;
    [SerializeField] ParticleSystem _gunParticle;
    [SerializeField] Transform _artifactStop;

    PlayerInput _playerInput;
    bool _isEquipped;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _gunParticle.Stop();
        _gun.SetActive(false);
        Equip();
    }

    private void Update()
    {
        if (_playerInput.actions["Fire"].WasPressedThisFrame())
            Shoot();
        if (_playerInput.actions["Fire"].WasReleasedThisFrame())
            ReleaseArtifact();
    }

    public void Equip()
    {
        _gun.SetActive(true);
        _isEquipped = true;
    }

    public void Shoot()
    {
        if (!_isEquipped)
            return;

        _gunParticle.Play();

        RaycastHit hit;
        bool didHit = Physics.Raycast(_gun.transform.position, -_gun.transform.forward, out hit, 5f, LayerMask.GetMask("Artifacts"));

        // Draw the ray for debugging - green if hit, white if no hit
        Color rayColor = didHit ? Color.green : Color.white;
        Debug.DrawRay(_gun.transform.position, -_gun.transform.forward * 5f, rayColor, 0.1f);
        if(hit.collider != null)
        {
            if(hit.collider.TryGetComponent(out Artifact artifact))
            {
                GrabArtifact(artifact);
            }
        }
    }

    void GrabArtifact(Artifact artifact)
    {
        artifact.transform.position = _artifactStop.position;
        artifact.transform.rotation = _artifactStop.rotation;
        artifact.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        artifact.GetComponent<Rigidbody>().useGravity = false;
        artifact.GetComponent<Rigidbody>().isKinematic = true;
        artifact.transform.SetParent(_artifactStop);
    }

    void ReleaseArtifact()
    {
        Artifact artifact = _artifactStop.GetComponentInChildren<Artifact>();
        if (artifact != null)
        {
            artifact.GetComponent<Rigidbody>().useGravity = true;
            artifact.GetComponent<Rigidbody>().isKinematic = false;
            _artifactStop.DetachChildren();
        }

        _gunParticle.Stop();
    }
}
