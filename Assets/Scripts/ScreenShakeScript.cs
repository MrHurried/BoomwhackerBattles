using System.Collections;
using UnityEngine;

public class ScreenShakeScript : MonoBehaviour
{
    public float duration = 1f;
    private float cameraZ = -10f;

    public AnimationCurve curve;

    public bool start = false;

    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            //used for checking time
            elapsedTime += Time.deltaTime;

            //we do this to make the shake not too heavy
            float strength = curve.Evaluate(elapsedTime/ duration);

            //this sets the offset
            Vector3 randOffset = Random.insideUnitSphere * strength;

            //changing the position
            transform.position = startPosition + new Vector3(randOffset.x, randOffset.y, cameraZ);

            //waiting till next frame (null)
            yield return null;
        }

        //returning to start pos
        transform.position = startPosition;
    }
}
