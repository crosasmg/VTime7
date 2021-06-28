<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.57
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "ag553"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct")
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"),  ,  ,  ,  ,  , "Setvalues(""Modulec"")")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , True, 4, GetLocalResourceObject("valModulecColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInstallmentsColumnCaption"), "tcnInstallments", 2, "0", False, GetLocalResourceObject("tcnInstallmentsColumnToolTip"), False, 0,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnStartMonthColumnCaption"), "tcnStartMonth", 2, "0", True, GetLocalResourceObject("tcnStartMonthColumnToolTip"), False, 0,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndMonthColumnCaption"), "tcnEndMonth", 2, "0", True, GetLocalResourceObject("tcnEndMonthColumnToolTip"), False, 0,  ,  ,  , True)
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "AG553"
		.Codisp = "AG553"
		.sCodisplPage = "AG553"
		.Top = 200
		.Left = 150
		.Height = 300
		.Width = 380
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeBranch").EditRecord = True
		
		.Columns("valModulec").Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("cbeBranch").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong)
	End With
End Sub

'%insPreAG553: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreAG553()
	'--------------------------------------------------------------------------------------------
	
	Dim lcolBranprod_allow As eAgent.branprod_allows
	Dim lclsBranprod_allow As Object
	Dim llngIntermedia As Object
	Dim lblnIntermedia As Boolean
	
	lcolBranprod_allow = New eAgent.branprod_allows
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 And CStr(Session("nLastIntermediary")) <> vbNullString Then
		lblnIntermedia = lcolBranprod_allow.Find(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
		llngIntermedia = Session("nIntermed")
		If Not lblnIntermedia Then
			lblnIntermedia = lcolBranprod_allow.Find(mobjValues.StringToType(Session("nLastIntermediary"), eFunctions.Values.eTypeData.etdDouble))
			llngIntermedia = Session("nLastIntermediary")
		End If
	Else
		llngIntermedia = Session("nIntermed")
		lblnIntermedia = lcolBranprod_allow.Find(mobjValues.StringToType(llngIntermedia, eFunctions.Values.eTypeData.etdDouble))
	End If
	
	
	Dim lclsBranprod_allowLast As eAgent.branprod_allow
	With mobjGrid
		If lblnIntermedia Then
			For	Each lclsBranprod_allow In lcolBranprod_allow
				.Columns("cbeBranch").DefValue = lclsBranprod_allow.nBranch
				.Columns("valProduct").DefValue = lclsBranprod_allow.nProduct
				.Columns("valModulec").Parameters.Add("nBranch", lclsBranprod_allow.nBranch)
				.Columns("valModulec").Parameters.Add("nProduct", lclsBranprod_allow.nProduct)
				.Columns("valModulec").Parameters.Add("dEffecdate", Today)
				.Columns("valModulec").DefValue = lclsBranprod_allow.nModulec
				.Columns("tcnInstallments").DefValue = lclsBranprod_allow.nInstallments
				.Columns("tcnStartMonth").DefValue = lclsBranprod_allow.nStartMonth
				.Columns("tcnEndMonth").DefValue = lclsBranprod_allow.nEndMonth
				
				'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
				'+ función insPostAG553 cuando se eliminen los registros seleccionados - VCVG - 11/12/2001
				.Columns("sParam").DefValue = "nIntermed=" & Session("nIntermed") & "&nModulec=" & lclsBranprod_allow.nModulec & "&nInstallments=" & lclsBranprod_allow.nInstallments & "&nStartMonth=" & lclsBranprod_allow.nStartMonth & "&nEndMonth=" & lclsBranprod_allow.nEndMonth & "&nBranch=" & lclsBranprod_allow.nBranch & "&nProduct=" & lclsBranprod_allow.nProduct & "&nUsercode=" & Session("nUsercode")
				If llngIntermedia = Session("nLastIntermediary") Then
					lclsBranprod_allowLast = New eAgent.branprod_allow
					With Request
						Call lclsBranprod_allowLast.insPostAG553("AG553", "Add", Session("nIntermed"), lclsBranprod_allow.nBranch, lclsBranprod_allow.nProduct, lclsBranprod_allow.nModulec, lclsBranprod_allow.nInstallments, lclsBranprod_allow.nStartMonth, lclsBranprod_allow.nEndMonth, Session("nUsercode"))
					End With
					lclsBranprod_allowLast = Nothing
				End If
				Response.Write(mobjGrid.DoRow())
			Next lclsBranprod_allow
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	lclsBranprod_allow = Nothing
	lcolBranprod_allow = Nothing
	mobjGrid = Nothing
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreAG553Upd. Se define esta función para construir el contenido de la ventana UPD de los Ramos y Productos permitidos
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreAG553Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsBranprod_allow As eAgent.branprod_allow
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsBranprod_allow = New eAgent.branprod_allow
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsBranprod_allow.insPostAG553("AG553", .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nInstallments"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nStartMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nEndMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			lclsBranprod_allow = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valAgentSeq.aspx", "AG553", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") <> "Del" Then
Response.Write("		" & vbCrLf)
Response.Write("		<SCRIPT>		" & vbCrLf)
Response.Write("			self.document.forms[0].valProduct.Parameters.Param1.sValue=self.document.forms[0].cbeBranch.value;" & vbCrLf)
Response.Write("			self.document.forms[0].valModulec.Parameters.Param1.sValue=self.document.forms[0].cbeBranch.value;" & vbCrLf)
Response.Write("			self.document.forms[0].valModulec.Parameters.Param2.sValue=self.document.forms[0].valProduct.value;		" & vbCrLf)
Response.Write("		</" & "SCRIPT>")

			
		End If
		
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ag553")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.22 $"        

//%Setvalues: Función que asigna los parámetros para los valores posibles de los campos "Producto" y "Modulos" 
//-------------------------------------------------------------------------------------------------------------
function Setvalues(FieldValue){
//-------------------------------------------------------------------------------------------------------------
	var lintAction=0;	
	var strParams=''; 

	if(sAction=="Add")
	    lintAction = 301
	else if(sAction=="Update")
	    lintAction = 302

	with (self.document.forms[0]){
		if(FieldValue=='Modulec'){
			if(sAction=="Update"){
				valModulec.disabled=true
				btnvalModulec.disabled=true
			}
			else{
				if(valProduct.value!=""){
					valModulec.Parameters.Param1.sValue=cbeBranch.value
					valModulec.Parameters.Param2.sValue=valProduct.value
					valModulec.disabled=false
					btnvalModulec.disabled=false
				}
			}
			
// proceso de cuotas 
			if(sAction!="Update"){
    		    strParams = "nBranch=" + cbeBranch.value + 
		                    "&nProduct=" + valProduct.value + 
		                    "&nAction=" + lintAction
			    insDefValues('Installments',strParams,'/VTimeNet/Agent/AgentSeq'); 
			}
		}
	}
}
</SCRIPT>
<HTML>
<HEAD>


    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
	<%Response.Write(mobjValues.StyleSheet())%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAssociaField" ACTION="valAgentSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

<%="<script>var sAction='" & Request.QueryString.Item("Action") & "'</script>"%>
<%
Response.Write(mobjValues.ShowWindowsName("AG553", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	Response.Write(mobjMenu.setZone(2, "AG553", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	mobjMenu = Nothing
	Call insPreAG553()
Else
	Call insPreAG553Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.57
Call mobjNetFrameWork.FinishPage("ag553")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




