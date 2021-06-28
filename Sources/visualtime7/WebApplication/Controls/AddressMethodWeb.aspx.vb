Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Data
Imports System.Linq
Imports DevExpress.Web.ASPxEditors

Partial Public Class Controls_AddressMethodWeb
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Updates the section/ Actualiza los datos de sección
    ''' </summary>
    ''' <param name="nameSource"></param>
    ''' <param name="textSource"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function UpdateValuesCacheTextBox(nameSource As String, textSource As String) As Boolean
        Dim result As Boolean = False
        result = FrontOffice.Controls.PhysicalAddress.UpdateValuesChache(nameSource, String.Empty, textSource, -1)
        Return result
    End Function



    ''' <summary>
    ''' Updates the section/ Actualiza los datos de sección
    ''' </summary>
    ''' <param name="nameSource"></param>
    ''' <param name="valueSource"></param>
    ''' <param name="textSource"></param>
    ''' <param name="indexSource"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function UpdateValuesCacheComboBox(nameSource As String, valueSource As String, textSource As String, indexSource As Integer, prefix As String) As Boolean
        Dim result As Boolean = False
        result = FrontOffice.Controls.PhysicalAddress.UpdateValuesChache(nameSource, valueSource, textSource, indexSource, prefix)
        Return result
    End Function

    ''' <summary>
    ''' Get List LookUp Possible Valúes Of Names Of Parts Of Address Table /Obtener lista de búsqueda posibles valores de nombres de los componentes de la mesa de Dirección
    ''' </summary>
    ''' <param name="countryCode"></param>
    ''' <param name="TypePhyTicalAddressCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetListLookUpPossibleValuesOfNamesOfPartsOfAddressTable(countryCode As Integer, TypePhyTicalAddressCode As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Return FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfNamesOfPartsOfAddressTable(countryCode, TypePhyTicalAddressCode)
    End Function

    ''' <summary>
    '''Get List LookUp Possible Values Allowed For Geographic Zone / Obtener lista de búsqueda Valores posibles permitidas para Zona Geográfica
    ''' </summary>
    ''' <param name="countryCode"></param>
    ''' <param name="geographicZoneLevelId"></param>
    ''' <param name="geographicZoneId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetListLookUpPossibleValuesAllowedForGeographicZone(countryCode As Integer,
                                                                               geographicZoneLevelId As Integer,
                                                                               geographicZoneId As Integer) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Return FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesAllowedForGeographicZone(countryCode, geographicZoneLevelId, geographicZoneId)
    End Function

    ''' <summary>
    ''' Obtener lista de búsqueda posibles valores de la Tabla País/Get List LookUp Possible Values Of Country Table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetListLookUpPossibleValuesOfCountryTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Return FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfCountryTable()
    End Function

    ''' <summary>
    ''' Obtener las operaciones de búsqueda posibles valores de zona horaria Tabla/Get LookUp Possible Values Of Time Zone Table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetListLookUpPossibleValuesOfGeographicZoneTableByLevel(countryCode As String) As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Return FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfGeographicZoneTableByLevel(countryCode)
    End Function




    ''' <summary>
    ''' Obtener las operaciones de búsqueda posibles valores de zona horaria Tabla/Get LookUp Possible Values Of Time Zone Table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetLookUpPossibleValuesOfTimeZoneTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Return FrontOffice.Controls.PhysicalAddress.GetLookUpPossibleValuesOfTimeZoneTable()
    End Function

    ''' <summary>
    ''' Obtener lista de búsqueda posibles valores de tipo de tabla de dirección física/Get List LookUp Possible Values Of Type Of Physical Address Table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetListLookUpPossibleValuesOfTypeOfPhysicalAddressTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Return FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfTypeOfPhysicalAddressTable()
    End Function

    ''' <summary>
    ''' Get List LookUp Possible Values Of Type Of Route Table/Obtener lista de búsqueda posibles valores de tipo de tabla de rutas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <WebMethod()> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetListLookUpPossibleValuesOfTypeOfRouteTable() As List(Of InMotionGIT.Common.DataType.LookUpValueExtend)
        Return FrontOffice.Controls.PhysicalAddress.GetListLookUpPossibleValuesOfTypeOfRouteTable()
    End Function

End Class