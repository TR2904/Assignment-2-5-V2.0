using System.Collections;

using UnityEngine;

using UnityEngine.UI;


public class ScreenFlash : MonoBehaviour

{

    public Image flashImage; // UI Image for the flash (should cover the entire screen)

    public Color flashColor = Color.white; // Color of the flash

    public float flashDuration = 0.5f; // How long the flash lasts


    private void Start()

    {

        // Ensure the flash starts invisible

        if (flashImage != null)

        {

            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);

        }

        else

        {

            Debug.LogError("FlashImage is not assigned! Please assign a UI Image.");

        }

    }


    // Public method to trigger the flash

    public void TriggerFlash()

    {

        if (flashImage != null)

        {

            StartCoroutine(FlashRoutine());

        }

        else

        {

            Debug.LogWarning("FlashImage is not assigned! Flash cannot be triggered.");

        }

    }


    private IEnumerator FlashRoutine()

    {

        float halfDuration = flashDuration / 2f;

        float elapsedTime = 0f;


        // Fade in the flash

        while (elapsedTime < halfDuration)

        {

            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(0, 1, elapsedTime / halfDuration);

            SetFlashAlpha(alpha);

            yield return null;

        }


        // Fade out the flash

        elapsedTime = 0f;

        while (elapsedTime < halfDuration)

        {

            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(1, 0, elapsedTime / halfDuration);

            SetFlashAlpha(alpha);

            yield return null;

        }


        // Ensure the flash is fully invisible at the end

        SetFlashAlpha(0);

    }


    // Helper method to set the alpha of the flash color

    private void SetFlashAlpha(float alpha)

    {

        if (flashImage != null)

        {

            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);

        }

    }

}

