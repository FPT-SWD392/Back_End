using MongoDB.Bson.Serialization.Attributes;

namespace BusinessObject.MongoDbObject
{
    public class ApplicationSetting : IMongoDbObject
    {
        [BsonId]
        public string Id { get; set; }
        public string ImageProcessorWatermarkText { get; set; }
        public int ImageProcessorMaxImageSize { get; set; }

        public int ApplicationSettingItemPerPage { get; set; }
        public int ArtUploadLimit { get; set; }
        public int ArtUploadSizeLimit { get; set; }
        public double SystemRevenuePercentage { get; set; }
    }

}
