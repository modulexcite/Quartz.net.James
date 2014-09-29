using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Xml.XPath;
using log4net;

namespace Topshelf.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CoolService" in code, svc and config file together.
    public class CoolService : ICoolService
    {
        private ILog log4netLogger = LogManager.GetLogger(typeof(CoolService));
        private List<Person> persons = new List<Person>();
        public CoolService()
        {
            var jack = new Person()
            {
                FirstName = "Jack",
                LastName = "Nicolson",
                BirthYear = "1976",
                Nickname = "Goose",
                Id = 1
            };
            var jane = new Person()
            {
                FirstName = "Jane",
                LastName = "Corino",
                BirthYear = "1971",
                Nickname = "Mary Poppins",
                Id = 2
            };
            persons = new List<Person>() { jack, jane };

            log4netLogger.Info("Persons count is " + persons.Count);
        }

        public Stream GetPersonInfo(string id)
        {
            int personId = 0;
            int.TryParse(id, out personId);
            Person personFound = persons.FirstOrDefault(p => p.Id == personId);

            if (WebOperationContext.Current != null)
                WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";

            var reader = new StringReader(Resource.Style);
            XmlReader xreader = XmlReader.Create(reader);
            var xslt = new XslCompiledTransform(false);
            xslt.Load(xreader);
            var serializer = new XmlSerializer(typeof(Person));

            var doc = new XDocument();
            using (XmlWriter writer = doc.CreateWriter())
            {
                if (personFound != null) 
                    serializer.Serialize(writer, personFound);
            }

            var outputWriter = new StringWriter();
            xslt.Transform(doc.CreateNavigator(), null, outputWriter);
            string htmlEmailText = outputWriter.ToString();

            var encoding = new System.Text.ASCIIEncoding();
            var stream = new MemoryStream(encoding.GetBytes(htmlEmailText));

            return stream;
        }
    }
}
