using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    List <Producto> carrito = new List <Producto> ();
    public escaner escaner;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void vaciarCarrito() {
        int i = 0;
        foreach (Producto p in carrito) {
            p.gameObject.transform.position = escaner.posicionesEnCaja[i]; 
            i++;
        }
    }
    public void agregarProsducto(GameObject productoGO) {
        carrito.Add(productoGO.GetComponent<Producto>());   
    }
}
