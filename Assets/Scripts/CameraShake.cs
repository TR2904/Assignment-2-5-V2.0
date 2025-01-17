using System.Collections;

using UnityEngine;


public class CameraShake : MonoBehaviour

{

    public static CameraShake instance; // Singleton for global access


    private void Awake()

    {

        // Ensure only one instance of CameraShake exists

        if (instance == null)

        {

            instance = this;

        }

        else

        {

            Destroy(gameObject);

        }

    }


    public IEnumerator Shake(float duration, float magnitude)

    {

        Vector3 originalPosition = transform.localPosition; // Store the original position

        float elapsed = 0.0f;


        while (elapsed < duration)

        {

            // Generate random offsets for the shake

            float offsetX = Random.Range(-1f, 1f) * magnitude;

            float offsetY = Random.Range(-1f, 1f) * magnitude;


            // Apply the offsets to the camera's local position

            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);


            elapsed += Time.deltaTime; // Increment elapsed time

            yield return null; // Wait until the next frame

        }


        // Reset the camera to its original position

        transform.localPosition = originalPosition;

    }

}
