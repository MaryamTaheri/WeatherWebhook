using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace WeatherWebhook.Domain.Framework.Helpers;

public static class AssemblyScanner
{
    private static readonly Dictionary<string, List<Type>> AllLoadedTypes = new();

    public static List<Type> AllTypes(string nameSpace, string pattern, IHttpContextAccessor accessor = null)
    {
        var theKey = nameSpace + "--" + pattern;
        
        if (AllLoadedTypes.ContainsKey(theKey) && AllLoadedTypes[theKey] != null)
            return AllLoadedTypes[theKey];

        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, $"{nameSpace}*.dll")
            .Where(path => new Regex($"{nameSpace.Replace(".", "\\.")}(.*){pattern}(.*)\\.dll$").IsMatch(path))
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        
        if (AllLoadedTypes.ContainsKey(theKey))
            AllLoadedTypes[theKey] = assemblies.ToArray().SelectMany(x => x.GetTypes()).ToList();
        else

            AllLoadedTypes.Add(theKey, assemblies.ToArray().SelectMany(x => x.GetTypes()).ToList());
        
        return AllLoadedTypes[theKey];
    }
}