using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _continueButton;
    [SerializeField] private GameObject _panel;

    [SerializeField] private GameObject _imagePanel;
    [SerializeField] private Image _image;
    [SerializeField] private float _speed = 2f;
    void Start()
    {
        instance = this;
    }

    public void StartDialgueOld(Speech speech)
    {
        ShowReplica(speech.StartSpeech());

        _panel.SetActive(true);

        _continueButton.onClick.RemoveAllListeners();
        _continueButton.onClick.AddListener(() =>
        {
            var replica = speech.Continue();

            if (replica == null)
                End();
            else
                ShowReplica(replica);
        });
    }

    public void StartDialgue(Speech speech)
    {
        _panel.SetActive(true);
        StartCoroutine(StartSpeech(speech));
    }

    private void ShowReplica(Replica replica)
    {
        if (replica.character)
        {
            _image.sprite = replica.character.sprite;
            _imagePanel.SetActive(true);
        }
        else _imagePanel.SetActive(false);

        _text.text = replica.text;
    }
    public void End()
    {
        _panel.SetActive(false);
    }

    IEnumerator StartSpeech(Speech speech)
    {
        var replica = speech.StartSpeech();
        while (replica != null)
        {
            ShowReplica(replica);
            yield return new WaitForSeconds(_speed);
            replica = speech.Continue();
        }
        End();
    }
}
