using UnityEngine;
using System.Collections;
public class SpringController : MonoBehaviour
{
    [SerializeField] Sprite springUp, springDown;
    GameObject target;
    bool springActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!springActivated)
        {
            if(collision.CompareTag("Wheel"))
            {
                target = collision.transform.parent.gameObject;
                StartCoroutine(nameof(SpringForce));
            }
            else if (collision.CompareTag("Car"))
            {
                target = collision.gameObject;
                StartCoroutine(nameof(SpringForce));
            }
        }        
    }
   IEnumerator SpringForce()
   {
        springActivated = true;
        yield return new WaitForSeconds(0.1f);

        target.GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(0, 100f), ForceMode2D.Impulse);

        GetComponent<SpriteRenderer>().sprite = springUp;

        yield return new WaitForSeconds(1.5f);

        GetComponent<SpriteRenderer>().sprite = springDown;
        springActivated = false;
        target = null;
   }
}
