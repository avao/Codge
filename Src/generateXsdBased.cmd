set scriptDir=%~dp0

"%scriptDir%\Codge.Console\bin\Debug\Codge.Console.exe" -m "%scriptDir%\Codge.Generator.Test\TestStore\XsdLoader\LoadXsd\Test.xsd" -o "%scriptDir%/Generated/CS_xsd" -n XsdBasedModel