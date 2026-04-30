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
    public int frameIndex =0;
    public float frameTimer = 0;

    [Header("이동 수치")]
    public float moveDistance = 1f;
    public float moveDuration = 0.3f;

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
    private void StopCurrentCorutine()
    {
        if(moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }
}