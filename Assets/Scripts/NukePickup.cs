using UnityEngine;


public class NukePickup : MonoBehaviour

{

    public AudioClip pickupSound; // Assign the pickup sound in the Inspector

    private AudioSource audioSource;


    private void Start()

    {

        // Add or get an AudioSource component on the pickup object

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)

        {

            audioSource = gameObject.AddComponent<AudioSource>();

        }


        // Configure the AudioSource properties

        audioSource.playOnAwake = false;

        audioSource.spatialBlend = 1f; // Make the sound 3D (optional)

    }


    private void OnTriggerEnter(Collider other)

    {

        // Check if the object is tagged "Player"

        if (other.CompareTag("Player"))

        {

            // Attempt to get the Player component

            Player player = other.GetComponent<Player>();


            if (player != null)

            {

                // Grant the nuke ability to the player

                player.EnableNuke();


                // Play the pickup sound

                if (pickupSound != null)

                {

                    audioSource.PlayOneShot(pickupSound);

                }


                // Destroy the pickup object after a short delay to allow the sound to play

                Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);


                // Debug log for confirmation

                Debug.Log("Nuke picked up!");

            }

            else

            {

                Debug.LogError("Player object missing Player component!");

            }

        }

    }

}
