using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Computador : MonoBehaviour
{
    public escaner escaner;
    public Factura facturaActual;


    public TextMeshProUGUI textoProductos;
    public TextMeshProUGUI textoDinero;
    // Start is called before the first frame update
    void Start()
    {
        facturaActual = new Factura();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarFactura() {
        textoProductos.text = facturaActual.toString();
        textoDinero.text = "Total: "+ facturaActual.total;
    }

    public void imprimir()
    {
        if (escaner.controladorEscena.clienteActual.carrito.Count < 1)
        {
            Debug.Log("Llego");
            escaner.controladorEscena.clienteActual.irse();
            escaner.controladorEscena.generarCliente();
        }
    }
}


