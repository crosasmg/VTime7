Imports Microsoft.VisualBasic

Public Class ctrlIdentificacionRiesgo
    Inherits System.Web.UI.UserControl 

    Public mObjValues As New eFunctions.Values
    Public mobjIdentificacionRiesgo As Object

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")
    
    Public Sub insPreMU700_PuntualValue(ByVal objPreParameters As Object) 
        mobjIdentificacionRiesgo = objPreParameters
    End Sub
End Class