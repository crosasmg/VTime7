Imports Microsoft.VisualBasic

Public Class ctrlInformacionParticularRobo
    Inherits System.Web.UI.UserControl

    Public mObjValues As New eFunctions.Values

    Public resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("MU700")

    Public mObjInformacionParticularRobo As Object

    Public Sub insPreMU700_PuntualValue(ByVal objPreParameters As Object) 
        mObjInformacionParticularRobo = objPreParameters
    End Sub

End Class