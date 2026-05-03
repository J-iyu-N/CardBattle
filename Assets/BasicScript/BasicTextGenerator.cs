using UnityEngine;

public class BasicTextGenerator : MonoBehaviour
{
    public GameObject textPrefab;

    private void OnMouseDown()
    {
        SpawnText();
    }
    public void SpawnText()
    {
        float posX = Random.Range(-3f, 3f);
        float posY = Random.Range(-1f, 5f);

        Vector2 spawnPoint = (Vector2)transform.position + new Vector2(posX, posY);

        GameObject textObject = Instantiate(textPrefab) as GameObject;
        textObject.transform.position = spawnPoint;

        int damage = Random.Range(1, 11);

        BasicTextController textController = textObject.GetComponent<BasicTextController>();

        if (textController != null)
        {
            textController.Init(damage);
        }
    }
}