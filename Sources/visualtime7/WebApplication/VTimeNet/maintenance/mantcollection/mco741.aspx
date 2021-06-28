<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la págin
Dim mobjMenu As eFunctions.Menues


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(40590, GetLocalResourceObject("cbeBankColumnCaption"), "cbeBank", "Table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insChangeField(this)", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBankColumnCaption"))
		Call .AddHiddenColumn("tcnAccount", CStr(eRemoteDB.Constants.strNull))
		Call mobjGrid.Columns.AddPossiblesColumn(40595, GetLocalResourceObject("valConvenioColumnCaption"), "valConvenio", "tabBank_Agree", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , True, 5, GetLocalResourceObject("valConvenioColumnToolTip"))
		Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", "",  , GetLocalResourceObject("tctClientColumnCaption"))
	End With
	
	'+ Se definen las columns del Grid
	
	With mobjGrid
		.Columns("valConvenio").Parameters.Add("nBank", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Codispl = "MCO741"
		.Codisp = "MCO741"
		.sCodisplPage = "MCO741"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
		End If
		
		.sDelRecordParam = "sType_BankAgree=" & Session("sTyp_BankAgree") & "&nBank='+ marrArray[lintIndex].cbeBank + '" & "&nAccount='+ marrArray[lintIndex].tcnAccount + '"
		.Height = 250
		.Width = 400
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'------------------------------------------------------------------------------
Private Sub insPreMCO741()
	'------------------------------------------------------------------------------
	Dim lcolBank_Agrees As eCollection.Bank_Agrees
	Dim lclsBank_Agre As Object
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("		case 301:" & vbCrLf)
Response.Write("		case 401:" & vbCrLf)
Response.Write("		    document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("		    break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	lcolBank_Agrees = New eCollection.Bank_Agrees
	
	If lcolBank_Agrees.Find(Session("sTyp_BankAgree"), False) Then
		For	Each lclsBank_Agre In lcolBank_Agrees
			With mobjGrid
				.Columns("cbeBank").DefValue = lclsBank_Agre.nBank
				.Columns("tcnAccount").DefValue = lclsBank_Agre.nAccount
				.Columns("valConvenio").Parameters.Add("nBank", lclsBank_Agre.nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valConvenio").DefValue = lclsBank_Agre.nAccount
				.Columns("tctClient").DefValue = lclsBank_Agre.sClient
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsBank_Agre
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolBank_Agrees = Nothing
	lclsBank_Agre = Nothing
End Sub

'------------------------------------------------------------------------------
Private Sub insPreMCO741Upd()
	'------------------------------------------------------------------------------
	Dim lclsBank_Agree As eCollection.Bank_Agree
	Dim lstrErrors As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsBank_Agree = New eCollection.Bank_Agree
		
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsBank_Agree
			.sType_BankAgree = Request.QueryString.Item("sType_BankAgree")
			.nBank = mobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdDouble)
			.nAccount = mobjValues.StringToType(Request.QueryString.Item("nAccount"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		lclsBank_Agree = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCollection.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MCO741"

%>


<HTML>
<HTML>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
    
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 14/01/04 11:58 $|$$Author: Nvaplat11 $"
    </SCRIPT> 

	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%=mobjValues.StyleSheet()%>
	
	<%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
	
	<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	Response.Write(mobjMenu.setZone(2, "MCO741", "MCO741"))
	mobjMenu = Nothing
End If
%>

<SCRIPT LANGUAGE=JavaScript>
//--------------------------------------------------------------------------------------------
function insChangeField(vObj){
//--------------------------------------------------------------------------------------------
	var sValue;
	
	sValue = vObj.value;
	
	with (self.document.forms[0]){
	    valConvenio.value='';
		valConvenio.Parameters.Param1.sValue=sValue;				
		valConvenio.disabled = (sValue == '0' || sValue == '');		
		btnvalConvenio.disabled = valConvenio.disabled;
	}
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTabBankAgree" ACTION="valMantCollection.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

Call insDefineHeader()

If Request.QueryString.Item("type") <> "PopUp" Then
	Call insPreMCO741()
Else
	Call insPreMCO741Upd()
End If

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






