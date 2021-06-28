<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eRemoteDB" %>
<%@ Import namespace="System.Text.RegularExpressions" %>


<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 31/3/03 17.17.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mstrQueryString As String
Dim mstrValPageQS As String


'% insGetFields: toma las descripciones de los parámetros que recibe en el QueryString
'--------------------------------------------------------------------------------------------
Private Sub insGetFields()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	Dim sNameControl As String
	Dim sValueControl As String
	Dim sArrFields() As String
	Dim sField As String
	Dim lintMax As Integer
	Dim lintEqual As Integer
	'- Campos que posee el String recibido en el QueryString    
	Dim lstrForm As String
	
	
	lintCount = 1
	
	If Request.QueryString.Item("sForm") = vbNullString Then
            lstrForm = HttpUtility.UrlDecode(Session("sForm")) & "&" & Request.QueryString.Item("sSource")
	Else
		lstrForm = Request.QueryString.Item("sForm")
	End If
	
	'+El ciclo que se presenta a continuación, carga todos los campos de la forma que llamo a la
	'+ventana de errores, como campos ocultos en la misma. 

'	sArrFields = Split(lstrForm, "&")
	sArrFields = lstrForm.Split("&")
	lintMax = UBound(sArrFields)
	For lintCount = 0 To lintMax
		sField = sArrFields(lintCount)
		'+Si existian dos "&" seguidos, la casilla de la matriz pudo quedar vacia
		If (sField <> vbNullString) Then
			lintEqual = InStr(1, sField, "=")
			If lintEqual > 0 Then
				sNameControl = Left(sField, lintEqual - 1)
				sValueControl = Mid(sField, lintEqual + 1)
                If IsNothing(sValueControl) Then
                    sValueControl = String.empty
                End If 
				
                    'sValueControl = Regex.Replace(sValueControl, "%2F", "/", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%2C", ",", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%F3", "ó", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "\+", " ", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%3F", "?", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%BF", "¿", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%ED", "í", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%E9", "é", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%0D%0A", "&#13", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%3A", ":", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%E1", "á", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%FA", "ú", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%28", "(", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%29", ")", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%F1", "ñ", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%D1", "Ñ", RegexOptions.IgnoreCase)
                    'sValueControl = Regex.Replace(sValueControl, "%A0", " ", RegexOptions.IgnoreCase)
                    sValueControl = Server.UrlDecode(sValueControl)
				Response.Write(mobjValues.HiddenControl(sNameControl, sValueControl))
			End If
		End If
	Next 
	
End Sub

'% getWindowty: se toman los datos de la ventana
'--------------------------------------------------------------------------------------------
Private Function getWindowty() As String
	'--------------------------------------------------------------------------------------------
	Dim lobjQuery As eRemoteDB.Query
	Dim lintBeginCodispl As Integer
	Dim lstrCodispl As String
	
	lobjQuery = New eRemoteDB.Query
	
	lintBeginCodispl = InStr(1, mstrQueryString, "sCodispl=") + 9
	
	If InStr(lintBeginCodispl, mstrQueryString, "&") > 0 Then
		lstrCodispl = Mid(mstrQueryString, lintBeginCodispl, InStr(lintBeginCodispl, mstrQueryString, "&") - lintBeginCodispl)
	End If
	
	If lobjQuery.OpenQuery("Windows", "nWindowty", "sCodispl = '" & lstrCodispl & "'") Then
		getWindowty = lobjQuery.FieldToClass("nWindowty")
	End If
	
	lobjQuery = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("errors")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 17.17.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "errors"

mstrQueryString = Request.QueryString.Item("sQueryString")
mstrValPageQS = Request.QueryString.Item("sValPage")

%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
        .Write(mobjValues.WindowsTitle("GE002", GetLocalResourceObject("WindowTitle")))
End With
%>    
<SCRIPT LANGUAGE="JavaScript" SRC="../Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="../Scripts/valFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 11/09/03 17:44 $|$$Author: Nvaplat40 $"

	var mstrQueryString = '<%=mstrQueryString%>'
	var mstrValPage = '<%=mstrValPageQS%>'

//% CancelErrors: Se cierra la ventana al presionar el botón de Cancelar.
//-------------------------------------------------------------------------------------------
function CancelErrors(){
//-------------------------------------------------------------------------------------------
    
    self.close();
}

//% updateStatus: Actualiza estado de botones y cursor de mouse
//-------------------------------------------------------------------------------------------
function updateStatus(bClose){
//-------------------------------------------------------------------------------------------
    var lintZone = mstrQueryString.substr(mstrQueryString.indexOf("nZone=") + 6, 1)
<%
If Request.QueryString.Item("nWindowTy") = vbNullString Then
	Response.Write("var lintWindowty = '" & getWindowty & "'")
Else
	Response.Write("var lintWindowty = '" & Request.QueryString.Item("nWindowTy") & "'")
End If
%>    
    var lintActionType = '<%=Request.QueryString.Item("ActionType")%>' 
    var lintIndex = '<%=Request.QueryString.Item("nIndex")%>'
    var lintMainAction = '<%=Request.QueryString.Item("nMainAction")%>' 
    var lstrKey = '<%=Request.QueryString.Item("sKey")%>' 
    var lobjErr
    
    if(typeof(bClose)=='undefined')
		bClose = true
	
	if(typeof(opener.top)!='unknown')

        if(typeof(opener.top.fraFolder)!='undefined')
            if(typeof(opener.top.fraFolder.document)!='undefined')        
                if(typeof(opener.top.fraFolder.document.cmdAccept)!='undefined')
		            opener.top.fraFolder.document.cmdAccept.disabled = false;
	
//+ Se habilitan/deshabilitan las acciones del ToolBar
        if(typeof(opener.top.fraHeader)!='undefined'){
			with(opener.top.fraHeader){
			    if (document.location.href.indexOf("InSequence")>=0 && (lintWindowty=='7' || lintWindowty=='9'))
			    	insHandImage("A390", true);
			    else
			        insHandImage("A390", !(lintZone==2 || lintWindowty==5));

			    insHandImage("A301", !(lintZone==2));
			    insHandImage("A302", !(lintZone==2));
			    insHandImage("A303", !(lintZone==2));
			    insHandImage("A304", !(lintZone==2));
			    insHandImage("A401", !(lintZone==2));
			    insHandImage("A402", !(lintZone==2));
			    insHandImage("A392", (lintZone==2 || lintWindowty==5));
			    insHandImage("A393", (lintZone==2));
			    insHandImage("A391", true);
			}
		}
        
        try{
            opener.top.fraHeader.setPointer('');
        }
        catch(lobjErr){
			if(typeof(opener.top.fraFolder)!='undefined')
				opener.top.fraFolder.setPointer('');
			else {
				opener.top.setPointer('');
			}
        }

	if(bClose){
	    if (lintActionType=='Check' &&
	        typeof(self.document.forms[0].cmdAccept)=='undefined'){
	    	lintIndex-=1;
	    	cancelEditRecord(mstrQueryString,lintIndex,lintMainAction,lintZone);
	    }
		window.close();
    }
}

//% insChangeSubmit: se arma la acción a ejecutar dependiendo del módulo y proyecto que se 
//%                  se está ejecutando. Ej.: /VTimeNet/Module/Project/valProject.aspx
//-------------------------------------------------------------------------------------------
function insChangeSubmit(sQueryString){
//-------------------------------------------------------------------------------------------
	var lstrSubProject
	var lstrValPage
	with (self.document.forms[0]){
		if (typeof(sSubProject)!='undefined'){
			lstrSubProject = "/" + sSubProject.value;
			lstrValPage = sSubProject.value;
		}
		else{
			lstrSubProject = "";
			lstrValPage = sProject.value;
		}

//Cuando la página de validación no tiene el mismo nombre del proyecto,
//se debe indicar su nombre en el Querystring sValPage

		if (mstrValPage!=''){
			lstrValPage = mstrValPage;
		}

		action = "/VTimeNet/" + sModule.value + "/" +
                 sProject.value + lstrSubProject + "/val" + lstrValPage + ".aspx" +
				 "?sCodisplReload=" + sCodisplReload.value +
				 "&" + sQueryString

	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="updateStatus(false);closeWindows();">
<FORM NAME="frmErrors" ACTION="/VTimeNet/sModule/sProject/Page.aspx" METHOD="POST">
<%
If Not IsNothing(Request.QueryString.Item("sCommand")) Then
	Response.Write(Request.QueryString.Item("sCommand"))
Else
	Response.Write(Session("sErrorTable"))
	Session("sErrorTable") = vbNullString
End If

Call insGetFields()

With Response
	.Write("<SCRIPT>")
	.Write("insChangeSubmit(""" & mstrQueryString & """);")
	.Write("</SCRIPT>")
End With

Session("sForm") = vbNullString

mobjValues = Nothing
%>
	</FORM>	
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 17.17.03
Call mobjNetFrameWork.FinishPage("errors")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




