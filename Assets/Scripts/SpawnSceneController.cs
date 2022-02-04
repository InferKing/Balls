using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSceneController : MonoBehaviour
{
    [SerializeField] private GameObject[] gObjs;
    private GameObject gObj;
    private void Awake()
    {
        gObj = gObjs[Random.Range(0, gObjs.Length)];
        GameObject g = Instantiate(gObj);
        g.transform.position = transform.position;
    }
}
