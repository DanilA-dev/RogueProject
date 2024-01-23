using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInterface : MonoBehaviour
{
    [SerializeField] private RectTransform _selectImg;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Image _healthFill;

    private Camera _cam;

    private HealthSystem _healthSystem;
    private void Start()
    {
        _selectImg?.gameObject.SetActive(false);
        _cam = Camera.main;
        SubscribeToUnitHealthSystem();
        ChangeHealthBar(_healthSystem.CurrentHealth, _healthSystem.MaxHealth);
    }

    private void OnDisable()
    {
        if(_healthSystem)
            _healthSystem.OnHealthChange -= ChangeHealthBar;
    }

    private void LateUpdate()
    {
        _healthFill.transform.parent.LookAt(_cam.transform);
        _healthFill.transform.parent.Rotate(0, 180, 0);
    }

    private void SubscribeToUnitHealthSystem()
    {
        if(transform.parent.TryGetComponent(out _healthSystem))
            _healthSystem.OnHealthChange += ChangeHealthBar;
    }

    private void ChangeHealthBar(float curr, float max)
    {
        _healthFill.fillAmount = curr / max;
        _healthText.SetText($"{curr}/{max}");
    }

    public void ToggleSelectImg(bool value) => _selectImg?.gameObject.SetActive(value);
}
