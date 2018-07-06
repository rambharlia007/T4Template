using ExcelDataReader;
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
        public string RegulationDefinition { get; set; }
        public Dictionary<string, Type> InputProperties { get; set; }
        public Dictionary<string, Type> OutputProperties { get; set; }
        public Dictionary<string, Type> WorkingProperties { get; set; }

        public string ConditionDefinition { get; set; }

        public RuntimeTextTemplate1(string regulationDefinition, string conditionDefinition, Dictionary<string, Type> inputProperties,
            Dictionary<string, Type> outputProperties, Dictionary<string, Type> workingProperties)
        {
            RegulationDefinition = regulationDefinition;
            ConditionDefinition = conditionDefinition;
            InputProperties = inputProperties;
            OutputProperties = outputProperties;
            WorkingProperties = workingProperties;
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


    public class Excel
    {
        public List<Dictionary<string, string>> InputData { get; set; }
        public List<List<ExcelOutputData>> OutputData { get; set; }
        public SummaryData SummaryData { get; set; }
    }

    public class SummaryData
    {
        public string Date { get { return DateTime.UtcNow.ToString("dd-MM-yyyy"); } }
        public string Time { get { return DateTime.UtcNow.ToString("h:mm tt"); } }
        public int Regulation { get; set; }
        public int Sequence { get; set; }
        public int Total { get; set; }
        public int Passed { get; set; }
        public int Failed { get { return Total - Passed; } }
        public string Result {
            get
            {
                if (Total == Passed) return "Pass";
                else return "Fail";
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

            var inputCsvData = GetCsvInputValues();
            var columns = inputCsvData.FirstOrDefault().Select(e => e.Key).ToList();
            var randomOuputData = GetRandomOutputData(columns);


            var x = randomOuputData.Select(e => e.Count);

            var summaryData = new SummaryData
            {
                Total = randomOuputData.Sum(e => e.Count),
                Passed = randomOuputData.Sum(e => e.Count(f => f.IsTestCasePass)),
                Regulation = 151651,
                Sequence = 17
            };

            var excelResult = new Excel
            {
                InputData = inputCsvData,
                OutputData = randomOuputData,
                SummaryData = summaryData
            };


            //var csvInputValues = GetCsvInputValues();
            //var T4Template = new RuleTextTemplate(ruleMethodDefination, inputProperties, outputProperties, csvInputValues);
            //var ruleCode = T4Template.TransformText();


            var T4Template1 = new RuntimeTextTemplate1(ruleMethodDefination,"" , inputProperties, outputProperties, new Dictionary<string, Type> { { "W1", typeof(int) } });
            var ruleCode1 = T4Template1.TransformText();


            PrintCalculationRuleResult(ruleCode1);


            WriteExcel.WriteExcelFile(excelResult);



        }

        private static List<List<ExcelOutputData>> GetRandomOutputData(List<string> columns)
        {
            var outputData = new List<List<ExcelOutputData>>();
            var random = new Random();

            for (int i = 0; i < 9; i++)
            {
                var dat = new List<ExcelOutputData>();
                foreach (var name in columns)
                {
                    var num = random.Next(1000, 10000).ToString();
                    dat.Add(new ExcelOutputData()
                    {
                        Name = name.ToString(),
                        ActualValue = num,
                        ExpectedValue = i % 2 == 0 ? num : num + 1
                    });
                }
                outputData.Add(dat);
            }
            return outputData;
        }

        private static List<Dictionary<string, string>> GetCsvInputValues()
        {
            var inputDataValues = new List<Dictionary<string, string>>();
            using (var stream = File.Open(@"D:\10312819_1.xlsx", FileMode.Open, FileAccess.Read))
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
        }

        private static Dictionary<string, Type> GetOutputProperties()
        {
            // return new Dictionary<string, Type> { { "O1", typeof(int) }, { "O2", typeof(int) } };
            return new Dictionary<string, Type> {
                                                    {"Aard_arbeidsverh", typeof(string) },
                                                    {"Dag_bedrag", typeof(string) },
                                                    {"Einddatum_afrper", typeof(string) },
                                                    {"Kolomgrondslag_WAOb", typeof(string) },
                                                    {"Kolomgrondslag_WW", typeof(string) },
                                                    {"Kolomgrondslag_ZVW", typeof(string) },
                                                    {"Kwartaal_bedrag", typeof(string) },
                                                    {"Leeftijd", typeof(string) },
                                                    {"Maand_bedrag", typeof(string) },
                                                    {"MutatieOK_Invlverzp", typeof(string) },
                                                    {"MutatieOK_VerzsitZVW", typeof(string) },
                                                    {"Uitz_WAOb", typeof(string) },
                                                    {"Uitz_WW", typeof(string) },
                                                    {"Uitz_ZVW", typeof(string) },
                                                    {"Verz_situatie_ZVW", typeof(string) },
                                                    {"Vierweken_bedrag", typeof(string) },
                                                    {"Voorselectie", typeof(string) },
                                                    {"Week_bedrag", typeof(string) }
            };
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
                    Type type = assembly.GetType("Regulations_UnitTestRunner.CalculationRule");
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


