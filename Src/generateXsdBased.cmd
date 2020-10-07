set scriptDir=%~dp0

"%scriptDir%\Codge.Console\bin\Debug\Codge.Console.exe" -m "%scriptDir%..\tests\Codge.Generator.Test\TestStore\XsdModel\Model.xsd" -o "%scriptDir%/Generated/CS_xsd" -n XsdBasedModel