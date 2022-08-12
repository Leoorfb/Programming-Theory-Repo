using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterIndicator : MonoBehaviour
{
    public IEnumerator TeleporterIndicatorAnimation(float duration)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y - 2, startPos.z);

        Vector3 startRotation = transform.eulerAngles;
        Vector3 endRotation = new Vector3(startRotation.x, startRotation.y + 360f, startRotation.z);

        float elapsedTime = 0;

        Debug.Log("Começou animação do indicador de teleporte");
        while (transform.position != endPos)
        {
            elapsedTime += Time.deltaTime;
            float interpolationRatio = (elapsedTime / duration);

            Debug.Log("Entrou no While");
            transform.position = Vector3.Lerp(startPos, endPos, interpolationRatio);
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, interpolationRatio);
            yield return null;
        }
        Debug.Log("Terminou animação do indicador de teleporte");
        gameObject.SetActive(false);
    }
}
