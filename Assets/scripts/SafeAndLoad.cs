using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SafeAndLoad : MonoBehaviour
{
    public List<int> list1 = new List<int>();

    public Vector3 xyz = new Vector3();

    public SaveData data;


    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Save();
    }

    public void Save()
    {
        if (!Directory.Exists (Application.dataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.dataPath + "/saves");
        }

        BinaryFormatter bf = new BinaryFormatter();
        CopySaveData();

        FileStream file = File.Create(Application.dataPath + "/saves/SaveData.dat");

        bf.Serialize(file, data);

        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.dataPath + "/saves/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/saves/SaveData.dat", FileMode.Open);
            data = (SaveData)bf.Deserialize(file);
            CopyLoadData();
            file.Close();
        }
    }

    public void CopyLoadData()
    {
        list1.Clear();

        foreach(int i in data.list1)
        {
            list1.Add(i);
        }

        xyz = SerVector3ToVector3(data.position);
    }
    
    public void CopySaveData()
    {
        data.list1.Clear ();

        foreach(int i in list1)
        {
            data.list1.Add(i);
        }

        data.position = Vector3ToSerVector3(xyz);
    }

    public static SerVector3 Vector3ToSerVector3(Vector3 V3)
    {
        SerVector3 SV3 = new SerVector3();

        SV3.x = V3.x;
        SV3.y = V3.y;
        SV3.z = V3.z;

        return SV3;
    }

    public static Vector3 SerVector3ToVector3(SerVector3 SV3)
    {
        Vector3 V3 = new Vector3();

        V3.x = SV3.x;
        V3.y = SV3.y;
        V3.z = SV3.z;

        return V3;
    }
}

//Inserire qui i tipi di dati da salvare che servono al nostro gioco
[System.Serializable]
public class SaveData
{
    public List<int> list1 = new List<int>();
    public SerVector3 position;


}

[System.Serializable]
public class SerVector3
{
    public float x;
    public float y;
    public float z;

}
