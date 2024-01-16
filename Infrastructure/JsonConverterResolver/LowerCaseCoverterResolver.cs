using Newtonsoft.Json.Serialization;

namespace TaskManager.Infrastructure.JsonConverterResolver
{
    public class LowerCaseCoverterResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}