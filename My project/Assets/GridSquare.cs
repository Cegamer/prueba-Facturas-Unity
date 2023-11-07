using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool colocarObjetoConstruible()
    {
        var a = checkAvailability();
        if (a != null)
        {
            parent.actualizarCuadrosOcupados(a);
            if (objeto.getGameObject().GetComponent<MeshRenderer>() != null)
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