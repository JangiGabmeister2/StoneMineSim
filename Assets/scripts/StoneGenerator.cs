using System.Collections;
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
        int r = Random.Range(0, _stonePrefabs.Length - 1);

        GameObject mound = Instantiate(_stonePrefabs[r], transform);
        mound.transform.position = FindRandomPosition();

        yield return new WaitForSeconds(_generationInterval);
    }

    private Vector2 FindRandomPosition()
    {
        Vector2 pos = new Vector2(
            Random.Range(-15, 15),
            Random.Range(4, -9));

        return pos;
    }
}
