using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
public interface IConstruible
{
    public void colocar(GridSquare[] cuadrosAOcupar) { }
    public void rotar(int rotado) { }
    public GameObject getGameObject();
    public int getEstadoRotacion();
    public int getCuadrosHorizontal();
    public int getCuadrosVertical();
    public bool puedeColocarse(GridSquare[] cuadrosAOcupar)
    {
        foreach (var cuadro in cuadrosAOcupar)
            if (cuadro == null || cuadro.ocupado)
                return false;
        return true;
    }
    public void setGameObject(GameObject go);
    public void preview();
}

public class Silla : IConstruible
{
    public int estadoRotacion = 0;
    public GameObject objetoConstruible;
    public void preview()
    {
        objetoConstruible.transform.localPosition = Vector3.zero;
    }
    public void rotar(int rotado)
    {
        if (rotado > 3) rotado = 0;
        estadoRotacion = rotado;
        switch (rotado)
        {
            case 0:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 0, 0);
                break;
            case 1:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 90, 0);
                break;
            case 2:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 180, 0);
                break;
            case 3:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 270, 0);
                break;
        }
    }
    public int getEstadoRotacion() { return estadoRotacion; }
    public int getCuadrosHorizontal() { return 1; }
    public int getCuadrosVertical() { return 1; }
    public GameObject getGameObject() { return objetoConstruible; }
    public void setGameObject(GameObject go) { objetoConstruible = go; Debug.Log("Definido objeto como " + go); }
}
abstract class soporte : IConstruible
{
    public int cuadrosHorizontal;
    public int cuadrosVertical;
    public int estadoRotacion = 0;
    public GameObject objetoConstruible;

    public abstract void rotar(int rotado);
    public abstract void preview();
    public int getEstadoRotacion() { return estadoRotacion; }
    public int getCuadrosHorizontal() { return cuadrosHorizontal; }
    public int getCuadrosVertical() { return cuadrosVertical; }
    public GameObject getGameObject() { return objetoConstruible; }
    public void setGameObject(GameObject go) { objetoConstruible = go; Debug.Log("Definido objeto como " + go); }

}

class soporteLargo:soporte {
    public soporteLargo() { cuadrosHorizontal = 1; cuadrosVertical = 3; }
    public override void preview()
    {
        objetoConstruible.transform.localPosition = Vector3.zero;
    }
    public override void rotar(int rotado) {
        if (rotado > 3) rotado = 0;
        estadoRotacion = rotado;
        switch (rotado)
        {
            case 0:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 0, 0);
                break;
            case 1:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 90, 0);
                break;
            case 2:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 180, 0);
                break;
            case 3:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 270, 0);
                break;
        }

    }
}

class soporteSnacks : soporte {
    public soporteSnacks() { cuadrosHorizontal = 1; cuadrosVertical = 1; }
    public override void preview()
    {
        objetoConstruible.transform.localPosition = new Vector3(0,12,0);
    }
    public override void rotar(int rotado)
    {
        if (rotado > 3) rotado = 0;
        estadoRotacion = rotado;
        switch (rotado)
        {
            case 0:
                objetoConstruible.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                objetoConstruible.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                objetoConstruible.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case 3:
                objetoConstruible.transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
        }
    }
}
class soporteBebidas : soporte {
    public soporteBebidas() { cuadrosHorizontal = 1; cuadrosVertical = 1; }
    public override void preview()
    {
        objetoConstruible.transform.localPosition = new Vector3(0,1,0);
        objetoConstruible.transform.localScale = new Vector3(51,51, 1450.151f);
    }
    public override void rotar(int rotado)
    {
        if (rotado > 3) rotado = 0;
        estadoRotacion = rotado;
        switch (rotado)
        {
            case 0:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 0, 180);
                break;
            case 1:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 90, 180);
                break;
            case 2:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 180, 180);
                break;
            case 3:
                objetoConstruible.transform.rotation = Quaternion.Euler(-90, 270, 180);
                break;
        }
    }

}

