using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TeacherUtilityBelt.Core;
using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;
using TeacherUtilityBelt.Infrastructure;

namespace TeacherUtilityBelt.Test;

public class CrossWordTest
{
    private Mock<IWordDictionary> wordDictionary;
    private IWordGridNavigator wordGridNavigator;
    private Mock<IGridHelper> gridHelper;
    private Mock<ILogger<RequestManager>> logger;
    private Mock<IOptions<AppSettings>> options;

    public CrossWordTest()
    {
        wordDictionary = new Mock<IWordDictionary>();
        wordGridNavigator = new WordGridNavigation();
        gridHelper = new Mock<IGridHelper>();
        logger = new Mock<ILogger<RequestManager>>();
        options = new Mock<IOptions<AppSettings>>();
    }

    private RequestManager BuildSystemUnderTest()
    {
        return new RequestManager(wordDictionary.Object, wordGridNavigator, gridHelper.Object, logger.Object, options.Object);
    }

    private string[][] GenerateTestGrid()
    {
        string[][] s = new string[8][];

        s[0] = new[] { "1", "2", "3", "4", "a", "F", "O", "O"};
        s[1] = new[] { "a", "a", "a", "a", "a", "a", "O", "O"};
        s[2] = new[] { "H", "C", "R", "A", "a", "a", "O", "Y"};
        s[3] = new[] { "a", "a", "a", "a", "a", "a", "O", "M"};
        s[4] = new[] { "1", "2", "3", "4", "5", "6", "7", "O"};
        s[5] = new[] { "a", "a", "Z", "a", "a", "O", "O", "O"};
        s[6] = new[] { "a", "Y", "a", "A", "Z", "a", "O", "O"};
        s[7] = new[] { "X", "a", "a", "a", "R", "a", "O", "O"};

        return s;
    }

    
    [Theory]
    [InlineData("1234", "1234")]
    [InlineData("FOO", "FOO")]
    [InlineData("Arch", "Arch")]
    [InlineData("XYZ", "XYZ")]
    [InlineData("7654321", "7654321")]
    public async void Test1(string key, string value)
    {
        options.Setup(s => s.Value).Returns( new AppSettings{ FoundWordMinCount = value.Length });
        gridHelper.Setup(s => s.GenerateRandomGrid(It.IsAny<Coordinate>())).Returns( Task.FromResult(GenerateTestGrid()));
        IDictionary<string, string> d = new Dictionary<string, string>
        {
            { key, value }
        };

        wordDictionary.Setup(s => s.GetWordDictionary(It.IsAny<string>())).Returns(Task.FromResult(d));

        var sut = BuildSystemUnderTest();

        var response = await sut.GenerateCrosswordGrid(new Coordinate(8,8));
        
        Assert.True(response.GridAnswer.Count() == 1);
    }
}