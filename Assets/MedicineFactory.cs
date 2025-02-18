using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class MedicineFactory : MonoBehaviour
{
    [Header("Medicine Materials")]
    public List<Object> medicineMaterialList = new List<Object>();
    
    [Header("Medicines")]
    public List<Object> medicineList = new List<Object>();

    public static MedicineFactory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        Debug.Log("----Initialization started----");
        
        Object[] medicineMaterials = Resources.LoadAll("MedicineMaterials", typeof(GameObject));
        Object[] medicines = Resources.LoadAll("Medicines", typeof(GameObject));

        foreach (var item in medicineMaterials)
        {
            medicineMaterialList.Add(item);
            Debug.Log("---Loaded Medicine Material: " + item.name + " was added to the list---");
        }

        foreach (var item in medicines)
        {
            medicineList.Add(item);
            Debug.Log("---Loaded Medicine: " + item.name + " was added to the list---");
        }
        
        Debug.Log("----Initialization completed----");
    }
    
    public GameObject GetMedicine(string _medicineName)
    {
        Debug.Log("----GetMedicine----");
        
        GameObject medicine = null;
        foreach (var item in medicineList)
        {
            if (item.name == _medicineName)
            {
                medicine = item as GameObject;
                break;
            }
        }

        if (medicine == null)
        {
            Debug.LogError("Medicine not found");
            return null;
        }

        Debug.Log("----GetMedicine completed----");

        return medicine;
    }
    
    public GameObject GetMedicineMaterial(string _medicineMaterialName)
    {
        Debug.Log("----GetMedicineMaterial----");
        
        GameObject medicineMaterial = null;
        foreach (var item in medicineMaterialList)
        {
            if (item.name == _medicineMaterialName)
            {
                medicineMaterial = item as GameObject;
                break;
            }
        }

        if (medicineMaterial == null)
        {
            Debug.LogError("Medicine Material not found");
            return null;
        }
        
        Debug.Log("----GetMedicineMaterial completed----");

        return medicineMaterial;
    }

    public void CreateMedicine(string _medicineName, Vector3 _position)
    {
        Debug.Log("----CreateMedicine----");
        
        Object medicine = null;
        foreach (var item in medicineList)
        {
            if (item.name == _medicineName)
            {
                medicine = item;
                break;
            }
        }

        if (medicine == null)
        {
            Debug.LogError("Medicine not found");
            return;
        }

        Instantiate(medicine, _position, Quaternion.identity);

        Debug.Log("----CreateMedicine completed----");
    }
    
    public void CreateMedicineMaterial(string _medicineMaterialName, Vector3 _position)
    {
        Debug.Log("----CreateMedicineMaterial----");
        
        Object medicineMaterial = null;
        foreach (var item in medicineMaterialList)
        {
            if (item.name == _medicineMaterialName)
            {
                medicineMaterial = item;
                break;
            }
        }

        if (medicineMaterial == null)
        {
            Debug.LogError("Medicine Material not found");
            return;
        }

        Debug.Log("----CreateMedicineMaterial completed----");

        Instantiate(medicineMaterial, _position, Quaternion.identity);
    }
}
