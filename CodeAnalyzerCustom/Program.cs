using CodeAnalyzer;

if (args.Length == 0)
{
    Console.WriteLine("Usage: CodeAnalyzer <folder-path>");
    Console.WriteLine("Example: CodeAnalyzer ../src");
    return;
}

var folderPath = args[0];
if (!Directory.Exists(folderPath))
{
    Console.WriteLine($"Error: Folder '{folderPath}' does not exist.");
    return;
}

Console.WriteLine($"Analyzing C# files in: {Path.GetFullPath(folderPath)}");

var analyzer = new CodeAnalyzer.CodeAnalyzer();
await analyzer.ProcessCsFilesAsync(folderPath);

Console.WriteLine("Analysis complete!");
