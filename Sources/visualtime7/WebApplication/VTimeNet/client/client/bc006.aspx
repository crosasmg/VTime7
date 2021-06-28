<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo del control del Grid
Dim mobjGrid As eFunctions.Grid
'- Declaración de variables de uso interno
Dim mintClient As Object
Dim mintPremium As Object
Dim mintClaim As Object
Dim mblnClient As Object


'%insLoadBC006: Se solicitan los campos de la ventana a procesar.
'-------------------------------------------------------------------------------------------------------------------
Private Sub insLoadBC006()
	'-------------------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40334><A NAME=""Cliente a asignar"">" & GetLocalResourceObject("AnchorCliente a asignarCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4"" BORDER=0><HR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=9683>" & GetLocalResourceObject("gmtClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.ClientControl("gmtClient", "", True, GetLocalResourceObject("gmtClientToolTip"), "CheckedReceiptClaim();", False, "lblClieName", True,  ,  ,  , eFunctions.Values.eTypeClient.SearchClient))
Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")

	Response.Write(mobjValues.DIVControl("lblClieName"))
Response.Write("</TD>" & vbCrLf)
Response.Write("                ")

	Response.Write("<SCRIPT>UpdateDiv('lblTitular','""','Normal');</" & "Script>")
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL>" & GetLocalResourceObject("cbeSexclienCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeSexclien", "Table18", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSexclienToolTip")))


Response.Write("</TD>	" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcdBirthdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdBirthdate",  ,  , GetLocalResourceObject("tcdBirthdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("cbeOccupatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeOccupat", "Table16", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOccupatToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    	<TD>")


Response.Write(mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), "1",  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("btnPolicyValuesCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">")


Response.Write(mobjValues.AnimatedButtonControl("btnPolicyValues", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnPolicyValuesToolTip"),  , "ShowSports(""gmtClient"",gmtClient.value)", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.HiddenControl("hddsClient", Session("sCodeClient")))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD WIDTH = ""30%"" COLSPAN=2>")


Response.Write(mobjValues.CheckControl("chkPremium", GetLocalResourceObject("chkPremiumCaption"), CStr(CShort(mintPremium)), CStr(0),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkClaim", GetLocalResourceObject("chkClaimCaption"), CStr(CShort(mintClaim)), CStr(0),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=40335><A NAME=""Clientes asociados"">" & GetLocalResourceObject("AnchorClientes asociadosCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4""><HR></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    ")

	
End Sub
'%insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddCheckColumn(40338, GetLocalResourceObject("chkSelColumnCaption"), "chkSel", CStr(eRemoteDB.Constants.strNull),  , "1")
		Call .AddPossiblesColumn(40336, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
		Call .AddClientColumn(40337, GetLocalResourceObject("deClientColumnCaption"), "deClient", vbNullString)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSexclienColumnCaption"), "cbeSexclien", "table18", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSexclienColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdBirthdateColumnCaption"), "tcdBirthdate", "",  , GetLocalResourceObject("tcdBirthdateColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOccupatColumnCaption"), "cbeOccupat", "table16", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOccupatColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkSmokingColumnCaption"), "chkSmoking", "",  ,  ,  , True, GetLocalResourceObject("chkSmokingColumnToolTip"))
		Call .AddAnimatedColumn(100693, GetLocalResourceObject("btnQueryColumnCaption"), "btnQuery", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnQueryColumnToolTip"))
		Call .AddHiddenColumn("tcnAuxRoles", CStr(0))
		Call .AddHiddenColumn("tcnAuxClient", CStr(0))
		Call .AddHiddenColumn("tcnAuxRole", CStr(0))
		Call .AddHiddenColumn("chkAuxSel", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "BC006"
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.AddButton = False
	End With
End Sub
'%insPreGridBC006: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreGridBC006()
	'--------------------------------------------------------------------------------------------
	Dim lclsRoles As ePolicy.Roles
	Dim lclsClient As eClient.Client
	Dim lcolRoleses As ePolicy.Roleses
	Dim lintCount As Short
	Dim lblnChecked As Boolean
	Dim lblnPerson_typ As Boolean
	lintCount = 0
	With Server
		lclsClient = New eClient.Client
		lclsRoles = New ePolicy.Roles
		lcolRoleses = New ePolicy.Roleses
	End With
	If lcolRoleses.Find_by_Policy("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), vbNullString, Today) Then
		For	Each lclsRoles In lcolRoleses
			With mobjGrid
				.Columns("chkSel").checked = 2
				.Columns("cbeRole").DefValue = CStr(lclsRoles.nRole)
				.Columns("tcnAuxRoles").DefValue = CStr(lclsRoles.nRole)
				.Columns("deClient").DefValue = lclsRoles.sClient
				.Columns("tcnAuxClient").DefValue = lclsRoles.sClient
				.Columns("tcnAuxRole").DefValue = CStr(lcolRoleses.Count)
				.Columns("chkSel").OnClick = "changeValue(this," & lintCount & "," & lcolRoleses.Count & ")"
				.Columns("chkAuxSel").checked = 2
				With lclsClient
					Call .Find(lclsRoles.sClient)
					If .sSmoking = "1" Then
						lblnChecked = True
					Else
						lblnChecked = False
					End If
					If .nPerson_typ = 1 Then
						lblnPerson_typ = False
					Else
						lblnPerson_typ = True
					End If
					mobjGrid.Columns("cbeSexclien").DefValue = .sSexclien
					mobjGrid.Columns("tcdBirthdate").DefValue = mobjValues.TypeToString(.dBirthdat, eFunctions.Values.eTypeData.etdDate)
					If .nPerson_typ = 1 Then
						mobjGrid.Columns("cbeOccupat").TableName = "Table16"
					Else
						mobjGrid.Columns("cbeOccupat").TableName = "Table417"
					End If
					mobjGrid.Columns("cbeOccupat").DefValue = mobjValues.TypeToString(.nSpeciality, eFunctions.Values.eTypeData.etdDouble)
					mobjGrid.Columns("chkSmoking").checked = mobjValues.StringToType(.sSmoking, eFunctions.Values.eTypeData.etdDouble)
				End With
				.Columns("btnQuery").Disabled = lblnPerson_typ
				.Columns("btnQuery").HRefScript = "ShowSportsPopUp(" & lintCount & ")"
				Response.Write(.DoRow)
			End With
			lintCount = lintCount + 1
		Next lclsRoles
	End If
	Response.Write(mobjGrid.closeTable())
	lclsClient = Nothing
	lclsRoles = Nothing
	lcolRoleses = Nothing
	Response.Write(mobjValues.BeginPageButton)
End Sub

</script>
<%Response.Expires = -1
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mobjGrid = New eFunctions.Grid
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
	var nMainAction = 304;
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 7/11/03 17:08 $|$$Author: Nvaplat26 $"
//------------------------------------------------------------------------------------------
//%changeValue: se cambia el valor del campo auxiliar del Check, para poder utilizarlo en 
//			    valClaim.aspx
//-------------------------------------------------------------------------------------------
function changeValue(Field,Index,Count){
	if(Count > 1)
        (Field.checked)?self.document.forms[0].chkAuxSel[Index].value=1:self.document.forms[0].chkAuxSel[Index].value=0
    else
        (Field.checked)?self.document.forms[0].chkAuxSel.value=1:self.document.forms[0].chkAuxSel.value=0
    with (self.document.forms[0])
        mstrClient = self.document.forms[0].tcnAuxClient.value; 
}
var mstrClient
//% ShowSports: Muestra los deportes más frecuentes del cliente a asignar
//---------------------------------------------------------------------------------------------------------------------------------------------------
function ShowSports(sField, sValue){
//---------------------------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
	    switch (sField){
	        case 'gmtClient':
			    mstrClient = sValue;
				break;
		}
		if (mstrClient != ''){
	        ShowPopUp('/VTimeNet/Common/SCA006.aspx?sCodispl=SCA006&nMainAction=' + nMainAction + '&sClient=' + mstrClient,'BC005',450,300,'no','no',200,80);
        }		
	}
}
//% ShowSportsPopUp: Muestra los deportes más frecuentes del cliente asociado (página PopUp)
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ShowSportsPopUp(nIndex){
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (nIndex != ''){
            ShowPopUp('/VTimeNet/Common/SCA006.aspx?sCodispl=SCA006&nMainAction=' + nMainAction + '&sClient=' + marrArray[nIndex].tcnAuxClient,'BC006',450,300,'no','no',200,80); 		
        }		
	}
}
//%CheckedReceiptClaim: Selecciona la opción Recibos pendientes y Siniestros pendientes
//------------------------------------------------------------------------------------------
function CheckedReceiptClaim(){
//------------------------------------------------------------------------------------------
    insDefValues('Client', "sCodispl=" + 'BC006' + '&sClient=' + self.document.forms[0].gmtClient.value, '/VTimeNet/Client/Client');
	ShowPopUp("/VTimeNet/Client/Client/ShowDefValues.aspx?Field=ReceiptClaim" + "&sClient=" + self.document.forms[0].gmtClient.value , "ShowDefValuesClient", 1, 1,"no","no",2000,2000);
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "BC006", "BC006.aspx"))
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmChClientPolicy" ACTION="ValCLient.aspx?x=1">
<%
Call insLoadBC006()
Call insDefineHeader()
Call insPreGridBC006()
%>
</FORM>
</BODY>
</HTML>





