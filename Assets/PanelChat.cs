using Assets.Kolejka.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChat : MonoBehaviour {

    public GameObject rowPrefab;
    public Transform container;

    public void CreateRow(Message msg)
    {
        GameObject newRow = Instantiate(rowPrefab) as GameObject;
        newRow.GetComponent<MessageView>().Initialize(msg);
        newRow.transform.SetParent(container, false);
    }
}
