using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ControladorEscena : MonoBehaviour
{
    public GameObject prefabClientes;
    public escaner escaner;
    public List<GameObject> listaProductos;
    public Cliente clienteActual;
    public GameObject[] posicionesCliente;

    // Start is called before the first frame update
    void Start()
    {
        generarCliente();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void generarCliente() {
        if (clienteActual == null)
        {
            GameObject cliente = Instantiate(prefabClientes);
            clienteActual = cliente.GetComponent<Cliente>();
            var clienteData = cliente.GetComponent<Cliente>();
            clienteData.Posiciones = posicionesCliente;
            for (int i = 0; i < 10; i++)
            {
                var producto = Instantiate(listaProductos[Random.Range(0, listaProductos.Count - 1)]);
                producto.transform.parent = cliente.transform;
                producto.transform.localPosition = new Vector3(0, -0.77f, 2.244f);
                producto.GetComponent<Rigidbody>().useGravity = true;
                producto.GetComponent<Rigidbody>().isKinematic = true;
                clienteData.agregarProsducto(producto);

            }
        }
    }
}
