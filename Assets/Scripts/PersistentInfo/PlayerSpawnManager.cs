using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SpawnPoint
{
    public string id;
    public Transform point;
}
public class PlayerSpawnManager : MonoBehaviour
{
    //Lista con todos los puntos de Spawn de una única escena
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private string spawnID;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //Para acceder a la escena en la que nos encontramos actualmente
        Scene currentScene = SceneManager.GetActiveScene();

        //Guardar el valor de la ID de Spawn asignada en Persistent info
        spawnID = PersistentInfo.Singleton.currentSpawnPointID;

        //Buscar el punto con la ID guardada
        Transform spawnPoint = GetSpawnPoint(spawnID);

        if(spawnPoint != null)
        {
            //Modificar posicion y rotación del player
            player.position = spawnPoint.position;
            player.rotation = spawnPoint.rotation;
        }
        
    }

    Transform GetSpawnPoint(string idToGet)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i].id == idToGet)
            {
                return spawnPoints[i].point;
            }
        }
        return null;
    }
}
