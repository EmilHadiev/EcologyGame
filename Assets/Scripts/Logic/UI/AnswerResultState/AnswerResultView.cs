using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Image _resultImage;
    [SerializeField] private Sprite _dontKnowImage;

    public void SetData(Question question)
    {
        foreach (var q in question.QuestVersions)
        {
            if (q.IsCorrect)
            {
                _resultText.text = q.Description;
                _resultImage.sprite = q.Sprite;
                break;
            }
        }
    }

    public void SetData(QuestVersion version)
    {
        if (version.Sprite == null)
        {
            _resultText.text = "Не знаю";
            _resultImage.sprite = _dontKnowImage;
        }
        else
        {
            _resultText.text = version.Description;
            _resultImage.sprite = version.Sprite;
        }
    }
}