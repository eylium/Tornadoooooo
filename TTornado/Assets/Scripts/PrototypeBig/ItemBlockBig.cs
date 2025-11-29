using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemBlockkBig : MonoBehaviour
{

    public bool IsThrown;

    private bool isScaling;
    private bool hasScaled;

    private bool _isCounted;
    private Vector3 originalScale;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
 

    }
    public int AddCounter(int counter)
    {
        if (!_isCounted)
        {
            counter += 1;
            _isCounted = true;
            
            return counter;

        }
        return counter;
    }

    public void ThrowObject()
    {
        ValueManager.IsThrown = true;
        transform.SetParent(null, true);
        Vector3 difference = transform.forward;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward, ForceMode.Impulse);
        ValueManager.IsThrown = false;

        StopAllCoroutines();
    }

    public void Move(GameObject go, GameObject target, float maxSpeed, float vibrateTimer)
    {

        //_elapsed += Time.fixedDeltaTime;
        //float duration = 10f;
        //float t = _elapsed / duration;

        go.transform.SetParent(target.transform,true);
        float range = 1.87f;
        range = Random.Range(0, range);
        Vector3 targetPosition = new Vector3(target.transform.position.x, range, target.transform.position.z);

        go.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, maxSpeed * Time.fixedDeltaTime);


    }

    public void StartScale(GameObject go)
    {
        // Only start the coroutine if we haven’t scaled yet
        if (!hasScaled && !isScaling)
        {
            StartCoroutine(ScaleOnce(go));
        }
    }

    public void ReverseScale(GameObject go)
    {

        transform.localScale = originalScale;


    }
    private IEnumerator ScaleOnce(GameObject go)
    {
        
        isScaling = true;
        hasScaled = true;



        Vector3 startScale = go.transform.localScale;
        Vector3 newScale = new Vector3(0.1f, 0.1f, 0.1f);

        float duration = 1.5f; // how long the scaling should take
        float elapsed = 0f;



        while (elapsed < duration)
        {
            //float t = elapsed / duration; // normalized time [0, 1]

            float t = elapsed / duration;
            t = Mathf.Pow(t, 4f); // exponential ease-in
            go.transform.localScale = Vector3.Lerp(startScale, newScale, t);


            //go.transform.localScale = Vector3.Lerp(startScale, newScale, t);
            elapsed += Time.deltaTime;
            yield return null; // wait for next frame
        }

        // Ensure it ends exactly on the target scale
        go.transform.localScale = newScale;

        isScaling = false;

    }

    //private IEnumerator ScaleOnceBack(GameObject go)
    //{

    //    isScaling = true;
    //    hasScaled = true;

    //    Vector3 startScale = new Vector3(0, 0, 0);
    //    Vector3 newScale = originalScale;

    //    float duration = 1.5f; // how long the scaling should take
    //    float elapsed = 0f;



    //    while (elapsed < duration)
    //    {
    //        //float t = elapsed / duration; // normalized time [0, 1]

    //        float t = elapsed / duration;
    //        t = Mathf.Pow(t, 4f); // exponential ease-in
    //        go.transform.localScale = Vector3.Lerp(startScale, newScale, t);


    //        //go.transform.localScale = Vector3.Lerp(startScale, newScale, t);
    //        elapsed += Time.deltaTime;
    //        yield return null; // wait for next frame
    //    }

    //    // Ensure it ends exactly on the target scale
    //    go.transform.localScale = newScale;

    //    isScaling = false;

    //}
    public void Jitter(GameObject go, GameObject target, float maxSpeed, float vibrateTimer)
    {

        float vibrateRange = Random.Range(-5 * Time.fixedDeltaTime, 5 * Time.fixedDeltaTime);
        Vector3 vibratePosition = gameObject.transform.position;
        go.transform.position = new Vector3(vibratePosition.x += vibrateRange, vibratePosition.y, vibratePosition.z += vibrateRange);


    }
}
