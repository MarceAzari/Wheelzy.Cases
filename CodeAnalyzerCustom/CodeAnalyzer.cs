using System.Text.RegularExpressions;

namespace CodeAnalyzer;

public class CodeAnalyzer
{
    public async Task ProcessCsFilesAsync(string folderPath)
    {
        var csFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);
        
        foreach (var file in csFiles)
        {
            var content = await File.ReadAllTextAsync(file);
            var modified = ProcessFileContent(content);
            
            if (modified != content)
            {
                await File.WriteAllTextAsync(file, modified);
                Console.WriteLine($"Modified: {Path.GetRelativePath(folderPath, file)}");
            }
        }
    }

    public string ProcessFileContent(string content)
    {
        content = RenameAsyncMethods(content);
        content = FixCasingConventions(content);
        content = AddBlankLinesBetweenMethods(content);
        return content;
    }

    private string RenameAsyncMethods(string content)
    {
        var pattern = @"(public|private|protected|internal)?\s*(static\s+)?(async\s+Task[<>?\w]*\s+)(\w+)(\s*\()";
        return Regex.Replace(content, pattern, match =>
        {
            var methodName = match.Groups[4].Value;
            
            // No renombrar métodos que implementan interfaces conocidas
            if (methodName == "Handle" || methodName == "Dispose" || methodName == "ToString" || 
                methodName == "Equals" || methodName == "GetHashCode")
                return match.Value;
            
            if (!methodName.EndsWith("Async"))
                methodName += "Async";
            
            return $"{match.Groups[1].Value} {match.Groups[2].Value}{match.Groups[3].Value}{methodName}{match.Groups[5].Value}";
        });
    }

    private string FixCasingConventions(string content)
    {
        content = Regex.Replace(content, @"\b(\w+)Vm\b", "$1VM");
        content = Regex.Replace(content, @"\b(\w+)Vms\b", "$1VMs");
        content = Regex.Replace(content, @"\b(\w+)Dto\b", "$1DTO");
        content = Regex.Replace(content, @"\b(\w+)Dtos\b", "$1DTOs");
        return content;
    }

    private string AddBlankLinesBetweenMethods(string content)
    {
        var pattern = @"(\}\s*\r?\n)(\s*)(public|private|protected|internal)";
        return Regex.Replace(content, pattern, "$1\n$2$3");
    }
}