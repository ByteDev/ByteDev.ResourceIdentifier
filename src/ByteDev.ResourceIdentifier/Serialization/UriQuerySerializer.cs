using System.Linq;
using System.Reflection;
using System.Text;

namespace ByteDev.ResourceIdentifier.Serialization
{
    public static class UriQuerySerializer
    {
        public static string Serialize(object obj)
        {
            var nameValues = obj.GetType()
                .GetRuntimeProperties()
                .Where(p => p.GetValue(obj, null) != null)
                .Select(p => new
                {
                    Key = p.Name,
                    Value = p.GetValue(obj, null).ToString()
                });

            var sb = new StringBuilder();

            foreach (var nameValue in nameValues)
            {
                sb.Append(sb.Length == 0 ? "?" : "&");
                sb.Append(nameValue.Key);
                sb.Append("=");
                sb.Append(nameValue.Value);
            }

            return sb.ToString();
        }
    }
}