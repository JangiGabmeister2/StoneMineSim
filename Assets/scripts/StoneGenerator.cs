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
            case OperationState.Checking:
                StoneBreak();
                if (_stoneCount < _maxStoneCount)
                {
                    _opState = OperationState.Creating;
                }
                break;
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

    private Vector2 FindRandomPosition()
    {
        Vector2 pos = new Vector2(
            Random.Range(-15, 15),
            Random.Range(4, -9));

        return pos;
    }

    private void StoneBreak()
    {
        for (int i = 0; i < _stones.Count; i++)
        {
            if (_stones[i].health <= 0)
            {
                _stonePositions.RemoveAt(i);

                Destroy(_stones[i].gameObject);

                _stones.RemoveAt(i);
            }
        }
    }
}
