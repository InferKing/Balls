using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusBehaviour : MonoBehaviour
{
    private int count = 30;

    public void Calculate(int x)
    {
        count -= x;
        if (count <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
