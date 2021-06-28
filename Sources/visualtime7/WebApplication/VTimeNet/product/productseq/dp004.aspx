<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mobjClient_req As eProduct.Client_req
Dim mobjClient_reqs As eProduct.Client_reqs
Dim mobjErrors As eFunctions.Errors


'% insDefineHeader : Configura los datos del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	mobjGrid.ActionQuery = Session("bQuery")
	mobjGrid.sCodisplPage = "DP004"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeFieldColumnCaption"), "cbeField", "Table8017", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeFieldColumnToolTip"))
		Call .AddHiddenColumn("hddField", "")
		
		Call .AddCheckColumn(0, GetLocalResourceObject("chkRequieredColumnCaption"), "chkRequiered", "",  ,  ,  , mobjGrid.ActionQuery, GetLocalResourceObject("chkRequieredColumnToolTip"))
		Call .AddHiddenColumn("hddRequiered", "")
		
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP004"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		'.Width = 450
		'.Height = 400
	End With
End Sub
'% insPreDP004 : Muestra los datos repetitivos de la página.
'---------------------------------------------------------------------------------------------
Private Sub insPreDP004()
	'---------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Call insDefineHeader()
	
	Response.Write(mobjValues.HiddenControl("hddMassive", "1"))
	
	
Response.Write("        " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <BR>" & vbCrLf)
Response.Write("        <TR>        " & vbCrLf)
Response.Write("		<TD WIDTH=10%><LABEL ID=14431>" & GetLocalResourceObject("valRoleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>" & vbCrLf)
Response.Write("		")

	
	With mobjValues
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valRole", "TabCliallopro1", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nRole"), True, False,  ,  ,  , "OnChangeValues()", False, 5, GetLocalResourceObject("valRoleToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=10%><LABEL ID=41339>" & GetLocalResourceObject("valTratypePCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("valTratypeP", "Table221", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTratypeP"),  , False,  ,  ,  , "OnChangeValues()", False, 5, GetLocalResourceObject("valTratypePToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        ")

	mobjValues.ActionQuery = Session("bQuery")
	If Not mobjValues.ActionQuery Then
		Response.Write("<TD WIDTH=""10%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
	End If
	
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <BR>")

	
	If mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdLong) > 0 And mobjValues.StringToType(Request.QueryString.Item("nTratypeP"), eFunctions.Values.eTypeData.etdLong) > 0 Then
		If CBool(Trim(CStr(mobjClient_req.insValDP004(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) = vbNullString))) Then
			
			If mobjClient_reqs.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nTratypeP"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToDate(Session("dEffecdate"))) Then
				lintCount = 0
				
				For	Each mobjClient_req In mobjClient_reqs
					With mobjGrid
						'+ Campo            
						.Columns("cbeField").DefValue = CStr(mobjClient_req.nFieldabe)
						.Columns("cbeField").Descript = mobjClient_req.sDescript
						.Columns("hddField").DefValue = CStr(mobjClient_req.nFieldabe)
						'+ Requerido
						If IsDbNull(mobjClient_req.sRequired) Or Trim(mobjClient_req.sRequired) = vbNullString Or Trim(mobjClient_req.sRequired) = "2" Then
							.Columns("chkRequiered").Checked = CShort("0")
							.Columns("hddRequiered").DefValue = ""
						Else
							.Columns("chkRequiered").Checked = CShort("1")
							.Columns("hddRequiered").DefValue = "1"
						End If
						.Columns("chkRequiered").OnClick = "UpdateCheck(this," & lintCount & ")"
						
						Response.Write(mobjGrid.DoRow)
					End With
					lintCount = lintCount + 1
				Next mobjClient_req
			End If
		Else
			Response.Write(mobjGrid.closeTable)
			Response.Write(mobjErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 11348,  ,  ,  , True))
		End If
	Else
		Response.Write(mobjGrid.closeTable)
	End If
End Sub

</script>
<%Response.Expires = 0
With Server
	mobjMenu = New eFunctions.Menues
	mobjGrid = New eFunctions.Grid
	mobjValues = New eFunctions.Values
	mobjClient_req = New eProduct.Client_req
	mobjClient_reqs = New eProduct.Client_reqs
	mobjErrors = New eFunctions.Errors
End With
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "DP004"
%>
<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:01 $"
       
//+	UpdateCheck: Actualiza el campo hidden relacionado con el check en selección
//-------------------------------------------------------------------------------------------
function UpdateCheck(ObjCheck, Index){
//-------------------------------------------------------------------------------------------
    var sValue=''
    if(ObjCheck.checked==true){
        sValue = "1"
    }
    else{
        sValue = ""
    }
	if (typeof(self.document.forms[0].hddRequiered[Index]) == 'undefined')
	{	
		self.document.forms[0].hddRequiered.value = sValue
	}
	else
	{
		self.document.forms[0].hddRequiered[Index].value = sValue
	}
}
//+ Se recarga la página para que muestre las coberturas del módulo seleccionado
//----------------------------------------------------------------------------------------------------------------------
function OnChangeValues(){
//----------------------------------------------------------------------------------------------------------------------
    var lstrstring = '';
    var nRole = 0
    var nTratypeP = 0
    
    nRole = self.document.forms[0].valRole.value
	nTratypeP = self.document.forms[0].valTratypeP.value
	
	if (nRole != '<%=Request.QueryString.Item("nRole")%>' ||
	    nTratypeP != '<%=Request.QueryString.Item("nTratypeP")%>'){
	    lstrstring += document.location;
	    lstrstring = lstrstring.replace(/&nRole=.*/, "");
	    lstrstring = lstrstring.replace(/&nTratypeP=.*/, "");
	    lstrstring = lstrstring + "&nRole=" + nRole + "&nTratypeP=" + nTratypeP;
	    document.location.href = lstrstring;
	}
}
//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
	var nRole = 0
    var nTratypeP = 0
    
    nRole = self.document.forms[0].valRole.value
	nTratypeP = self.document.forms[0].valTratypeP.value
	
	if (nRole != 0 && nTratypeP != 0){
		self.document.forms[0].hddMassive.value=2;
		top.frames['fraHeader'].ClientRequest(390,2);
	}
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP004"))
	.Write(mobjMenu.setZone(2, "DP004", "DP004.aspx"))
End With
mobjMenu = Nothing
%>    
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="POST" ID="FORM" NAME="frmDP004" ACTION="valProductSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
            <BR>
            <%Call insPreDP004()%>
        </FORM>
    </BODY>
</HTML>
<%
mobjGrid = Nothing
mobjValues = Nothing
mobjClient_req = Nothing
mobjClient_reqs = Nothing
mobjErrors = Nothing
%>




