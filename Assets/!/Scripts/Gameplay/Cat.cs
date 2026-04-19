using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour
{
    NavMeshAgent _navAgent;
    Transform _artifact;
    AudioSource _audioSource;
    Transform _player;
    bool _IsAggressive = false;

    [SerializeField] float _aggressiveSpeed = 4f;
    [SerializeField] float _passiveSpeed = 2f;
    [SerializeField] AudioClip _agressiveClip;
    [SerializeField] AudioClip _passiveClip;
    [SerializeField] float _randomDestinationRadius = 10f;
    [SerializeField] float _targetReachedDistance = 0.05f;
    [SerializeField] Collider hitbox;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _player = FindFirstObjectByType<PlayerController>().transform;
        BecomePassive();
        SetNewDestination();
    }

    void SetNewDestination()
    {
        if(_IsAggressive)
        {
            _navAgent.SetDestination(_player.position);
            _navAgent.stoppingDistance = .25f;
        }
        else if(_artifact != null)
        {
            _navAgent.SetDestination(_artifact.position);
            _navAgent.stoppingDistance = 1.3f;
        }
        else
        {
            _navAgent.SetDestination(SetRandomDestination());
            _navAgent.stoppingDistance = 0f;
        }
    }

    Vector3 SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _randomDestinationRadius;
        return transform.position + randomDirection;
    }

    public void EnterArtifactInfluence(Transform artifact)
    {
        _artifact = artifact;
    }

    public void ExitArtifactInfluence()
    {
        _artifact = null;
        SetNewDestination();
    }

    public void BecomeAggressive()
    {
        _IsAggressive = true;
        SetNewDestination();
        _audioSource.clip = _agressiveClip;
        _audioSource.Play();
        hitbox.enabled = true;
        _navAgent.speed = _aggressiveSpeed;
    }

    public void BecomePassive()
    {
        _IsAggressive = false;
        _audioSource.clip = _passiveClip;
        _audioSource.Play();
        hitbox.enabled = false;
        _navAgent.speed = _passiveSpeed;
    }

    private void Update()
    {
        CheckTargetDistance();
    }

    void CheckTargetDistance()
    {
        if (_IsAggressive)
            return;

        // NavAgent has reached the target
        if(!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude == 0f)
        {
            SetNewDestination();
        }

        if(_navAgent.path.status == NavMeshPathStatus.PathInvalid)
        {
            SetNewDestination();
        }
    }


}
