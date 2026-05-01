using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpUIEnemyController : MonoBehaviour
{
    public EnemyController enemyController;
    public GameObject hpGuage;
    public TextMeshProUGUI hpText;
    public void Start()
    {
        RefreshHP();
    }
    public void RefreshHP()
    {
        hpGuage.GetComponent<Image>().fillAmount = 
        (float)enemyController.State.CurrentHp/enemyController.Data.maxHP;
        hpText.text = enemyController.State.CurrentHp+"/"+enemyController.Data.maxHP;
    }
}
