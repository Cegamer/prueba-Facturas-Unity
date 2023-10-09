using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridSquare[,] grid = new GridSquare[20, 20];
    public GameObject gridObject;
    public cameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        int x = 0, y = 0;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                var obj = Instantiate(gridObject, new Vector3(x, 0, y), Quaternion.identity);
                obj.AddComponent<GridSquare>();
                obj.GetComponent<GridSquare>().build(obj, i, j, this);
                grid[i, j] = obj.GetComponent<GridSquare>();
                x += 5;
            }
            x = 0;
            y += 5;
        }

        GridSquare[] inicial = new GridSquare[] { grid[0, 0],grid[0, 1],grid[0, 2],grid[1, 0],grid[1, 1],grid[1, 2]};
        actualizarOcupado(inicial);
        IConstruible soporteDefault1 = new soporteLargo();
        soporteDefault1.setGameObject(Instantiate(cameraController.construiblesGO[0]));
        soporteDefault1.rotar(1);
        grid[3, 1].preview(soporteDefault1);
        grid[3, 1].colocar();
        cameraController.construibles.Add(soporteDefault1);

        IConstruible soporteDefault2 = new soporteLargo();
        soporteDefault2.setGameObject(Instantiate(cameraController.construiblesGO[0]));
        soporteDefault2.rotar(1);

        grid[3, 3].preview(soporteDefault2);
        grid[3, 3].colocar();
        cameraController.construibles.Add(soporteDefault2);

        IConstruible soporteDefault3 = new soporteLargo();
        soporteDefault3.setGameObject(Instantiate(cameraController.construiblesGO[0]));
        soporteDefault3.rotar(1);
        grid[3, 5].preview(soporteDefault3);
        grid[3, 5].colocar();
        cameraController.construibles.Add(soporteDefault3);

    }

    // Update is called once per frame
    void Update()
    {



        
    }
    public void actualizarOcupado(GridSquare[] cuadrosAOcupar)
    {
        foreach (var cuadro in cuadrosAOcupar)
        {
            cuadro.gridObject.GetComponent<MeshRenderer>().material.color = Color.red;
            cuadro.ocupado = true;
        }
    }
    public void setVisible() { foreach (var a in grid) { a.gridObject.GetComponent<MeshRenderer>().enabled = true; } }
    public void setInvisible() { foreach (var a in grid) { a.gridObject.GetComponent<MeshRenderer>().enabled = false; } }
}

public class GridSquare : MonoBehaviour
{
    public GameObject gridObject = null;
    public int gridIndexX, gridIndexY;
    public bool ocupado = false;
    IConstruible objeto = null;
    public Grid parent;
    public void build(GameObject gridObject, int gridIndexX, int gridIndexY, Grid parent)
    {
        this.gridObject = gridObject;
        this.gridIndexX = gridIndexX;
        this.gridIndexY = gridIndexY;
        this.parent = parent;
    }

    public int[] getGridPos()
    {
        return new int[] { gridIndexX, gridIndexY };
    }
    public bool colocar()
    {
        var a = checkAvailability();
        if (a != null)
        {
            parent.actualizarOcupado(a);
            if(objeto.getGameObject().GetComponent<MeshRenderer>() != null)
                objeto.getGameObject().GetComponent<MeshRenderer>().material.color = Color.white;
            return true;
        }
        return false;
    }
    public void preview(IConstruible objetoe)
    {

        objetoe.getGameObject().transform.SetParent(gridObject.transform);
        objetoe.preview();
        objetoe.rotar(objetoe.getEstadoRotacion());
        objeto = objetoe;
        checkAvailability();
    }


    public GridSquare[] checkAvailability()
    {
        if (objeto != null)
        {
            int width = objeto.getCuadrosHorizontal();
            int height = objeto.getCuadrosVertical();
            int rotation = objeto.getEstadoRotacion();

            // Coordenadas del cuadro principal
            int mainX = gridIndexX;
            int mainY = gridIndexY;

            // Ajustar la posición del cuadro principal según la rotación
            GridSquare[] cuadrosAOcupar = new GridSquare[width * height];
            int index = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int xPos, yPos;

                    if (rotation == 0)
                    {
                        xPos = mainX + x;
                        yPos = mainY - y;
                    }
                    else if (rotation == 1)
                    {
                        xPos = mainX + y;
                        yPos = mainY + x;
                    }
                    else if (rotation == 2)
                    {
                        xPos = mainX - x;
                        yPos = mainY + y;
                    }
                    else // rotation == 3
                    {
                        xPos = mainX - y;
                        yPos = mainY - x;
                    }

                    // Verificar los límites de la cuadrícula antes de asignar a cuadrosAOcupar
                    if (xPos >= 0 && xPos < parent.grid.GetLength(0) && yPos >= 0 && yPos < parent.grid.GetLength(1))
                    {
                        cuadrosAOcupar[index] = parent.grid[xPos, yPos];
                        index++;
                    }
                    else
                    {
                        // Si el objeto se encuentra fuera de los límites de la cuadrícula, no puede colocarse.
                        objeto.getGameObject().GetComponent<MeshRenderer>().material.color = Color.red;
                        return null;
                    }
                }
            }

            // Verificar si el objeto puede colocarse en los cuadros especificados
            bool puedeColocarse = objeto.puedeColocarse(cuadrosAOcupar);

            // Cambiar el color del objeto en consecuencia
            if (puedeColocarse)
            {
                try
                {
                    objeto.getGameObject().GetComponent<MeshRenderer>().material.color = Color.green;
                }
                catch { }
                return cuadrosAOcupar;
            }
            else
            {
                if (objeto.getGameObject().GetComponent<MeshRenderer>() != null)
                    objeto.getGameObject().GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }

        return null;
    }


}


