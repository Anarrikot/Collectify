using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CreateItem : Page
{
    public TMP_InputField Name;
    //public TextMeshProUGUI Notification;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = true;
        labelActive = true;
        labelText = "��� ��������";
        footerActive=true;
    }
    public void CreateCollectionButton()
    {
        AsyncCreateCollectionButton();
    }
    public async Task AsyncCreateCollectionButton()
    {
        Debug.Log("������� ���������");
        if (Name.text != "")
        {
            Debug.Log("������� ���������");
            //Notification.enabled = false;
            Item newItem = new Item(Name.text);
            await FirestoreManager.Instance.AddNewItem(newItem, Main.main.collection.id);
            //Main.main.CollectionsList.Add(newCollection);
            Main.main.Back();
        }
        else
        {
            Debug.Log("������� �����������");
            //Notification.enabled = true;
        }

    }
}
