using System.Collections;
using UnityEngine;

public class CardTransformController : MonoBehaviour
{
    // 카드 애니메이션
    public RectTransform rectTransform;

    // 기본 상태
    private Vector2 basePosition;
    private Vector3 baseScale = Vector3.one;
    private float baseRotationZ = -15f;

    // 최종 호버 상태
    private Vector2 hoverPositonOffset = new Vector2(0,15f);
    private Vector3 hoverScale = new Vector3(1.5f,1.5f,1f);
    private float hoverRotationZ = -10f;

    // 호버 올라갈때 중간 디용 효과
    private Vector2 bouncePositionOffset = new Vector2(0,2f);
    private Vector3 bounceScale = new Vector3(1.2f,1.1f,1f);
    private float bounceRotationZ = -15f;

    // 호버 내려갈때
    private Vector2 outBouncePositionOffset = new Vector2 (0,-5f);
    private Vector3 outBounceScale = new Vector3(0.95f,0.95f,1f);
    private float outBounceRotationZ = -20f;

    // 레이어
    public Canvas CardCanvas;

    private Coroutine moveRoutine;

    void Awake()
    {
        basePosition = this.rectTransform.anchoredPosition;
        baseScale = this.rectTransform.localScale;
        baseRotationZ = this.rectTransform.localEulerAngles.z;
    }
    public IEnumerator Animate(Vector2 targetPosition, Vector3 targetScale, float targetRotationZ, float duration)
    {
        // 애니메이션 시작 전 위치
        Vector2 startPosition = this.rectTransform.anchoredPosition;
        Vector3 startScale = this.rectTransform.localScale;
        float startRotationZ = this.rectTransform.localEulerAngles.z;

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time/duration;
            t = Mathf.SmoothStep(0f,1f,t);

            this.rectTransform.anchoredPosition = Vector2.Lerp(startPosition,targetPosition,t);
            this.rectTransform.localScale = Vector3.Lerp(startScale,targetScale,t);
            float rotationZ = Mathf.LerpAngle(startRotationZ,targetRotationZ,t);
            this.rectTransform.localRotation = Quaternion.Euler(0f,0f,rotationZ);

            yield return null;
        }

        // 최종 결과
        rectTransform.anchoredPosition = targetPosition;
        rectTransform.localScale = targetScale;
        rectTransform.localRotation = Quaternion.Euler(0f,0f,targetRotationZ);
    }
    public IEnumerator PlayHoverRoutine()
    {
        // 바운스 후 호버 애니메이션
        yield return Animate(basePosition+bouncePositionOffset, bounceScale, bounceRotationZ,0.05f);
        yield return Animate(basePosition+hoverPositonOffset, hoverScale, hoverRotationZ,0.09f);
    }
    public IEnumerator PalyOutHoverRoutine()
    {
        // 바운스 후 호버 아웃 애니메이션
        yield return Animate(basePosition+outBouncePositionOffset, outBounceScale, outBounceRotationZ,0.05f);
        yield return Animate(basePosition,baseScale,baseRotationZ,0.05f);
    }
    private void StopCurrentCorutine()
    {
        if(moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
    }
    public void PlayHoverIn()
    {
        CardCanvas.sortingOrder = 100;
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(PlayHoverRoutine());
    }
    public void PlayHoverOut()
    {
        CardCanvas.sortingOrder = 0;
        StopCurrentCorutine();
        moveRoutine = StartCoroutine(PalyOutHoverRoutine());
    }
}
