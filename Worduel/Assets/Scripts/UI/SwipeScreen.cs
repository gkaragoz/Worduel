using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeScreen : MonoBehaviour, IDragHandler, IEndDragHandler {

    [SerializeField]
    private float _percentThreshold = 0.2f;
    [SerializeField]
    private float _easing = 0.5f;
    [SerializeField]
    private int _totalPages = 1;

    private Vector3 _panelLocation;
    private int _currentPage = 1;

    private void Start() {
        _panelLocation = transform.position;
    }

    private IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds) {
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    public void OnDrag(PointerEventData data) {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = _panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data) {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= _percentThreshold) {
            Vector3 newLocation = _panelLocation;
            if (percentage > 0 && _currentPage < _totalPages) {
                _currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            } else if (percentage < 0 && _currentPage > 1) {
                _currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, _easing));
            _panelLocation = newLocation;
        } else {
            StartCoroutine(SmoothMove(transform.position, _panelLocation, _easing));
        }
    }

}
