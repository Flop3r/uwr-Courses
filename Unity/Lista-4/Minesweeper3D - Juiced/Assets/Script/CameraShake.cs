using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Parametry trzęsienia kamery
    public float shakeDuration = 0.5f; // Czas trzęsienia
    public float shakeAmount = 0.2f; // Intensywność trzęsienia

    private Vector3 originalPosition;
    private float shakeTimer = 0f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            // Trzęsienie kamery
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;

            // Zmniejsz czas trzęsienia z efektem interpolacji liniowej
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Powrót do oryginalnej pozycji po zakończeniu trzęsienia
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5f);
        }
    }

    // Wywołanie trzęsienia kamery po wybuchu
    public void Shake()
    {
        Debug.Log("TRZĘSIENIE!");
        shakeTimer = shakeDuration;
    }
}
