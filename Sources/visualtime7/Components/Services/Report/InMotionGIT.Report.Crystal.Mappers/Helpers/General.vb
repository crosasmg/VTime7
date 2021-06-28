Imports System.Xml.Serialization
Imports System.IO
Imports InMotionGIT.Common.Extensions
Imports InMotionGIT.Common.Helpers
Imports System.Data.Common
Imports System.Data
Imports InMotionGIT.Report.Crystal.Designs.ReportCargo

Public Class General

    Public Shared Function LoadCargo(name As String, instance As Object) As Cargo
        Dim cargo As New Cargo
        Dim xml As String = New General().SerializeReportContract(instance)
        With cargo
            .Name = name
            .XMLContract = xml
        End With
        Return cargo
    End Function

    Public Shared Function LoadEmptyCargo(name As String) As Cargo
        Dim cargo As New Cargo
        With cargo
            .Name = name
            .XMLContract = String.Empty
        End With
        Return cargo
    End Function

    Public Shared Function GetDescription(tableName As String, code As String, Optional accessToken As String = "", Optional provider As String = "CORE") As String
        Try
            Dim result = InMotionGIT.General.Proxy.General.TableValueDescription(tableName, code, accessToken, provider)
            Return result
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
    End Function

    Public Function SerializeReportContract(Info As Object) As String
        Try
            Dim xml_serializer As New XmlSerializer(Info.GetType)
            Dim string_writer As New StringWriter
            xml_serializer.Serialize(string_writer, Info)
            Return string_writer.ToString()
        Catch ex As Exception
            Throw New ArgumentException(ex.Message)
        End Try
    End Function

    Public Function YearsByNow(ByVal dob As Date) As Integer
        Dim age As Integer
        age = Today.Year - dob.Year
        If (dob > Today.AddYears(-age)) Then age -= 1
        Return age
    End Function

    Public Function GetSCONDSVS(productCode As Integer) As String 'Obtiene el Condicionado general del producto
        Dim currentConnection As DbConnection = DataAccessLayer.OpenDbConnection("EntityServices")
        Dim commandItem As DbCommand = currentConnection.CreateCommand()
        Dim WhereStatement As String = String.Empty
        Dim result As String = ""
        WhereStatement = String.Format(" WHERE NPRODUCT = {0} AND DNULLDATE IS NULL", productCode)

        commandItem.CommandType = CommandType.Text
        commandItem.CommandText =
                "SELECT SCONDSVS" &
             " FROM INSUDB.PRODUCT (NOLOCK)" & " " & WhereStatement
        Dim dataReaderItem As DbDataReader = InMotionGIT.Common.Helpers.DataAccessLayer.QueryExecute(commandItem, currentConnection, CommandBehavior.Default, "PRODUCT")

        With dataReaderItem
            If .HasRows Then
                Do While .Read

                    If Not .IsDBNull(0) Then
                        result = .GetString(0)
                    End If
                Loop
            Else
                .Close()
            End If
        End With
        Return result
    End Function

End Class
