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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarFactura() {
        textoProductos.text = facturaActual.toString();
        textoDinero.text = "Total: "+ facturaActual.total;
    }
}


