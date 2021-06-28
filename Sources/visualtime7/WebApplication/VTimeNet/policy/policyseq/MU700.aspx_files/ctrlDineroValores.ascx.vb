Imports Microsoft.VisualBasic

Public Class ctrlDineroValores
    Inherits System.Web.UI.UserControl

    Public mObjValues As New eFunctions.Values

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")

       Public mObjInformacionDineroValores As Object

    Public Sub insPreMU700_PuntualValue(ByVal objPreParameters As Object) 
        mObjInformacionDineroValores = objPreParameters
    End Sub

End Class
