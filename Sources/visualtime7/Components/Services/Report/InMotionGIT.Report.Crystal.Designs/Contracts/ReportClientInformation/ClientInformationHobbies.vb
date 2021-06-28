Imports System.ComponentModel
Imports System.ComponentModel.Composition
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports InMotionGIT.Common.Attributes

Namespace ReportClientInformation

    <Export(GetType(Interfaces.IReportContract))>
    <ExportMetadata("ReportType", "SubReport")>
    <ExportMetadata("ContractName", "ClientInformationHobbies")>
    <ExportMetadata("ContractType", GetType(ClientInformationHobbies))>
    <DataContract(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")>
    <Serializable()>
    <XmlType(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")>
    <XmlRoot(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")>
    Public Class ClientInformationHobbies

        Private _CodigoHobby As Integer
        Private _Descripcion As String

        Sub New()
        End Sub

        Public Sub New(_CodigoHobby As Integer, _Descripcion As String)
            Me._CodigoHobby = _CodigoHobby
            Me._Descripcion = _Descripcion
        End Sub

        <DataMember(EmitDefaultValue:=False)>
        <ElementBehavior(5)>
        <ElementRequired(True)>
        <XmlAttribute(), DefaultValue(GetType(Integer), "0")>
        Public Property CodigoHobby As Integer
            Get
                Return _CodigoHobby
            End Get
            Set(value As Integer)
                _CodigoHobby = value
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