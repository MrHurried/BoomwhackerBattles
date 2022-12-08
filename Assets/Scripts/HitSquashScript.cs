using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSquashScript : MonoBehaviour
{

    [SerializeField] float squashMagnitude;

    [SerializeField] float squashDuration;
    // Start is called before the first frame update

    private bool squashing;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Squash()
    {
        if (squashing == false)
        {
            StartCoroutine(SquashRoutine());
        }
    }

    IEnumerator SquashRoutine()
    {
        animator.enabled = false;
        Debug.Log("starting squashing");

        squashing = true;

        Vector3 originalScale = transform.localScale;

        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(squashMagnitude, squashMagnitude, 1f));//Vector3.Scale(transform.localScale, new Vector3(squashMagnitude, squashMagnitude, 1));

        yield return new WaitForSeconds(squashDuration);

        transform.localScale = originalScale;

        animator.enabled = true;

        squashing = false;
    }
}

