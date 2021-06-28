<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'+ Se definen las columnas del grid 
	With mobjGrid.Columns
		If Request.QueryString("nMainAction") = 301 Then
			Call .AddHiddenColumn("chkSel", "1")
		Else
			Call .AddCheckColumn(0, "Sel", "chkSel", "",  ,  ,  , False)
		End If
		Call .AddNumericColumn(0, "Cantidad", "tcnQuantity", 5, "",  , "Cantidad de Vehículos a reponer", True,  ,  ,  , "ChangeValues(this);", False)
		Call .AddTextColumn(0, "Descripción", "tctDescript", 60, "",  , "Descripción del vehículo a reponer",  ,  ,  , False)
		Call .AddNumericColumn(0, "Valor unitario", "tcnAmount", 18, "",  , "Valor del vehículo a reponer", True, 6,  ,  , "ChangeValues(this);", False)
		Call .AddNumericColumn(0, "Total", "tcnTotalAmount", 18, "",  , "Valor Total de Repuestos", True, 6,  ,  ,  , True)
		Call .AddHiddenColumn("tcnId", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "SI830"
		.Codisp = "SI830"
		.sCodisplPage = "si830"
		.Top = 250
		.Left = 100
		.Width = 600
		.Height = 300
		.Columns("tctDescript").EditRecord = True
		.nMainAction = Request.QueryString("nMainAction")
		.DeleteButton = Request.QueryString("nMainAction") = 301
		.AddButton = Request.QueryString("nMainAction") = 301
		.ActionQuery = Request.QueryString("nMainAction") = 401
		.Columns("Sel").GridVisible = Request.QueryString("nMainAction") = 301
		.sDelRecordParam = "nServ_ord=' + self.document.forms[0].tcnServ_ord.value + '" & "&nId=' + marrArray[lintIndex].tcnId + '"
		.sEditRecordParam = "nServ_Ord=' + self.document.forms[0].tcnServ_ord.value + '" & "&dQuot_date=' + self.document.forms[0].tcdQuot_date.value + '" & "&sAtention=' + self.document.forms[0].hddAtention.value + '" & "&nVehbrand=' + self.document.forms[0].cbeVehbrand.value + '" & "&sVehmodel=' + self.document.forms[0].tctVehmodel.value + '" & "&nyear=' + self.document.forms[0].tcnyear.value + '"
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub
'% insPreSI021: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI830()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	Dim ldblTotalAmount As Double
	Dim ldblTotalAmountNet As Double
	Dim ldblTotalAmountIva As Double
	Dim ldblIva As Double
	Dim lintId As Integer
	Dim lstrAtention As String
	Dim lstrVehbrand As Object
	Dim lstrVehmodel As String
	Dim lstrYear As Object
	
	Dim lclsQuot_Auto As eClaim.Quot_auto
	Dim lclsQuot_Autos As eClaim.Quot_autos
	Dim lclsTax_Fixval As eAgent.tax_fixval
	
	lclsQuot_Autos = New eClaim.Quot_autos
	
	Response.Write(mobjValues.HiddenControl("tcnServ_ord", Request.QueryString("nServ_Ord")))
	Response.Write(mobjValues.HiddenControl("tcdQuot_date", Request.QueryString("dQuot_date")))
	
	If Request.QueryString("nMainAction") = 302 Then
		
Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD WIDTH=""15%""><LABEL>Acción a ejecutar</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD WIDTH=""25%"">")


Response.Write(mobjValues.OptionControl(3, "tcnOperat", "Aprobar", CStr(1), CStr(2), "ChangeValues(this.value);", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			    <TD WIDTH=""10%"">")


Response.Write(mobjValues.OptionControl(4, "tcnOperat", "Rechazar",  , CStr(3), "ChangeValues(this.value);", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("		<BR>")

		
	Else
		Response.Write(mobjValues.HiddenControl("tcnOperat", CStr(1)))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("			    <TD WIDTH=""15%""><LABEL>Marca </LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeVehbrand", "table7042", eFunctions.Values.eValuesType.clngComboType, lstrVehbrand,  ,  ,  ,  ,  ,  , Request.QueryString("nMainAction") = 302,  , "Marca del vehículo seleccionado"))


Response.Write("</TD> " & vbCrLf)
Response.Write("			    <TD WIDTH=""15%""><LABEL>Modelo</LABEL></TD>" & vbCrLf)
Response.Write("			    <TD>")


Response.Write(mobjValues.TextControl("tctVehmodel", 20, lstrVehmodel,  , "Modelo del vehículo",  ,  ,  ,  , Request.QueryString("nMainAction") = 302))


Response.Write("</TD> " & vbCrLf)
Response.Write("			    <TD WIDTH=""15%""><LABEL>Año</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnyear", 4, lstrYear,  , "Año del Vehiculo",  ,  ,  ,  ,  ,  , Request.QueryString("nMainAction") = 302))


Response.Write("</TD> " & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("		<BR>")

	
	lstrAtention = Request.QueryString("sAtention")
	lstrVehbrand = Request.QueryString("nVehbrand")
	lstrVehmodel = Request.QueryString("sVehmodel")
	lstrYear = Request.QueryString("nYear")
	Response.Write(mobjValues.HiddenControl("hddAtention", lstrAtention))
	
	If lclsQuot_Autos.Find(mobjValues.StringToType(Request.QueryString("nServ_Ord"), eFunctions.Values.eTypeData.etdDouble), 6) Then
		lintCount = lclsQuot_Autos.Count
		ldblTotalAmountNet = 0
		For	Each lclsQuot_Auto In lclsQuot_Autos
			With mobjGrid
				.Columns("tcnQuantity").DefValue = CStr(lclsQuot_Auto.nQuantity)
				.Columns("tctDescript").DefValue = lclsQuot_Auto.sDescript
				.Columns("tcnAmount").DefValue = CStr(lclsQuot_Auto.nAmount)
				If lclsQuot_Auto.nAmount < 0 Then
					ldblTotalAmount = CDbl(lclsQuot_Auto.nQuantity * 0)
				Else
					ldblTotalAmount = CDbl(lclsQuot_Auto.nQuantity * lclsQuot_Auto.nAmount)
				End If
				.Columns("tcnTotalAmount").DefValue = CStr(ldblTotalAmount)
				.Columns("tcnId").DefValue = CStr(lclsQuot_Auto.nId)
				.Columns("chkSel").DefValue = lclsQuot_Auto.sSel
				.Columns("chkSel").checked = CShort(lclsQuot_Auto.sSel)
				.Columns("chkSel").OnClick = "insSelected(this.checked,this," & CStr(lclsQuot_Auto.nId) & ")"
			End With
			If lstrAtention = vbNullString Then
				lstrAtention = lclsQuot_Auto.sCliename
			End If
			If lstrVehbrand = vbNullString Then
				lstrVehbrand = lclsQuot_Auto.nVehbrand
			End If
			If lstrVehmodel = vbNullString Then
				lstrVehmodel = lclsQuot_Auto.sVehmodel
			End If
			If lstrYear = vbNullString Then
				lstrYear = lclsQuot_Auto.nYear
			End If
			If lclsQuot_Auto.sSel = "1" Then
				ldblTotalAmountNet = ldblTotalAmountNet + ldblTotalAmount
			End If
			lintId = lclsQuot_Auto.nId
			Response.Write(mobjGrid.DoRow())
		Next lclsQuot_Auto
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.HiddenControl("hddVehbrand", lstrVehbrand))
	Response.Write(mobjValues.HiddenControl("hddVehmodel", lstrVehmodel))
	Response.Write(mobjValues.HiddenControl("hddYear", lstrYear))
	
	If lintCount > 0 Then
		lintId = lintId + 1
		Response.Write(mobjValues.HiddenControl("nCounter", CStr(lintId)))
	Else
		Response.Write(mobjValues.HiddenControl("nCounter", CStr(1)))
	End If
	
	'+ Se obtiene el porcentaje fijo de IVA (Tabla Tax_Fixval) 
	lclsTax_Fixval = New eAgent.tax_fixval
	If lclsTax_Fixval.Find(1, Request.QueryString("dQuot_date")) Then
		ldblIva = lclsTax_Fixval.nPercent
		ldblTotalAmountIva = ldblTotalAmountNet * (1 + (ldblIva / 100))
	Else
		ldblIva = 0
		ldblTotalAmountIva = ldblTotalAmountNet
	End If
	'UPGRADE_NOTE: Object lclsTax_Fixval may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsTax_Fixval = Nothing
	
	'+ Campos puntuales de la ventana:
	
Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL>Total Neto</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnTotalNet", 18, CStr(ldblTotalAmountNet), False, "Sumatoria de la columna de los repuestos en la cotización", True, 6, False, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL>Iva</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnTotalIva", 18, CStr(ldblIva), False, "Porcentaje del Impuesto", True, 6, False, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("				<TD><LABEL>Total</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnTotal", 18, CStr(ldblTotalAmountIva), False, "Monto total de la cotización", True, 6, False, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL>Atención</LABEL></TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""5"">")


Response.Write(mobjValues.TextControl("tctAtention", 60, lstrAtention,  , "Nombre del vendedor que hace el negocio",  ,  ,  , "ChangeValues(this);", Request.QueryString("nMainAction") = 302))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("	")

	
	If lintCount > 0 Then
		Response.Write("<SCRIPT>SetValues(""1"");</" & "Script>")
		'		Response.Write "<NOTSCRIPT>CalculateTotal();</" & "Script>"
	End If
	
	'UPGRADE_NOTE: Object lclsQuot_Auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsQuot_Auto = Nothing
	'UPGRADE_NOTE: Object lclsQuot_Autos may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsQuot_Autos = Nothing
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreSI830Upd()
	'----------------------------------------------------------------------------------------------
	Dim lclsQuot_Auto As eClaim.Quot_auto
	Dim lblnPost As Boolean
	Dim lintAction As Object
	
	If Request.QueryString("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		lclsQuot_Auto = New eClaim.Quot_auto
		With lclsQuot_Auto
			Select Case Request.QueryString("Action")
				Case "Add"
					lintAction = 1
				Case "Update"
					lintAction = 2
				Case "Del", "Delete"
					lintAction = 3
			End Select
			
			lblnPost = lclsQuot_Auto.InsPostSI830Upd(mobjValues.StringToType(lintAction, eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString("nServ_Ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nId"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form("tcdQuot_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble), Request.Form("tctdescript"), mobjValues.StringToType(Request.Form("cbeVehbrand"), eFunctions.Values.eTypeData.etdLong), Request.Form("tctVehmodel"), mobjValues.StringToType(Request.Form("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form("tcnyear"), eFunctions.Values.eTypeData.etdLong), Request.Form("tctCliename"), Request.Form("chkSel"), mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble))
		End With
		
		'UPGRADE_NOTE: Object lclsQuot_Auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		lclsQuot_Auto = Nothing
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaim.aspx", "SI830", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
		If Request.QueryString("Action") = "Add" Then
			Response.Write("<SCRIPT>self.document.forms[0].elements['tcnId'].value = top.opener.document.forms[0].elements['nCounter'].value;</" & "Script>")
			Response.Write("<SCRIPT>self.document.forms[0].elements['chkSel'].checked = 1;</" & "Script>")
		End If
	End With
	With Response
		.Write(mobjValues.HiddenControl("tcnServ_Ord", Request.QueryString("nServ_Ord")))
		.Write(mobjValues.HiddenControl("tcdQuot_date", Request.QueryString("dQuot_date")))
		.Write(mobjValues.HiddenControl("tctCliename", Request.QueryString("sAtention")))
		.Write(mobjValues.HiddenControl("cbeVehbrand", Request.QueryString("nVehbrand")))
		.Write(mobjValues.HiddenControl("tctVehmodel", Request.QueryString("sVehmodel")))
		.Write(mobjValues.HiddenControl("tcnyear", Request.QueryString("nyear")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "si830"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 10/12/03 17:28 $|$$Author: Nvaplat22 $"

//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//------------------------------------------------------------------------------------------
function ChangeValues(Field){
//------------------------------------------------------------------------------------------
	var strParams; 
	switch(Field.name){ 
		case "tcnQuantity": 
		case "tcnAmount": 
			with(self.document.forms[0]){ 
				if(tcnQuantity.value!="" && 
				   tcnAmount.value!="" ) {
				    tcnTotalAmount.value = insConvertNumber(tcnQuantity.value) * insConvertNumber(tcnAmount.value);
				    tcnTotalAmount.value = VTFormat(tcnTotalAmount.value,'','','',6,true);
				}
				else{
				    tcnTotalAmount.value = 0;
				}
			}
			break;
		
		case "tctAtention": 
			with(self.document.forms[0]){
				hddAtention.value = tctAtention.value;
			}
			break;

	}
}

//% CalculateTotal: Calcula el total una vez que se añaden las cantidades del IVA y el flete
//-------------------------------------------------------------------------------------
function CalculateTotal(){
//-------------------------------------------------------------------------------------
	var ldblIVA=0;
	var ldblTotal=0;
	var ldblTotalClear=0;
	var ldblShippingAmount;

	if(self.document.forms[0].elements["tcnIVA"].value!="") 
		ldblIVA = insConvertNumber(self.document.forms[0].elements["tcnIVA"].value);

	if(ldblIVA > 0){
		ldblIVA = (ldblIVA / 100) + 1;
		ldblTotalClear = insConvertNumber(self.document.forms[0].elements["tcnTotalAmountClear"].value);
		ldblShippingAmount = insConvertNumber(self.document.forms[0].elements["tcnShipping"].value);
		ldblTotal = (ldblTotalClear + ldblShippingAmount) * ldblIVA;
		self.document.forms[0].elements["tcnTotal"].value = VTFormat(ldblTotal, '', '', '', 6, true);
	}else{
		ldblTotalClear = insConvertNumber(self.document.forms[0].elements["tcnTotalAmountClear"].value);
		ldblShippingAmount = insConvertNumber(self.document.forms[0].elements["tcnShipping"].value);
		ldblTotal = ldblTotalClear + ldblShippingAmount;
		self.document.forms[0].elements["tcnTotal"].value = VTFormat(ldblTotal, '', '', '', 6, true);
	} 
} 

//% insSelected: Asigna valor a una columna oculta una vez que se presiona el checkbox de la columna SEL 
//------------------------------------------------------------------------------------------------------ 
function insSelected(blnChecked, Field, lintIndex){ 
//------------------------------------------------------------------------------------------------------ 
	var ldblTotalAmount = 0; 
	var ldblTotalNet = 0; 
	var ldblTotalIva = 0; 
	var ldblTotal = 0; 
	var strParams; 

    ldblTotalAmount = insConvertNumber(marrArray[lintIndex-1].tcnTotalAmount,'','', true); 
    ldblTotalNet    = insConvertNumber(self.document.forms[0].tcnTotalNet.value,'','', true); 
    ldblTotalIva    = insConvertNumber(self.document.forms[0].tcnTotalIva.value,'','', true); 
    
    strParams  = "nServ_ord=" + self.document.forms[0].tcnServ_ord.value + "&nId=" + marrArray[lintIndex-1].tcnId; 

    with (document.forms[0]){
		if(!blnChecked){
		    ldblTotalNet = ldblTotalNet - ldblTotalAmount;
			strParams = strParams + "&sSel=2" 
		}
		else{
			ldblTotalNet = ldblTotalNet + ldblTotalAmount;
			strParams = strParams + "&sSel=1" 
		}
//+Se asigna el monto total
	    ldblTotal = ldblTotalNet + (ldblTotalNet*(ldblTotalIva/100))

		self.document.forms[0].elements["tcnTotalNet"].value = VTFormat(ldblTotalNet, '', '', '', 6, true);
		self.document.forms[0].elements["tcnTotal"].value = VTFormat(ldblTotal, '', '', '', 6, true);
//+Se actualiza la tabla quot_parts
		insDefValues('Quot_Auto',strParams,'/VTimeNet/Claim/Claim');
	}
}
//%SetValues: Asignan e inhabilita/habilita los campos segun los valores 
//-------------------------------------------------------------------------------------------	
function SetValues(Option){
//-------------------------------------------------------------------------------------------	    
    switch(Option){
        case "1":
        {
            with(self.document.forms[0]){
			    cbeVehbrand.value = hddVehbrand.value;
				tctVehmodel.value = hddVehmodel.value;
				tcnyear.value     = hddYear.value;
            } 
            break; 
        } 
    }  
} 
</SCRIPT>
<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SI830", "SI830.aspx"))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI830"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI830" ACTION="ValClaim.aspx?x=1&nTransacio=SI830&sOriginalForm=<%=Session("sOriginalForm")%>">
<%
Response.Write(mobjValues.ShowWindowsName("SI830"))
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI830Upd()
Else
	Call insPreSI830()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

   






