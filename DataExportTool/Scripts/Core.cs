using ExcelDataReader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks.Sources;
using System.Windows.Forms.Layout;

public static class Core
{
    // 테이블 최적화
    private static DataTable OptimizeTable(DataTable Table)
    {
        int StartRow = 1;
        int EndRow = -1;
        int StartCol = 1;
        int EndCol = -1;

        for (int Row = 0; Row < Table.Rows.Count; Row++)
        {
            for (int Column = 0; Column < Table.Columns.Count; Column++)
            {
                var CellValue = Table.Rows[Row][Column]?.ToString()?.Trim();

                if (CellValue == "@")
                {
                    if (Row > EndRow)
                        EndRow = Row;

                    if (Column > EndCol)
                        EndCol = Column;
                }
            }
        }

        DataTable extractedData = new DataTable(Table.Rows[StartRow][StartCol].ToString());

        for (int i = StartCol; i < EndCol; i++)
        {
            extractedData.Columns.Add($"{Table.Rows[2][i].ToString()}");
        }

        for (int i = StartRow + 1; i < EndRow; i++)
        {
            DataRow newRow = extractedData.NewRow();
            for (int j = StartCol; j < EndCol; j++)
            {
                newRow[j - 1] = Table.Rows[i][j];
            }
            extractedData.Rows.Add(newRow);
        }

        return extractedData;
    }

    public static List<TableInfo> GetTableList()
    {
        var SettingInfo = User.Instance.GetCurrentSetting();

        List<TableInfo> result = new List<TableInfo>();

        string TablePath = Path.Combine(SettingInfo.ExcelPath, Def.TableListName);

        using (var Stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
        {
            using (var Reader = ExcelReaderFactory.CreateReader(Stream))
            {
                var DataSet = Reader.AsDataSet();

                var TableList = OptimizeTable(DataSet.Tables["TableList"]);

                for (int count = 1; count < TableList.Rows.Count; count++)
                {
                    TableInfo TableInfo = new TableInfo();
                    TableInfo.ExcelName = TableList.Rows[count][0].ToString();
                    TableInfo.SheetName = TableList.Rows[count][1].ToString();
                    result.Add(TableInfo);
                }

                return result;
            }
        }
    }

    public static DataTable GetTable(string TableName, string TableSheetName)
    {
        var SettingInfo = User.Instance.GetCurrentSetting();

        string TablePath = Path.Combine(SettingInfo.ExcelPath, $"{TableName}.xlsx");

        using (var Stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
        {
            using (var Reader = ExcelReaderFactory.CreateReader(Stream))
            {
                var DataSet = Reader.AsDataSet();

                var Table = OptimizeTable(DataSet.Tables[$"{TableSheetName}"]);

                return Table;
            }
        }
    }

    // 테이블 데이터 추출
    public static void ExportTable()
    {
        var SettingInfo = User.Instance.GetCurrentSetting();

        var TableList = GetTableList();

        var VersionCompress = Util.Compress($"1");
        var VersionEncrypt = Util.Encrypt(VersionCompress);
        File.WriteAllBytes(Path.Combine(SettingInfo.ResourcesPath, $"Version.bytes"), VersionEncrypt);

        foreach (var Table in TableList)
        {
            string TablePath = Path.Combine(SettingInfo.ExcelPath, $"{Table.ExcelName}.xlsx");

            using (var Stream = File.Open(TablePath, FileMode.Open, FileAccess.Read))
            {
                string DataPath = string.Empty;

                using (var Reader = ExcelReaderFactory.CreateReader(Stream))
                {
                    var DataSet = Reader.AsDataSet();

                    var TableData = OptimizeTable(DataSet.Tables[Table.SheetName]);
                    
                    DataPath = Path.Combine(SettingInfo.ResourcesPath, $"{TableData.TableName}.bytes");

                    JArray Rows = new JArray();

                    string[] ColumnNames = TableData.Rows[0].ItemArray.Select(col => col.ToString()).ToArray();

                    for (int i = 2; i < TableData.Rows.Count; i++)
                    {
                        DataRow row = TableData.Rows[i];

                        JObject jsonRow = new JObject();
                        for (int j = 0; j < ColumnNames.Length; j++)
                        {
                            jsonRow[ColumnNames[j]] = JToken.FromObject(row.ItemArray[j]);
                        }

                        Rows.Add(jsonRow);
                    }

                    var Compress = Util.Compress(Rows.ToString());
                    var Encrypt = Util.Encrypt(Compress);

                    File.WriteAllBytes(DataPath, Encrypt);
                }
            }
        }
    }

    // 테이블 CS파일 추출
    /* 현제 추출 예시
    using System;
    using System.Collections.Generic;

    public class GameTable
    {
        public int Version;
    }

    public class TestTable : GameTable
    {
        int Key { get; private set };
        int intValue;
        float floatValue;
        string stringValue;
        long longValue;
        double doubleValue;

        public TestTable Parse(List<string> Data)
        {
            TestTable TestTable = new TestTable();
            int Key = int.Parse(Data[0]);
            int intValue = int.Parse(Data[1]);
            float floatValue = float.Parse(Data[2]);
            string stringValue = Data[3];
            long longValue = long.Parse(Data[4]);
            double doubleValue = double.Parse(Data[5]);
            return TestTable;
        }
    }

    public enum TableType
    {
        Start,
        TestTable,
        End
    }
    */
    public static void MakeTable()
    {
        var SettingInfo = User.Instance.GetCurrentSetting();
        var TableList = GetTableList();
        string TablePath = Path.Combine(SettingInfo.TablePath, $"GameTable.cs");

        // Create CompilationUnit
        var CompilationUnit = SyntaxFactory.CompilationUnit();

        // Added Using to CompilationUnit
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")));
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Newtonsoft.Json.Linq")));
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Newtonsoft.Json")));
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Newtonsoft.Json.Serialization")));
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Reflection")));

        // Creeate NonPublicDefaultContractResolver
        var ResolverClassDeclaration = SyntaxFactory.ClassDeclaration("NonPublicDefaultContractResolver")
    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
    .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("DefaultContractResolver")))
    .AddMembers(
        SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("JsonProperty"), "CreateProperty")
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword), SyntaxFactory.Token(SyntaxKind.OverrideKeyword))
            .AddParameterListParameters(
                SyntaxFactory.Parameter(SyntaxFactory.Identifier("member"))
                    .WithType(SyntaxFactory.ParseTypeName("MemberInfo")),
                SyntaxFactory.Parameter(SyntaxFactory.Identifier("memberSerialization"))
                    .WithType(SyntaxFactory.ParseTypeName("MemberSerialization"))
            )
            .WithBody(
                SyntaxFactory.Block(
                    SyntaxFactory.LocalDeclarationStatement(
                        SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("var"))
                            .AddVariables(
                                SyntaxFactory.VariableDeclarator("property")
                                    .WithInitializer(
                                        SyntaxFactory.EqualsValueClause(
                                            SyntaxFactory.InvocationExpression(
                                                SyntaxFactory.MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    SyntaxFactory.BaseExpression(),
                                                    SyntaxFactory.IdentifierName("CreateProperty")
                                                )
                                            ).WithArgumentList(
                                                SyntaxFactory.ArgumentList(
                                                    SyntaxFactory.SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[] {
                                                        SyntaxFactory.Argument(SyntaxFactory.IdentifierName("member")),
                                                        SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                        SyntaxFactory.Argument(SyntaxFactory.IdentifierName("memberSerialization"))
                                                    })
                                                )
                                            )
                                        )
                                    )
                            )
                    ),
                    SyntaxFactory.IfStatement(
                        SyntaxFactory.PrefixUnaryExpression(
                            SyntaxKind.LogicalNotExpression,
                            SyntaxFactory.MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                SyntaxFactory.IdentifierName("property"),
                                SyntaxFactory.IdentifierName("Writable")
                            )
                        ),
                        SyntaxFactory.Block(
                            SyntaxFactory.LocalDeclarationStatement(
                                SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("var"))
                                    .AddVariables(
                                        SyntaxFactory.VariableDeclarator("propertyInfo")
                                            .WithInitializer(
                                                SyntaxFactory.EqualsValueClause(
                                                    SyntaxFactory.BinaryExpression(
                                                        SyntaxKind.AsExpression,
                                                        SyntaxFactory.IdentifierName("member"),
                                                        SyntaxFactory.ParseTypeName("PropertyInfo")
                                                    )
                                                )
                                            )
                                    )
                            ),
                            SyntaxFactory.IfStatement(
                                SyntaxFactory.BinaryExpression(
                                    SyntaxKind.NotEqualsExpression,
                                    SyntaxFactory.IdentifierName("propertyInfo"),
                                    SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)
                                ),
                                SyntaxFactory.Block(
                                    SyntaxFactory.LocalDeclarationStatement(
                                        SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("var"))
                                            .AddVariables(
                                                SyntaxFactory.VariableDeclarator("setter")
                                                    .WithInitializer(
                                                        SyntaxFactory.EqualsValueClause(
                                                            SyntaxFactory.InvocationExpression(
                                                                SyntaxFactory.MemberAccessExpression(
                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                    SyntaxFactory.IdentifierName("propertyInfo"),
                                                                    SyntaxFactory.IdentifierName("GetSetMethod")
                                                                )
                                                            ).WithArgumentList(
                                                                SyntaxFactory.ArgumentList(
                                                                    SyntaxFactory.SingletonSeparatedList(
                                                                        SyntaxFactory.Argument(
                                                                            SyntaxFactory.LiteralExpression(
                                                                                SyntaxKind.TrueLiteralExpression
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                            )
                                    ),
                                    SyntaxFactory.IfStatement(
                                        SyntaxFactory.BinaryExpression(
                                            SyntaxKind.NotEqualsExpression,
                                            SyntaxFactory.IdentifierName("setter"),
                                            SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression)
                                        ),
                                        SyntaxFactory.Block(
                                            SyntaxFactory.ExpressionStatement(
                                                SyntaxFactory.AssignmentExpression(
                                                    SyntaxKind.SimpleAssignmentExpression,
                                                    SyntaxFactory.MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        SyntaxFactory.IdentifierName("property"),
                                                        SyntaxFactory.IdentifierName("Writable")
                                                    ),
                                                    SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    SyntaxFactory.ReturnStatement(SyntaxFactory.IdentifierName("property"))
                )
            )
    );

        CompilationUnit = CompilationUnit.AddMembers(ResolverClassDeclaration);

        // Create GameTable
        var GameTableClass = SyntaxFactory.ClassDeclaration("GameTable");
        GameTableClass = GameTableClass.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

        //  Create Method Parameter
        var MethodParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier("Obj"));
        MethodParameter = MethodParameter.WithType(SyntaxFactory.ParseTypeName("JArray"));

        // Create GameTable Method
        var Method = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("Dictionary<TableType, object>"), "Parse");
        Method = Method.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword));
        Method = Method.AddParameterListParameters(MethodParameter);

        // Create Method Dictionary for Return Dictionary
        var ResultGenericName = SyntaxFactory.GenericName(SyntaxFactory.Identifier("Dictionary"));
        ResultGenericName = ResultGenericName.AddTypeArgumentListArguments(SyntaxFactory.ParseTypeName("TableType"));
        ResultGenericName = ResultGenericName.AddTypeArgumentListArguments(SyntaxFactory.ParseTypeName("object"));

        var ResultObjectCreationExpression = SyntaxFactory.ObjectCreationExpression(ResultGenericName);
        ResultObjectCreationExpression = ResultObjectCreationExpression.WithArgumentList(SyntaxFactory.ArgumentList());

        var ResultEqualsValueClause = SyntaxFactory.EqualsValueClause(ResultObjectCreationExpression);

        var ResultVariableDeclarator = SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("Result"));
        ResultVariableDeclarator = ResultVariableDeclarator.WithInitializer(ResultEqualsValueClause);

        var ResultVariableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("Dictionary<TableType, object>"));
        ResultVariableDeclaration = ResultVariableDeclaration.AddVariables(ResultVariableDeclarator);

        var ResultLocalDeclarationStatement = SyntaxFactory.LocalDeclarationStatement(ResultVariableDeclaration);

        var settingsVariableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("var"))
    .AddVariables(
        SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("settings"))
        .WithInitializer(
            SyntaxFactory.EqualsValueClause(
                SyntaxFactory.ObjectCreationExpression(SyntaxFactory.ParseTypeName("JsonSerializerSettings"))
                .WithInitializer(
                    SyntaxFactory.InitializerExpression(
                        SyntaxKind.ObjectInitializerExpression,
                        SyntaxFactory.SeparatedList<ExpressionSyntax>(new SyntaxNodeOrToken[] {
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("ContractResolver"),
                                SyntaxFactory.ObjectCreationExpression(
                                    SyntaxFactory.ParseTypeName("NonPublicDefaultContractResolver")
                                ).WithArgumentList(SyntaxFactory.ArgumentList())
                            ),
                            SyntaxFactory.Token(SyntaxKind.CommaToken),
                            SyntaxFactory.AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                SyntaxFactory.IdentifierName("Formatting"),
                                SyntaxFactory.MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    SyntaxFactory.IdentifierName("Formatting"),
                                    SyntaxFactory.IdentifierName("Indented")
                                )
                            )
                        })
                    )
                )
            )
        )
    );
        var settingsLocalDeclaration = SyntaxFactory.LocalDeclarationStatement(settingsVariableDeclaration);

        // Create For Loop
        var ForLoopPrefixUnaryExpression = SyntaxFactory.PrefixUnaryExpression(SyntaxKind.PreIncrementExpression,SyntaxFactory.IdentifierName("Count"));

        var ForLoopSingletonSeparatedList = SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(ForLoopPrefixUnaryExpression);

        var ForLoopMemberAccessExpression = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("Obj"), SyntaxFactory.IdentifierName("Count"));

        var ForLoopBinaryExpression = SyntaxFactory.BinaryExpression(SyntaxKind.LessThanExpression, SyntaxFactory.IdentifierName("Count"), ForLoopMemberAccessExpression);

        var ForLoopLiteralExpression = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0));

        var ForLoopEqualsValueClause = SyntaxFactory.EqualsValueClause(ForLoopLiteralExpression);

        var ForLoopVariableDeclarator = SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("Count"));
        ForLoopVariableDeclarator = ForLoopVariableDeclarator.WithInitializer(ForLoopEqualsValueClause);

        var ForLoopVariableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("int"));
        ForLoopVariableDeclaration = ForLoopVariableDeclaration.AddVariables(ForLoopVariableDeclarator);
        
        var ForLoopBlock = SyntaxFactory.Block();
        var ForLoop = SyntaxFactory.ForStatement(ForLoopBlock);
        ForLoop = ForLoop.WithDeclaration(ForLoopVariableDeclaration);
        ForLoop = ForLoop.WithCondition(ForLoopBinaryExpression);
        ForLoop = ForLoop.WithIncrementors(ForLoopSingletonSeparatedList);

        // Create CurTableName
        var CurTableNameGenericName = SyntaxFactory.GenericName(SyntaxFactory.Identifier("Value"));
        CurTableNameGenericName = CurTableNameGenericName.AddTypeArgumentListArguments(SyntaxFactory.ParseTypeName("string"));

        var CurTableNameArgument = SyntaxFactory.Argument(CurTableNameGenericName);  

        var CurTableNameSingletonSeparatedList = SyntaxFactory.SingletonSeparatedList(CurTableNameArgument);

        var CurTableNameArgumentList = SyntaxFactory.ArgumentList(CurTableNameSingletonSeparatedList);

        var CurTableNameKeyLiteralExpression = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("Key"));
        var CurTableNameInArrayElementAccessExpression = SyntaxFactory.ElementAccessExpression(SyntaxFactory.IdentifierName("Obj"));
        CurTableNameInArrayElementAccessExpression = CurTableNameInArrayElementAccessExpression.AddArgumentListArguments(SyntaxFactory.Argument(SyntaxFactory.IdentifierName("Count")));
        var CurTableNameElementAccessExpression = SyntaxFactory.ElementAccessExpression(CurTableNameInArrayElementAccessExpression);
        CurTableNameElementAccessExpression = CurTableNameElementAccessExpression.AddArgumentListArguments(SyntaxFactory.Argument(CurTableNameKeyLiteralExpression));

        var CurTableNameMemberAccessExpression = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, CurTableNameElementAccessExpression, CurTableNameGenericName);

        var CurTableNameInvocationExpression = SyntaxFactory.InvocationExpression(CurTableNameMemberAccessExpression);

        var CurTableNameEqualsValueClause = SyntaxFactory.EqualsValueClause(CurTableNameInvocationExpression);

        var CurTableNameVariableDeclarator = SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("CurTableName"));
        CurTableNameVariableDeclarator = CurTableNameVariableDeclarator.WithInitializer(CurTableNameEqualsValueClause);

        var CurTableNameVariableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("var"));
        CurTableNameVariableDeclaration = CurTableNameVariableDeclaration.AddVariables(CurTableNameVariableDeclarator);

        var CurTableNameLocalDeclarationStatement = SyntaxFactory.LocalDeclarationStatement(CurTableNameVariableDeclaration);

        // Create CurTableData
        var CurTableDataGenericName = SyntaxFactory.GenericName(SyntaxFactory.Identifier("Value"));
        CurTableDataGenericName = CurTableDataGenericName.AddTypeArgumentListArguments(SyntaxFactory.ParseTypeName("string"));

        var CurTableDataArgument = SyntaxFactory.Argument(CurTableDataGenericName);

        var CurTableDataSingletonSeparatedList = SyntaxFactory.SingletonSeparatedList(CurTableDataArgument);

        var CurTableDataArgumentList = SyntaxFactory.ArgumentList(CurTableDataSingletonSeparatedList);

        var CurTableDataKeyLiteralExpression = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("Value"));
        var CurTableDataInArrayElementAccessExpression = SyntaxFactory.ElementAccessExpression(SyntaxFactory.IdentifierName("Obj"));
        CurTableDataInArrayElementAccessExpression = CurTableDataInArrayElementAccessExpression.AddArgumentListArguments(SyntaxFactory.Argument(SyntaxFactory.IdentifierName("Count")));
        var CurTableDataElementAccessExpression = SyntaxFactory.ElementAccessExpression(CurTableDataInArrayElementAccessExpression);
        CurTableDataElementAccessExpression = CurTableDataElementAccessExpression.AddArgumentListArguments(SyntaxFactory.Argument(CurTableDataKeyLiteralExpression));

        var CurTableDataMemberAccessExpression = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, CurTableDataElementAccessExpression, CurTableDataGenericName);

        var CurTableDataInvocationExpression = SyntaxFactory.InvocationExpression(CurTableDataMemberAccessExpression);

        var CurTableDataEqualsValueClause = SyntaxFactory.EqualsValueClause(CurTableDataInvocationExpression);

        var CurTableDataVariableDeclarator = SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier("CurTableData"));
        CurTableDataVariableDeclarator = CurTableDataVariableDeclarator.WithInitializer(CurTableDataEqualsValueClause);

        var CurTableDataVariableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("var"));
        CurTableDataVariableDeclaration = CurTableDataVariableDeclaration.AddVariables(CurTableDataVariableDeclarator);

        var CurTableDataLocalDeclarationStatement = SyntaxFactory.LocalDeclarationStatement(CurTableDataVariableDeclaration);

        // Create Switch Statement
        var SwitchStatement = SyntaxFactory.SwitchStatement(SyntaxFactory.IdentifierName("CurTableName"));

        // Create Switch Sections
        var SwitchSections = new List<SwitchSectionSyntax>();

        // Create TableEnum
        var TableEnum = SyntaxFactory.EnumDeclaration("TableType").AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

        foreach (var Table in TableList)
        {
            string ExcelPath = Path.Combine(SettingInfo.ExcelPath, $"{Table.ExcelName}.xlsx");

            using (var Stream = File.Open(ExcelPath, FileMode.Open, FileAccess.Read))
            {
                using (var Reader = ExcelReaderFactory.CreateReader(Stream))
                {
                    var DataSet = Reader.AsDataSet();
                    var TableData = OptimizeTable(DataSet.Tables[Table.SheetName]);

                    // Added Enum Member to TableEnum
                    var EnumMemberDeclaration = SyntaxFactory.EnumMemberDeclaration($"{TableData.TableName}");
                    TableEnum = TableEnum.AddMembers(EnumMemberDeclaration);

                    // Create Switch Case
                    var CaseLabelLiteralExpression = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal($"{TableData.TableName}"));
                    var CaseLabel = SyntaxFactory.CaseSwitchLabel(CaseLabelLiteralExpression);

                    var CaseMemberAccessExpression = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("TableType"), SyntaxFactory.IdentifierName($"{TableData.TableName}"));
                    var ResultMemberAccess = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("Result"), SyntaxFactory.IdentifierName("Add"));

                    var CurTableArgument = SyntaxFactory.Argument(CaseMemberAccessExpression);

                    var ConvertArgumentGenericName = SyntaxFactory.GenericName("DeserializeObject");
                    ConvertArgumentGenericName = ConvertArgumentGenericName.AddTypeArgumentListArguments(SyntaxFactory.ParseTypeName($"List<{TableData.TableName}>"));
                    var jsonConvertMemberAccess = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("JsonConvert"), ConvertArgumentGenericName);
                    
                    var curTableDataArgument = SyntaxFactory.Argument(SyntaxFactory.IdentifierName("CurTableData"));
                    var SettingArgument = SyntaxFactory.Argument(SyntaxFactory.IdentifierName("settings"));
                    var deserializeObjectInvocation = SyntaxFactory.InvocationExpression(jsonConvertMemberAccess);
                    deserializeObjectInvocation = deserializeObjectInvocation.AddArgumentListArguments(curTableDataArgument, SettingArgument);

                    var addInvocation = SyntaxFactory.InvocationExpression(ResultMemberAccess);
                    addInvocation = addInvocation.AddArgumentListArguments(CurTableArgument, SyntaxFactory.Argument(deserializeObjectInvocation));

                    var expressionStatement = SyntaxFactory.ExpressionStatement(addInvocation);
                    var breakStatement = SyntaxFactory.BreakStatement();

                    var statements = new List<StatementSyntax> { expressionStatement, breakStatement };

                    var switchSection = SyntaxFactory.SwitchSection();
                    switchSection = switchSection.AddLabels(CaseLabel);
                    switchSection = switchSection.AddStatements(statements.ToArray());

                    SwitchSections.Add(switchSection);

                    // Create TableClass
                    var TableClass = SyntaxFactory.ClassDeclaration($"{TableData.TableName}");
                    TableClass = TableClass.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

                    for (int Col = 0; Col < TableData.Columns.Count; Col++)
                    {
                        string Type = GetType($"{TableData.Rows[1][Col]}");
                        var TableType = SyntaxFactory.ParseTypeName(Type);

                        // PropertyInfo
                        var PropertyKeyword = SyntaxFactory.Token(GetPredefinedType($"{TableData.Rows[1][Col]}"));
                        var PropertyType = SyntaxFactory.PredefinedType(PropertyKeyword);
                        var PropertyName = SyntaxFactory.Identifier($"{TableData.Rows[0][Col]}");
                        // Get Accessor
                        var GetAccessor = SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration);
                        GetAccessor = GetAccessor.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
                        // Set Accessor
                        var SetAccessor = SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration);
                        SetAccessor = SetAccessor.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
                        SetAccessor = SetAccessor.AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
                        // Setting AssessorList
                        var List = SyntaxFactory.List(new[] { GetAccessor, SetAccessor });
                        var AssessorList = SyntaxFactory.AccessorList(List);
                        // Setting Property
                        var PropertyDeclaration = SyntaxFactory.PropertyDeclaration(PropertyType, PropertyName);
                        PropertyDeclaration = PropertyDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                        PropertyDeclaration = PropertyDeclaration.WithAccessorList(AssessorList);
                        // Added Member Property to TableClass
                        TableClass = TableClass.AddMembers(PropertyDeclaration);
                    }
                    // Add TableClass at CopilationUnit
                    CompilationUnit = CompilationUnit.AddMembers(TableClass);
                }
            }
        }

        // Add Default Case
        var defaultLabel = SyntaxFactory.DefaultSwitchLabel();
        var defaultThrowExpression = SyntaxFactory.ObjectCreationExpression(SyntaxFactory.ParseTypeName("ArgumentException"));
        var defaultThrowArgument = SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("Invalid TableType")));
        var defaultThrowArgumentList = SyntaxFactory.ArgumentList(SyntaxFactory.SingletonSeparatedList(defaultThrowArgument));
        defaultThrowExpression = defaultThrowExpression.WithArgumentList(defaultThrowArgumentList);
        var defaultThrowStatement = SyntaxFactory.ThrowStatement(defaultThrowExpression);

        var defaultSection = SyntaxFactory.SwitchSection();
        defaultSection = defaultSection.AddLabels(defaultLabel);
        defaultSection = defaultSection.AddStatements(defaultThrowStatement);

        SwitchSections.Add(defaultSection);

        // Add Switch Sections to Switch Statement
        SwitchStatement = SwitchStatement.AddSections(SwitchSections.ToArray());

        // Add Statements to For Loop
        ForLoopBlock = ForLoopBlock.AddStatements(CurTableNameLocalDeclarationStatement);
        ForLoopBlock = ForLoopBlock.AddStatements(CurTableDataLocalDeclarationStatement);
        ForLoopBlock = ForLoopBlock.AddStatements(SwitchStatement);


        ForLoop = ForLoop.WithStatement(ForLoopBlock);

        // Add Statements to Method
        Method = Method.AddBodyStatements(ResultLocalDeclarationStatement);
        Method = Method.AddBodyStatements(settingsLocalDeclaration);
        Method = Method.AddBodyStatements(ForLoop);
        Method = Method.AddBodyStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.IdentifierName("Result")));

        GameTableClass = GameTableClass.AddMembers(Method);

        // Added GameTable to CopilationUnit
        CompilationUnit = CompilationUnit.AddMembers(GameTableClass);

        var EndEnumMemberDeclaration = SyntaxFactory.EnumMemberDeclaration($"End");
        TableEnum = TableEnum.AddMembers(EndEnumMemberDeclaration);

        CompilationUnit = CompilationUnit.AddMembers(TableEnum);

        File.WriteAllText(TablePath, CompilationUnit.NormalizeWhitespace().ToFullString());
    }

    private static string GetType(string Type)
    {
        switch (Type)
        {
            case "I4":
                return "int";
            case "I8":
                return "long";
            case "F4":
                return "float";
            case "F8":
                return "double";
            case "STR":
                return "string";
            default:
                return string.Empty;
        }
    }

    private static SyntaxKind GetPredefinedType(string Type)
    {
        switch (Type)
        {
            case "I4":
                return SyntaxKind.IntKeyword;
            case "I8":
                return SyntaxKind.LongKeyword;
            case "F4":
                return SyntaxKind.FloatKeyword;
            case "F8":
                return SyntaxKind.DoubleKeyword;
            case "STR":
                return SyntaxKind.StringKeyword;
            default:
                return SyntaxKind.None;
        }
    }
}
