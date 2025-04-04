using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using Google.MiniJSON;
using UnityEngine;

public class FirestoreManager : MonoBehaviour
{

    const string url = "https://collectorsapp-9330a.firestore.app";
    private FirebaseAuth auth;
    public static FirestoreManager Instance;
    private FirebaseFirestore firestore;
    void Awake()
    {
        Instance = this;
        firestore = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    }

    public async Task AddNewCollection(Collection collection)
    {
        try
        {
            // ������ �� ������������ ������������
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName);
            CollectionReference collectionsRef = userRef.Collection("Collections");

            // ������� ����� �������� � ������������� ��������������� ID
            Dictionary<string, object> newCollection = new Dictionary<string, object>
            {
                { "Name", collection.collection_name },
                { "CreatedAt", FieldValue.ServerTimestamp } // ��������� ����� �������
            };

            // ��������� ����� �������� � ���������
            DocumentReference addedDocRef = await collectionsRef.AddAsync(newCollection);

            Debug.Log($"����� ��������� ��������� � ID: {addedDocRef.Id}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������ ��� ���������� ���������: {e.Message}");
        }
    }

    public async Task AddNewItem(Item item, string collectionId)
    {
        try
        {
            // ������ �� ���������� ��������� ������������
            DocumentReference collectionRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(collectionId);

            // ������ �� ������������ Items ������ ���������
            CollectionReference itemsRef = collectionRef.Collection("Items");

            // ������� ����� �������� ��������
            Dictionary<string, object> newItem = new Dictionary<string, object>
        {
            { "Name", item.item_name },
            { "Year", item.item_year },
            { "Production", item.item_production },
            { "Description", item.item_description },
            { "CreatedAt", FieldValue.ServerTimestamp },
            // �������� ������ ���� �������� �� �������������
        };

            // ��������� ����� �������� � ������������ Items
            DocumentReference addedItemRef = await itemsRef.AddAsync(newItem);

            Debug.Log($"����� ������� �������� � ID: {addedItemRef.Id} � ��������� {collectionId}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������ ��� ���������� ��������: {e.Message}");
        }
    }

    public async Task LoadUserCollections()
    {

        try
        {
            Main.main.CollectionsList.Clear();
            // ������ �� ������������ ������������
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName);
            CollectionReference collectionsRef = userRef.Collection("Collections");

            // ��������� ���� ����������
            QuerySnapshot snapshot = await collectionsRef.GetSnapshotAsync();

            // ��������� �����������
            List<Dictionary<string, object>> collections = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Collection newCollection = new Collection(document.GetValue<string>("Name"),document.Id);
                Main.main.CollectionsList.Add(newCollection);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������: {e.Message}");
        }
    }
    public async Task LoadUserItems()
    {

        try
        {
            Main.main.ItemList.Clear();
            // ������ �� ������������ ������������
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName).Collection("Collections").Document(Main.main.collection.id);
            CollectionReference itemsRef = userRef.Collection("Items");

            // ��������� ���� ����������
            QuerySnapshot snapshot = await itemsRef.GetSnapshotAsync();

            // ��������� �����������
            List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Item newItem = new Item(document.GetValue<string>("Name"),document.Id);
                Main.main.ItemList.Add(newItem);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������: {e.Message}");
        }
    }

    public async Task DeleteDocumentAsync(Collection collections)
    {
        try
        {
            // �������� ������ �� ��������
            DocumentReference docRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(collections.id);

            // ��������� ������������� ���������
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Console.WriteLine("�������� �� ������");
                return;
            }

            // ������� ��������
            await docRef.DeleteAsync();
            Console.WriteLine($"�������� {collections.collection_name} ������� ������");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"������ ��������: {ex.Message}");
        }
    }


    public async Task DeleteItemAsync(Item item)
    {
        try
        {
            // �������� ������ �� ��������
            DocumentReference docRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(Main.main.collection.id).Collection("Items").Document(item.id);

            // ��������� ������������� ���������
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Console.WriteLine("�������� �� ������");
                return;
            }

            // ������� ��������
            await docRef.DeleteAsync();
            Console.WriteLine($"�������� {item.item_name} ������� ������");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"������ ��������: {ex.Message}");
        }
    }

}