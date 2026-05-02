using UnityEngine;
using TMPro;

public class TextUIGenerator : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healTextPrefab;
    public Vector2 spawnPoint;
    public float lifeTime = 1f;
    public void SpawnText(GameObject prefab, Vector2 targetPos, int value)
    {
        // 여기서 판단해서 아래 매서드 호출
        float posX = Random.Range(-1f,1f);
        float posY = Random.Range(-0.5f,0.5f);

        Vector2 spawnPoint = targetPos + new Vector2(posX,posY);

        GameObject textPrefab = Instantiate(prefab);
        textPrefab.transform.position = spawnPoint;

        if(prefab == damageTextPrefab)
        textPrefab.GetComponent<TextMeshPro>().text = "-"+value.ToString();
        else if(prefab == healTextPrefab)
        textPrefab.GetComponent<TextMeshPro>().text = "+"+value.ToString();

        Destroy(textPrefab,lifeTime);
    }
    public void SpawnDamageText(Vector2 targetPos, int value)
    {
        SpawnText(damageTextPrefab,targetPos,value);
    }
    public void SpawnHealText(Vector2 targetPos, int value)
    {
        SpawnText(healTextPrefab,targetPos,value);
    }
    public void ApplyForce()
    {
        
    }
}