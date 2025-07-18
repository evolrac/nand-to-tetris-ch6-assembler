namespace Assembler
{
    public interface IParser
    {
        //Given list of assembler rows, return list of machine code rows
        public List<string> Parse(List<string> rows);
    }
}
