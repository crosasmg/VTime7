Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalReportsDataDefModelLib
Imports System.Reflection


Public Class CrystalReportFixes
    Public Shared Function OraclePackageFix(ByVal report As ReportDocument, ByVal connInfo As TableLogOnInfo) As ReportDocument

        Dim tablaPrincipal As CrystalDecisions.CrystalReports.Engine.Table = report.Database.Tables(0)

        'Se obtiene el nombre cualificado de la tabla principal del reporte 
        Dim tableQualifiedName As String = CType(tablaPrincipal.GetType().GetProperty(
                                                                                        "RasTable",
                                                                                        Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance
                                                                                ).GetValue(tablaPrincipal, Nothing), 
                                                  ISCRTable).QualifiedName

        'Se obtiene el prefijo de la tabla el cual puede ser un package o simplemente el owner
        Dim tablePrefix As String = String.Empty
        If tableQualifiedName.LastIndexOf(".") > 0 Then
            tablePrefix = tableQualifiedName.Substring(0, tableQualifiedName.LastIndexOf(".") + 1)
        End If

        'Se asigna al location del reporte el nombre cualificado 
        report.Database.Tables(0).ApplyLogOnInfo(connInfo)
        report.Database.Tables(0).Location = String.Concat(tablePrefix, report.Database.Tables(0).Location.ToString())

        'Se asigna al location de cada uno de los supreportes el nombre cualificado 
        For Each subReport As ReportDocument In report.Subreports
            subReport.Database.Tables(0).ApplyLogOnInfo(connInfo)
            subReport.Database.Tables(0).Location = String.Concat(tablePrefix, subReport.Database.Tables(0).Location.ToString())
        Next

        Return report

    End Function
End Class
