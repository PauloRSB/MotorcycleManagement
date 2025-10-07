using Challenge.Common.Data.Mongo.Interfaces;
using Challenge.Microservices.RiderApi.Infra.Data.Entities;

namespace Challenge.Microservices.RiderApi.Infra.Data.Repositories
{
    /// <summary>
    /// Repository interface for Rider entity operations
    /// </summary>
    public interface IRiderRepository : IMongoRepository<Rider>
    {
    }
}