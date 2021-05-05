using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicaBarca : MonoBehaviour
{
    // Salas que conecta la barca 1
    public OffMeshLink celda;
    public OffMeshLink sotanoOeste;

    // Salas que conecta la barca 2
    public OffMeshLink sotanoNorte;
    public OffMeshLink sotanoEste_Norte;

    // Salas que conecta la barca 3
    public OffMeshLink patioMusica;
    public OffMeshLink sotanoEste__Musica;

    // representaciones graficas de las barcas
    public MeshRenderer barca1_1;
    public MeshRenderer barca1_2;
    public MeshRenderer barca2_1;
    public MeshRenderer barca2_2;
    public MeshRenderer barca3_1;
    public MeshRenderer barca3_2;

    // Estas están a true si están en la posición en la que
    // empieza la simulación
    private bool barca1 = true;
    private bool barca2 = true;
    private bool barca3 = true;

    public Vector3 UsarBarca1()
    {
        if (celda.activated)
        {
            celda.activated = false;
            sotanoOeste.activated = true;
            barca1_1.enabled = false;
            barca1_2.enabled = true;

            barca1 = false;

            return sotanoOeste.transform.position;
        }
        else
        {
            celda.activated = true;
            sotanoOeste.activated = false;
            barca1_1.enabled = true;
            barca1_2.enabled = false;

            barca1 = true;

            return celda.transform.position;
        }
    }

    public Vector3 UsarBarca2()
    {
        if (sotanoNorte.activated)
        {
            sotanoNorte.activated = false;
            sotanoEste_Norte.activated = true;
            barca2_1.enabled = false;
            barca2_2.enabled = true;
            
            barca2 = false;

            return sotanoEste_Norte.transform.position;
        }
        else
        {
            sotanoNorte.activated = true;
            sotanoEste_Norte.activated = false;
            barca2_1.enabled = true;
            barca2_2.enabled = false;

            barca2 = true;

            return sotanoNorte.transform.position;
        }
    }

    public Vector3 UsarBarca3()
    {
        if (patioMusica.activated)
        {
            patioMusica.activated = false;
            sotanoEste__Musica.activated = true;
            barca3_1.enabled = false;
            barca3_2.enabled = true;

            barca3 = false;

            return sotanoEste__Musica.transform.position;
        }
        else
        {
            patioMusica.activated = true;
            sotanoEste__Musica.activated = false;
            barca3_1.enabled = true;
            barca3_2.enabled = false;

            barca3 = true;

            return patioMusica.transform.position;
        }
    }

    public bool GetBarca1() { return barca1; }
    public bool GetBarca2() { return barca2; }
    public bool GetBarca3() { return barca3; }
}
