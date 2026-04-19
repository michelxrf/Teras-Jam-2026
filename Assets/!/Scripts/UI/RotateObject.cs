using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float yRotationSpeed = 30f;
    [SerializeField] private float zRotationSpeed = 45f;

    private void Update()
    {
        transform.Rotate(0, yRotationSpeed * Time.deltaTime, zRotationSpeed * Time.deltaTime);
    }
}
