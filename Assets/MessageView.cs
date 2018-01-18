using Assets.Kolejka.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour {

    public Text namet;
    public Text url;
    public Text content;

    public void Initialize(Message msg)
    {
        namet.text = msg.namet;
        url.text = msg.url;
        content.text = msg.content;
    }
}
