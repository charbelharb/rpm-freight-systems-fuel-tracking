using System.Reflection;

namespace RpmFsRepositories.Helpers;

public static class ResourcesUtility
{
    public static string LoadQuery(string fileName, string folderName, Assembly? sourceAssembly = null)
    {
        sourceAssembly ??= Assembly.GetCallingAssembly();

        var assemblyName = sourceAssembly.GetName().Name;

        var resourceName = $"{folderName}.{fileName}";

        var fullResourceName = $"{assemblyName}.{resourceName}";

        var stream = sourceAssembly.GetManifestResourceStream(fullResourceName);
        if (stream == null)
        {
            throw new ArgumentException($"The embedded resource '{fullResourceName}' could not be loaded or is empty.");
        }

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}