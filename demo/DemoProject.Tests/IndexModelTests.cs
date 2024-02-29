using System.Reflection;
using Demo.Shared.Entities;
using DemoProject.Pages;
using DemoProject.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;

namespace DemoProject.Tests;

[TestClass]
public class IndexModelTests
{
    private Mock<ICharacterRepository> _mockCharacterRepository = null!;
    private Mock<INationRepository> _mockNationRepository = null!;
    private IndexModel _sut = null!;
    private List<Character> _characterTestData = null!;
    private List<Nation> _nationTestData = null!;
    
    [TestInitialize]
    public void Init()
    {
        // Arrange setup
        _characterTestData = new List<Character>
        {
            new() { Name = "Frank" },
            new() { Name = "Henk" },
            new() { Name = "Piet" }
        };
        _nationTestData = new List<Nation> { new(), new() };
        _mockCharacterRepository = new Mock<ICharacterRepository>();
        _mockNationRepository = new Mock<INationRepository>();
        _mockNationRepository.Setup(x => x.GetAll()).Returns(_nationTestData);
        _mockCharacterRepository.Setup(x => x.GetAll()).Returns(_characterTestData);
        _sut = new IndexModel(_mockCharacterRepository.Object, _mockNationRepository.Object); // system under test
    }
    
    [TestMethod]
    public void OnGet_Test()
    {
        // Act doen
        _sut.OnGet();

        // Assert toetsen
        _mockCharacterRepository.Verify(x => x.GetAll(), Times.Once());
        Assert.AreEqual(3, _sut.AvatarCharacters.Count());
        AssertDataRetrieval();
    }

    [TestMethod]
    public void OnPost_ValidModel_RepositoryUsedWithRedirectResult() // given-when-then
    {
        // Act
        var mockCharacter = new Character();
        _sut.NewCharacter = mockCharacter;
        var result = _sut.OnPost();
        
        // Assert
        _mockCharacterRepository.Verify(x => x.Add(It.IsAny<Character>()), Times.Once());
        _mockCharacterRepository.Verify(x => x.GetAll(), Times.Never());
        _mockNationRepository.Verify(x => x.GetAll(), Times.Never());

        result.Should().BeOfType<RedirectToPageResult>();
        // Assert.IsInstanceOfType<RedirectToPageResult>(result);
    }
    
    [TestMethod]
    public void OnPost_InvalidModel_RepositoryNotUsedAndPageReturned()
    {
        // Act
        var mockCharacter = new Character();
        _sut.NewCharacter = mockCharacter;
        _sut.ModelState.AddModelError("qw", "er");
        var result = _sut.OnPost();
        
        // Assert
        _mockCharacterRepository.Verify(x => x.Add(It.IsAny<Character>()), Times.Never());
        Assert.IsInstanceOfType<PageResult>(result);
        AssertDataRetrieval();
    }

    private void AssertDataRetrieval()
    {
        _mockCharacterRepository.Verify(x => x.GetAll(), Times.Once());
        _mockNationRepository.Verify(x => x.GetAll(), Times.Once());
        Assert.AreEqual(_characterTestData, _sut.AvatarCharacters);
        Assert.AreEqual(_nationTestData.Count, _sut.NationOptions.Count());
    }
}



