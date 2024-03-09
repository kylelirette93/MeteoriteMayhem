using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 0.5f;

    public void StartDestroyCoroutine()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    public IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject); // Destroy the bullet clone
    }
}