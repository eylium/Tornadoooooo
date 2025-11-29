using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    [SerializeField]
    private Canvas _winCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _winCanvas.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (ValueManager.StarCounter >= 5)
        {
            _winCanvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.R))
            {
                ValueManager.StarCounter =0;
                SceneManager.LoadScene(0);
            }
        }
    }
}
