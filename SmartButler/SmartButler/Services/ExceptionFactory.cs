using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SmartButler.Services
{
    /// <summary>
    /// This is a static class which helps throwing exceptions
    /// The "CallerMemberName", "CallerLineNumber" and "CallerLineNumber" will displayed automatically
    /// </summary>
    public static class ExceptionFactory
    {
        /// <summary>
        /// The "CallerMemberName", "CallerLineNumber" and "CallerLineNumber" will displayed automatically
        /// add a custom message to "parameters"
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="callerName"></param>
        /// <param name="lineNumber"></param>
        /// <param name="parameters"></param>
        public static TException Get<TException>(IEnumerable<string> parameters = null, 
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
        where TException : Exception
        {
            string msg = BuildExceptionMsg<TException>(parameters, filePath, callerName, lineNumber);

            return (TException)Activator.CreateInstance(typeof(TException), msg);
        }   

        public static string BuildExceptionMsg<TException>(IEnumerable<string> parameters,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string callerName = "",
            [CallerLineNumber] int lineNumber = 0)
            where TException : Exception
        {

            return $@"Exception of this type was thrown: {typeof(TException).Name} {Environment.NewLine}
                      File path: {filePath} {Environment.NewLine}
                      Caller: {callerName} {Environment.NewLine}
                      Line number: {lineNumber} {Environment.NewLine}
                      Message/Parameters: {string.Join("\n", parameters)}"; 

        }
    }
}
