using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class cameraController : MonoBehaviour

{
    public float sensitivity = 3f, velocidadMoviminento = 5f;
    public Transform selected = null;
    Vector3 defaultPosition = new Vector3(-0.84799999f, 3.76999998f, -9.81799984f);

    public GameObject[] construiblesGO;
    public List<IConstruible> construibles = new List<IConstruible>();
    public IConstruible construibleSeleccionado;

    public EstadosVista estadoVista;
    public enum EstadosVista : int
    {
        ModoCajero = 0,
        ModoComputador = 1,
        ModoConstruccion = 2
    }
    // Start is called before the first frame update
    void Start()

    {
        estadoVista = EstadosVista.ModoConstruccion;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = retornarObjetoRaycast().transform;
            if (selected.name == "Monitor" && estadoVista != EstadosVista.ModoComputador)
                cambiarEstadoVista(EstadosVista.ModoComputador);
        }

        if (Input.GetMouseButtonUp(0))
            selected = null;

        if (estadoVista == EstadosVista.ModoCajero)
        {
            this.transform.eulerAngles += sensitivity * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            moverProducto();
        }

        if (Input.GetKeyDown(KeyCode.F))
            cambiarEstadoVista(EstadosVista.ModoCajero);

        if (estadoVista == EstadosVista.ModoConstruccion)
        {
            var a = retornarObjetoRaycast();
            if(a != null)
                selected =a.transform;
            ModoConstruccion();
        }

        ajustarVistaCamara();
    }

    public void moverProducto()
    {
        if (selected == null) return;

        Producto producto = selected.GetComponent<Producto>();
        if (producto == null) return;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        selected.transform.position = worldPosition;
    }

    public void setConstructionObject(int objetoId) {
        var sop = new soporte();
        sop.setGameObject(Instantiate(construiblesGO[objetoId]));
        construibles.Add(sop);
        construibleSeleccionado = sop;
    }

    public void ModoConstruccion()
    {

        if (selected == null) return;

        GridSquare gridSquare = selected.GetComponent<GridSquare>();

        if (gridSquare == null) return;

        if (construibleSeleccionado == null) return;

        selected.GetComponent<GridSquare>().preview(construibleSeleccionado);

        if (Input.GetMouseButtonDown(0))
        {
            if(selected.GetComponent<GridSquare>().colocar())
                construibleSeleccionado = null;
        }
    }

    public GameObject retornarObjetoRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000000)) return hit.transform.gameObject;
        return null;
    }


    void cambiarEstadoVista(EstadosVista estado)
    {
        estadoVista = estado;
    }
    void ajustarVistaCamara()
    {
        switch (estadoVista)
        {
            case EstadosVista.ModoCajero:
                this.transform.position = defaultPosition;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case EstadosVista.ModoComputador:
                this.transform.position = new Vector3(2.53299999f, 3.46f, -9.38199997f);
                this.transform.rotation = Quaternion.Euler(0f, 83.672081f, 0f);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;

            case EstadosVista.ModoConstruccion:
                this.transform.position = new Vector3(43.4000015f, 30.2000008f, -8.89999962f);
                this.transform.rotation = Quaternion.Euler(45, 0, 0);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}

