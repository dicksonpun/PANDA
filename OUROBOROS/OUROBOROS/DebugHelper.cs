﻿using System.Diagnostics;

namespace OUROBOROS
{
    class DebugHelper
    {
        // Example Usage: "var whoCalledMe = DebugHelper.GetCaller();"
        public static string GetCaller(int level = 2)
        {
            var m = new StackTrace().GetFrame(level).GetMethod();

            // .Name is the name only, .FullName includes the namespace
            var className = m.DeclaringType.FullName;

            //the method/function name you are looking for.
            var methodName = m.Name;

            //returns a composite of the namespace, class and method name.
            return className + "->" + methodName;
        }
    }
}
