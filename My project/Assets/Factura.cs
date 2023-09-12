using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Factura : MonoBehaviour
{
    public List<Producto> productos = new List<Producto>();
    public TextMeshProUGUI factura;
    public float total = 0;
    public int idFactura;

    public Factura(int idFactura) { this.idFactura = idFactura; }
    public void addProduct(Producto producto) { 
        productos.Add(producto);
        total += producto.precio;
    }

    private int calcularCantidad(Producto producto) {
        int cantidad = 0;
        foreach (Producto p in productos) {
            if(p.id == producto.id)
                cantidad ++;   
        }
        return cantidad;
    }


    public string toString() { 
        string facturaString = string.Empty;
        facturaString += "Factura "+idFactura+"\n";
        List<int> yaContados = new List<int>();
        foreach (var producto in productos)
            if (!yaContados.Contains(producto.id))
            {
                int cantidad = calcularCantidad(producto);
                facturaString += $"{producto.nombre}\t{producto.precio}\t{cantidad}\t{cantidad*producto.precio}\n";
                yaContados.Add(producto.id);
            }
        return facturaString;
    }
}
