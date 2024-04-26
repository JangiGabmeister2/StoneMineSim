using UnityEngine;

public class Pebble : MonoBehaviour
{
    private void OnMouseEnter()
    {
        ScoreManager.instance.IncreaseDecreasePebbles(1);

        Destroy(gameObject, 0.2f);
    }
}
