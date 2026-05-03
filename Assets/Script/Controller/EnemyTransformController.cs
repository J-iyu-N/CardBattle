using System.Collections;
using UnityEngine;

public class EnemyTransformController : MonoBehaviour
{
    // 적 애니메이션... moveto 뺀 playertransform
    private Vector2 basePosition;
    private Vector3 baseScale;
    public bool isDead;
    public SpriteRenderer spriteRenderer;

    [Header("스프라이트")]
    public Sprite[] currentSprite;
    public Sprite[] idle;
    public Sprite[] dead;
    public int frameIndex = 0;
    public float frameTimer = 0;

    [Header("사운드")]
    public AudioClip attackSound;
    public AudioClip shieldSound;
    public AudioClip damageSound;
    public AudioClip deadSound;
    public AudioSource audioSource;

    private Coroutine moveRoutine;

    void Awake()
    {
        basePosition = this.transform.position;
        baseScale = this.transform.localScale;
        currentSprite = idle;
    }
    void Update()
    {
        if (isDead == true) return;
        UpdateSprite();
    }
    public IEnumerator DeadRoutine()
    {
        currentSprite = dead;
        frameIndex = 0;
        frameTimer = 0f;
        yield return new WaitForSeconds(0.5f);
    }
    public void UpdateSprite()
    {
        // 프레임마다 (0.1초) 스프라이트 바꿈
        frameTimer += Time.deltaTime;
        if (frameTimer > 0.1f)
        {
            frameTimer = 0f;
            spriteRenderer.sprite = currentSprite[frameIndex];
            frameIndex = (frameIndex + 1) % currentSprite.Length;
        }
    }
    public void PlayAttack()
    {
        if (isDead == true) return;
        audioSource.clip = attackSound;
        audioSource.volume = 0.5f;
        audioSource.Play();
        StopCurrentCorutine();
    }
    public void PlayDamaged()
    {
        if (isDead == true) return;
        audioSource.clip = damageSound;
        audioSource.volume = 0.5f;
        audioSource.Play();
        StopCurrentCorutine();
    }
    public void PlayShield()
    {
        if (isDead == true) return;
        audioSource.clip = shieldSound;
        audioSource.volume = 0.6f;
        audioSource.Play();
    }
    public void PlayDead()
    {
        if (isDead == true) return;
        isDead = true;
        audioSource.clip = deadSound;
        audioSource.volume = 1f;
        audioSource.Play();
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(DeadRoutine());
        spriteRenderer.sprite = dead[dead.Length - 1];
    }
    public void SetIdle()
    {
        //복귀
        currentSprite = idle;
        frameIndex = 0;
        moveRoutine = null;
    }
    private void StopCurrentCorutine()
    {
        if(moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
        this.transform.position = basePosition;
        this.transform.localScale = baseScale;
    }
}