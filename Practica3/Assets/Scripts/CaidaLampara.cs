using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaLampara : MonoBehaviour
{
    bool lamparaCaida;
    private Vector3 posIni;
    private Rigidbody _rigidbody;

    private void Start()
    {
        lamparaCaida = false;
        _rigidbody = GetComponent<Rigidbody>();
        posIni = transform.position;
    }

    public void Caer()
    {
        lamparaCaida = true;
        _rigidbody.useGravity = true;
    }

    public void Restaurar()
    {
        lamparaCaida = false;
        _rigidbody.useGravity = false;
        transform.position = posIni;
    }

    private void Update()
    {
        if (transform.position.y < 0.5f && !_rigidbody.isKinematic)
        {
            _rigidbody.isKinematic = true;
        }
        else if (transform.position.y > 0.5f && _rigidbody.isKinematic) {
            _rigidbody.isKinematic = false;
        }
    }

    public bool LamparaHaCaido()
    {
        return lamparaCaida;
    }
}
