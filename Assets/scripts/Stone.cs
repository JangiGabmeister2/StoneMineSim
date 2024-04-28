using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int health = 5;

    public GameObject[] pebbles;

    private int _healthBase = 0;

    private void Start()
    {
        _healthBase = health;
    }

    private void OnMouseDown()
    {
        StartCoroutine("DecreaseHealth");
    }

    private IEnumerator DecreaseHealth()
    {
        while (health > 0)
        {
            health--;

            yield return new WaitForSeconds(1);
        }

    }

    private void OnMouseUp()
    {
        StopCoroutine("DecreaseHealth");

        health = _healthBase;
    }

    private void OnMouseEnter()
    {
        //change sprite as highlighted
    }

    private void OnMouseExit()
    {
        //change sprite as unhighlighted        
    }

    public void SpawnPebbles()
    {
        int rand = Random.Range(1, 3);

        for (int i = 0; i < rand; i++)
        {
            GameObject pleb = Instantiate(pebbles[Random.Range(0, pebbles.Length)]);
            pleb.transform.position = GetRandomPosition();
        }
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 pos = Random.insideUnitCircle.normalized;

        pos += (Vector2)transform.position;

        return pos;
    }
}
