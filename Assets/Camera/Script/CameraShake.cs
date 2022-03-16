using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Méthode utilisé par DropperTrigger pour faire trembler la caméra
    public IEnumerator Shake(float magnitude, float duration)
    {
        Vector3 originalPosition = transform.localPosition;
        float timeElapsed = 0f;

        while(timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
