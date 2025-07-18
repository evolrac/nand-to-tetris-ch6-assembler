![CI](https://github.com/evolrac/nand-to-tetris-ch6-assembler/actions/workflows/dotnet-ci.yml/badge.svg)

# nand-to-tetris-ch6-assembler
Ch 6 in NAND to tetris book using .NET 8/C#. See https://www.nand2tetris.org/software for more background information.

## Usage

```
# Clone
$ git clone https://github.com/evolrac/nand-to-tetris-ch6-assembler.git
$ cd nand-to-tetris-ch6-assembler

# Check all tests running
$ dotnet test

# Build
$ dotnet build

#Run commandline app assuming you have a valid hack-assembler file called Add.asm
$ /src/Commandline/bin/Debug/net8.0/Commandline --Inputfile Add.asm --Outputfile Add.hack
```
