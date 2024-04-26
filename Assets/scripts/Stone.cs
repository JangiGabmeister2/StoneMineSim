using UnityEngine;

public class Stone : MonoBehaviour
{
    public int health = 5;

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
}
