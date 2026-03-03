using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Librería para poder crear y leer archivos

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

    public static void Save(List<uint> openChests)
    {
        //Crear unos datos de guardado nuevos
        SaveData saveData = new SaveData();

        //Inicializar la lista para que no de error
        saveData.openChestsIDs = new List<uint>();

        //Asignar a los datos de guardado la lista de cofres abiertos (nueva lista con los mismos valores, sino las cuando cambias una la otra también)
        saveData.openChestsIDs = new List<uint>(saveData.openChestsIDs);

        //Transformar el SaveData en un string con formato JSON
        string dataJson = JsonUtility.ToJson(saveData);

        //Generar la ruta del archivo y crearlo
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        //Crear el archivo de guardado en una ruta con un nombre y los datos JSON
        File.WriteAllText(filePath, dataJson);
    }
}
