using System.Reflection;
using DemoProject.Entities;
using DemoProject.Pages;
using DemoProject.Repositories;
using Moq;

namespace DemoProject.Tests;

[TestClass]
public class IndexModelTests
{
    private Mock<ICharacterRepository> _mockCharacterRepository = null!;
    private Mock<INationRepository> _mockNationRepository = null!;
    private IndexModel _sut = null!;
    
    [TestInitialize]
    public void Init()
    {
        // Arrange setup
        _mockCharacterRepository = new Mock<ICharacterRepository>();
        _mockNationRepository = new Mock<INationRepository>();
        _mockCharacterRepository.Setup(x => x.GetAll()).Returns(new List<Character>
        {
            new() { Name = "Frank" },
            new() { Name = "Henk" },
            new() { Name = "Piet" }
        });
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
    }

    [TestMethod]
    public void OnPost_ValidModel_TodoAdded() // given-when-then
    {
        // _mockCharacterRepository.Verify(x => x.Add(It.IsAny<Character>()), Times.Once());
    }
    
    [TestMethod]
    public void OnPost_InvalidModel_TodoAdded()
    {
        
    }
}

