using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace Topshelf.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICoolService" in both code and config file together.
    [ServiceContract]
    public interface ICoolService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/id/{id}", ResponseFormat = WebMessageFormat.Xml)]
        Stream GetPersonInfo(string id);
    }
}
