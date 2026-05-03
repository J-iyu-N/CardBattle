using UnityEngine;

public class EnemySkillHoverController : MonoBehaviour
{
    public EnemyUIDirecotr enemyUIDirecotr;

    private void OnMouseEnter()
    {
        GetComponent<AudioSource>().Play();
        enemyUIDirecotr.ShowPanel();
    }

    private void OnMouseExit()
    {
        enemyUIDirecotr.HidePanel();
    }
}