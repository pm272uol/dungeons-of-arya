using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPosition = transform.position;

        var percentComplete = 0.0f;

        while (percentComplete < 1.0f)
        {
            //percentComplete += Time.deltaTime / duration; // Move in straight line

            //transform.position = Vector3.Lerp(startPosition, destination, percentComplete);

            //yield return null;

            var currentHeight = Mathf.Sin(Mathf.PI * percentComplete);

            transform.position = Vector3.Lerp(startPosition, destination, percentComplete) + Vector3.up * currentHeight;

            percentComplete += Time.deltaTime / duration;

            yield return null;

        }

        gameObject.SetActive(false);
    }
}
