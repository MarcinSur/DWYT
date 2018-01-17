using Assets.Kolejka.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour {

    public Text name;
    public Text url;
    public Text content;

    public void Initialize(Message msg)
    {
        name.text = msg.name;
        url.text = msg.url;
        content.text = msg.content;
    }
}
