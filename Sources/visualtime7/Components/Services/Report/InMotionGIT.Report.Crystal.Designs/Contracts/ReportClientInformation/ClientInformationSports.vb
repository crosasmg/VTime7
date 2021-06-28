Imports System.ComponentModel
Imports System.ComponentModel.Composition
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports InMotionGIT.Common.Attributes

Namespace ReportClientInformation

    <Export(GetType(Interfaces.IReportContract))>
    <ExportMetadata("ReportType", "SubReport")>
    <ExportMetadata("ContractName", "ClientInformationSports")>
    <ExportMetadata("ContractType", GetType(ClientInformationSports))>
    <DataContract(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")>
    <Serializable()>
    <XmlType(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")>
    <XmlRoot(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")>
    <EntityPrimaryKeyMembers("CodigoDeporte, sport identifier")>
    Public Class ClientInformationSports

        Private _CodigoDeporte As Integer
        Private _Descripcion As String

        Sub New()
        End Sub

        Public Sub New(_CodigoDeporte As Integer, _Descripcion As String)
            Me._CodigoDeporte = _CodigoDeporte
            Me._Descripcion = _Descripcion
        End Sub

        <DataMember(EmitDefaultValue:=False)>
        <ElementBehavior(5)>
        <ElementRequired(True)>
        <XmlAttribute(), DefaultValue(GetType(Integer), "0")>
        Public Property CodigoDeporte As Integer
            Get
                Return _CodigoDeporte
            End Get
            Set(value As Integer)
                _CodigoDeporte = value
            End Set
        End Property


        <DataMember(EmitDefaultValue:=False)>
        <ElementRequired(False)>
        <XmlAttribute(), DefaultValue(GetType(String), "")>
        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

    End Class

End Namespace