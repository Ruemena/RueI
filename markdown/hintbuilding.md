# HintBuilding Extensions
RueI provides extensions to [StringBuilder](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder?view=net-7.0) through the [HintBuilding](../api/RueI.Extensions.HintBuilding.HintBuilding.html) class. this provides a large number of methods on StringBuilder to make adding tags easier and less error-prone.

to use this, you first bring the class into scope:
```csharp
using RueI.Extensions.HintBuilding;
```
then, you can use it like so: 
```csharp
StringBuilder sb = new()
  .SetColor(255, 0, 0)
  .SetSize(1.5, MeasurementStyle.Ems)
  .Append("hello!")
  .CloseColor()
  .CloseSize();
```
since all of the methods return the original StringBuilder, you can chain them easily.