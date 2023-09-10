using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ControladorEscena : MonoBehaviour
{
    public List<GameObject> prefabClientes;
    public escaner escaner;
    public List<GameObject> listaProductos;
    public Cliente clienteActual;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) { generarCliente(); }
    }

    public void generarCliente() {
        if (clienteActual == null)
        {
            GameObject cliente = Instantiate(prefabClientes[0]);
            clienteActual = cliente.GetComponent<Cliente>();
            var clienteData = cliente.GetComponent<Cliente>();
            for (int i = 0; i < 10; i++)
            {
                var producto = Instantiate(listaProductos[Random.Range(0, listaProductos.Count - 1)]);
                producto.transform.parent = cliente.transform;
                producto.transform.localPosition = new Vector3(-2f, -0.3f, 0);
                producto.GetComponent<Rigidbody>().useGravity = true;
                producto.GetComponent<Rigidbody>().isKinematic = true;
                clienteData.agregarProsducto(producto);

            }
        }
    }
}
