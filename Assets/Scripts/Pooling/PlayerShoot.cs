using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Para poder usar el sistema de Pool de Unity
using UnityEngine.Pool;

public class PlayerShoot : MonoBehaviour
{
    //El prefab que utilizará para crear los objetos del pool
    [SerializeField] private PlayerProjectile projectilePrefab;

    private ObjectPool<PlayerProjectile> projectilePool;

    [SerializeField] private Transform shootOrigin;

    // Start is called before the first frame update
    void Start()
    {
        //Opción 1: Cantidad de objetos mínima (10) y máxima (10000) por defecto
        projectilePool = new ObjectPool<PlayerProjectile>(CreateProjectile, GetProjectile, ReleaseProjectile);
        //Opción 2: Especificar cantidad mínima y máxima
        //projectilePool = new ObjectPool<PlayerProjectile>(CreateProjectile, GetProjectile, ReleaseProjectile, null, true, 20, 100);
    }

    //Esta función se llama al crear el pool tantas veces como objetos pueda tener   
    private PlayerProjectile CreateProjectile()
    {
        //Crear un nuevo proyectil
        PlayerProjectile projectile = Instantiate(projectilePrefab);

        //Asignar el pool del proyectil
        projectile.pool = projectilePool;

        //Desactivar el proyectil para que empiece oculto
        projectile.gameObject.SetActive(false);

        return projectile;
    }

    //Se llama cada vez que se coja un proyectil del bool
    private void GetProjectile(PlayerProjectile projectile)
    {
        //Al sacar un objeto del pool, lo primero es activarlo
        projectile.gameObject.SetActive(true);

        //Mover el proyectil al punto de origen del disparo
        projectile.transform.position = shootOrigin.position;

        //Añadir fuerza al proyectil

    }

    //Se llama cada vez que un proyectil vuelve al bool
    private void ReleaseProjectile(PlayerProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            projectilePool.Get();
        }
    }
}
