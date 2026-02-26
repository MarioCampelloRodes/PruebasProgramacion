using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackCrt());
        }
    }

    private IEnumerator AttackCrt()
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        hitbox.SetActive(false);
    }
}
