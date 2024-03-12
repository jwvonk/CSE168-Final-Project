using UnityEngine;

public class SpatialAudioController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float triggerDistance = 5f; // Distance at which the audio starts playing

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check the distance between the player and this object
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is close enough, play the audio
        if (distanceToPlayer < triggerDistance)
        {
            // If the audio is not already playing
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If the audio is playing but the player is not close anymore
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}