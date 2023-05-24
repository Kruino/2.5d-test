using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ClickDestroy : MonoBehaviour
{
    public int Health = 3;  // Number of clicks required to destroy the object
    public GameObject itemToDrop;   // Item to drop when destroyed
    public float respawnTime = 30f;  // Time in seconds for the object to respawn
    public int value = 5;

    private int DamageAmount = 0;
    private bool isDestroying = false;
    private bool isRespawning = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float respawnTimer;
    private GameObject player;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;

    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        if (isDestroying) return;
        DamageAmount += player.GetComponentInChildren<PlayerInfo>().damageAmount;

        if (DamageAmount >= Health)
        {
            StartDestroy();
        }
        else
        {
            StartCoroutine(BlinkWhite());
            BounceObject();
        }
    }

    private void StartDestroy()
    {
        isDestroying = true;
        StartCoroutine(DestroyObject());
    }

    private System.Collections.IEnumerator DestroyObject()
    {
        // Blink the object white for a few moments
        BlinkWhite();

        // Drop the item
        //Instantiate(itemToDrop, transform.position, Quaternion.identity);

        // Disable the object renderer
        spriteRenderer.enabled = false;
        player.GetComponentInChildren<PlayerInfo>().AddPoints(value);
            
        // Start the respawn timer
        respawnTimer = 0f;
        isRespawning = true;

        while (respawnTimer < respawnTime)
        {
            respawnTimer += Time.deltaTime;
            yield return null;
        }

        // Enable the object renderer and reset its properties
        spriteRenderer.enabled = true;
        ResetObject();
    }

    private System.Collections.IEnumerator BlinkWhite()
    {
        // Blink the object white briefly
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void BounceObject()
    {
        // Bounce the object by scaling it up and down
        const float bounceScale = -0.2f;
        const float bounceTime = 0.1f;

        StartCoroutine(BounceAnimation(Vector3.one * bounceScale, bounceTime));
    }

    private System.Collections.IEnumerator BounceAnimation(Vector3 targetScale, float duration)
    {
        var originalScale = transform.localScale;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }

    private void ResetObject()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        DamageAmount = 0;
        isDestroying = false;
        isRespawning = false;
    }

    private void Update()
    {
        if (!isRespawning) return;
        var remainingTime = Mathf.Max(0f, respawnTime - respawnTimer);

        if (!(remainingTime <= 0f)) return;
        
        // Respawn completed, reset the object
        isRespawning = false;
        ResetObject();
    }
}
