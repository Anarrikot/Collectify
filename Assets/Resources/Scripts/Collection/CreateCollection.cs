using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCollection : Page
{
    public TMP_InputField Name;
    //public TextMeshProUGUI Notification;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = true;
        labelActive = true;
        labelText = "��� ���������";
        footerActive=true;
    }
    public void CreateCollectionButton()
    {
        Debug.Log("������� ���������");
        if (Name.text != "")
        {
            Debug.Log("������� ���������");
            //Notification.enabled = false;
            Collection newCollection = new Collection(Name.text);
            Main.main.CollectionsList.Add(newCollection);
            Main.main.Back();
        }
        else
        {
            Debug.Log("������� �����������");
            //Notification.enabled = true;
        }

    }
}
