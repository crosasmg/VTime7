<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'+ Objeto para el manejo general de la tabla a mostrar las páginas que forman la secuencia

Dim mclsSequence As eFunctions.Sequence
Dim mobjValues As eFunctions.Values
Dim mclsPolicy As ePolicy.Out_moveme


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sequence")

mclsSequence = New eFunctions.Sequence
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mclsSequence.sSessionID = Session.SessionID
mclsSequence.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sequence"
%>
<HTML>
<HEAD>
   <META HTTP-EQUIV="CONTENT-LANGUAGE" CONTENT="ES">
   <BASE TARGET="fraFolder">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
   <%
With Response
	.Write("<SCRIPT>")
	.Write("var pblnQuery = false")
	.Write("</SCRIPT>")
End With
%>
<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"  
</SCRIPT>
</HEAD>
<BODY <%=mclsSequence.BODYParameters()%>>
<%
'+ Si la acción pasada como parámetro posee algún valor, se carga la secuencia 
'+ de la transacción del sistema.
If Request.QueryString.Item("nAction") <> vbNullString Then
	Select Case Request.QueryString.Item("nOpener")
		Case "CA036_K", "CA036", "CA036A", "CA039"
			mclsPolicy = New ePolicy.Out_moveme
			Response.Write(mclsPolicy.LoadTabs(CInt(Request.QueryString.Item("nAction")), Session("sSche_code"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("sTypeMov"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sSel"), mobjValues.StringToType(Session("nCertifCA039"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTratypei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nSituation"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdDouble)))
			
			Response.Write("mclsPolicy.LoadTabs(" & Request.QueryString.Item("nAction") & "," & Session("sSche_code") & "," & mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(Session("nTypeMov"), eFunctions.Values.eTypeData.etdDouble) & "," & Request.QueryString.Item("sSel") & "," & mobjValues.StringToType(Session("nCertifCA039"), eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(Session("nMonth"), eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(Session("nMovType"), eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(Session("nSituation"), eFunctions.Values.eTypeData.etdDouble) & "," & mobjValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdDouble) & ")")
            Case "VI7501_K", "VI7501_C", "VI7501_E", "VI7501_F", "VI7501_D", "VI7501_A", "VI7501_B", "VI7501_G"
                Dim mclsSaapv As New eSaapv.Saapv
                mclsSaapv = New eSaapv.Saapv
                Response.Write(mclsSaapv.LoadTabs(mobjValues.StringToType(CStr(Session("nCod_saapv")), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("nAction"), Session("sSche_code"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nInstitution")), eFunctions.Values.eTypeData.etdLong)))
                mclsSaapv = Nothing
        End Select
	If Request.QueryString.Item("sGoToNext") <> "NO" Then
		Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
	End If
Else
	'+ En el caso que no se encuentre secuencia asociada, se carga la imagen del FRAME principal
	'+ por defecto
	%>      <SCRIPT>top.fraFolder.document.location = "/VTimeNet/Common/Blank.htm"</SCRIPT> <%	
End If

Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction = '" & Request.QueryString.Item("nAction") & "';</SCRIPT>")
Session("bQuery") = False

mclsSequence = Nothing
mclsPolicy = Nothing
mobjValues = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("sequence")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




