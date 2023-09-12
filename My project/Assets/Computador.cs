using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Computador : MonoBehaviour
{
    public escaner escaner;
    public Factura facturaActual;
    public List<Factura> historialFacturas;


    public TextMeshProUGUI textoProductos;
    public TextMeshProUGUI textoDinero;

    public GameObject panelFactura, panelHistorialFacturas,contentHistorialFacturas;
    public GameObject facturaTemplate;
    int historialColumnaActual = 0;
    float totalObtenido = 0;


    public TextMeshProUGUI textoClientesSatisfechos,textoDineroObtenido;

    Vector3[] posicionColumnasVistaHistorial = 
        {
        new Vector3(-82, 60, 0), 
        new Vector3(-3, 60, 0), 
        new Vector3(77, 60, 0)
    };
    float posicionFilasVistaHistorial = 60;
    public List<GameObject> UIPanels;
    // Start is called before the first frame update
    void Start()
    {
        facturaActual = new Factura(historialFacturas.Count);
        UIPanels.Add(panelFactura);
        UIPanels.Add(panelHistorialFacturas);
        mostrarPanel(panelFactura);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarFactura() {
        textoProductos.text = facturaActual.toString();
        textoDinero.text = "Total: "+ facturaActual.total;
    }

    public void imprimir()
    {
        if (escaner.controladorEscena.clienteActual.carrito.Count < 1)
        {
            escaner.controladorEscena.clienteActual.irse();
            escaner.controladorEscena.clienteActual = null;
            escaner.controladorEscena.generarCliente();
            agregarAHistorialDeFacturas();
            eliminarProductosFacturados();
            facturaActual = new Factura(historialFacturas.Count);
            mostrarFactura();
        }
    }

    public void mostrarPanel(GameObject panel) { 
        foreach (GameObject p in UIPanels)
        {
            if(p != panel) 
                p.SetActive(false);
        }
        panel.SetActive(true);
    }
    public void agregarAHistorialDeFacturas()
    {
        historialFacturas.Add(facturaActual);
        var fact = Instantiate(facturaTemplate);
        rendedrizarFacturaEnVista(fact);
    }
    public void rendedrizarFacturaEnVista(GameObject fact) {
        fact.transform.SetParent(contentHistorialFacturas.transform,false);

        var posicion = posicionColumnasVistaHistorial[historialColumnaActual];
        posicion.y = posicionFilasVistaHistorial;

        fact.GetComponent<RectTransform>().anchoredPosition = posicion;
        fact.transform.localScale = new Vector3(22.7994328f, 35.0209351f, 23.1320515f);

        TextMeshProUGUI texto = fact.GetComponentInChildren<Transform>().GetChild(0).GetComponent<Transform>().GetComponent<TextMeshProUGUI>();
        texto.text = facturaActual.toString();
        
        historialColumnaActual += 1;
        if (historialColumnaActual == 3)
        {
            historialColumnaActual = 0;
            posicionFilasVistaHistorial -= 134;
        }
        totalObtenido += facturaActual.total;

        textoClientesSatisfechos.text = Convert.ToInt32(textoClientesSatisfechos.text) + 1 +"";
        textoDineroObtenido.text = "$ " + totalObtenido + "";

    }
    public void eliminarProductosFacturados() {
        foreach (var p in facturaActual.productos) { 
            Destroy(p.gameObject);
        }
    }
    public void salir() { Application.Quit(); }
}


