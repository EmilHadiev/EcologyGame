using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class MusicToggler : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioSource _backgroundSource;
    [SerializeField] private Image _image;
    [SerializeField] private Color _workColor;
    [SerializeField] private Color _muteColor;

    [Inject] private readonly ISoundContainer _sound;

    bool _isOn;

    private void OnValidate() => _button ??= GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(Toggle);

    private void OnDisable() => _button.onClick.RemoveListener(Toggle);

    private void Start()
    {
        Mute();
    }

    private void Toggle()
    {
        if (_isOn)
        {
            Mute();
        }
        else
        {
            UnMute();
        }

        _sound.MuteToggle(_isOn);
        _backgroundSource.mute = _isOn;
    }

    private void UnMute()
    {
        _isOn = true;
        _image.color = _workColor;
    }

    private void Mute()
    {
        _isOn = false;
        _image.color = _muteColor;
    }
}
