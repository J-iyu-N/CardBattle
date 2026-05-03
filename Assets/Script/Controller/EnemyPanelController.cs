using UnityEngine;

public class EnemySkillHoverController : MonoBehaviour
{
    public EnemyUIDirecotr enemyUIDirecotr;

    private void OnMouseEnter()
    {
        enemyUIDirecotr.ShowPanel();
    }

    private void OnMouseExit()
    {
        enemyUIDirecotr.HidePanel();
    }
}