using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bulletSpawner;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = Instantiate(bullet.gameObject, bulletSpawner.transform.position, transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
        }       
    }
}
