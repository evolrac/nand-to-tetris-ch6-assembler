using Assembler;
namespace UnitTests.Assembler;

public class CodeGeneratorTests
{
    [Theory]
    [InlineData("D", "A+1", "JGT", "1110110111010001")]
    [InlineData("M", "D|M", "JMP", "1111010101001111")]
    [InlineData("", "!A", "", "1110110001000000")]
    public void CodeGeneratorCInstruction_ValidMnemonicInput_ReturnsValidMachineCode(
        string destMnemonic,
        string compMnemonic,
        string jumpMnemonic,
        string expectedMachineCode)
    {
        // Arrange
        CodeGenerator codeGenerator = new CodeGenerator();

        // Act
        var result = codeGenerator.GenerateCodeForCInstruction(destMnemonic, compMnemonic, jumpMnemonic);

        // Assert
        Assert.Equal(expectedMachineCode, result);
    }

    [Theory]
    [InlineData(123, "0000000001111011")]
    [InlineData(0, "0000000000000000")]
    public void CodeGeneratorAInstruction_ValidNumericValue_ReturnsValidMachineCode(
        int value, string expectedMachineCode)
    {
        // Arrange
        CodeGenerator codeGenerator = new CodeGenerator();

        // Act
        var result = codeGenerator.GenerateCodeForAInstruction(value);

        // Assert
        Assert.Equal(expectedMachineCode, result);
    }
}