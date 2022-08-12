using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIndicator : MonoBehaviour
{
    //private Vector3 startScale_;
    private float reduceScale_ = 4f;
    private float animationDurantion_ = .7f;

    private void Start()
    {
        //startScale_ = transform.localScale;
        StartCoroutine(IndicatePositionAnimation(animationDurantion_));
    }

    private IEnumerator IndicatePositionAnimation(float duration)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(startScale.x / reduceScale_, startScale.y, startScale.z / reduceScale_);

        float elapsedTime = 0;

        //Debug.Log("Começou animação do indicador posição");
        while (transform.localScale != endScale)
        {
            elapsedTime += Time.deltaTime;
            float interpolationRatio = (elapsedTime / (duration/2));

            //Debug.Log("Diminuindo");
            transform.localScale = Vector3.Lerp(startScale, endScale, interpolationRatio);
            yield return null;
        }

        elapsedTime = 0;

        while (transform.localScale != startScale)
        {
            elapsedTime += Time.deltaTime;
            float interpolationRatio = (elapsedTime / (duration / 2));

            //Debug.Log("Aumentando");
            transform.localScale = Vector3.Lerp(endScale, startScale, interpolationRatio);
            yield return null;
        }

        //Debug.Log("Terminou animação do indicador posição");
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
