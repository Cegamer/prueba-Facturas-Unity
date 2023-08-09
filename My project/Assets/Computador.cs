using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Computador : MonoBehaviour
{
    public escaner escaner;
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
        textoProductos.text = escaner.facturaActual.toString();
        textoDinero.text = "Total: "+ escaner.facturaActual.total;
    }
}


