using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUp : MonoBehaviour
{
    [SerializeField]
    float time = 10.0f;

    [SerializeField]
    float sizeIncreaseAmount = 1.0f;

    PlayerBumper bumper;
    Transform bumperTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CollidedWithABall(collision))
            return;

        bumper = collision.GetComponent<Ball>().lastCollidedBumper;

        if (bumper == null)
            return;

        PsuedoDestroyGameObject();
        bumperTransform = bumper.transform;

        StartCoroutine(SizeIncreaser());
        
    }

    IEnumerator SizeIncreaser()
    {
        Vector3 oldScale = bumperTransform.localScale;

        if (BumperHasVerticalControls())
        {
            float newSize = sizeIncreaseAmount + bumperTransform.localScale.y;
            bumperTransform.localScale = new Vector3(bumperTransform.localScale.x, newSize, bumperTransform.localScale.z);
        }
        else
        {
            float newSize = sizeIncreaseAmount + bumperTransform.localScale.x;
            bumperTransform.localScale = new Vector3(newSize, bumperTransform.localScale.y, bumperTransform.localScale.z);
        }

        yield return new WaitForSeconds(time);
        bumperTransform.localScale = oldScale;
    }

    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Split Ball");
    }

    private bool BumperHasVerticalControls()
    {
        return bumper.bumperNumber == 1 || bumper.bumperNumber == 2;
    }

    private void PsuedoDestroyGameObject()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
        Destroy(this.gameObject.GetComponent<CircleCollider2D>());
    }
}
