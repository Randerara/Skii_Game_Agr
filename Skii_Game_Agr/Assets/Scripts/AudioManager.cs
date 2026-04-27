using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip obstacleHitSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       audioSource = GetComponent<AudioSource>(); 
    }

    void OnEnable()
    {
        Obstacle.OnPlayerHit += PlayObstacleHitSound;
    }
    
    private void PlayObstacleHitSound()
    {
        audioSource.PlayOneShot(obstacleHitSound);
    }
}
