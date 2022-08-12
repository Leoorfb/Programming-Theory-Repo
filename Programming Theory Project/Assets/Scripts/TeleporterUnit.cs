using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterUnit : Unit
{
    protected override IEnumerator MoveTo(Vector3 position)
    {
        position = new Vector3(position.x, transform.position.y, position.z);

        float distancia = Vector3.Distance(transform.position, position);
        float tempo = distancia / speed;

        yield return new WaitForSeconds(tempo);

        transform.position = position;
    }
}
