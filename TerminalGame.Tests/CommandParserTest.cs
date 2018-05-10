// <copyright file="CommandParserTest.cs">Copyright ©  2017</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerminalGame.Utilities;

namespace TerminalGame.Utilities.Tests
{
    /// <summary>This class contains parameterized unit tests for CommandParser</summary>
    [PexClass(typeof(CommandParser))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CommandParserTest
    {
        /// <summary>Test stub for ParseCommand(String)</summary>
        [PexMethod]
        public string ParseCommandTest(string command)
        {
            string result = CommandParser.ParseCommand(command);
            return result;
            // TODO: add assertions to method CommandParserTest.ParseCommandTest(String)
        }
    }
}
