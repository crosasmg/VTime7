#Region "Copyright (c) 2007, Global Insurance Technology"

'************************************************************************************
'
' Copyright (c) 2007, Global Insurance Technology
'
'***********************************************************************************

#End Region

#Region "Imports directive"

Imports System.Globalization
Imports System.Threading
Imports System.Web.Configuration
Imports InMotionGIT.Core.Configuration

#End Region

Namespace InMotionGIT.Web.Base

    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BackOfficePageBase
        Inherits System.Web.UI.Page

#Region "Properties"

        Private _UserContext As InMotionGIT.Membership.Providers.MemberContext

        ''' <summary>
        ''' Return user information conditional on the type of security
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UserContext() As InMotionGIT.Membership.Providers.MemberContext
            Get
                If _UserContext.IsEmpty Then
                    _UserContext = New InMotionGIT.Membership.Providers.MemberContext
                End If
                Return _UserContext
            End Get
            Set(ByVal value As InMotionGIT.Membership.Providers.MemberContext)
                _UserContext = value
            End Set
        End Property

#End Region

        Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
            ' GIT.Core.Helpers.LogManager.Log(String.Empty)
            'Dim xd As System.Xml.Linq.XDocument = XDocument.Load(Server.MapPath("~\App_GlobalResources\JavaScriptResource.resx"))
            Dim xd As System.Xml.Linq.XDocument = XDocument.Load(Server.MapPath("~\Bin\JavaScriptResource.resx"))
            ' Styles
            Dim _values As New eFunctions.Values

            Dim msUserDateFormat As String
            Dim msUserDateSeparator As String
            Dim msUserDecimalSeparator As String
            Dim msUserThousandSeparator As String

            ' Current culture settings
            Dim _currentCultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture
            With _currentCultureInfo.DateTimeFormat
                msUserDateFormat = .ShortDatePattern.ToUpper
                msUserDateSeparator = .DateSeparator
            End With
            With _currentCultureInfo.NumberFormat
                msUserDecimalSeparator = .NumberDecimalSeparator
                msUserThousandSeparator = .NumberGroupSeparator
            End With

            Dim sb As New StringBuilder
            With sb
                '.Append("<!DOCTYPE html>")
                .Append("<meta charset='utf-8' />")
                .Append("<script type='text/javascript' src='/scripts/jquery.min.js'></script>")

                .Append("<script type='text/javascript' src='/scripts/jquery-ui.js'></script>")
                .Append("<script type='text/javascript' src='/scripts/jquery.ui.datepicker-es.js'></script>")

                '.Append("<script type='text/javascript' src='/scripts/moment.min.js'></script>")
                .Append("<script type='text/javascript' src='/scripts/bootstrap.min.js'></script>")

                .Append("<link rel='stylesheet' href='/styles/bootstrap.min.css' />")
                '.Append("<link rel='stylesheet' href='/styles/bootstrap-theme.min.css' />")
                .Append("<link rel='stylesheet' href='/styles/font-awesome.min.css' />")

                .Append("<link rel='stylesheet' href='/Styles/ui.jqgrid.css' />")
                .Append("<link rel='stylesheet' href='/styles/jquery-ui.min.css' />")

                .Append("<link id='customVTstyle' rel='stylesheet' href='/VTimeNet/common/" & _values.sStyleSheetName & ".css'/>")
                .Append("<script>mstrSrvDecSep = '" & msUserDecimalSeparator & "'; " & "mstrUsrDecSep = '" & msUserDecimalSeparator & "'; </script>")

                .Append("<script type='text/javascript'> ")
                .Append("var nextOnblur = false;")
                .Append("var resValues = { ")

                For Each _xmlElement As XElement In xd.Descendants("data")
                    .Append(String.Format("{0}:""{1}"", ", _xmlElement.Attribute("name").Value,
                                                           GetGlobalResourceObject("JavaScriptResource", _xmlElement.Attribute("name").Value)))
                Next

                .Append(String.Format("todayValue:'{0}'", DateTime.Today.ToShortDateString))
                .Append("}; ")
                .Append("</script> ")
            End With

            Response.Write(sb.ToString())

            MyBase.OnLoad(e)
        End Sub

        Protected Overrides Sub InitializeCulture()
            Dim config As VisualTIME = TryCast(ConfigurationManager.GetSection("VisualTIMEConfigurationGroup/VisualTIMESection"), VisualTIME)

            'Se realiza manejo para leer en web.config configuración regional que debe utilizar toda la aplicación
            Dim configuration As Configuration = WebConfigurationManager.OpenWebConfiguration("/")
            Dim globalizationSection As GlobalizationSection = configuration.GetSection("system.web/globalization")
            Dim currentCultureInfo As CultureInfo = New CultureInfo(globalizationSection.Culture)

            'Se asigna cultura por defecto a utilizar por la aplicación, según configuración establecida en web.config
            Dim cultureName As String = currentCultureInfo.Name

            'Si se preestablece el lenague por medio del parametro del url 'culture'
            If Not String.IsNullOrEmpty(Request.QueryString("culture")) Then
                cultureName = Request.QueryString("culture")
            End If

            If config.General.EnabledFrontOffice Then
                'Si no hya ningun lenaguje seleccionado, entonces se estable el lenaguaje pre establecido por el usuario
                If cultureName = String.Empty AndAlso UserContext.Language <> String.Empty Then
                    cultureName = UserContext.Language
                End If
            End If

            'Si no hya ningun lenaguje seleccionado, entonces se toma el default para el portal
            If cultureName = String.Empty AndAlso UserContext.IsAnonymous Then
                cultureName = config.General.DefaultLanguage
            End If

            'Si no hya ningun lenaguje seleccionado, entonces se toma el languaje por default del browser
            If cultureName = String.Empty Then
                Dim contextLanguages As String() = HttpContext.Current.Request.UserLanguages
                If contextLanguages.Length > 0 Then
                    cultureName = contextLanguages(0)
                End If
            End If

            'Establece una cualtura por default en caso de no tratarse de alguna de la cultura manejadas
            If Not cultureName.ToLower.StartsWith("es") AndAlso
               Not cultureName.ToLower.StartsWith("pt") AndAlso
               Not cultureName.ToLower.StartsWith("en") Then
                cultureName = "en-US"
            End If

            'Manejo del error 'CultureNotFoundException', el cual solo ha sido generado en ADS, donde falla por un codigo de
            ' cultura es-419 el cual no es valido.
            Try
                UICulture = cultureName
                Culture = cultureName
            Catch ex As CultureNotFoundException
                InMotionGIT.Common.Helpers.LogHandler.ErrorLog("PageBase", "page", ex)
                InMotionGIT.Common.Helpers.LogHandler.TraceLog("PageBase", String.Format("CultureNotFoundException({0}) fixed", cultureName))
                cultureName = ConfigurationManager.AppSettings("DefaultLanguage")
                UICulture = cultureName
                Culture = cultureName
            End Try

            Dim ShortDatePattern As String = currentCultureInfo.DateTimeFormat.ShortDatePattern

            If ShortDatePattern.Length < 10 Then
                ShortDatePattern = ShortDatePattern.Replace("M", "MM")
                ShortDatePattern = ShortDatePattern.Replace("MMMM", "MM")
                ShortDatePattern = ShortDatePattern.Replace("d", "dd")
                ShortDatePattern = ShortDatePattern.Replace("dddd", "dd")
                ShortDatePattern = ShortDatePattern.Replace("yy", "yyyy")
                ShortDatePattern = ShortDatePattern.Replace("yyyyyyyy", "yyyy")
                currentCultureInfo.DateTimeFormat.ShortDatePattern = ShortDatePattern
            End If
            Thread.CurrentThread.CurrentCulture = currentCultureInfo
            MyBase.InitializeCulture()
        End Sub

        Private Sub Page_Error(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Error
            If Not System.Diagnostics.Debugger.IsAttached Then
                Dim lasterror As Exception = Server.GetLastError().GetBaseException()
                InMotionGIT.Common.Helpers.LogHandler.ErrorLog("BackOfficePageBase", "page", lasterror)

                If (ConfigurationManager.AppSettings("GeneralExceptionUnhandled").ToLower = "true") Then
                    Server.ClearError()
                    Throw lasterror
                End If
            End If
        End Sub

        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
            Dim windowLogicalCode As String = HttpContext.Current.Request.QueryString("sCodispl")
            Dim LinkSpecialFrontOffice As String = HttpContext.Current.Request.QueryString("LinkFront")
            Dim mainAction As Integer = IIf(HttpContext.Current.Request.QueryString("lnknMainAction") = "", 0, HttpContext.Current.Request.QueryString("lnknMainAction"))
            Dim ControlName As String = String.Empty
            Dim ControlValue As String = String.Empty
            Dim contador As Integer = 0
            Dim Delay As String = ConfigurationManager.AppSettings("BackOfficeDelay")
            Dim ExtraDelay As String = ConfigurationManager.AppSettings("BackOfficeExtraDelay")
            Dim physicalPageName As String = IO.Path.GetFileName(Page.AppRelativeVirtualPath)
            Dim automaticAccept As Boolean = True
            MyBase.Render(writer)

            If LinkSpecialFrontOffice = "1" Then
                With Response
                    .Write("<script type='text/javascript'>")

                    .Write(String.Format("ClientRequest('{0}');", mainAction))

                    For Each CurrentQueryString As String In Context.Request.QueryString.AllKeys
                        If CurrentQueryString.IsNotEmpty AndAlso CurrentQueryString.StartsWith("lnk") Then
                            ControlValue = HttpContext.Current.Request.QueryString(CurrentQueryString)
                            ControlName = CurrentQueryString.Substring(3)

                            .Write(String.Format("if (document.forms[0].elements['{0}'] != null){{", ControlName))

                            .Write(String.Format("setTimeout('SetValues(\'{0}\',\'{1}\')', {2});", ControlName, ControlValue, contador))
                            .Write(String.Format("setTimeout('ExecEvent(\'{0}\',\'{1}\')', {2});", ControlName, ControlValue, contador))

                            If (windowLogicalCode.ToUpper = "CA001" And ControlName = "tcnPolicy") Or
                               (windowLogicalCode.ToUpper = "SI001" And ControlName = "tcnClaim") Then
                                contador = contador + ExtraDelay
                            Else
                                contador = contador + Delay
                            End If

                            .Write("}")
                            If CurrentQueryString.Equals("lnkAccept", StringComparison.CurrentCultureIgnoreCase) AndAlso
                                HttpContext.Current.Request.QueryString(CurrentQueryString).IsNotEmpty AndAlso
                                HttpContext.Current.Request.QueryString(CurrentQueryString).ToString.Equals("False", StringComparison.CurrentCultureIgnoreCase) Then
                                automaticAccept = False
                            End If
                        End If
                    Next
                    If automaticAccept Then
                        .Write("setTimeout('ClientRequest(\'" & 390 & "\')'," & contador & ");")
                    End If
                    .Write("</script>")
                End With
            End If

        End Sub

        Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

            InMotionGIT.BackOffice.Support.Integration.Process()

            If ConfigurationManager.AppSettings("TrackingMode.Enable") = "True" Then
                Try
                    InMotionGIT.BackOffice.Support.Tracking.Track(Page.AppRelativeVirtualPath)
                Catch ex As Exception
                    InMotionGIT.Common.Helpers.LogHandler.ErrorLog("Application", "VisualTIME.Net: Error Exception: " + ex.Message)
                End Try
            End If

        End Sub

    End Class

End Namespace