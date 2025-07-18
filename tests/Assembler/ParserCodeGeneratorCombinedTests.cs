using System.Runtime.CompilerServices;
using Assembler;
namespace UnitTests.Assembler;

public class ParserCodeGeneratorCombinedTests
{
    [Theory]
    [InlineData("D=A+1;JGT", "1110110111010001")]
    [InlineData("D = A+1 ; JGT ", "1110110111010001")]
    [InlineData("@123", "0000000001111011")]
    public void ParserAndCodeGenerator_SingleInstruction_ReturnsValidMachinecode(
        string instruction,
        string expectedMachineCode
    )
    {
        // Arrange
        var codeGenerator = new CodeGenerator();
        var parser = new Parser(codeGenerator);

        // Act
        var result = parser.Parse(new List<string> { instruction });

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedMachineCode, result[0]);
    }

    [Theory]
    [InlineData(
        new string[] {
            "@2",
            "D=A",
            "@3",
            "D=D+A",
            "@0",
            "M=D"
        },
        new string[] {
            "0000000000000010",
            "1110110000010000",
            "0000000000000011",
            "1110000010010000",
            "0000000000000000",
            "1110001100001000"
        }
    )]
    [InlineData(new string[] { }, new string[] { })]
    [InlineData(new string[] { "  ", "// comment" }, new string[] { })] //Empty input
    public void ParserAndCodeGenerator_MultipleLineValidInput_ReturnsEmptyList(
        string[] instructions,
        string[] expectedMachineCode
    )
    {
        // Arrange
        var codeGenerator = new CodeGenerator();
        var parser = new Parser(codeGenerator);

        // Act
        var result = parser.Parse(instructions.ToList());

        // Assert
        Assert.Equal(expectedMachineCode.Length, result.Count);
        for (int i = 0; i < expectedMachineCode.Length; i++)
        {
            Assert.Equal(expectedMachineCode[i], result[i]);
        }

    }    
}