namespace Assembler;

public class Parser : IParser
{
    private readonly ICodeGenerator _codeGenerator;
    public Parser(ICodeGenerator codeGenerator)
    {
        _codeGenerator = codeGenerator;
    }
    public List<string> Parse(List<string> rows)
    {
        var machineCodes = new List<string>();
        foreach (string row in rows)
        {
            string machineCodeRow;
            if (string.IsNullOrWhiteSpace(row) || row.StartsWith("//"))
            {
                //Skip empty lines and comments
                continue;
            }
            else if (row.StartsWith('@'))
            {
                // Handle A-instruction
                machineCodeRow = ParseAInstructionRow(row);
            }
            else
            {
                // Handle C-instruction
                machineCodeRow = ParseCInstructionRow(row);
            }
            machineCodes.Add(machineCodeRow);

        }
        return machineCodes;
    }

    string ParseAInstructionRow(string row)
    {
        // Assuming each row is an A-instruction in the format "@value"
        var value = row.TrimStart('@');
        if (int.TryParse(value, out int numericValue))
        {
            return _codeGenerator.GenerateCodeForAInstruction(numericValue);
        }
        else
        {
            throw new ArgumentException($"Invalid A-instruction value: {value}");
        }
    }

    string ParseCInstructionRow(string row)
    {
        // Assuming each row is a C-instruction in the format "dest=comp;jump"
        var destMnemonic = row.Contains('=') ? row.Split('=')[0].Trim() : "";
        var rowWithoutDest = row.Contains('=') ? row.Split('=')[1] : row;
        var compMnemonic = rowWithoutDest.Contains(';') ? rowWithoutDest.Split(';')[0].Trim() : rowWithoutDest.Trim();
        var rowWithoutComp = rowWithoutDest.Contains(';') ? rowWithoutDest.Split(';')[1] : "";
        var jumpMnemonic = rowWithoutComp.Trim();
        return _codeGenerator.GenerateCodeForCInstruction(destMnemonic, compMnemonic, jumpMnemonic);
    }
    


}
