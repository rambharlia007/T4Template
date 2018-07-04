﻿using ExcelDataReader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CSharp;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeDemo
{

    public partial class RuleTextTemplate
    {
        public string RuleDefination { get; set; }
        public Dictionary<string, Type> InputProperties { get; set; }
        public Dictionary<string, Type> OutputProperties { get; set; }

        public List<Dictionary<string, int>> CsvInputValues { get; set; }

        public RuleTextTemplate(string ruleDefination, Dictionary<string, Type> inputProperties,
             Dictionary<string, Type> outputProperties, List<Dictionary<string, int>> csvInputValues)
        {
            RuleDefination = ruleDefination;
            InputProperties = inputProperties;
            OutputProperties = outputProperties;
            CsvInputValues = csvInputValues;
        }
    }



    public partial class RuntimeTextTemplate1
    {
        public string RuleDefination { get; set; }
        public Dictionary<string, Type> InputProperties { get; set; }
        public Dictionary<string, Type> OutputProperties { get; set; }

        public RuntimeTextTemplate1(string ruleDefination, Dictionary<string, Type> inputProperties,
             Dictionary<string, Type> outputProperties)
        {
            RuleDefination = ruleDefination;
            InputProperties = inputProperties;
            OutputProperties = outputProperties;
        }
    }

    public partial class RuleInputData{
    }

    public partial class RuleOutputData
    {
            
    }

    public interface IRule
    {
        dynamic ComputeRule(Dictionary<string, string> inputData);
    }

    public class ExcelOutputData
    {
        public string Name { get; set; }
        public string ActualValue { get; set; }
        public string ExpectedValue { get; set; }

        public bool IsTestCasePass
        {
            get
            {
                return ExpectedValue.Trim().Equals(ActualValue.Trim());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ruleMethodDefination = GetRuleMethodDefination();
            var inputProperties = GetInputProperties();
            var outputProperties = GetOutputProperties();

            var outputData = new List<List<ExcelOutputData>>();
            var data = GetCsvInputValues();
            var columns = data.FirstOrDefault().Select(e => e.Key).ToList();

            for (int i=0; i < 20; i++)
            {
                var dat = new List<ExcelOutputData>();
                foreach(var name in columns)
                {
                    var random = new Random().Next(1000,10000).ToString();
                    dat.Add(new ExcelOutputData()
                    {
                        Name = name.ToString(),
                        ActualValue = random,
                        ExpectedValue = random
                    });
                }
                outputData.Add(dat);
            }



            //var csvInputValues = GetCsvInputValues();
            //var T4Template = new RuleTextTemplate(ruleMethodDefination, inputProperties, outputProperties, csvInputValues);
            //var ruleCode = T4Template.TransformText();


            //var T4Template1 = new RuntimeTextTemplate1(ruleMethodDefination, inputProperties, outputProperties);
            //var ruleCode1 = T4Template1.TransformText();


            //PrintCalculationRuleResult(ruleCode1);


            WriteExcel.WriteExcelFile(outputData);



        }


        private static List<Dictionary<string, string>> GetCsvInputValues()
        {
            var inputDataValues = new List<Dictionary<string, string>>();
            using (var stream = File.Open(@"D:\TestData.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var noOfTestCases = reader.AsDataSet().Tables[0].Columns.Count - 1;
                    var inputRowValues = reader.AsDataSet().Tables[0].Rows.Cast<DataRow>().Skip(1);

                    for (var i = 0; i < noOfTestCases; i++)
                    {
                        inputDataValues.Add(new Dictionary<string, string>());
                    }

                    foreach (DataRow row in inputRowValues)
                    {
                        var columnValue = row.ItemArray.Skip(1).ToList();
                        var inputPropertyName = row.ItemArray.First().ToString();
                        for (int i = 0; i < columnValue.Count; i++)
                        {
                            inputDataValues[i].Add(inputPropertyName, columnValue[i].ToString() ?? string.Empty);
                        }
                    }
                }
            }

            return inputDataValues;
            //return new List<Dictionary<string, string>>
            //{   
            //    new Dictionary<string, string> { { "I1", "5" }, { "I2", "10" } },
            //    new Dictionary<string, string> { { "I1", "6" }, { "I2", "11" } },
            //    new Dictionary<string, string> { { "I1", "7" }, { "I2", "12" } },
            //    new Dictionary<string, string> { { "I1", "8" }, { "I2", "13" } },
            //    new Dictionary<string, string> { { "I1", "9" }, { "I2", "14" } }
            //};
        }

        private static Dictionary<string, Type> GetOutputProperties()
        {
            return new Dictionary<string, Type> { { "O1", typeof(int) }, { "O2", typeof(int) } };
        }

        private static Dictionary<string, Type> GetInputProperties()
        {
            return new Dictionary<string, Type> { { "I1", typeof(int) }, { "I2", typeof(int) }, { "I3", typeof(float)}, {"I4", typeof(long) }, { "I5", typeof(double)} };
        }

        private static string GetRuleMethodDefination()
        {
            return "res.O1 = ruleInputData.I1 + ruleInputData.I2; res.O2 = ruleInputData.I1 - ruleInputData.I2;";
        }

        private static void PrintCalculationRuleResult(string ruleCode)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(ruleCode);


            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IRule).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(RuleOutputData).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));



            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());
                    Type type = assembly.GetType("RuntimeDemo.CalculationRule");
                    var obj = (IRule)Activator.CreateInstance(type);

                    var csvInputValues = GetCsvInputValues();



                    foreach (var inputData in csvInputValues)
                    {
                       var outputData = obj.ComputeRule(inputData);
                       Console.WriteLine(JsonConvert.SerializeObject(outputData));
                    }
                }
            }
        }
    }
}


