using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : MonoBehaviour
{
    private enum OperationState
    {
        Checking,
        Creating,
    }

    [SerializeField] private GameObject[] _stonePrefabs;
    [SerializeField] private int _maxStoneCount = 15;
    [SerializeField] private float _generationInterval = 5f;

    private List<Vector2> _stonePositions = new List<Vector2>();
    private List<Stone> _stones = new List<Stone>();

    private bool _isSpawning = false;
    private OperationState _opState;

    private int _stoneCount => transform.childCount;

    private void Start()
    {
        _opState = OperationState.Checking;
    }

    private void Update()
    {
        switch (_opState)
        {
            //checking state - checks whether a stone has been broken therefore needs to refill the area.
            case OperationState.Checking:
                StoneBreak();
                if (_stoneCount < _maxStoneCount)
                {
                    _opState = OperationState.Creating;
                }
                break;
                //creating state - creates stone mounds in the area.
            case OperationState.Creating:
                while (_stoneCount < _maxStoneCount && !_isSpawning)
                {
                    StartCoroutine("TrySpawnStone");
                    _isSpawning = true;
                }
                _opState = OperationState.Checking;
                _isSpawning = false;
                break;
            default:
                break;
        }
    }

    //attempts to spawn a stone mound in the area
    private IEnumerator TrySpawnStone()
    {
        int r = Random.Range(0, _stonePrefabs.Length);

        Stone mound = Instantiate(_stonePrefabs[r], transform).GetComponent<Stone>();
        _stones.Add(mound);

        Vector2 randPos = Vector2.zero;

        foreach (Vector2 pos in _stonePositions)
        {
            randPos = FindRandomPosition();

            if (randPos != pos)
            {
                continue;
            }

            yield return null;
        }

        mound.transform.position = randPos;
        _stonePositions.Add(randPos);

        yield return new WaitForSeconds(_generationInterval);
    }

    //creates random position in the area
    private Vector2 FindRandomPosition()
    {
        Vector2 pos = new Vector2(
            Random.Range(-15, 15),
            Random.Range(4, -9));

        return pos;
    }

    //checks all stone health and deletes it if it has no health
    private void StoneBreak()
    {
        for (int i = 0; i < _stones.Count; i++)
        {
            if (_stones[i].health <= 0)
            {
                ScoreManager.instance.IncreaseDecreaseStone(1);

                _stones[i].SpawnPebbles();

                _stonePositions.RemoveAt(i);

                Destroy(_stones[i].gameObject);

                _stones.RemoveAt(i);
            }
        }
    }
}
