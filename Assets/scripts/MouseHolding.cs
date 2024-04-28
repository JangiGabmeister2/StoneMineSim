using UnityEngine;
using UnityEngine.UI;

public class MouseHolding : MonoBehaviour
{
    [SerializeField] private GameObject _circle;
    [SerializeField] private float _fillAmount = 1;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            _circle.SetActive(true);
            _circle.transform.position = Input.mousePosition;

            _fillAmount--;
            _circle.GetComponent<Image>().fillAmount = _fillAmount;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _circle.SetActive(false);
        }
    }
}
