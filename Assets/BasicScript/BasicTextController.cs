using UnityEngine;
using TMPro;

public class BasicTextController : MonoBehaviour
{
    public TextMeshPro text;
    public Rigidbody2D rigid;
    public float lifeTime = 1.5f;


    public void Init(int damage)
    {
        if (text != null)
        {
            text.text = "-" + damage.ToString();
        }

        ApplyRandomForce();

        Destroy(gameObject, lifeTime);
    }

    public void ApplyRandomForce()
    {
        if (rigid == null)
        {
            return;
        }

        float xForce = Random.Range(-1f, 1f);
        float yForce = Random.Range(-1f, 1f);

        rigid.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }
}