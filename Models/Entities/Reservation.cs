using Assignment_4_SI.Models.API.RequestObjects;
using Assignment_4_SI.Models.API.ResponseObjects;
using Assignment_4_SI.Utility;

namespace Assignment_4_SI.Models.Entities;

public class Reservation : MQEntity
{
     public Activity Activity { get; set; }
            public User Visitor { get; set; }
            public long ActivityId { get; set; }
            public long VisitorId { get; set; }
            public bool PayedFee { get; set; } = false;
            public bool HasAttended { get; set; } = false;
            public bool WithInvoice { get; set; }
    
            public ReservationResponseObject ToResponseObject()
            {
                return new ReservationResponseObject()
                {
                    Id = (long)Id,
                    ActivityId = ActivityId,
                    VisitorId = VisitorId,
                    PayedFee = PayedFee,
                    HasAttended = HasAttended,
                    WithInvoice = WithInvoice,  
                };
            }
    
            public static Reservation FromCreateObjectTuple(Tuple<ReservationCreateObject,string,string> createObjTuple)
            {
                var createObj = createObjTuple.Item1;
                var activityUUID = createObjTuple.Item2;
                var visitorUUID = createObjTuple.Item3;
    
                Reservation r = new Reservation()
                {
                    UUID = UUIDGenerator.GenerateReservationUUID(activityUUID, visitorUUID),
                    ActivityId = createObj.ActivityId,
                    VisitorId = createObj.VisitorId,
                    WithInvoice = createObj.WithInvoice
                };
    
                return r;
            }
    public override string ToXMLMessage()
    {
        throw new NotImplementedException();
    }
}