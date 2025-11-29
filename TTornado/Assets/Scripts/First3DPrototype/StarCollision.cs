using TMPro;
using UnityEngine;

public class StarCollision : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _starText;

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            ValueManager.StarCounter+=1;
            _starText.text = $"Star Count {ValueManager.StarCounter}/5";
            Destroy(this.gameObject);
        }
    }
}
