using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escaner : MonoBehaviour
{
    // Start is called before the first frame update
    public Factura facturaActual;
    public Computador computador;
    public List<Vector3> posicionesEnCaja;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { facturaActual.mostrar(); }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Producto>() != null)
        {
            var producto = other.gameObject.GetComponent<Producto>();
            if (!producto.facturado)
            {
                producto.facturado = true;
                facturaActual.addProduct(producto);
                computador.mostrarFactura();
            }
        }
    }

}
