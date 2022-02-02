using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class Data : MonoBehaviour
{
    private string filename = "/data.dat";
    [HideInInspector] public int scoreToSer = 0;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath + filename);
    }
    public void LoadGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + filename))
        {
            FileStream file = File.Open(Application.persistentDataPath + filename, FileMode.Open);
            scoreToSer = (int)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            FileStream file = File.Open(Application.persistentDataPath + filename, FileMode.CreateNew);
            bf.Serialize(file,0);
            file.Close();
        }
    }

    public void SaveGameData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + filename, FileMode.Create);
        bf.Serialize(file, scoreToSer);
        file.Close();
    }
    
}
