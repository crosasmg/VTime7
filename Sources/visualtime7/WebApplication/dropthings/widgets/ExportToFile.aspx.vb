#Region "using"

Imports System.Data
Imports GIT.EDW.Query.Model
Imports GIT.EDW.Query.Model.Widget
Imports GIT.Core
Imports GIT.EDW
Imports InMotionGIT.Common.Proxy
Imports System.Globalization
Imports InMotionGIT.Common.Enumerations
Imports InMotionGIT.Common.Helpers
Imports DevExpress.Web.ASPxGridView
Imports System.Threading

#End Region

''' <summary>
''' Export to file class
''' </summary>
''' <remarks></remarks>
Partial Class ExportToFile
    Inherits Page

    Private _InternalRelease As Integer
    Private _InternalModelId As String
    Private _metadata As GIT.EDW.Query.Model.metadata

    Private currentTable As tablequery

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim dataTable As DataTable = Nothing
            _InternalModelId = Request.QueryString("ModelId")
            _InternalRelease = Request.QueryString("Release")

            _metadata = LoadMetadataRepository(Request("ModelId"))

            Dim haveParentNode As Boolean = IIf(String.Equals(Request("haveParentNode"), "True"), True, False)
            Dim isChecked As Boolean = IIf(String.Equals(Request("isChecked"), "True"), True, False)
            Dim language As Integer = IIf(String.Equals(Request("language"), "English"), 1, 2)
            Dim isPlural As Boolean = IIf(String.Equals(Request("isPlural"), "True"), True, False)

            currentTable = GetTableQuery(_metadata, Request("indexExpression"), haveParentNode, Request("parentNodeValue"))

            If Not IsNothing(Session("StoredProcedureParameters")) Then
                Dim parametersList As Dictionary(Of Query.Model.DataType.Parameter, Object) = Session("StoredProcedureParameters")

                With New DataManagerFactory(True, Session("DataQuery"), String.Format(CultureInfo.InvariantCulture, "Linked.{0}", currentTable.Source))
                    For Each parameterData As KeyValuePair(Of Query.Model.DataType.Parameter, Object) In parametersList
                        .AddParameter(parameterData.Key.Name, ParameterTypeConvert(parameterData.Key.Type),
                                      parameterData.Key.Length, False, parameterData.Value)
                    Next

                    .MaxNumberOfRecord = currentTable.MaxNumberOfRecords
                    dataTable = .ProcedureExecuteToTable(True)
                End With

            Else
                With New DataManagerFactory(Session("DataQuery"), String.Format(CultureInfo.InvariantCulture, "Linked.{0}", currentTable.Source))
                    .MaxNumberOfRecord = currentTable.MaxNumberOfRecords
                    dataTable = .QueryExecuteToTable(True)
                End With
            End If

            SetGridViewColumns(grExport, currentTable, isChecked, language, isPlural, dataTable.Rows.Count, Request("schemaLevel"),
                        String.Empty, False, Request("queryID"), Request("enviroment"))

            grExport.DataSource = dataTable
            grExport.DataBind()



            ASPxGridViewExporter.DataBind()

            'Gets the format to export
            Dim formatType As String = Request.QueryString("format")
            Dim fileName As String = String.Format(CultureInfo.InvariantCulture, "{0}_ExportedData", currentTable.name)

            'Verifies the format to export
            Select Case formatType
                Case "xls"
                    ASPxGridViewExporter.WriteXlsToResponse(fileName)

                Case "pdf"
                    ASPxGridViewExporter.WritePdfToResponse(fileName)

                Case "csv"
                    ASPxGridViewExporter.WriteCsvToResponse(fileName)

                Case "rtf"
                    ASPxGridViewExporter.WriteRtfToResponse(fileName)

            End Select

            'Cleans the session var
            Session("DataQuery") = Nothing
        Catch ex As ThreadAbortException
            Session("DataQuery") = Nothing
            'do nothing, ignore
        Catch ex As Exception
            Session("DataQuery") = Nothing
            InMotionGIT.Common.Helpers.LogHandler.ErrorLog("ExportToFile", "Page_Load", ex)
        End Try
    End Sub

    Protected Sub GridViewQueries_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs) Handles grExport.CustomColumnDisplayText
        Dim data As DataTable
        Dim rows() As DataRow
        Dim conditions As String = String.Empty
        Dim codeField As String = String.Empty
        Dim dependencyField As String = String.Empty

        If Not IsNothing(currentTable) Then
            For Each columnData As columnquery In currentTable.columns
                With columnData

                    If Not IsNothing(.Lookup) AndAlso Not IsNothing(.Lookup.QueryTable) Then
                        With .Lookup
                            If Not IsNothing(.Dependency) AndAlso .Dependency.Count > 0 Then

                                If String.Equals(e.Column.FieldName, columnData.RealName, StringComparison.CurrentCultureIgnoreCase) Then

                                    With DirectCast(grExport.Columns(e.Column.FieldName.ToUpper), GridViewDataComboBoxColumn).PropertiesComboBox
                                        If IsNothing(.DataSource) Then
                                            .DataSource = Caching.GetItem(String.Format(CultureInfo.InvariantCulture, "{0}_{1}", _InternalModelId, columnData.name))
                                        End If

                                        data = .DataSource
                                    End With

                                    For Each dependencyData As InMotionGIT.Actions.Designer.DataDependency In .Dependency
                                        With dependencyData
                                            If .CodeField.Contains("@") Then
                                                codeField = .CodeField.Split("@")(1).ToUpper
                                            Else
                                                codeField = .CodeField.Split(".")(1).ToUpper
                                            End If

                                            If conditions.Length > 0 Then
                                                conditions += " AND "
                                            End If

                                            If .ControlName.Contains("@") Then
                                                dependencyField = .ControlName.Split("@")(1).ToUpper
                                            Else
                                                dependencyField = .ControlName.Split(".")(1).ToUpper
                                            End If

                                            conditions += String.Format(CultureInfo.InvariantCulture, "{0} = {1}",
                                                                        codeField, GetCodeFieldDbType(.CodeFieldType, e.GetFieldValue(dependencyField)))
                                        End With
                                    Next

                                    rows = data.Select(String.Format(CultureInfo.InvariantCulture, "{0} AND {1} = {2}", conditions, .Code.ToUpper, e.Value))

                                    If rows.Count > 0 Then
                                        e.DisplayText = rows(0)(.Description(0).Name.ToUpper)
                                    Else
                                        e.DisplayText = String.Empty
                                    End If
                                End If
                            End If
                        End With
                    End If
                End With
            Next
        End If
    End Sub

    Private Sub FindRepositoryMetadata()
        If IsNothing(_metadata) Then
            '_InternalModelId = ModelId
            '_InternalRelease = Release

            If _InternalModelId.IsEmpty Then
                _InternalModelId = Request.QueryString("ModelId")
            End If

            If _InternalRelease.IsEmpty Then
                _InternalRelease = Request.QueryString("Release")
            End If

            If _InternalModelId.IsNotEmpty Then
                _metadata = GIT.EDW.Query.Model.Widget.LoadRepository(_InternalModelId,
                                                                      _InternalRelease,
                                                                     (Request.QueryString("debug") = "y"))
            ElseIf Request.QueryString("Name").IsNotEmpty Then
                _metadata = GIT.EDW.Query.Model.Widget.LoadRepositoryByName(Request.QueryString("Name"),
                                                                            (Request.QueryString("debug") = "y"))
            End If
        End If
    End Sub

    Private Function GetCodeFieldDbType(kind As String, value As Object) As String
        Dim result As String = String.Empty

        Select Case kind
            Case "String", "Char", "DateTime", "Date"
                If DBNull.Value.Equals(value) Then
                    result = String.Format(CultureInfo.InvariantCulture, "'{0}'", String.Empty)
                Else
                    result = String.Format(CultureInfo.InvariantCulture, "'{0}'", value)
                End If

            Case Else '"Integer", "Int16", "Int32", "Int64", "Numeric", "Decimal", "Double", "Boolean"
                If DBNull.Value.Equals(value) Then
                    result = String.Format(CultureInfo.InvariantCulture, "{0}", 0)
                Else
                    result = String.Format(CultureInfo.InvariantCulture, "{0}", value)
                End If
        End Select

        Return result
    End Function



End Class
