using UnityEngine;

[System.Serializable]
public class SimplifiedArmorType : IArmorType
{
    [SerializeField] private float _armor;
    public float? Defence => _armor;
}
