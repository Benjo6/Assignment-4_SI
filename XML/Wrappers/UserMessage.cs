using System.Xml.Serialization;
using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.Utility;

namespace Assignment_4_SI.XML.Wrappers;

 [XmlRoot(ElementName = "address", Namespace = "http://s2it.ehb.be/messages")]
    public class Address
    {
        [XmlElement(ElementName = "street", Namespace = "http://s2it.ehb.be/messages")]
        public string Street { get; set; }
        [XmlElement(ElementName = "number", Namespace = "http://s2it.ehb.be/messages")]
        public string Number { get; set; }
        [XmlElement(ElementName = "bus", Namespace = "http://s2it.ehb.be/messages")]
        public string Bus { get; set; }
        [XmlElement(ElementName = "zipCode", Namespace = "http://s2it.ehb.be/messages")]
        public string ZipCode { get; set; }
        [XmlElement(ElementName = "city", Namespace = "http://s2it.ehb.be/messages")]
        public string City { get; set; }
    }

    [XmlRoot(ElementName = "roles", Namespace = "http://s2it.ehb.be/messages")]
    public class Roles
    {
        [XmlElement(ElementName = "role", Namespace = "http://s2it.ehb.be/messages")]
        public List<string> Role { get; set; }
    }

    [XmlRoot(ElementName = "message", Namespace = "http://s2it.ehb.be/messages")]
    public class UserMessage
    {
        private static readonly string XMLNS = "http://s2it.ehb.be/messages";
        private static readonly string SCHEMALOCATION = "http://s2it.ehb.be/messages http://dtsw.ehb.be/~jan.moriaux/xml/UserMessage.xsd";
        private static readonly string TYPEID = "1";
        private static readonly string SENDERUUID = "0a6cca47-af4c-41d8-a3ed-86ff5a00f388";

        [XmlElement(ElementName = "uuid", Namespace = "http://s2it.ehb.be/messages")]
        public string Uuid { get; set; }
        [XmlElement(ElementName = "version", Namespace = "http://s2it.ehb.be/messages")]
        public string Version { get; set; }
        [XmlElement(ElementName = "isActive", Namespace = "http://s2it.ehb.be/messages")]
        public string IsActive { get; set; }
        [XmlElement(ElementName = "firstName", Namespace = "http://s2it.ehb.be/messages")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "lastName", Namespace = "http://s2it.ehb.be/messages")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "email", Namespace = "http://s2it.ehb.be/messages")]
        public string Email { get; set; }
        [XmlElement(ElementName = "address", Namespace = "http://s2it.ehb.be/messages")]
        public Address Address { get; set; }
        [XmlElement(ElementName = "roles", Namespace = "http://s2it.ehb.be/messages")]
        public Roles Roles { get; set; }
        [XmlElement(ElementName = "emailVerified", Namespace = "http://s2it.ehb.be/messages")]
        public string EmailVerified { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
        [XmlAttribute(AttributeName = "typeId")]
        public string TypeId { get; set; }
        [XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }
        [XmlAttribute(AttributeName = "senderUUID")]
        public string SenderUUID { get; set; }
        
        public static UserMessage FromUser(User u)
        {
            var roles = new Roles
            {
                Role = u.Roles.Select(r => r.ToString()).ToList()
            };


            var address = new Address
            {
                Street = u.Street,
                Number = u.Number.ToString(),
                Bus = u.Bus,
                ZipCode = u.ZipCode.ToString(),
                City = u.City,
            };

            return new UserMessage
            {
                Uuid = u.UUID,
                Version = u.Version.ToString().ToLower(),
                IsActive = u.IsActive.ToString().ToLower(),
                FirstName = u.Firstname,
                LastName = u.Lastname,
                Email = u.Email,
                Address = address,
                Roles = roles,
                EmailVerified = u.EmailVerified.ToString().ToLower(),
                Xmlns = XMLNS,
                SchemaLocation = SCHEMALOCATION,
                TypeId = TYPEID,
                Timestamp = DateUtility.ConvertToStringWithZoneOffset(DateTime.Now.ToLocalTime()),
                SenderUUID = SENDERUUID
            };
        }

        public static UserMessage FromMessage(string xml)
        {
            if (xml != null)
            {
                UserMessage umsg = null;
                XmlSerializer serializer = new XmlSerializer(typeof(UserMessage));
                using (StringReader reader = new StringReader(xml))
                {
                    umsg = (UserMessage)serializer.Deserialize(reader);
                    return umsg;
                }
            }
            return null;
        }
        public User ToUser()
        {
            var roles = new HashSet<User.UserRole>();
            foreach (var r in Roles.Role)
            {
                Enum.TryParse(r, out User.UserRole role);
                roles.Add(role);
            }
            try
            {
                return new User
                {
                    UUID = Uuid,
                    Version = long.Parse(Version),
                    IsActive = bool.Parse(IsActive),
                    Firstname = FirstName,
                    Lastname = LastName,
                    Email = Email,
                    Street = Address.Street,
                    Number = int.Parse(Address.Number),
                    Bus = Address.Bus,
                    ZipCode = int.Parse(Address.ZipCode),
                    City = Address.City,
                    Roles = roles,
                    EmailVerified = bool.Parse(EmailVerified)
                };
            }
            catch (FormatException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }
    }