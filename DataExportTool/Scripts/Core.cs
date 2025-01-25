using ExcelDataReader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Data;
using System.Globalization;

public static class Core
{
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

    public static void ExportTable()
    {
        var SettingInfo = User.Instance.GetCurrentSetting();

        var TableList = GetTableList();

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

                    if (Table.IsLocalTable)
                        DataPath = Path.Combine(SettingInfo.ResourcesPath, $"{TableData.TableName}.bytes");
                    else
                        DataPath = Path.Combine(SettingInfo.AddressablePath, $"{TableData.TableName}.bytes");

                    List<List<string>> Rows = new List<List<string>>();

                    for (int Row = 2; Row < TableData.Rows.Count; Row++)
                    {
                        List<string> Columns = new List<string>();
                        for (int Col = 0; Col < TableData.Columns.Count; Col++)
                        {
                            Columns.Add(TableData.Rows[Row][Col].ToString());
                        }

                        Rows.Add(Columns);
                    }

                    var Compress = Util.Compress(Util.ToJson(Rows));
                    var Encrypt = Util.Encrypt(Compress);

                    File.WriteAllBytes(DataPath, Encrypt);
                }
            }
        }
    }

    public static void MakeTable()
    {
        var SettingInfo = User.Instance.GetCurrentSetting();
        var TableList = GetTableList();
        string TablePath = Path.Combine(SettingInfo.TablePath, $"GameTable.cs");

        // 접근한정자 생성
        var PublicToken = SyntaxFactory.Token(SyntaxKind.PublicKeyword);

        // 컴파일 생성
        var CompilationUnit = SyntaxFactory.CompilationUnit();

        // 컴파일에 using 추가
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
        CompilationUnit = CompilationUnit.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")));

        // 클래스 생성
        var GameTableClass = SyntaxFactory.ClassDeclaration("GameTable");
        // 클래스 접근한정자 설정
        GameTableClass = GameTableClass.AddModifiers(PublicToken);

        // 리터럴 값 생성
        var GameTableLiteral = SyntaxFactory.Literal(1);

        // 리터럴 값 추가
        var GameTableLiteralExpression = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, GameTableLiteral);

        // 초기값 추가
        var GameTableEqualsValueClause = SyntaxFactory.EqualsValueClause(GameTableLiteralExpression);

        // 변수 생성
        var GameTableVariableDeclarator = SyntaxFactory.VariableDeclarator("Version");

        // 변수 초기값 설정
        GameTableVariableDeclarator = GameTableVariableDeclarator.WithInitializer(GameTableEqualsValueClause);

        // 변수 타입 선언
        var GameTableVariableDeclaration = SyntaxFactory.VariableDeclaration(SyntaxFactory.ParseTypeName("int"));

        // 변수 타입 설정
        GameTableVariableDeclaration = GameTableVariableDeclaration.AddVariables(GameTableVariableDeclarator);
        
        // 필드 생성
        var GameTableField = SyntaxFactory.FieldDeclaration(GameTableVariableDeclaration);
        GameTableField = GameTableField.AddModifiers(PublicToken);

        // 클래스에 필드추가
        GameTableClass = GameTableClass.AddMembers(GameTableField);

        // 컴파일에 게임테이블 클래스 추가
        CompilationUnit = CompilationUnit.AddMembers(GameTableClass);

        // 이넘 생성
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

                    // 이넘 추가
                    var EnumMemberDeclaration = SyntaxFactory.EnumMemberDeclaration($"{TableData.TableName}");
                    TableEnum = TableEnum.AddMembers(EnumMemberDeclaration);

                    // 클래스 생성
                    var TableClass = SyntaxFactory.ClassDeclaration($"{TableData.TableName}");
                    // 클래스 접근한정자 설정
                    TableClass = TableClass.AddModifiers(PublicToken);
                    // 게임 테이블 상속
                    var BaseClass = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("GameTable"));
                    TableClass = TableClass.AddBaseListTypes(BaseClass);

                    // 타입네임 생성
                    var ParseTypeName = SyntaxFactory.ParseTypeName($"{TableData.TableName}");

                    // 메소드 생성
                    var Method = SyntaxFactory.MethodDeclaration(ParseTypeName, "Parse");

                    // 메소드 매개변수 생성
                    var MethodParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier("Data"));
                    MethodParameter = MethodParameter.WithType(SyntaxFactory.ParseTypeName("List<string>"));

                    // 메소드 매개변수 추가
                    Method = Method.AddParameterListParameters(MethodParameter);
                    Method = Method.AddModifiers(PublicToken);

                    //메소드 내용 생성
                    var MethodBlock = SyntaxFactory.Block();

                    var ObjectCreationExpression = SyntaxFactory.ObjectCreationExpression(ParseTypeName);

                    ObjectCreationExpression = ObjectCreationExpression.WithArgumentList(SyntaxFactory.ArgumentList());

                    // 변수 초기화 생성
                    var EqualsValueClause = SyntaxFactory.EqualsValueClause(ObjectCreationExpression);

                    // 변수 이름 생성
                    var VariableDeclarator = SyntaxFactory.VariableDeclarator($"{TableData.TableName}");
                    // 변수 초기화
                    VariableDeclarator = VariableDeclarator.WithInitializer(EqualsValueClause);

                    // 변수 타입 생성
                    var VariableDeclaration = SyntaxFactory.VariableDeclaration(ParseTypeName);
                    VariableDeclaration = VariableDeclaration.AddVariables(VariableDeclarator);

                    var MethodBlockStatement = SyntaxFactory.LocalDeclarationStatement(VariableDeclaration);
                    MethodBlock = MethodBlock.AddStatements(MethodBlockStatement);
                    for (int Col = 0; Col < TableData.Columns.Count; Col++)
                    {
                        // 변수 타입
                        string Type = GetType($"{TableData.Rows[1][Col]}");
                        var TableType = SyntaxFactory.ParseTypeName(Type);

                        // 변수 생성
                        var TableVariableDeclarator = SyntaxFactory.VariableDeclarator($"{TableData.Rows[0][Col]}");
                        var TableVariableDeclaration = SyntaxFactory.VariableDeclaration(TableType);
                        TableVariableDeclaration = TableVariableDeclaration.AddVariables(TableVariableDeclarator);
                        var TableFieldDeclaration = SyntaxFactory.FieldDeclaration(TableVariableDeclaration);
                        TableClass = TableClass.AddMembers(TableFieldDeclaration);


                        // type name = type.Parse(Data[col]

                        // Col
                        var MethodLiteral = SyntaxFactory.Literal(Col);
                        // [Col]
                        var MethodLiteralExpression = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, MethodLiteral);
                        var MethodArgument = SyntaxFactory.Argument(MethodLiteralExpression);
                        // Data
                        var MethodElementAccessExpression = SyntaxFactory.ElementAccessExpression(SyntaxFactory.IdentifierName("Data"));
                        // Data[Col]
                        MethodElementAccessExpression = MethodElementAccessExpression.AddArgumentListArguments(MethodArgument);
                        var Arg = SyntaxFactory.Argument(MethodElementAccessExpression);

                        // Parse
                        var IdentifierName = SyntaxFactory.IdentifierName("Parse");
                        // TableType.Parse
                        var MemberAccessExpression = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, TableType, IdentifierName);
                        var InvocationExpression = SyntaxFactory.InvocationExpression(MemberAccessExpression);
                        var MethodEqualsValueClause = SyntaxFactory.EqualsValueClause(InvocationExpression);


                        var MethodVariableDeclarator = SyntaxFactory.VariableDeclarator($"{TableData.Rows[0][Col]}");

                        MethodVariableDeclarator = MethodVariableDeclarator.WithInitializer(MethodEqualsValueClause);
                        MethodVariableDeclarator = MethodVariableDeclarator.AddArgumentListArguments(Arg);
                        var MethodVariableDeclaration = SyntaxFactory.VariableDeclaration(TableType);
                        MethodVariableDeclaration = MethodVariableDeclaration.AddVariables(MethodVariableDeclarator);
                        var MethodLocalDeclarationStatement = SyntaxFactory.LocalDeclarationStatement(MethodVariableDeclaration);
                        MethodBlock = MethodBlock.AddStatements(MethodLocalDeclarationStatement);
                    }


                    // 메소드 리턴 생성
                    var Return = SyntaxFactory.ReturnStatement(SyntaxFactory.IdentifierName($"{TableData.TableName}"));
                    // 메소드 리턴 추가
                    MethodBlock = MethodBlock.AddStatements(Return);

                    // 메소드 내용 추가
                    Method = Method.WithBody(MethodBlock);

                    // 클래스에 메소드 추가
                    TableClass = TableClass.AddMembers(Method);

                    CompilationUnit = CompilationUnit.AddMembers(TableEnum);

                    // 컴파일에 클래스 추가
                    CompilationUnit = CompilationUnit.AddMembers(TableClass);
                }
            }
        }

        File.WriteAllText(TablePath, CompilationUnit.NormalizeWhitespace().ToFullString());
    }

    private static string GetType(string Type)
    {
        switch(Type)
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
}
