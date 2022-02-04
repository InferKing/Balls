using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject[] _pull;
    public float sensitivity;
    public bool _isTouched = false;
    private bool stopMotion = false;
    private void Start()
    {
        StartCoroutine(StartControl());
    }

    private IEnumerator StartControl()
    {
        while (!_isTouched && !stopMotion)
        {
            Vector3 mousePos = Input.mousePosition, worldPos;
            mousePos.z = 17.5f;
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.y = transform.position.y;
            if (worldPos.x < -3.8f)
            {
                worldPos.x = -3.8f;
            }
            else if (worldPos.x > 3.8f)
            {
                worldPos.x = 3.8f;
            }
            transform.position = worldPos;
            if (Input.GetKeyDown(KeyCode.Mouse0) && !_isTouched)
            {
                stopMotion = true;
                StartCoroutine(Spawn());
            }
            yield return null;
        }
    }
    private IEnumerator Spawn()
    {
        GameObject gObj = null;
        while ((gObj = GetBall()) != null)
        {
            gObj.SetActive(true);
            gObj.transform.position = _spawner.transform.position;
            yield return new WaitForSeconds(0.4f);
        }
        _isTouched = true;
        yield return null;
    }

    private GameObject GetBall()
    {
        foreach (var item in _pull)
        {
            if (!item.activeSelf && !item.GetComponent<BallBehaviour>().GetStatus()) return item;
        }
        return null;
    }
}
