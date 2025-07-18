using System.Diagnostics.Contracts;

namespace Assembler;

public class CodeGenerator : ICodeGenerator
{
    private readonly Dictionary<string, string> destMnemonictToBinary;
    private readonly Dictionary<string, (string, string)> compMnemonictToBinary;
    private readonly Dictionary<string, string> jumpMnemonictToBinary;
    public CodeGenerator()
    {
        destMnemonictToBinary = new Dictionary<string, string>
        {
            { "",     "000" },
            { "M",    "001" },
            { "D",    "010" },
            { "MD",   "011" },
            { "A",    "100" },
            { "AM",   "101" },
            { "AD",   "110" },
            { "AMD",  "111" }
        };

        // CompMnemonictToBinary is a dictionary that maps mnemonics to their binary representations.        
        //Translates Mnemomics to string for a-value and string for c1-c6
        compMnemonictToBinary = new Dictionary<string, (string, string)>
        {
            //a=0
            { "0",   ("0","101010") },
            { "1",   ("0","111111") },
            { "-1",  ("0","111010") },
            { "D",   ("0","001100") },
            { "A",   ("0","110000") },
            { "!D",  ("0","001101") },
            { "!A",  ("0","110001") },
            { "-D",  ("0","001111") },
            { "-A",  ("0","110011") },
            { "D+1", ("0","011111") },
            { "A+1", ("0","110111") },
            { "D-1", ("0","001110") },
            { "A-1", ("0","110010") },
            { "D+A", ("0","000010") },
            { "D-A", ("0","010011") },
            { "A-D", ("0","000111") },
            { "D&A", ("0","000000") },
            { "D|A", ("0","010101") },

            //a=1
            { "M",   ("1","110000") },
            { "!M",  ("1","110001") },
            { "-M",  ("1","110011") },
            { "M+1", ("1","110111") },
            { "M-1", ("1","110010") },
            { "D+M", ("1","000010") },
            { "D-M", ("1","010011") },
            { "M-D", ("1","000111") },
            { "D&M", ("1","000000") },
            { "D|M", ("1","010101") }
        };

        jumpMnemonictToBinary = new Dictionary<string, string>
        {
            { "",     "000" },
            { "JGT",  "001" },
            { "JEQ",  "010" },
            { "JGE",  "011" },
            { "JLT",  "100" },
            { "JNE",  "101" },
            { "JLE",  "110" },
            { "JMP",  "111" }
        };
    }

    public string GenerateCodeForAInstruction(int value)
    {
        return Convert.ToString(value, 2).PadLeft(16, '0');
    }

    public string GenerateCodeForCInstruction(string destMnemonic, string compMnemonic, string jumpMnemonic)
    {
        if (!compMnemonictToBinary.TryGetValue(compMnemonic, out var comp))
        {
            throw new ArgumentException($"Invalid comp mnemonic: {compMnemonic}");
        }

        if (!destMnemonictToBinary.TryGetValue(destMnemonic, out var dest))
        {
            throw new ArgumentException($"Invalid dest mnemonic: {destMnemonic}");
        }

        if (!jumpMnemonictToBinary.TryGetValue(jumpMnemonic, out var jump))
        {
            throw new ArgumentException($"Invalid jump mnemonic: {jumpMnemonic}");
        }

        return $"111{comp.Item1}{comp.Item2}{dest}{jump}";
    }
    

    
}