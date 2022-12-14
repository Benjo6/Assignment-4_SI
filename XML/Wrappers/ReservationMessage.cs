using System.Xml.Serialization;
using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.Utility;

namespace Assignment_4_SI.XML.Wrappers;

[XmlRoot(ElementName = "message", Namespace = "http://s2it.ehb.be/messages")]
    public class ReservationMessage
    {
        private static readonly string XMLNS = "http://s2it.ehb.be/messages";
        private static readonly string SCHEMALOCATION = "http://s2it.ehb.be/messages http://dtsw.ehb.be/~jan.moriaux/xml/ReservationMessage.xsd";
        private static readonly string TYPEID = "4";
        private static readonly string SENDERUUID = "0a6cca47-af4c-41d8-a3ed-86ff5a00f388";

        [XmlElement(ElementName = "uuid", Namespace = "http://s2it.ehb.be/messages")]
        public string Uuid { get; set; }
        [XmlElement(ElementName = "version", Namespace = "http://s2it.ehb.be/messages")]
        public string Version { get; set; }
        [XmlElement(ElementName = "isActive", Namespace = "http://s2it.ehb.be/messages")]
        public string IsActive { get; set; }
        [XmlElement(ElementName = "activityUUID", Namespace = "http://s2it.ehb.be/messages")]
        public string ActivityUUID { get; set; }
        [XmlElement(ElementName = "visitorUUID", Namespace = "http://s2it.ehb.be/messages")]
        public string VisitorUUID { get; set; }
        [XmlElement(ElementName = "payedFee", Namespace = "http://s2it.ehb.be/messages")]
        public string PayedFee { get; set; }
        [XmlElement(ElementName = "hasAttended", Namespace = "http://s2it.ehb.be/messages")]
        public string HasAttended { get; set; }
        [XmlElement(ElementName = "withInvoice", Namespace = "http://s2it.ehb.be/messages")]
        public string WithInvoice { get; set; }
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

        public static ReservationMessage FromReservationTuple(Tuple<Reservation, string, string> reservationWithActivityUUIDAndVisitorUUID)
        {
            var r = reservationWithActivityUUIDAndVisitorUUID.Item1;
            var actUUID = reservationWithActivityUUIDAndVisitorUUID.Item2;
            var visUUID = reservationWithActivityUUIDAndVisitorUUID.Item3;

            return new ReservationMessage
            {
                Uuid = r.UUID,
                Version = r.Version.ToString().ToLower(),
                IsActive = r.IsActive.ToString().ToLower(),
                ActivityUUID = actUUID,
                VisitorUUID = visUUID,
                PayedFee = r.PayedFee.ToString().ToLower(),
                HasAttended = r.HasAttended.ToString().ToLower(),
                WithInvoice = r.WithInvoice.ToString().ToLower(),
                Xmlns = XMLNS,
                SchemaLocation = SCHEMALOCATION,
                TypeId = TYPEID,
                Timestamp = DateUtility.ConvertToStringWithZoneOffset(DateTime.Now.ToLocalTime()),
                SenderUUID = SENDERUUID
            };
        }

        public static ReservationMessage FromMessage(string xml)
        {
            if (xml != null)
            {
                ReservationMessage resMessage = null;
                XmlSerializer serializer = new XmlSerializer(typeof(ReservationMessage));
                using (StringReader reader = new StringReader(xml))
                {
                    resMessage = (ReservationMessage)serializer.Deserialize(reader);
                    return resMessage;
                }
            }
            return null;
        }//returns a Tuple with the partial Reservation, an ActivityUUID and a VisitorUUID
        //UUID's should be processed further to local ID's
        public Tuple<Reservation, string, string> ToReservationTuple()
        {
            try
            {
                var res = new Reservation
                {
                    UUID = Uuid,
                    Version = long.Parse(Version),
                    IsActive = bool.Parse(IsActive),
                    PayedFee = bool.Parse(PayedFee),
                    HasAttended = bool.Parse(HasAttended),
                    WithInvoice = bool.Parse(WithInvoice)
                };
                return new Tuple<Reservation, string, string>(res, ActivityUUID, VisitorUUID);
            }
            catch (FormatException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }
    }