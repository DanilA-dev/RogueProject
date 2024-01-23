using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;

[System.Serializable]
public class EquipableArmorType : IArmorType
{
    [SerializeField, OnValueChanged("UpdateArmor")] private ArmorPiece _head;
    [SerializeField, OnValueChanged("UpdateArmor")] private ArmorPiece _torso;
    [SerializeField, OnValueChanged("UpdateArmor")] private ArmorPiece _gloves;
    [SerializeField, OnValueChanged("UpdateArmor")] private ArmorPiece _pants;
    [SerializeField, OnValueChanged("UpdateArmor")] private ArmorPiece _boots;
    [SerializeField, OnValueChanged("UpdateArmor")] private ArmorPiece _cloak;
    [SerializeField, OnValueChanged("UpdateArmor")] private List<ArmorBodyItem> _armorBodyItems;

    public float? Defence => _head?.ArmorValue + _torso?.ArmorValue + _gloves?.ArmorValue
     + _pants.ArmorValue + _boots.ArmorValue + _cloak.ArmorValue;

    private void UpdateArmor()
    {
        EquipArmor(_head, _head);
        EquipArmor(_torso, _torso);
        EquipArmor(_gloves, _gloves);
        EquipArmor(_pants, _pants);
        EquipArmor(_boots, _boots);
        EquipArmor(_cloak, _cloak);
    }

    public void EquipArmor(ArmorPiece curr, ArmorPiece newArmor)
    {
        if(newArmor == null)
            return;

        if(curr.Type != newArmor.Type)
            return;

        curr = newArmor;
        FindArmorBodyIem(curr);
    }

    private void FindArmorBodyIem(ArmorPiece armor)
    {
        var armorType = _armorBodyItems.Where(t => t.Type == armor.Type).ToList();
        if(armorType != null)
            armorType.ForEach(a => a.gameObject.SetActive(false));
        

        var neededArmor = _armorBodyItems.Where(t => t.Type == armor.Type && t.BodyId == armor.BodyId).FirstOrDefault();
        if(neededArmor != null)
             neededArmor.gameObject.SetActive(true);
        else
            Debug.Log($"Can't find armor with id{armor.BodyId}");
    }

}
