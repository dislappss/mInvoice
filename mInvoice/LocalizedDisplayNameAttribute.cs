using System.ComponentModel;

namespace mInvoice
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute() { }

        public LocalizedDisplayNameAttribute(string resourceId)
            : base(GetMessageFromResource(resourceId))
        { }

        public static string GetMessageFromResource(string resourceId)
        {
            return Resources.Resource.ResourceManager.GetString(resourceId);           
        }
    }

    public class LocalizedDisplay
    {
        public static string GetMessageFromResource(string resourceId)
        {
            return Resources.Resource.ResourceManager.GetString(resourceId);           
        }
    }
}