using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public class GuidToObjectIdSerializer : SerializerBase<Guid>
{
    public override Guid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var type = context.Reader.GetCurrentBsonType();
        switch (type)
        {
            case BsonType.ObjectId:
                var oid = context.Reader.ReadObjectId();
                return Guid.Parse(oid.ToString());
            default:
                var message = string.Format("Cannot convert a {0} to a Guid.", type);
                throw new NotSupportedException(message);
        }
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Guid value)
    {
        context.Writer.WriteObjectId(ObjectId.Parse(value.ToString()));
    }
}