using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IService2
    {
    
    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public bool Correct { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public Exception Ex { get; set; }

        [DataMember]
        public object Object { get; set; }

        [DataMember]
        public List<object> Objects { get; set; }
    }

}
