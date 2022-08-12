using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterUnit : Unit // INHERITANCE
{
    [SerializeField] protected GameObject teleportIndicator_;

    protected override IEnumerator MoveTo(Vector3 position) // POLYMORPHISM
    {
        position = new Vector3(position.x, transform.position.y, position.z);
        
        float distancia = Vector3.Distance(transform.position, position);
        float tempo = distancia / speed;

        SetTeleporterIndicator();// ABSTRACTION

        yield return StartCoroutine(teleportIndicator_.GetComponent<TeleporterIndicator>().TeleporterIndicatorAnimation(tempo));

        transform.position = position;
    }

    private void SetTeleporterIndicator()
    {
        teleportIndicator_.SetActive(true);
        teleportIndicator_.transform.position = new Vector3(transform.position.x, selectionIndicatorHeight_, transform.position.z);
    }
}
