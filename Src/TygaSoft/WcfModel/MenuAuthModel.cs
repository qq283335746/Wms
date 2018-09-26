using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "MenuAuthModel")]
    public class MenuAuthModel
    {
        [DataMember]
        public string AllowRole { get; set; }

        [DataMember]
        public string DenyUser { get; set; }

        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public int IsView { get; set; }

        [DataMember]
        public int IsAdd { get; set; }

        [DataMember]
        public int IsEdit { get; set; }

        [DataMember]
        public int IsDel { get; set; }
    }
}
