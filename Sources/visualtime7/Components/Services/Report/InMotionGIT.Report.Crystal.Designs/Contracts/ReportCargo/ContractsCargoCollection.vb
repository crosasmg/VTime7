
Imports System.Collections.ObjectModel
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports InMotionGIT.Common.Attributes

Namespace ReportCargo

    <CollectionDataContract()>
    <Serializable()>
    <XmlType(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    <XmlRoot(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    Public Class ContractsCargoCollection
        Inherits Collection(Of Cargo)

        Sub New()
        End Sub

    End Class

End Namespace