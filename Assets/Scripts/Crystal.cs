using UnityEngine;

public class Crystal : MonoBehaviour
{
    // Assign these in the inspector
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Collider2D[] colliders = new Collider2D[0];
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] miningClips = new AudioClip[0];
    [SerializeField] AudioClip[] breakClips = new AudioClip[0];
    [SerializeField] float health = 10f;
    [SerializeField] float miningSpeed = 2f;

    /// <summary>
    /// Make progress mining this crystal
    /// </summary>
    public void Mine(out bool fullyMined)
    {
        fullyMined = health <= miningSpeed;
        if (fullyMined)
        {
            health = 0f;
            AudioClip breakClip = breakClips[Random.Range(0, breakClips.Length)];
            audioSource.PlayOneShot(breakClip);
            MainScreen.CrystalsCollected++;
            sr.enabled = false;
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
            Destroy(gameObject, breakClip.length);
        }
        else
        {
            health -= miningSpeed;
            audioSource.PlayOneShot(miningClips[Random.Range(0, miningClips.Length)]);
        }
    }
}
