﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace RuntimeDemo
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class RuntimeTextTemplate1 : RuntimeTextTemplate1Base
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuntimeDemo;

namespace Regulations_UnitTestRunner
{

	public static class Extensions
	{
		public static bool In<T>(this T obj, params object[] list)
		{
			return list.Contains(obj);
		}
	} 

	public partial class RuleInputData 
	{
		");
            
            #line 27 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 foreach (var data in InputProperties)   
	   { 
            
            #line default
            #line hidden
            this.Write("  \r\n\t\t\tpublic ");
            
            #line 29 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Value));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 29 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write(" {get;set;}\r\n\t\t");
            
            #line 30 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 }  
            
            #line default
            #line hidden
            this.Write("\r\n\t   public void SetInputData(Dictionary<string,string> inputData)\r\n\t   {\r\n\t\tCon" +
                    "sole.WriteLine(\"Setting Input Data\");\r\n\r\n\t\t  ");
            
            #line 36 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
  foreach(var data in InputProperties){ 
            
            #line default
            #line hidden
            this.Write("\t\t\t  if(inputData.ContainsKey(\"");
            
            #line 37 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write("\")){\r\n\t\t\t\t  ");
            
            #line 38 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 if (data.Value.ToString().ToLower().Contains("string")) {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t  ");
            
            #line 39 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write(" = inputData[\"");
            
            #line 39 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write("\"];\r\n\t\t\t\t  ");
            
            #line 40 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
} else {
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t  ");
            
            #line 41 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write(" = ");
            
            #line 41 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Value));
            
            #line default
            #line hidden
            this.Write(".Parse(inputData[\"");
            
            #line 41 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write("\"]);\r\n\t\t\t\t  ");
            
            #line 42 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t\t  }\r\n\t\t\t");
            
            #line 44 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\tConsole.WriteLine(\"Setting Input Data completed\");\r\n\t   }\r\n\r\n\t}\r\n\r\n\tpublic p" +
                    "artial class RuleOutputData \r\n\t{\r\n\t\t");
            
            #line 53 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 foreach (var data in OutputProperties)   
	   { 
            
            #line default
            #line hidden
            this.Write("  \r\n\t   public ");
            
            #line 55 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Value));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 55 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write(" {get;set;}\r\n\t");
            
            #line 56 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 }  
            
            #line default
            #line hidden
            this.Write("\t}\r\n\r\n\tpublic class CalculationRule : IRule \r\n\t{\r\n\t\t#region WorkingDataElements S" +
                    "tart\r\n\t\t");
            
            #line 62 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 foreach (var data in WorkingProperties)   
		   { 
            
            #line default
            #line hidden
            this.Write("  \r\n\t\t   public ");
            
            #line 64 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Value));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 64 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(data.Key));
            
            #line default
            #line hidden
            this.Write(" {get;set;}\r\n\t\t");
            
            #line 65 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
 }  
            
            #line default
            #line hidden
            this.Write(@"		
		#endregion WorkingDataElements Start

		public RuleOutputData ruleOutputData = new RuleOutputData();
		public RuleInputData ruleInputData = new RuleInputData();

		public bool Condition()
		{
		Console.WriteLine(""Executing Condition Block started"");
			return ");
            
            #line 75 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ConditionDefinition));
            
            #line default
            #line hidden
            this.Write(";\r\n\t\t}\r\n\r\n\t\tpublic void Regulation()\r\n\t\t{\r\n\t\tConsole.WriteLine(\"Executing Regulat" +
                    "ion Block started\");\r\n\t\t\t");
            
            #line 81 "D:\RuntimeDemo\RuntimeDemo\RuntimeTextTemplate1.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(RegulationDefinition));
            
            #line default
            #line hidden
            this.Write(@"
			Console.WriteLine(""Executing Regulation Block completed"");
		}

		public dynamic ComputeRule(Dictionary<string,string> inputData)
		{
			ruleInputData.SetInputData(inputData);

			var res = new RuleOutputData();

			if(Condition()){
				Regulation();
			}		
			return res;
		}
	}
}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class RuntimeTextTemplate1Base
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
