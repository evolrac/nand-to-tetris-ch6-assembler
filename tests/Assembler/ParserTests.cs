using Assembler;
using Moq;
namespace UnitTests.Assembler;

public class ParserTests
{
    [Theory]
    [InlineData("D=A+1;JGT", "D", "A+1", "JGT", "1110110111010001")]
    [InlineData("D = A+1 ; JGT ", "D", "A+1", "JGT", "1110110111010001")] //Handle spaces
    [InlineData("D=M", "D", "M", "", "1110110110010000")] //No jump
    [InlineData("0;JMP", "", "0", "JMP", "1110000000000000")] //No dest, only comp and jump
    public void Parser_SingleCInstruction_ReturnsValidMachinecode(
        string instruction,
        string expectedDestMnemonic,
        string expectedCompMnemonic,
        string expectedJumpMnemonic,
        string expectedMachineCode
    )
    {
        // Arrange
        var codeGenerator = new Mock<ICodeGenerator>();
        codeGenerator.Setup(cg => cg.GenerateCodeForCInstruction(
            expectedDestMnemonic,
            expectedCompMnemonic,
            expectedJumpMnemonic))
            .Returns(expectedMachineCode);
        var parser = new Parser(codeGenerator.Object);

        // Act
        var result = parser.Parse(new List<string> { instruction });

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedMachineCode, result[0]);
    }

    [Theory]
    [InlineData("@123", 123, "0000000001111011")]
    [InlineData("@ 123", 123, "0000000001111011")]
    [InlineData("@0", 0, "0000000000000000")]
    public void Parser_SingleAInstruction_ReturnsValidMachineCode(
        string instruction,
        int expectedValue,
        string expectedMachineCode)
    {
        // Arrange
        var codeGenerator = new Mock<ICodeGenerator>();
        codeGenerator.Setup(cg => cg.GenerateCodeForAInstruction(expectedValue))
            .Returns(expectedMachineCode);
        var parser = new Parser(codeGenerator.Object);

        // Act
        var result = parser.Parse(new List<string> { instruction });

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedMachineCode, result[0]);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("// This is a comment")]
    public void Parser_SkippedInstruction_SkipsGeneratingMachineCode(string instruction)
    {
        // Arrange
        var codeGenerator = new Mock<ICodeGenerator>();
        var parser = new Parser(codeGenerator.Object);

        // Act
        var result = parser.Parse(new List<string> { instruction });

        // Assert
        Assert.Empty(result);
    }
}
