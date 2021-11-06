using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _continueButton;
    [SerializeField] private GameObject _panel;
    void Start()
    {
        instance = this;
    }

    public void StartDialgue(Speech speech)
    {
        _text.text = speech.StartSpeech();

        _panel.SetActive(true);

        _continueButton.onClick.RemoveAllListeners();
        _continueButton.onClick.AddListener(() =>
        {
            _text.text = speech.Continue();
            if (_text.text == "")
                End();
        });
    }

    public void End()
    {
        _panel.SetActive(false);
    }
}
