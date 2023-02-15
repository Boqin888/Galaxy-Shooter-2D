using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private float _shakeTime = 0.4f;
    public IEnumerator CameraShake()
    {
        Vector3 originalPosition = transform.localPosition;
        while (_shakeTime > 0)
        {
            float randX = Random.Range(-.3f, .3f);
            float randY = Random.Range(-.3f, .3f);
            transform.localPosition = new Vector3(randX, randY, transform.position.z);
            _shakeTime -= Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
        _shakeTime = 0.4f;
    }
}

