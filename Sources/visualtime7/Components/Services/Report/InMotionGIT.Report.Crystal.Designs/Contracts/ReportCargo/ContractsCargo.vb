
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports InMotionGIT.Common.Attributes

Namespace ReportCargo

    <DataContract(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    <Serializable()>
    <XmlType(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    <XmlRoot(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    Public Class ContractsCargo

        Sub New()
            ReportStructure = New List(Of Cargo)
        End Sub

        Private _reportStructure As List(Of Cargo)

        <DataMember(EmitDefaultValue:=False)>
        <ElementRequired(False)>
        Public Property ReportStructure As List(Of Cargo)
            Get
                Return _reportStructure
            End Get
            Set(value As List(Of Cargo))
                _reportStructure = value
            End Set
        End Property

    End Class

End Namespace