using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [HideInInspector] public List<int> indexes = new List<int>();
    private bool IAmDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BucketTrigger")
        {
            gameObject.SetActive(false);
            IAmDead = true;
            GameManager.OnBallDestroyed?.Invoke(1);
            PlayerData.OnPlayerDataChange?.Invoke(1);
            Destroy(gameObject, 3);
        }
        else if (other.tag == "multi")
        {
            if (!IsUsed(other.gameObject))
            {
                GameManager.OnBallDown?.Invoke(other.gameObject, gameObject);
            }
        }
        else if (other.tag == "minus")
        {
            gameObject.SetActive(false);
            IAmDead = true;
            other.gameObject.GetComponent<MinusBehaviour>().Calculate(1);
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "multi")
        {
            if (!IsUsed(other.gameObject))
            {
                GameManager.OnBallDown?.Invoke(other.gameObject, gameObject);
            }
        }
        else if (other.tag == "minus")
        {
            gameObject.SetActive(false);
            IAmDead = true;
            other.gameObject.GetComponent<MinusBehaviour>().Calculate(1);
            Destroy(gameObject, 2);
        }
    }

    private bool IsUsed(GameObject gObj)
    {
        if (indexes.Contains(gObj.GetInstanceID())) 
        {
            return true;
        }
        else
        {
            indexes.Add(gObj.GetInstanceID());
            return false;
        }
    }

    public bool GetStatus()
    {
        return IAmDead;
    }
}
