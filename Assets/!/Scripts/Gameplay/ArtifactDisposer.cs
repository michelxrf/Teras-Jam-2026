using UnityEngine;

public class ArtifactDisposer : MonoBehaviour
{
    private int _artifactCount = 0;

    private void Start()
    {
        CountArtifacts();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Artifact artifact = collision.GetComponent<Artifact>();

        if (artifact != null)
        {
            Destroy(collision.gameObject);
            _artifactCount--;

            if (_artifactCount <= 0)
            {
                FindFirstObjectByType<GameOver>().Gameover(true);
            }
        }
    }

    private void CountArtifacts()
    {
        Artifact[] artifacts = FindObjectsByType<Artifact>(FindObjectsSortMode.None);
        _artifactCount = artifacts.Length;
    }
}
