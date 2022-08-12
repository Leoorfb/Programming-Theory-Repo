using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerUnit : Unit
{
    protected override IEnumerator MoveTo(Vector3 position)
    {
        position = new Vector3(position.x, transform.position.y, position.z);
        Debug.Log("Entrou corrotina");
        float step = 0.1f;

        while (Vector3.Distance(transform.position, position) > step)
        {
            //Debug.Log("corrotina while " + Vector3.Distance(transform.position, position) + " - " + step);
            Vector3 direction = (position - transform.position).normalized;
            //Debug.Log(direction);
            step = speed * Time.deltaTime;
            transform.Translate(direction * step);
            yield return null;
        }
        Debug.Log("Terminou corrotina");
    }
}
