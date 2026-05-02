using System.Collections;
using UnityEngine;

public class PlayerTransformController : MonoBehaviour
{
    // 플레이어 애니메이션
    private Vector2 basePosition;
    private Vector3 baseScale;

    public SpriteRenderer spriteRenderer;
    [Header("스프라이트")]
    public Sprite[] currentSprite;
    public Sprite[] idle;
    public Sprite[] atttck;
    public Sprite[] attackGun;
    public Sprite[] damaged;
    public Sprite[] heal;
    public Sprite[] healOther;
    public int frameIndex =0;
    public float frameTimer = 0;

    [Header("이동 수치")]
    public float moveDistance = 1.5f;
    public float moveDuration = 0.5f;

    private Coroutine moveRoutine;

    void Awake()
    {
        basePosition = this.transform.position;
        baseScale = this.transform.localScale;
        currentSprite = idle;
    }
    void Update()
    {
        UpdateSprite();
    }
    public IEnumerator MoveTo(Vector2 startPosition, Vector2 targetPosition, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time/duration;
            t = Mathf.SmoothStep(0f,1f,t);

            transform.position = Vector2.Lerp(startPosition,targetPosition,t);
            yield return null;
        }
        transform.position = targetPosition;
    }
    public IEnumerator AttackRoutine()
    {
        currentSprite = atttck;
        frameIndex = 0;
        frameTimer = 0f;

        Vector2 startPosition = this.transform.position;
        Vector2 targetPosition = startPosition + Vector2.right*moveDistance;

        yield return MoveTo(startPosition,targetPosition,moveDuration*0.5f); // 가고
        yield return MoveTo(targetPosition,startPosition,moveDuration*0.5f); // 돌아옴

        SetIdle();
    }
    public IEnumerator AttackGunRoutine()
    {
        currentSprite = attackGun;
        float newMonveDistance = 1f;
        frameIndex = 0;
        frameTimer = 0f;

        Vector2 startPosition = this.transform.position;
        Vector2 targetPosition = startPosition + Vector2.right*newMonveDistance;

        yield return MoveTo(startPosition,targetPosition,moveDuration*0.5f); // 가고
        yield return MoveTo(targetPosition,startPosition,moveDuration*0.5f); // 돌아옴

        SetIdle();
    }
    public IEnumerator DamagedRoutine()
    {
        currentSprite = damaged;
        frameIndex = 0;
        frameTimer = 0f;
        yield return new WaitForSeconds(0.5f);

        SetIdle();
    }
    public IEnumerator HealRoutine()
    {
        currentSprite = heal;
        frameIndex = 0;
        frameTimer = 0f;
        yield return new WaitForSeconds(0.5f);

        SetIdle();

    }
    public IEnumerator HealOtherRoutine()
    {
        currentSprite = healOther;
        frameIndex = 0;
        frameTimer = 0f;
        yield return new WaitForSeconds(0.5f);

        SetIdle();
    }
    public void UpdateSprite()
    {
        // 프레임마다 (0.1초) 스프라이트 바꿈
        frameTimer += Time.deltaTime;
        if (frameTimer > 0.1f)
        {
            frameTimer = 0f;
            spriteRenderer.sprite = currentSprite[frameIndex];
            frameIndex = (frameIndex +1)%currentSprite.Length;
        }
    }
    public void PlayAttack()
    {
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(AttackRoutine());
    }
    public void PlayAttackGun()
    {
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(AttackGunRoutine());
    }
    public void PlayDamaged()
    {
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(DamagedRoutine());
    }
    public void PlayHeal()
    {
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(HealRoutine());
    }
    public void PlayHealOther()
    {
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(HealOtherRoutine());
    }
    public void SetIdle()
    {
        //복귀
        currentSprite = idle;
        frameIndex =0;
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