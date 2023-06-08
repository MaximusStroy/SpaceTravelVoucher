using Microsoft.Extensions.Localization;
using System.Reflection;

namespace SpaceTravelVoucher.Main.Models
{
    public class LanguageService
    {
        private IStringLocalizer _local;
        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _local = factory.Create("SharedResource", assemblyName.Name);

        }

        public LocalizedString GetKey(string key)
        {
            return _local[key];
        }
    }
}
