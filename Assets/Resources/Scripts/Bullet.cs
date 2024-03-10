using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float lifetime = 0.5f;

    public void StartDestroyCoroutine()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject); // Destroy the bullet clone
    }
}