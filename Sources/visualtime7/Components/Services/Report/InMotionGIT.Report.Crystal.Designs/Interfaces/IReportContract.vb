Namespace Interfaces

    Public Interface IReportContract

        Function BuildReport(reportFileName As String, resultURLPath As Boolean, reportCargo As ReportCargo.ContractsCargoCollection) As String

    End Interface

End Namespace