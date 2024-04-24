using UnityEngine;
using UnityEngine.EventSystems;

public class CursorMineStone : MonoBehaviour
{
    [SerializeField] int _health = 5;

    private void OnMouseDown()
    {
        //particles on mining
        //reduces health of stone
        _health--;
        //mine threshold reach = break, spawn pebbles as items
        if (_health <= 0)
        {
            BreakStone();
        }
    }

    private void OnMouseEnter()
    {
        //change sprite as highlighted
    }

    private void OnMouseExit()
    {
        //change sprite as unhighlighted        
    }

    private void BreakStone()
    {
        Destroy(gameObject);
    }
}
