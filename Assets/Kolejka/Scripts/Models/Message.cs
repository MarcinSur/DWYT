using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Kolejka.Scripts
{
    public class Message
    {
        public string namet;
        public string url;
        public string content;

        public Message()
        {

        }

        public Message(string namet, string url, string content)
        {
            this.namet = namet;
            this.url = url;
            this.content = content;
        }

        public Message(IDictionary<string, object> dict)
        {
            this.namet = dict["namet"].ToString();
            this.url = dict["url"].ToString();
            this.content = dict["content"].ToString();
        }

    }
}
