using System.Text.Json.Serialization;

namespace Challenge.Microservices.RiderApi.Commands
{
    /// <summary>
    /// Command to upload a CNH (driver's license) image for an existing rider
    /// </summary>
    public class UploadRiderCnhImageCommand : ICommand
    {
        /// <summary>
        /// The unique identifier of the rider who is uploading the CNH image
        /// </summary>
        [JsonPropertyName("id")]
        public string? RiderId { get; set; }

        /// <summary>
        /// Base64 encoded image with data URI prefix (must be PNG or BMP format, e.g., data:image/png;base64,...)
        /// </summary>
        [JsonPropertyName("imagem_cnh")]
        public required string CnhImage { get; set; }
    }
}
