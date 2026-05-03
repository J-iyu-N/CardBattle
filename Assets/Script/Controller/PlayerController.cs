using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public SpriteRenderer spriteRenderer;
    public Sprite idle;
    public Sprite[] walk;
    public Sprite[] attack;

    private bool isMoving;
    private float time;
    private int idx;

    [System.Obsolete]
    void Update()
    {
        isMoving = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = true;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = false;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            isMoving = true;
        }
        

        if (isMoving == false)
        {
            spriteRenderer.sprite = idle;
            idx = 0;
            time = 0f;
        }
        else
        {
            
            if (walk == null || walk.Length == 0) return;

            time += Time.deltaTime;

            if (time > 0.1f)
            {
                time = 0f;
                spriteRenderer.sprite = walk[idx];
                idx = (idx + 1) % walk.Length;
            }

        }
    }
}

