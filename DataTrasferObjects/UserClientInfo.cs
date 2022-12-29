using Newtonsoft.Json;

namespace Api.DataTransferObjects;
public class UserClientInfo
{
    [JsonProperty(PropertyName = "username")]
    public string Username { get; set; }

}