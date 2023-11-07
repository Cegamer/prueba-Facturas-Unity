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
    Vector3 defaultPosition = new Vector3(3.81f, 3.68f, -1.02f);
    public Grid grid;

    public GameObject[] construiblesGO;
    public GameObject estructuraGO;
    public List<IConstruible> construibles = new List<IConstruible>();
    public IConstruible construibleSeleccionado;

    public EstadosVista estadoVista;
    public GameObject canvasConstruccion;
    ICreadorConstruibles creadorSoportes = new CreadorSoportes();
    ICreadorConstruibles creadorCajeros = new CreadorSillas();

    public enum EstadosVista : int
    {
        ModoCajero = 0,
        ModoComputador = 1,
        ModoConstruccion = 2
    }
    // Start is called before the first frame update
    void Start()

    {
        estadoVista = EstadosVista.ModoCajero;
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
        if(Input.GetKeyDown(KeyCode.C)) cambiarEstadoVista(EstadosVista.ModoConstruccion);

        if (estadoVista == EstadosVista.ModoConstruccion)
        {
            canvasConstruccion.SetActive(true);
            grid.setVisible();
            estructuraGO.GetComponent<MeshRenderer>().enabled = false;
            var a = retornarObjetoRaycast();
            if (a != null)
                selected = a.transform;
            ModoConstruccion();
        }
        else { estructuraGO.GetComponent<MeshRenderer>().enabled = true; canvasConstruccion.SetActive(false); grid.setInvisible();
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
        IConstruible construibleActual = null;
        
        if (objetoId > 2) construibleActual = creadorCajeros.crearConstruible(objetoId);
        else construibleActual = creadorSoportes.crearConstruible(objetoId);

        construibleActual.setGameObject(Instantiate(construiblesGO[objetoId]));
        construibleSeleccionado = construibleActual;

        construibles.Add(construibleActual);
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
            if (selected.GetComponent<GridSquare>().colocarObjetoConstruible())
                construibleSeleccionado = null;
            
        }
        if(Input.GetMouseButtonDown(1)) { Destroy(construibleSeleccionado.getGameObject()); construibleSeleccionado = null; }

        if (Input.GetKeyDown(KeyCode.R))
        {
            construibleSeleccionado.rotar(construibleSeleccionado.getEstadoRotacion() + 1);
            Debug.Log(construibleSeleccionado.getEstadoRotacion());
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
                this.transform.position = new Vector3(7.96f, 3.46f, 1.45f);
                this.transform.rotation = Quaternion.Euler(0f, 83.672081f, 0f);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;

            case EstadosVista.ModoConstruccion:
                this.transform.position = new Vector3(46.8f, 43.3f, -22f);
                this.transform.rotation = Quaternion.Euler(45, 0, 0);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
}

interface ICreadorConstruibles {
    public IConstruible crearConstruible(int objetoId);
}

public class CreadorSoportes : ICreadorConstruibles {
    public IConstruible crearConstruible(int objetoId)
    {
        {
            IConstruible sop = null;
            switch (objetoId)
            {
                case 0:
                    sop = new soporteLargo(); break;
                case 1:
                    sop = new soporteBebidas(); break;
                case 2:
                    sop = new soporteSnacks(); break;
            }

            return sop;
        }
    }
}

class CreadorSillas: ICreadorConstruibles { 
    public IConstruible crearConstruible(int objetoId) { return new Silla(); }

}

