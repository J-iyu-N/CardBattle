using UnityEngine;

public class EnemyPanelController : MonoBehaviour
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