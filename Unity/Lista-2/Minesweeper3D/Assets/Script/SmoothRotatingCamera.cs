using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target; // Punkt, wokół którego obraca się kamera
    public float rotationSpeed = 1.0f; // Szybkość obrotu kamery
    public float returnSpeed = 2.0f; // Szybkość powrotu kamery do początkowego ułożenia
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private bool isReturning = false;

    private void Start()
    {
        initialRotation = transform.rotation;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Obrót kamery na podstawie ruchu myszką
            transform.RotateAround(target.position, Vector3.up, mouseX * rotationSpeed);
            transform.RotateAround(target.position, transform.right, -mouseY * rotationSpeed);

            // Wyłącz płynne wracanie do początkowego ułożenia
            isReturning = false;
        }
        else if (!isReturning)
        {
            // Rozpocznij płynne wracanie do początkowego ułożenia
            ReturnToInitialPosition();
        }
    }

    private void ReturnToInitialPosition()
    {
        isReturning = true;
        float t = 0;
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        while (t < 1)
        {
            t += Time.deltaTime * returnSpeed;
            transform.position = Vector3.Lerp(currentPosition, initialPosition, t);
            transform.rotation = Quaternion.Slerp(currentRotation, initialRotation, t);
        }
    }
}