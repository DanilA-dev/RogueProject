using UnityEngine;
using Sirenix.OdinInspector;

public enum ArmorType
{
    None = 0,
    Head,
    Torso,
    Gloves,
    Pants,
    Boots,
    Cloak

}

[CreateAssetMenu(menuName ="Data/ArmorPiece")]
public class ArmorPiece : ScriptableObject
{
    [SerializeField] private ArmorType _type;
    [SerializeField,Min(0)] private float _armorValue;
    [SerializeField] private int _bodyId;

    [PreviewField(75, ObjectFieldAlignment.Right)]
    [SerializeField] private Sprite _icon;

    public ArmorType Type => _type;
    public float ArmorValue => _armorValue;
    public int BodyId => _bodyId;
    public Sprite Icon => _icon;
}
