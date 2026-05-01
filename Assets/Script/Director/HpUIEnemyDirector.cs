using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpUIEnemyDirector : MonoBehaviour
{
    public EnemyController enemyController;
    public GameObject hpGuage;
    public TextMeshProUGUI hpText;
    public GameObject shieldGuage;
    public TextMeshProUGUI shiledText;
    public void Start()
    {
        RefreshHP();
    }
    public void RefreshHP()
    {
        hpGuage.GetComponent<Image>().fillAmount = 
        (float)enemyController.State.CurrentHp/enemyController.Data.maxHP;
        hpText.text = enemyController.State.CurrentHp+"/"+enemyController.Data.maxHP;

        if(enemyController.shield == 0)
        {
            shieldGuage.SetActive(false);
        }
        else
        {
            shieldGuage.SetActive(true);
            shieldGuage.GetComponent<Image>().fillAmount = 1f;
            shiledText.text = enemyController.shield.ToString();
        }
    }
}
