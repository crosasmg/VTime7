<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenues As eFunctions.Menues


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100018, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnCodeColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(100021, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(100019, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddNumericColumn(100020, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 8, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 5)
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("tctDescript").EditRecord = True
		.Codispl = "MAG008"
		.Codisp = "MAG008"
		.sCodisplPage = "MAG008"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "dEffecdate=" & mobjValues.typeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nCode='+ marrArray[lintIndex].tcnCode + '" & "&nUsercode=" & mobjValues.typeToString(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
		.Height = 250
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMAG008()
	'------------------------------------------------------------------------------
	Dim lcolInt_fixvals As eAgent.Int_fixvals
	Dim lclsInt_fixval As Object
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insStateZone(){}" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	
	lcolInt_fixvals = New eAgent.Int_fixvals
	
	'+Se realiza la lectura de los registros a ser mostrados
	If lcolInt_fixvals.Find(mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsInt_fixval In lcolInt_fixvals
			With mobjGrid
				.Columns("tcnCode").DefValue = lclsInt_fixval.nCode
				.Columns("tctDescript").DefValue = lclsInt_fixval.sDescript
				.Columns("tcnAmount").DefValue = lclsInt_fixval.nAmount
				.Columns("tcnRate").DefValue = lclsInt_fixval.nRate
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsInt_fixval
	End If
	Response.Write(mobjGrid.closeTable())
	lcolInt_fixvals = Nothing
	lclsInt_fixval = Nothing
End Sub

'------------------------------------------------------------------------------
Private Sub insPreMAG008Upd()
	'------------------------------------------------------------------------------
	Dim lclsInt_fixval As eAgent.Int_fixval
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsInt_fixval = New eAgent.Int_fixval
		
		'+Se asignan los parametros necesarios para la eliminación
		Response.Write(mobjValues.ConfirmDelete())
		With lclsInt_fixval
			.dEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
			.nCode = mobjValues.StringToType(Request.QueryString.Item("nCode"), eFunctions.Values.eTypeData.etdDouble)
			.nUsercode = mobjValues.StringToType(Request.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		
		lclsInt_fixval = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
	lclsInt_fixval = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG008"

%>



<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:34 $"
</SCRIPT>    
    
    <%=mobjValues.StyleSheet()%>
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MAG008", "MAG008"))
	mobjMenues = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmTabGralComm" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG008()
Else
	Call insPreMAG008Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





