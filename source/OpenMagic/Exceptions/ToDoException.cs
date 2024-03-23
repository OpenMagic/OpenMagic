using System;
using System.Diagnostics;
using System.Linq;

namespace OpenMagic.Exceptions;

public class ToDoException() : NotImplementedException(GetMessage())
{
    private static string GetMessage()
    {
        var calledBy = new StackTrace().GetFrame(2)!.GetMethod();
        var methodName = calledBy!.Name == ".ctor" ? "ctor" : calledBy.Name;
        var parameters = string.Join(", ", calledBy.GetParameters().Select(p => $"{p.ParameterType} {p.Name}"));
        var message = string.Format("{2}.{0}({1})", methodName, parameters, calledBy.DeclaringType!.Name);

        return message;
    }
}