using Demo.Shared.Entities;
using DemoProject.Pages;
using DemoProject.Repositories;
using FakeItEasy;

namespace DemoProject.FakingAmazing;

[TestClass]
public class UnitTest1
{
    private ICharacterRepository _mockCharacterRepository = null!;
    private INationRepository _mockNationRepository = null!;
    private IndexModel _sut = null!;
    
    [TestInitialize]
    public void Init()
    {
        // Arrange setup

        _mockCharacterRepository = A.Fake<ICharacterRepository>();
        _mockNationRepository = A.Fake<INationRepository>();

        A.CallTo(() => _mockCharacterRepository.Add(A.Fake<Character>())).DoesNothing();
        
        A.CallTo(() => _mockCharacterRepository.GetAll()).Returns(new List<Character>
        {
            new() { Name = "Frank" },
            new() { Name = "Henk" },
            new() { Name = "Piet" }
        });
        _sut = new IndexModel(_mockCharacterRepository, _mockNationRepository); // system under test
    }
    
    [TestMethod]
    public void OnGet_Bla()
    {
        // Act doen
        _sut.OnGet();

        // Assert toetsen
        A.CallTo(() => _mockCharacterRepository.GetAll()).MustHaveHappened();
        Assert.AreEqual(3, _sut.AvatarCharacters.Count());
    }
    
    [TestMethod]
    public void OnPost_ValidModel()
    {
        var aCharacter = A.Fake<Character>();
        _sut.NewCharacter = aCharacter;
        
        // Act doen
        _sut.OnPost();

        // Assert toetsen
        A.CallTo(() => _mockCharacterRepository.Add(aCharacter)).MustHaveHappened();
    }
}