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
    public GameObject barca1_1;
    public GameObject barca1_2;
    public GameObject barca2_1;
    public GameObject barca2_2;
    public GameObject barca3_1;
    public GameObject barca3_2;

    public Vector3 UsarBarca1()
    {
        if (celda.activated)
        {
            celda.activated = false;
            sotanoOeste.activated = true;
            barca1_1.SetActive(false);
            barca1_2.SetActive(true);

            return sotanoOeste.transform.position;
        }
        else
        {
            celda.activated = true;
            sotanoOeste.activated = false;
            barca1_1.SetActive(true);
            barca1_2.SetActive(false);

            return celda.transform.position;
        }
    }

    public Vector3 UsarBarca2()
    {
        if (sotanoNorte.activated)
        {
            sotanoNorte.activated = false;
            sotanoEste_Norte.activated = true;
            barca2_1.SetActive(false);
            barca2_2.SetActive(true);

            return sotanoEste_Norte.transform.position;
        }
        else
        {
            sotanoNorte.activated = true;
            sotanoEste_Norte.activated = false;
            barca2_1.SetActive(true);
            barca2_2.SetActive(false);

            return sotanoNorte.transform.position;
        }
    }

    public Vector3 UsarBarca3()
    {
        if (patioMusica.activated)
        {
            patioMusica.activated = false;
            sotanoEste__Musica.activated = true;
            barca3_1.SetActive(false);
            barca3_2.SetActive(true);

            return sotanoEste__Musica.transform.position;
        }
        else
        {
            patioMusica.activated = true;
            sotanoEste__Musica.activated = false;
            barca3_1.SetActive(true);
            barca3_2.SetActive(false);

            return patioMusica.transform.position;
        }
    }
}
