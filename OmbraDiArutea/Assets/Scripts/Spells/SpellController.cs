using System;
using UnityEngine;

namespace OmbreDiAretua
{

public class SpellController : MonoBehaviour
{
    public Action<SpellController> OnChangeSpellRequest;
    [SerializeField] private bool isInCooldown;
    [SerializeField] private SpellViewer _spellViewer;
    [SerializeField] private KeyCode spellCode;
    private bool isBlocked = true;

    public bool IsInCooldown => isInCooldown;

    public void SetInCooldown(SpellStat spellStat)
    {
        isInCooldown = true;
        _spellViewer.ResetCooldown(spellStat.cooldown, () =>
        {
            isInCooldown = false;
        });
    }

    public void Setup(SpellData spellData)
    {
        isInCooldown = false;
        _spellViewer.Setup(spellData);
        isBlocked = false;
    }

    public void Update()
    {
        if (isBlocked)
        {
            return;
        }

        if (Input.GetKeyDown(spellCode))
        {
            OnChangeSpellRequest?.Invoke(this);
        }
        
    }


    public void Selected()
    {
        _spellViewer.Selected();
    } 
    public void UnSelected()
    {
        _spellViewer.UnSelected();
    }
}


[Serializable]
public class SpellStat
{
    public float cooldown;
    public int damage;
    public float speed;
    public float lifetime;
    public bool hasTrapassing;
    public float timeEffectApplied;
    public int damageEffectApplied;

}

[Serializable]
public class SpellContainerData
{
    public SpellData SpellData;
    public SpellController SpellController;
}
}
