using System;
using System.Diagnostics;
using System.Linq;

namespace OpenMagic.Exceptions
{
    public class ToDoException : NotImplementedException
    {
        public ToDoException()
            : base(GetMessage())
        {
        }

        private static string GetMessage()
        {
            var calledBy = (new StackTrace()).GetFrame(2).GetMethod();
            var methodName = calledBy.Name == ".ctor" ? "ctor" : calledBy.Name;
            var parameters = string.Join(", ", calledBy.GetParameters().Select(p => string.Format("{0} {1}", p.ParameterType, p.Name)));
            var message = string.Format("{2}.{0}({1})", methodName, parameters, calledBy.DeclaringType.Name);

            return message;
        }
    }
}