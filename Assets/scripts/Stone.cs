using UnityEngine;

public class Stone : MonoBehaviour
{
    public int health = 5;

    public GameObject[] pebbles;

    private void OnMouseDown()
    {
        //particles on mining
        //reduces health of stone
        health--;
        //mine threshold reach = break, spawn pebbles as items

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
