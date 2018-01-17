using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Kolejka.Scripts
{
    public class Message
    {
        public string name;
        public string url;
        public string content;

        public Message()
        {

        }

        public Message(string name, string url, string content)
        {
            this.name = name;
            this.url = url;
            this.content = content;
        }

        public Message(IDictionary<string, object> dict)
        {
            this.name = dict["name"].ToString();
            this.url = dict["url"].ToString();
            this.content = dict["content"].ToString();
        }

    }
}
