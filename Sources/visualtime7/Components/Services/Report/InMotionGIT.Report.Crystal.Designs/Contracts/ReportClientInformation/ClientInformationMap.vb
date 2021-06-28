Imports System.Xml.Serialization
Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports InMotionGIT.Common.Attributes
Imports System.ComponentModel.Composition

Namespace ReportClientInformation

    <Export(GetType(Interfaces.IReportContract))> 'N° 1
    <ExportMetadata("ReportType", "MainReport")> 'N° 2
    <ExportMetadata("ContractName", "ClientInformationMap")> 'N° 3 
    <ExportMetadata("ContractType", GetType(ClientInformationMap))> 'N° 4 
    <DataContract(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")> 'N° 5 
    <Serializable()> 'N° 6 
    <XmlType(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")> 'N° 7 
    <XmlRoot(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportClientInformation")> 'N° 8 
    Public Class ClientInformationMap

        Private _ClientID As String
        Private _CompleteName As String
        Private _Birthdate As Date

        Sub New()
        End Sub


        <DataMember(EmitDefaultValue:=False)>
        <ElementRequired(True)>
        <XmlAttribute(), DefaultValue(GetType(String), "")>
        Public Property ClientID As String
            Get
                Return _ClientID
            End Get
            Set(value As String)
                _ClientID = value
            End Set
        End Property

        <DataMember(EmitDefaultValue:=False)>
        <ElementRequired(False)>
        <XmlAttribute(), DefaultValue(GetType(String), "")>
        Public Property CompleteName As String
            Get
                Return _CompleteName
            End Get
            Set(value As String)
                _CompleteName = value
            End Set
        End Property

        <DataMember(EmitDefaultValue:=False)>
        <ElementRequired(False)>
        <XmlAttribute(), DefaultValue(GetType(Date), "01/01/0001 12:00:00 AM")>
        Public Property Birthdate As Date
            Get
                Return _Birthdate
            End Get
            Set(value As Date)
                _Birthdate = value
            End Set
        End Property

    End Class

End Namespace