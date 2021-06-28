<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjnsumamountlocal As String
Dim mobjnsumamount As String
'**- The object to handling the general function for the loads of values is defined.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values



'**- The variable mobjGrid to handling the Grid of the window is defined.
'- Se define la variable mobjGrid para el manejo del Grid de la ventana.

Dim mobjGrid As eFunctions.Grid

'**- The object to control the page zones is defined.
'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues



'**% insDefineHeader: The Grid columns are defined.
'% insDefineHeader: Se definen las columnas del grid.
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	'**+ The Grid columns are defined.
	'+ Se definen todas las columnas del Grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valInstitutionColumnCaption"), "valInstitution", "TabTab_Fn_Institu", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valInstitutionColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOriginColumnCaption"), "cbeOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOriginColumnToolTip"))
		mobjGrid.Columns("cbeOrigin").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeOrigin").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeOrigin").Parameters.Add("NCOLLECDOCTYP", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTyp_ProfitColumnCaption"), "cbeTyp_Profit", "Table950", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTyp_ProfitColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("sType_transfColumnCaption"), "sType_transf", "",  , CStr(2),  ,  , GetLocalResourceObject("sType_transfColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_pesoColumnCaption"), "tcnAmount_peso", 18, CStr(0),  , GetLocalResourceObject("tcnAmount_pesoColumnToolTip"), True, 6,  ,  , "insCalAmountUF();")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_UFColumnCaption"), "tcnAmount_UF", 18, CStr(0),  , GetLocalResourceObject("tcnAmount_UFColumnToolTip"), True, 6,  ,  ,  , True)
		
		Call .AddHiddenColumn("nType_transf", CStr(0))
	End With
	
	With mobjGrid
		.Codispl = "VI7005"
		.Codisp = "VI7005"
		.Top = 100
		.Height = 300
		.Width = 460
		
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valInstitution").EditRecord = True
		.Columns("valInstitution").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbeOrigin").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbeTyp_Profit").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sDelRecordParam = "nInstitution='+ marrArray[lintIndex].valInstitution + '" & "&nOrigin=' + marrArray[lintIndex].cbeOrigin + '" & "&nTyp_Profit=' + marrArray[lintIndex].cbeTyp_Profit + '"
		
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.Columns("sType_transf").Disabled = False
		Else
			.Columns("sType_transf").Disabled = True
		End If
		
		.Columns("sType_transf").OnClick = "insHandleGrid(this)"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'**% insPreVI7005: The mother window is create (Main window).
'% insPreVI7005: Se crea la ventana madre (Principal).
'------------------------------------------------------------------------------
Private Sub insPreVI7005()
	'------------------------------------------------------------------------------
	
	
	'- Objetos para el manejo de los datos repetitivos de la página.
	
	Dim lcolApv_Transfers As ePolicy.APV_Transfers
	Dim lclsApv_transfer As Object
	
	lclsApv_transfer = Nothing
	lcolApv_Transfers = New ePolicy.APV_Transfers
	
	
	
	If lcolApv_Transfers.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, 0) Then
		For	Each lclsApv_transfer In lcolApv_Transfers
			With mobjGrid
				.Columns("valInstitution").DefValue = lclsApv_transfer.nInstitution
				.Columns("cbeOrigin").DefValue = lclsApv_transfer.nOrigin
				
				If lclsApv_transfer.nType_transf = 1 Then
					.Columns("nType_transf").DefValue = CStr(1)
					.Columns("sType_transf").Checked = 1
				Else
					.Columns("nType_transf").DefValue = CStr(2)
					.Columns("sType_transf").Checked = 2
				End If
				
				.Columns("tcnAmount_peso").DefValue = lclsApv_transfer.nAmount_peso
				.Columns("tcnAmount_UF").DefValue = lclsApv_transfer.nAmount_UF
				.Columns("cbeTyp_Profit").DefValue = lclsApv_transfer.nTyp_ProfitWorker
				
				mobjnsumamountlocal = CStr(lclsApv_transfer.nAmount_peso + mobjnsumamountlocal)
				mobjnsumamount = CStr(lclsApv_transfer.nAmount_UF + mobjnsumamount)
				
				
				Response.Write(.DoRow)
				
				
			End With
		Next lclsApv_transfer
	End If
	
	Response.Write(mobjGrid.closeTable())
	
End Sub

'**% insPreVI7005Upd: Its defines this function to constructs the Pop Up window 
'**% when the action is update or delete.
'% insPreVI7005Upd: Se define esta funcion para contruir la ventana Pop Up
'% Cuando la acción es actualizar o borrar.
'------------------------------------------------------------------------------
Private Sub insPreVI7005Upd()
	'------------------------------------------------------------------------------
	Dim lclsApv_transfer As ePolicy.APV_Transfer
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			lclsApv_transfer = New ePolicy.APV_Transfer
			
			Call lclsApv_transfer.InsPostVI7005Upd(.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nInstitution"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 0, mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTyp_Profit"), eFunctions.Values.eTypeData.etdDouble))
			
			lclsApv_transfer = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "VI7005", CStr(301), Session("bQuery"), CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7005")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.15
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = "401"
%> 

<SCRIPT LANGUAGE="JavaScript">
//**+ For the Source Safe control.
//+ Para Control de Versiones. 
//------------------------------------------------------------------------------
document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $"
//------------------------------------------------------------------------------

//% insHandleGrid: Esta función permite marcar la columna oculta.
//-------------------------------------------------------------------------------------------
function insHandleGrid(Field){
//-------------------------------------------------------------------------------------------
//+ Se actualiza la columna oculta con la marcada.
 
    if (Field.checked)
        self.document.forms[0].nType_transf.value = 1
    else self.document.forms[0].nType_transf.value = 2  
}    

//% insCalAmountUF: Se ejecuta en el OnChange del campo Monto en pesos. Permite expresar en UF 
//% el monto indicado en pesos.
//--------------------------------------------------------------------------------------------
function insCalAmountUF(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (tcnAmount_peso.value != ""){ 
			ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=AmountUF&nAmount_peso=" + tcnAmount_peso.value , "ShowDefValues", 1, 1,"no","no",2000,2000);
		}
		else 
		    tcnAmount_UF.value = 0;
	}
}
</SCRIPT>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.15
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenu.setZone(2, "VI7005", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST"	ID="FORM" NAME="frmVI7005" ACTION="valPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
 
<%Response.Write(mobjValues.ShowWindowsName("VI7005", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI7005()
	%>
 
  <TABLE WIDTH="100%">
        	<TD><LABEL ID=12972><%= GetLocalResourceObject("tcnsumamountlocalCaption") %></LABEL></TD>
        	<TD><%=mobjValues.NumericControl("tcnsumamountlocal", 10, mobjnsumamountlocal,  , GetLocalResourceObject("tcnsumamountlocalToolTip"),  ,  , True,  ,  ,  , True, 1)%></TD>
        	<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnsumamountCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnsumamount", 10, mobjnsumamount,  , GetLocalResourceObject("tcnsumamountToolTip"),  , 6, True,  ,  ,  , True, 2)%></TD>
			
        </TR>
  </table > 
  
<%	
Else
	Call insPreVI7005Upd()
End If

%> 
	

<%
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.15
Call mobjNetFrameWork.FinishPage("VI7005")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




