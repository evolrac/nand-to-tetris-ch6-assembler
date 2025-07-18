namespace Assembler;

public interface ICodeGenerator
{
    string GenerateCodeForCInstruction(string destMnemonic, string compMnemonic, string jumpMnemonic);
    string GenerateCodeForAInstruction(int value);
}

