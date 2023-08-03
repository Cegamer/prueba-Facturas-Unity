using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factura : MonoBehaviour
{
    public List<Producto> productos = new List<Producto>();

    public void addProduct(Producto producto) { 
        productos.Add(producto);
    }


    public string toString() { 
        string facturaString = string.Empty;
        facturaString += "Factura \n";
        foreach (var producto in productos)
            facturaString += $"{producto.nombre}\n\t{producto.precio}";
        return facturaString;
    }
}
