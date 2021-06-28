#Region "Copyright (c) 2007, Global Insurance Technology, Inc."
#End Region

#Region "Imports directives"

Imports System
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports InMotionGIT.Common.Exceptions

#End Region

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _

Namespace GIT.Web.Helpers

    <System.Web.Services.WebService(Namespace:="http://tempuri.org/", Name:="Global Insurance Technology, Inc.")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class ListValues
        Inherits System.Web.Services.WebService

#Region ".Public Web Methods"

        ''' <summary>
        ''' Devuelve información de ciertas tablas a escoger
        ''' </summary>
        ''' <param name="tableList">Parámetro para indicar la(s) tabla(s) del cual se desea obtener información </param>
        ''' <returns>Retorna un DataSet con la información de las tablas seleccionadas</returns>
        <WebMethod(Description:="Devuelve los datos de los campos ID y Descripción de ciertas tablas a escoger")> _
        Public Function ListValues(ByVal Source As String, ByVal description As String, ByVal tableList As String, ByVal LanguageID As String) As DataSet
            Dim ConnectionStringName As String = String.Format("Linked.{0}", Source)
            Dim provider As String = ConfigurationManager.ConnectionStrings(ConnectionStringName).ProviderName
            Dim connectionString As String = ConfigurationManager.ConnectionStrings(ConnectionStringName).ConnectionString
            Dim dbpf As DbProviderFactory = DbProviderFactories.GetFactory(provider)

            Dim dbcon As DbConnection = dbpf.CreateConnection()
            Dim dbcmd As DbCommand

            Dim vloDataSet As New DataSet
            Dim vlnCount As Integer
            Dim vlcTablas As String()
            Dim vloDataTable As DataTable
            Dim vlcOwner As String = String.Empty
            Dim TableKeyField As String = String.Empty
            Dim TableTrans As String = String.Empty
            Try

                dbcon.ConnectionString = connectionString

                vlcTablas = tableList.Split(",")

                ListValues = New DataSet

                dbcon.Open()

                For vlnCount = 0 To vlcTablas.Length - 1
                    vloDataTable = New DataTable
                    dbcmd = dbpf.CreateCommand()
                    dbcmd.Connection = dbcon

                    TableKeyField = GetTablePrimaryKeyField(vlcOwner + vlcTablas(vlnCount), dbcon)

                    Dim vloDataAdapter As DbDataAdapter = dbpf.CreateDataAdapter()

                    'If "VisualTimeDB" = "VisualTimeDB" Then 'TODO: Depende de los settings
                    dbcmd.CommandText = "SELECT keyTable." + TableKeyField + " ID, " + description + " sDescript" + _
                                                                      " FROM " + vlcOwner + vlcTablas(vlnCount) + " keyTable " '+ _
                    '" WHERE keyTable.sStatRegt='1'"
                    'Else
                    'dbcmd.CommandText = "SELECT keyTable." + TableKeyField + " as ID, descTable.Description AS Description" + _
                    '                    " FROM " + vlcOwner + vlcTablas(vlnCount) + " AS keyTable " + _
                    '                    "JOIN " + TableTrans + " AS descTable " + _
                    '                    "ON keyTable." + TableKeyField + " = descTable." + TableKeyField + _
                    '                    " WHERE keyTable.RecordStatus='1' and descTable.LanguageID= " + LanguageID
                    'End If

                    vloDataAdapter.SelectCommand = dbcmd

                    vloDataAdapter.Fill(vloDataTable)

                    vloDataTable.TableName = vlcTablas(vlnCount)

                    ListValues.Tables.Add(vloDataTable)
                Next

                dbcon.Close()

                Return ListValues

            Catch ex As SqlException

                Throw New Exception(ex.Message)

            End Try

            'Dim vloCommand As New System.Data.OleDb.OleDbCommand("SELECT keyTable." + TableKeyField + " as ID, descTable.Description AS Description" + _
            '                                                      " FROM " + vlcOwner + vlcTablas(vlnCount) + " AS keyTable " + _
            '                                                      "JOIN " + TableTrans + " AS descTable " + _
            '                                                      "ON keyTable." + TableKeyField + " = descTable." + TableKeyField + _
            '                                                     " WHERE keyTable.RecordStatus='1' and descTable.LanguageID= " + LanguageID, vcoConnection)

        End Function




        ''' <summary>
        ''' Devuelve el string de conexion brindado en los settings
        ''' </summary>
        ''' <returns></returns>
        <WebMethod(Description:="Devuelve el string de conexion brindado en los settings")> _
        Public Function CurrentConnetionString(ByVal Source As String) As String
            Dim ConnectionStringName As String = String.Format("Linked.{0}", Source)
            Return System.Configuration.ConfigurationManager.ConnectionStrings(ConnectionStringName).ConnectionString
        End Function


#End Region
#Region ".Private Methods"

        ''' <summary>
        ''' Initialize
        ''' </summary>
        Private Sub InitializeComponent()

        End Sub
        ''' <summary>
        ''' Función que obtiene el nombre del campo de la llave primaria
        ''' </summary>
        ''' <param name="tableName">Tabla a consultar</param>
        ''' <returns>Retorna el nombre de la columna de la llave principal</returns>
        Private Function GetTablePrimaryKeyField(ByVal tableName As String, ByVal dbcon As DbConnection) As String
            Dim vloRecordItem As DbDataReader
            Dim vlcField As String = String.Empty
            ' Se selecciona la estructura de la tabla
            Dim vloCommand As DbCommand = dbcon.CreateCommand()
            Try
                vloCommand.CommandText = "SELECT *" + _
                                         " FROM " + tableName + _
                                         " WHERE 1=2"

                ' Se limpia la variable a retornar
                GetTablePrimaryKeyField = String.Empty

                ' Se ejecuta la consulta
                vloRecordItem = vloCommand.ExecuteReader()

                With vloRecordItem
                    ' Se itera por todas las columnas
                    For vlnIndex As Integer = 0 To .FieldCount - 1
                        ' Se cambia el nombre del campo a minúscula
                        vlcField = .GetName(vlnIndex).ToLower

                        ' Si el nombre del campo no se encuentra en los siguientes...
                        If "dCompDate|sDescript|sShort_des|sStatRegt|nUserCode".IndexOf(vlcField) < 0 Then
                            ' Se guarda el nombre del campo
                            GetTablePrimaryKeyField = .GetName(vlnIndex)
                            Exit For
                        End If
                    Next vlnIndex
                End With

                vloRecordItem.Close()
                ' Se retorna el nombre del campo que contiene la llave primaria
                Return GetTablePrimaryKeyField

            Catch ex As SqlException
                If (ex.Number = 156) Then
                    Throw New InMotionGITException("You should to fill the field of the table name!" + vbCrLf + vbCrLf, ex)
                ElseIf (ex.Number = 102) Then
                    Throw New InMotionGITException("The table " + tableName + " not exists." + vbCrLf + vbCrLf, ex)
                ElseIf (ex.Number = 208) Then
                    Throw New InMotionGITException("The table " + tableName + " not exists." + vbCrLf + vbCrLf, ex)
                Else
                    Throw New Exception(ex.Message)
                End If
            End Try

        End Function

#End Region

    End Class

End Namespace