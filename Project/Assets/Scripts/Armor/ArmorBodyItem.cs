using UnityEngine;

public class ArmorBodyItem : MonoBehaviour
{   
    [SerializeField] private ArmorType _type;
    [SerializeField] private int _bodyId;

    public int BodyId => _bodyId;
    public ArmorType Type => _type;
}
