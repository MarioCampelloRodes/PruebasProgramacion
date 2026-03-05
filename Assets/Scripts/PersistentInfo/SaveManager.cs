using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Librería para poder crear y leer archivos
using UnityEngine.Events;


//Si se va a guardar con JSON, hay que marcar el struct como serializable
[System.Serializable]

public struct SaveData
{
    public List<uint> openChestsIDs;
}

public class SaveManager
{
    //Nombre del archivo creado y extensión (se puede poner la que sea)
    static string fileName = "Data.mondongo";

    public static UnityAction<SaveData> OnDataLoaded;

    public static void Save(List<uint> openChests)
    {
        //Crear unos datos de guardado nuevos
        SaveData saveData = new SaveData();

        //Asignar a los datos de guardado la lista de cofres abiertos (nueva lista con los mismos valores, sino las cuando cambias una la otra también)
        saveData.openChestsIDs = new List<uint>(openChests);

        //Transformar el SaveData en un string con formato JSON
        string dataJson = JsonUtility.ToJson(saveData);

        //Generar la ruta del archivo y crearlo
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        //Encriptar la información en formato JSON
        dataJson = XOREncryption.EncryptDecrypt(dataJson);

        //Crear el archivo de guardado en una ruta con un nombre y los datos JSON
        File.WriteAllText(filePath, dataJson);
    }

    public static void Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        //Si no hay ningún archivo de guardado, no intenta cargarlo
        if (!File.Exists(filePath))
        {
            return;
        }

        //Leer los archivos de guardado en formato JSON
        string dataJson = File.ReadAllText(filePath);

        //Desencriptar la información en formato JSON antes de transformarla en SaveData
        dataJson = XOREncryption.EncryptDecrypt(dataJson);

        SaveData saveData = JsonUtility.FromJson<SaveData>(dataJson);

        //Una vez está todo cargado, se llama al callback pasando esta información
        OnDataLoaded?.Invoke(saveData);
    }


}
