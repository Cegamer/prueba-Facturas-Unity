using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliente : MonoBehaviour
{
    public List<Producto> carrito = new List<Producto>();

    public escaner escaner;

    public GameObject[] Posiciones;
    public int Actual = 0, siguiente = 1;

    public Animator animator;
    public bool estaEnCaja = false;

    // Start is called before the first frame update
    void Start()

    {
        this.transform.position = Posiciones[0].GetComponent<Transform>().position ;
        this.estaEnCaja = false;
        escaner = FindObjectOfType<escaner>();
    }

    // Update is called once per frame
    void Update()
    {
        seguirRuta();
    }

    public void vaciarCarrito() {
        if (carrito.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    var p = carrito[i];
                    p.gameObject.transform.position = escaner.posicionesEnCaja[i];
                    p.transform.parent = null;
                    carrito.Remove(p);
                    i++;
                }
                catch { }
            }
            Debug.Log(carrito.Count);
        }
    }
    public void agregarProsducto(GameObject productoGO) {
        carrito.Add(productoGO.GetComponent<Producto>());   
    }

    public void seguirRuta()
    {
        if (Actual < 9)
        {
            if (transform.position == Posiciones[siguiente].GetComponent<Transform>().position)
            {
                Actual = siguiente;
                siguiente++;
            }

            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Posiciones[siguiente].GetComponent<Transform>().position, 10 * Time.deltaTime);
                transform.LookAt(Posiciones[siguiente].GetComponent<Transform>());
            }
        }
        else
        {
            llegarACaja();
        }
    }

    public void llegarACaja()
    {
        if (!estaEnCaja)
        {
            estaEnCaja = true;
            vaciarCarrito();
            animator.SetInteger("legs", 5);
        }
        
    }

    public void irse()
    {
        Destroy(this.gameObject);
    }
}
