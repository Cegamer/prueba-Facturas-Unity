using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escaner : MonoBehaviour
{
    // Start is called before the first frame update
    public Factura facturaActual;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("paso");
        if(other.gameObject.GetComponent<Producto>() != null)
            facturaActual.addProduct(other.gameObject.GetComponent<Producto>());
    }

}
