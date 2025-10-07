using Challenge.Common.Data.Mongo.Repositories;
using Challenge.Microservices.RiderApi.Infra.Data.Entities;
using MongoDB.Driver;

namespace Challenge.Microservices.RiderApi.Infra.Data.Repositories
{
    /// <summary>
    /// Repository implementation for Rider entity
    /// </summary>
    /// <param name="database">MongoDB database instance</param>
    public class RiderRepository(IMongoDatabase database) : MongoRepository<Rider>(database, "Riders"), IRiderRepository
    {
    }
}
