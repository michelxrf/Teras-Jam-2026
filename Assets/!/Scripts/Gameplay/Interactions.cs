using System.Collections;
using TMPro;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject BarkBalloon;
    [SerializeField] TMP_Text barkTextField;
    [SerializeField] string[] texts;
    [SerializeField] AudioClip[] audioClips;

    [Header("Settings")]
    [SerializeField] float interactionRange = 3f;
    [SerializeField] bool singleActivation = false;
    [SerializeField] float delayBetweenBarks = 1f;

    private bool isInteracting = false;
    private AudioSource audioSource;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = interactionRange;
        BarkBalloon.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInteracting && texts.Length > 0)
        {
            ActivateBark();
        }
    }

    private void ActivateBark()
    {
        Debug.Log("Activating Bark Sequence");
        isInteracting = true;
        BarkBalloon.SetActive(true);
        StartCoroutine(PlayBarkSequence());
    }

    private IEnumerator PlayBarkSequence()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            barkTextField.text = texts[i];

            if (audioClips.Length > i && audioClips[i] != null)
            {
                Debug.Log($"Playing audio for bark {i}");
                audioSource.PlayOneShot(audioClips[i]);
                yield return new WaitForSeconds(audioClips[i].length + delayBetweenBarks);
            }
        }

        BarkBalloon.SetActive(false);
        isInteracting = false;

        if (singleActivation)
        {
            enabled = false;
        }
    }
}
