using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Factura : MonoBehaviour
{
    public List<Producto> productos = new List<Producto>();
    public TextMeshProUGUI factura;

    public void addProduct(Producto producto) { 
        productos.Add(producto);
    }

    public void mostrar() {

        Debug.Log("FUNCIONA");
        factura.text = toString();
    }

    public string toString() { 
        string facturaString = string.Empty;
        facturaString += "Factura \n";
        foreach (var producto in productos)
            facturaString += $"{producto.nombre}\n\t{producto.precio}\n";
        return facturaString;
    }
}
