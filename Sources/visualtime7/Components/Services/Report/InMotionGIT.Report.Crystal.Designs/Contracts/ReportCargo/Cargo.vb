Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Xml.Serialization
Imports InMotionGIT.Common.Attributes

Namespace ReportCargo

    <DataContract(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    <Serializable()>
    <XmlType(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    <XmlRoot(Namespace:="urn:InMotionGIT.Report.Crystal.Designs.ReportCargo")>
    Public Class Cargo

        Private _XMLContract As String
        Private _Name As String

        <DataMember(EmitDefaultValue:=False)>
        <ElementRequired(False)>
        <XmlAttribute(), DefaultValue(GetType(String), "")>
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(value As String)
                _Name = value
            End Set
        End Property

        <DataMember(EmitDefaultValue:=False)>
        <ElementRequired(False)>
        <XmlAttribute(), DefaultValue(GetType(String), "")>
        Public Property XMLContract As String
            Get
                Return _XMLContract
            End Get
            Set(value As String)
                _XMLContract = value
            End Set
        End Property
    End Class

End Namespace