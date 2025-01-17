using System.Collections;

using UnityEngine;

using TMPro;

using UnityEngine.Rendering.PostProcessing;


public class Player : MonoBehaviour

{

    public float speed = 5f; // Player movement speed

    public int nukeCount = 0; // Tracks how many nukes the player has

    public TextMeshProUGUI nukeCounterText; // UI element for the nuke count


    public AudioClip nukeActivateSound; // Sound effect for activating the nuke

    private AudioSource audioSource;


    public ScreenFlash screenFlash; // Reference to the ScreenFlash script

    public ParticleSystem nukeParticles; // Particle effect for the nuke


    public PostProcessVolume postProcessVolume; // Reference to post-processing volume

    private Bloom bloom; // Bloom effect for visual feedback


    private bool hasNuke = true; // Indicates if the player currently has a nuke


    void Start()

    {

        // Ensure the player has an AudioSource

        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();


        // Get the bloom effect from the post-processing volume

        if (postProcessVolume != null)

        {

            postProcessVolume.profile.TryGetSettings(out bloom);

        }

    }


    void Update()

    {

        MovePlayer();


        // Activate the nuke if the player has one and presses the right mouse button

        if (hasNuke && Input.GetMouseButtonDown(1))

        {

            ActivateNuke();

        }

    }


    private void MovePlayer()

    {

        float moveX = Input.GetAxis("Horizontal");

        float moveZ = Input.GetAxis("Vertical");


        Vector3 move = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;

        transform.Translate(move, Space.World);

    }


    private void OnTriggerEnter(Collider other)

    {

        // Handle picking up the nuke

        if (other.CompareTag("NukePickup"))

        {

            EnableNuke();

            Destroy(other.gameObject); // Remove the pickup from the scene

        }

    }


    public void EnableNuke()

    {

        hasNuke = true;

        nukeCount++;

        UpdateNukeCounter();

        Debug.Log($"Nuke picked up! Total Nukes: {nukeCount}");

    }


    private void ActivateNuke()

    {

        // Find and destroy all enemies in the scene

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)

        {

            enemy.TakeDamage(enemy.health); // Instantly destroy the enemy

        }


        // Update the score based on the number of enemies killed

        int scoreGained = enemies.Length * 10; // Example: 10 points per enemy

        GameManager.instance.AddScore(scoreGained);


        // Trigger the Game Over sequence

        GameManager.instance.TriggerGameOver();


        // Play the nuke activation sound

        if (nukeActivateSound != null)

        {

            audioSource.PlayOneShot(nukeActivateSound);

        }


        // Trigger the screen flash effect

        if (screenFlash != null)

        {

            screenFlash.TriggerFlash();

        }


        // Play the particle effect

        if (nukeParticles != null)

        {

            nukeParticles.Play();

        }


        // Trigger the bloom effect

        if (bloom != null)

        {

            StartCoroutine(TriggerBloomEffect());

        }


        // Trigger camera shake

        if (CameraShake.instance != null)

        {

            StartCoroutine(CameraShake.instance.Shake(0.5f, 0.3f));

        }


        hasNuke = false; // Consume the nuke


        Debug.Log("Nuke activated! All enemies destroyed.");

    }


    private IEnumerator TriggerBloomEffect()

    {

        float initialIntensity = bloom.intensity.value;

        float targetIntensity = 20f; // Maximum bloom intensity

        float duration = 0.5f; // Duration of the bloom effect


        float elapsedTime = 0f;


        // Gradually increase the bloom intensity

        while (elapsedTime < duration)

        {

            bloom.intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;

        }


        yield return new WaitForSeconds(0.5f);


        elapsedTime = 0f;


        // Gradually decrease the bloom intensity

        while (elapsedTime < duration)

        {

            bloom.intensity.value = Mathf.Lerp(targetIntensity, initialIntensity, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;

        }


        bloom.intensity.value = initialIntensity; // Reset bloom intensity

    }


    private void UpdateNukeCounter()

    {

        if (nukeCounterText != null)

        {

            nukeCounterText.text = $"Nukes: {nukeCount}";

        }

    }

}