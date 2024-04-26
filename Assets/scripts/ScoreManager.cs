using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public int stoneBreaks, collectedPebbles;
    public Text stoneText, pebbleText;

    #region Singleton
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Update()
    {
        stoneText.text = $"{stoneBreaks} Stone Broken";
        pebbleText.text = $"{collectedPebbles} Pebbles Collected";
    }

    public void IncreaseDecreaseStone(int i)
    {
        stoneBreaks += i;
    }

    public void IncreaseDecreasePebbles(int i)
    {
        collectedPebbles += i;
    }
}
