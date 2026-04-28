using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private GameObject door;
    //La ID del punto de spawn al que debe ir en la siguiente escena
    [SerializeField] private string spawnPointID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Asignar la nueva ID de spawn en el PersistentInfo
            PersistentInfo.Singleton.currentSpawnPointID = spawnPointID;

            //Con el Paquete LeanTween se pueden animar objetos y sus propiedades sin usar animator
            //Este tween anima la puerta para que acabe teniendo 145 grados en la rotación Y y tarde 2 segundos.
            //Para buscar los setEases, it a la página easings.net. Sirven para cambiar la curva de animación
            //El setOnComplete es un callback al que se le añade una función que ejecutar al terminar el tween
            door.LeanRotateY(145f, 2f).setEaseOutBounce().setOnComplete(ChangeScene);
            SceneTransitions.Singleton.FadeIn();

            
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
