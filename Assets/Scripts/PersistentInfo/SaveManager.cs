using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Librería para poder crear y leer archivos
using UnityEngine.Events;


//Si se va a guardar con JSON, hay que marcar el struct como serializable
//Lo que previamente era un struct, se ha cambiado a class para poder modificar su valor desde distintos scripts y que se guarde
//Los structs no se pueden modificar desde otros scripts, ya que al referenciarlos se toma una copia de su valor, no el valor original
[System.Serializable]

public class SaveData
{
    public List<uint> openChestsIDs;
}

public class SaveManager
{
    //Nombre del archivo creado y extensión (se puede poner la que sea)
    static string fileName = "Data.mondongo";
    static SaveData saveData = new SaveData();

    public static UnityAction<SaveData> OnDataLoaded;
    public static UnityAction<SaveData> OnDataSaved;

    public static void Save(List<uint> openChests)
    {
        OnDataSaved?.Invoke(saveData);

        //Transformar el SaveData en un string con formato JSON
        string dataJson = JsonUtility.ToJson(saveData);

        //Generar la ruta del archivo y crearlo
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        //Encriptar la información en formato JSON
        dataJson = XOREncryption.EncryptDecrypt(dataJson);

        //Crear el archivo de guardado en una ruta con un nombre y los datos JSON
        File.WriteAllText(filePath, dataJson);
    }

    [RuntimeInitializeOnLoadMethod] //Esta función se llama después del awake, como si estuviera en Start()
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
