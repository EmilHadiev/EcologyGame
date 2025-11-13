using TMPro;
using UnityEngine;
using Zenject;

public class PointsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsText;

    [Inject] private IPointsContainer _pointsContainer;

    private void OnValidate() => _pointsText ??= GetComponent<TMP_Text>();

    private void OnEnable() => _pointsContainer.PointsChanged += OnPointsChanged;
    private void OnDisable() => _pointsContainer.PointsChanged -= OnPointsChanged;

    private void OnPointsChanged(int points) => _pointsText.text = $"+ {points}";
}