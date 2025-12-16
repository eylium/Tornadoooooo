using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEnderScript : MonoBehaviour
{
    [SerializeField]
    private Canvas _endCanvas;

    //[SerializeField] private Image _endImage;
    [SerializeField] private TMP_Text _timerText;

    private float _waitTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _endCanvas.gameObject.SetActive(false);
        //_endImage.transform.position = new Vector3(-617f, 466f, 0);
        //_endImage.transform.localScale = new Vector3(0.34f, 0.08f, 0);
        //_endImage.color = new Color(1,1,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ValueManager.GameHasEnded)
        {

            _waitTime += Time.deltaTime;

            if (_waitTime >= 2)
            {
                _endCanvas.gameObject.SetActive(true);
                _timerText.text = $"Completion Time: {ValueManager.Timer}";
                _waitTime = 0;
            }
            //float i = 0;
            //_endImage.color = new Color(1, 1, 1, i+=Time.deltaTime);
        }
    }
}
