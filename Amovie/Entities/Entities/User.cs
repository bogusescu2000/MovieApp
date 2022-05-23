using System.Text.Json.Serialization;

namespace Entities.Entities
{
    public class User :  BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string? UserRole { get; set; }

    }
}
