using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeAnalyzer.Tests;

[TestClass]
public class CodeAnalyzerTests
{
    private CodeAnalyzer _analyzer = new();

    [TestMethod]
    public void RenameAsyncMethods_ShouldAddAsyncSuffix()
    {
        var input = "public async Task<string> GetData() { }";
        var expected = "public async Task<string> GetDataAsync() { }";
        
        var result = _analyzer.ProcessFileContent(input);
        
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void RenameAsyncMethods_ShouldNotModifyExistingAsync()
    {
        var input = "public async Task<string> GetDataAsync() { }";
        
        var result = _analyzer.ProcessFileContent(input);
        
        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void FixCasingConventions_ShouldCorrectVmToVM()
    {
        var input = "public UserVm GetUser() { }";
        var expected = "public UserVM GetUser() { }";
        
        var result = _analyzer.ProcessFileContent(input);
        
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void FixCasingConventions_ShouldCorrectDtoToDTO()
    {
        var input = "List<UserDto> users; UserDtos collection;";
        var expected = "List<UserDTO> users; UserDTOs collection;";
        
        var result = _analyzer.ProcessFileContent(input);
        
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void AddBlankLinesBetweenMethods_ShouldAddSpacing()
    {
        var input = @"public void Method1() { }
    public void Method2() { }";
        var expected = @"public void Method1() { }

    public void Method2() { }";
        
        var result = _analyzer.ProcessFileContent(input);
        
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ProcessFileContent_ShouldApplyAllTransformations()
    {
        var input = @"public async Task<UserDto> GetUser() { }
    public UserVm CreateVm() { }";
        var expected = @"public async Task<UserDTO> GetUserAsync() { }

    public UserVM CreateVM() { }";
        
        var result = _analyzer.ProcessFileContent(input);
        
        Assert.AreEqual(expected, result);
    }
}