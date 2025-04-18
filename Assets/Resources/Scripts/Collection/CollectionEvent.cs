using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionEvent : MonoBehaviour
{
    Collection Collection;
    public TextMeshProUGUI Name;
    public Button Button;
    public Button Button_edit;  // Добавляем поле для кнопки редактирования

    public void Init(Collection newCollection)
    {
        this.Name.text = newCollection.collection_name;
        Collection = newCollection;
    }

    public void OnButtonClicked()
    {
        Main.main.OpenAllItems(Collection);
    }
    public void OnButton_editClicked()
    {
        Main.main.OpenEditCollection(Collection);
    }
}
