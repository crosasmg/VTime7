#Region "using"

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports InMotionGIT.Common.Helpers

#End Region

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class RefreshCache
    Inherits System.Web.Services.WebService

    <WebMethod(Description:="Refresh the cache of Query Manager")> _
    Public Sub RemoveCache(ByVal cacheKeyName As String)
        If cacheKeyName.Contains(",") Then

            For Each itemCache As String In cacheKeyName.Split(",")
                Caching.Remove(itemCache)
            Next

        Else
            Caching.Remove(cacheKeyName)
        End If
    End Sub

End Class
