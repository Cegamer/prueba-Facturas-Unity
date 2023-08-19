using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ControladorEscena : MonoBehaviour
{
    public List<Vector3> posicionesFila;
    public List<GameObject> prefabClientes;
    public escaner escaner;
    public List<GameObject> listaProductos;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.B)) { generarCliente(); }
    }

    void generarCliente() {
        GameObject cliente = Instantiate(prefabClientes[0], posicionesFila[0],Quaternion.identity);
        var clienteData = cliente.GetComponent<Cliente>();
        for(int i = 0; i < 10; i++)
        {
            var producto = Instantiate(listaProductos[Random.Range(0, listaProductos.Count)]);
            producto.transform.parent = cliente.transform;
            producto.transform.localPosition = new Vector3(-2f, -0.3f, 0);
            clienteData.agregarProsducto(producto);
        }
    
    }
}
