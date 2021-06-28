<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'**- Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues



'**% insDefineHeader: The field of the GRID is defined.
'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'**+ The column of the GRID are defined.
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctOriginColumnCaption"), "tctOrigin", 30, vbNullString,  , GetLocalResourceObject("tctOriginColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddCheckColumn(0, GetLocalResourceObject("chkActivFoundColumnCaption"), "chkActivFound", vbNullString,  ,  ,  , True, GetLocalResourceObject("chkActivFoundColumnToolTip"))
		'Call .AddNumericColumn(0, GetLocalResourceObject("tcnPartic_minColumnCaption"),"tcnPartic_min",4,0,, GetLocalResourceObject("tcnPartic_minColumnToolTip"),True,2,,,,True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 5, CStr(0),  , GetLocalResourceObject("tcnParticipColumnToolTip"),  , 2)
		Call .AddHiddenColumn("tcnOrigin", CStr(0))
		Call .AddHiddenColumn("tcnFunds", CStr(0))
		Call .AddHiddenColumn("hddPartic_min", CStr(0))
		Call .AddHiddenColumn("hddParticip", CStr(0))
		Call .AddHiddenColumn("hddEffecdate", CStr(0))
		Call .AddHiddenColumn("hddsVigen", "2")
	End With
	
	'**+ The properties of the GRID are defined.
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "VI7002"
		.Height = 250
		.Width = 400
		.AddButton = False
		.DeleteButton = False
		.bCheckVisible = False
		
		.Columns("Sel").Title = "Sel"
		.AddButton = False
		.DeleteButton = False
		If mobjValues.ActionQuery <> True Then
			.Columns("tctOrigin").EditRecord = True
			.Columns("tctDescript").EditRecord = True
		Else
			.Columns("Sel").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("Sel").OnClick = "insSelected(this)"
		
	End With
End Sub

'**% insPreVI7002: Read the information of the policy funds.
'% insPreVI7002: Obtiene los datos de los fondos de la póliza.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7002()
	'--------------------------------------------------------------------------------------------
	Dim lclsFunds As ePolicy.tmp_Funds_Pol
	Dim lcolFundss As ePolicy.tmp_Funds_pols
	Dim lintIndex As Long
	Dim lintOrigin(20, 1) As Object
	Dim lclsTab_Ord_Origin As Object
	Dim lcolTab_Ord_Origins As eBranches.Tab_Ord_Origins
	
	lclsFunds = New ePolicy.tmp_Funds_Pol
	lcolFundss = New ePolicy.tmp_Funds_pols
	
	If lcolFundss.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		lcolTab_Ord_Origins = New eBranches.Tab_Ord_Origins
		
		If lcolTab_Ord_Origins.Find(Session("nBranch"), Session("nProduct")) Then
			
			For	Each lclsTab_Ord_Origin In lcolTab_Ord_Origins
				lintOrigin(lclsTab_Ord_Origin.nOrigin, 0) = lclsTab_Ord_Origin.sDescript
				lintOrigin(lclsTab_Ord_Origin.nOrigin, 1) = 0
			Next lclsTab_Ord_Origin
		End If
		
		lclsTab_Ord_Origin = Nothing
		lcolTab_Ord_Origins = Nothing
		
		For	Each lclsFunds In lcolFundss
			With mobjGrid
				.Columns("Sel").checked = CShort(lclsFunds.sSel)
				.Columns("tctOrigin").DefValue = lclsFunds.nOrigin & " - " & lclsFunds.sOrigin
				.Columns("tcnOrigin").DefValue = CStr(lclsFunds.nOrigin)
				.Columns("tcnFunds").DefValue = CStr(lclsFunds.nFunds)
				.Columns("tctDescript").DefValue = .Columns("tcnFunds").DefValue & " - " & lclsFunds.sDescript
				'		.Columns("tcnPartic_min").DefValue = lclsFunds.nPartic_min
				.Columns("tcnParticip").DefValue = CStr(lclsFunds.nParticip)
				'		.Columns("hddPartic_min").DefValue=lclsFunds.nPartic_min
				.Columns("hddParticip").DefValue = CStr(lclsFunds.nParticip)
				.Columns("hddsVigen").DefValue = lclsFunds.sVigen
				.Columns("hddEffecdate").DefValue = CStr(lclsFunds.dEffecdate)
				
				If lclsFunds.sSel = "1" Then
					.Columns("chkActivFound").checked = CShort("1")
					lintOrigin(lclsFunds.nOrigin, 1) = lintOrigin(lclsFunds.nOrigin, 1) + lclsFunds.nParticip
				Else
					.Columns("chkActivFound").checked = CShort("2")
				End If
				If lclsFunds.sVigen = "1" Then
					.Columns("chkActivFound").checked = CShort("1")
				Else
					.Columns("chkActivFound").checked = CShort("2")
				End If
				
				
				Response.Write(.DoRow)
				
			End With
		Next lclsFunds
		
		
Response.Write("      " & vbCrLf)
Response.Write("	  <TABLE WIDTH=""30%"">	" & vbCrLf)
Response.Write("      <BR></BR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	")

		lintIndex = 1
		Do While lintIndex < 20
			If lintOrigin(lintIndex, 1) > 0 Then
				
Response.Write("" & vbCrLf)
Response.Write("	            <TR>" & vbCrLf)
Response.Write("	                <TD ><LABEL ID=0>")


Response.Write(lintOrigin(lintIndex, 0))


Response.Write(" ( % Participación )</LABEL></TD>" & vbCrLf)
Response.Write("	                <TD>")


Response.Write(mobjValues.NumericControl("tcnParticip", 3, lintOrigin(lintIndex, 1),  , GetLocalResourceObject("tcnParticipToolTip"),  ,  , True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	            </TR>" & vbCrLf)
Response.Write("	  " & vbCrLf)
Response.Write("	    ")

			End If
			lintIndex = lintIndex + 1
		Loop 
Response.Write("" & vbCrLf)
Response.Write("	    " & vbCrLf)
Response.Write("	    </TABLE>		" & vbCrLf)
Response.Write("	")

		
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolFundss = Nothing
	lclsFunds = Nothing
End Sub

'% insPreVI7002Upd: Muestra la ventana Popup para las actualizaciones.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7002Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsFunds_Pol As ePolicy.Funds_Pol
	Dim sActivFound As Object
	lclsFunds_Pol = New ePolicy.Funds_Pol
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicyTra.aspx", "VI7002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsFunds_Pol = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "vi7002"
mobjGrid = New eFunctions.Grid

mobjGrid.sCodisplPage = "vi7002"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
mobjMenu = New eFunctions.Menues

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">





<SCRIPT>    
//**+ For the Source Safe control. 
//+ Para Control de Versiones. 
    document.VssVersion="$$Revision: 2 $|$$Date: 7/02/06 17:05 $|$$Author: Clobos $"
	
//% insCheckSelClick: Permite levantar la ventana Popup para actualizar el registro.
//-------------------------------------------------------------------------------------------
function insSelected(Field){
//-------------------------------------------------------------------------------------------
	var lstrParams; 
	lstrParam = ''; 
	if(Field.checked) {
		EditRecord(Field.value,nMainAction, 'Update')
		Field.checked = !Field.checked
    }
    else{ 
		lstrParam = "sCodispl="			+ 'VI7002'   + 
		            "&nMainAction="		+ nMainAction   + 
		            "&Action="			+ 'Del'   + 
                    "&nFunds="			+ marrArray[Field.value].tcnFunds + 
                    "&nOrigin="		+ marrArray[Field.value].tcnOrigin + 
                    "&nParticip="		+ marrArray[Field.value].tcnParticip +
                    "&dEffecdate="		+ marrArray[Field.value].hddEffecdate

		insDefValues('UpdVi7002', lstrParam,'/VTimeNet/Policy/policytra');
    }
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("VI7002", Request.QueryString.Item("sWindowDescript")))
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjValues.ShowWindowsName("VI7002", Request.QueryString.Item("sWindowDescript")))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "VI7002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI7002" ACTION="../../Policy/PolicyTra/ValPolicyTra.aspx?mode=2">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI7002()
Else
	Call insPreVI7002Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





