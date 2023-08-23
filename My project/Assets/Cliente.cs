using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    List <Producto> carrito = new List <Producto> ();
    public escaner escaner;
    public Vector3[] Posiciones = new Vector3[10];
    public Vector3[] Rotaciones = new Vector3[10];
    public int Actual = 0, siguiente = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Posiciones[0];
        this.transform.rotation = Quaternion.Euler(Rotaciones[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == Posiciones[siguiente])
        {
            Actual = siguiente;
            siguiente++;
        }
        else { 
            transform.position = Vector3.MoveTowards(transform.position, Posiciones[siguiente], 3 * Time.deltaTime); 
        }
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
