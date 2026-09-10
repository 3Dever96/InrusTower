using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private float atk;

    public UnityEvent OnSpawn;
    public UnityEvent OnHit;
    public UnityEvent OnDie;

    [SerializeField] private Renderer avatar;
    [SerializeField] private float flashTime;
    [SerializeField] private float iFrames;
    [SerializeField] private float hitStop;
    [SerializeField] private Material hitMaterial;
    private bool isInvincible;

    private void Start()
    {
        currentHp = maxHp;
        OnSpawn?.Invoke();
    }

    private void OnEnable()
    {
        OnSpawn?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);

            if (currentHp > 0f)
            {
                StartCoroutine(HitEffect());
            }
            else
            {
                OnDie?.Invoke();
            }
        }
    }

    public IEnumerator HitEffect()
    {
        isInvincible = true;

        // 1. CACHE ORIGINAL MATERIAL
        // Use .sharedMaterial or store it at Start() to avoid creating memory leaks with .material
        Material originalMaterial = avatar.sharedMaterial;

        // 2. TRIGGER SOLID WHITE FLASH
        if (hitMaterial != null)
        {
            avatar.material = hitMaterial;
        }

        // 3. HIT STOP (Freeze game time)
        Time.timeScale = 0f;
        // CRITICAL: Must use WaitForSecondsRealtime when timeScale is 0!
        yield return new WaitForSecondsRealtime(hitStop);
        Time.timeScale = 1f;

        // 4. REVERT TO ORIGINAL MATERIAL
        avatar.material = originalMaterial;

        // 5. INVINCIBILITY BLINKING LOOP
        float currentFrame = 0f;
        float flashTimer = 0f;

        while (currentFrame < iFrames)
        {
            // Track time passing frame by frame
            flashTimer += Time.deltaTime;
            currentFrame += Time.deltaTime;

            // Toggle visibility when the flash interval is met
            if (flashTimer >= flashTime)
            {
                avatar.gameObject.SetActive(!avatar.gameObject.activeInHierarchy);
                flashTimer = 0f; // Reset blink timer
            }

            yield return null; // Wait for the next frame
        }

        // 6. CLEANUP & RESET
        avatar.gameObject.SetActive(true); // Ensure character is visible when done
        isInvincible = false;
    }
}
