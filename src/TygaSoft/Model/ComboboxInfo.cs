using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class ComboboxInfo
    {
        public ComboboxInfo() { }
        public ComboboxInfo(string id,string text)
        {
            this.Id = id;
            this.Text = text;
        }

        public string Id { get; set; }

        public string Text { get; set; }
    }
}
