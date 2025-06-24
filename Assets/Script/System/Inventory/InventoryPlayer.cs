using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _inventaryPool;
    private ICharacterData _characterData;

    public void AddItem(GameObject item)
    {
        if (item == null) return;
        IUiItem uiItem = item.GetComponent<IUiItem>();
        if (uiItem == null) return;
        GameObject iconItem = Instantiate(item, _inventaryPool.transform, false);
        iconItem.GetComponent<IUiItem>().Initialization(_characterData);
    }

    public void Initialization(ICharacterData characterData)
    {
        // ����� ����� ����� ��� �� ��������� �� �������� 
        _characterData = characterData;
    }
}

public interface IUiItem
{
    public void Initialization(ICharacterData characterData);
    public abstract void UseItem();
}
