<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

Dim mintBranch_Fire As Byte


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	
	mobjGrid.sCodisplPage = "si776"
	
	'+Se definen todas las columnas del Grid
	Call mobjGrid.Columns.AddNumericColumn(0, "Cantidad", "tcnQuantity_parts", 6, "", True, "Cantidad de repuestos", False, 0,  ,  ,  , False)
	If mintBranch_Fire = 1 Then
		Call mobjGrid.Columns.AddTextColumn(0, "Item", "tctItem", 60, "",  , "Item (texto libre)")
	Else
		Call mobjGrid.Columns.AddPossiblesColumn(0, "Repuesto", "valSpare_parts", "Table5579", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , 4, "Descripción del repuesto", eFunctions.Values.eTypeCode.eNumeric)
		Call mobjGrid.Columns.AddCheckColumn(0, "Repuesto original", "chkOriginal_spare", "")
	End If
	Call mobjGrid.Columns.AddNumericColumn(0, "Valor unitario", "tcnUnit_value", 18, "", False, "Valor del repuesto indicado", True, 6,  ,  ,  , False)
	Call mobjGrid.Columns.AddNumericColumn(0, "Total", "tcnTotal_value", 18, "", False, "Valor total de repuestos indicados en la línea en tratamiento", True, 6,  ,  ,  , False)
	
	'+ Campos auxiliares ocultos para grabar en la tabla Buy_ord - ACM - 19/07/2002
	
	Call mobjGrid.Columns.AddHiddenColumn("tcnQuantity_parts_AUX", CStr(0))
	Call mobjGrid.Columns.AddHiddenColumn("valSpare_parts_AUX", CStr(0))
	Call mobjGrid.Columns.AddHiddenColumn("chkOriginal_spare_AUX", "")
	Call mobjGrid.Columns.AddHiddenColumn("tcnUnit_value_AUX", CStr(0))
	Call mobjGrid.Columns.AddHiddenColumn("dOrder_date", "")
	Call mobjGrid.Columns.AddHiddenColumn("sClient_AUX", "")
	Call mobjGrid.Columns.AddHiddenColumn("nID_AUX", CStr(0))
	
	With mobjGrid
		.nMainAction = Request.QueryString("nMainAction")
		.Codispl = "SI776"
		.Codisp = "SI776"
		.Top = 100
		.Height = 288
		.Width = 380
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = True
		.Columns("Sel").Disabled = True
		If mintBranch_Fire <> 1 Then
			.Columns("chkOriginal_spare").Disabled = True
		End If
		
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
		
	End With
End Sub

'%insPreSI776. Se crea la ventana madre (Principal)
'-------------------------------------------------------------------------------------------------------------
Private Sub insPreSI776()
	'-------------------------------------------------------------------------------------------------------------
	Dim lclsBuy_Orders As eClaim.Buy_ord
	Dim lintCount As Integer
	Dim ldblTotal_Spares As Double
	Dim ldblIVA As Double
	Dim lblnCreateClient As Boolean
	Dim lclsQuot_parts As eClaim.Quot_parts
	Dim lclsProf_ord As eClaim.Prof_ord
	Dim ldblSendCost As Object
	Dim ldblFreightage As Integer
	Dim lclsFire_budgets As Object
	Dim lclsFire_budget As Object
	
	lclsQuot_parts = New eClaim.Quot_parts
	
	lblnCreateClient = False
	ldblTotal_Spares = 0
	ldblFreightage = 0
	
	lclsBuy_Orders = New eClaim.Buy_ord
	Call lclsBuy_Orders.LocateProvider(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble))
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Proveedor</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	With mobjValues
		.Parameters.ReturnValue("sClient",  ,  , True)
		.Parameters.ReturnValue("sClieName",  ,  , True)
		.Parameters.ReturnValue("sDigit",  ,  , True)
		Response.Write(mobjValues.PossiblesValues("cbeProvider", "tabtab_providersi012", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsBuy_Orders.nProvider),  ,  ,  ,  ,  , "SetValues(""2"");", False,  , "Código del proveedor"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>RUT</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.ClientControl("tctClientCode", lclsBuy_Orders.sClient,  , "RUT (código de cliente) asociado al proveedor", "SetValues(""3"");", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>Dirección</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.AnimatedButtonControl("cmdAddress", "/VTimeNet/images/ShowAddress.png", "Dirección del proveedor",  , "ShowAddress();"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>    " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41077>Dirección del despacho</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""Horline""></TD>		" & vbCrLf)
Response.Write("        </TR>		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Nombre</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctName_Cont", 60,  , False, "Nombre del contacto", False, "", "", "", False, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Dirección</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextAreaControl("tctAdd_Contact", 2, 50, "",  , "Dirección del despacho"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Región</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "insParameterLocat(this)",  ,  , "Región donde debe realizarse el despacho"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Ciudad</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valLocal", "tabTab_locat_a", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "insParameterMunicipality(this)",  ,  , "Ciudad donde debe realizarse el despacho"))
Response.Write("" & vbCrLf)
Response.Write("            </TD>			" & vbCrLf)
Response.Write("        <TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Comuna</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")

	mobjValues.Parameters.Add("nLocat", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("nLocal", False, vbNullString, True)
	Response.Write(mobjValues.PossiblesValues("valMunicipality", "tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "InsChangeMunicipality(this.value)",  ,  , "Comuna donde debe realizarse el despacho"))
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Teléfono</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctPhone_Cont", 11, lclsBuy_Orders.sPhone, True, "Número de teléfono del contacto",  ,  ,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.HiddenControl("blnCtrateClient", CStr(lblnCreateClient)))


Response.Write("" & vbCrLf)
Response.Write("        </TR>")

	
	'	If mintBranch_Fire = 1 Then
	'		Set lclsFire_budgets = Server.CreateObject("eClaim.Fire_budgets")
	'		If lclsFire_budgets.Find(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble)) Then
	'			lintCount = 1
	'			For Each lclsFire_budget In lclsFire_budgets
	'				mobjGrid.Columns("valSpare_parts_AUX").DefValue		= 999
	'				mobjGrid.Columns("tcnQuantity_parts").DefValue = lclsFire_budget.nQuantity_parts
	'				mobjGrid.Columns("tcnUnit_value").DefValue = lclsFire_budget.nAmount_parts
	'				mobjGrid.Columns("tcnTotal_value").DefValue = lclsFire_budget.nAmount_parts * lclsQuot_parts.nQuantity_parts 
	'				mobjGrid.Columns("tctItem").DefValue = lclsFire_budget.sItem
	'				mobjGrid.Columns("tcnQuantity_parts_AUX").DefValue	= lclsFire_budget.nQuantity_parts
	'				mobjGrid.Columns("tcnUnit_value_AUX").DefValue		= lclsFire_budget.nAmount_Parts
	'				mobjGrid.Columns("dOrder_date").DefValue			= lclsFire_budget.dBudg_date
	'				mobjGrid.Columns("nID_AUX").DefValue				= lclsFire_budget.nId
	'				
	'				ldblTotal_Spares = ldblTotal_Spares + (lclsFire_budget.nAmount_Parts * lclsFire_budget.nQuantity_Parts)
	'				Response.Write mobjGrid.DoRow()
	'				lintCount = lintCount + 1
	'			Next
	'		End If
	'		Set lclsFire_budgets = Nothing
	'		Set lclsFire_budget = Nothing
	'	Else
	If lclsQuot_parts.Find(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble), CStr(eRemoteDB.Constants.intNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, System.Date.FromOADate(eRemoteDB.Constants.intNull), "SI776") Then
		For lintCount = 1 To lclsQuot_parts.CountQuot_parts
			
			If lclsQuot_parts.Item(lintCount) Then
				mobjGrid.Columns("tcnQuantity_parts").DefValue = CStr(lclsQuot_parts.nQuantity_Parts)
				mobjGrid.Columns("valSpare_parts").DefValue = CStr(lclsQuot_parts.nAuto_parts)
				
				mobjGrid.Columns("tcnUnit_value").DefValue = CStr(lclsQuot_parts.nAmount_Part)
				mobjGrid.Columns("tcnTotal_value").DefValue = CStr(lclsQuot_parts.nAmount_Part * lclsQuot_parts.nQuantity_Parts)
				
				mobjGrid.Columns("tcnQuantity_parts_AUX").DefValue = CStr(lclsQuot_parts.nQuantity_Parts)
				mobjGrid.Columns("valSpare_parts_AUX").DefValue = CStr(lclsQuot_parts.nAuto_parts)
				
				mobjGrid.Columns("tcnUnit_value_AUX").DefValue = CStr(lclsQuot_parts.nAmount_Part)
				mobjGrid.Columns("dOrder_date").DefValue = CStr(lclsQuot_parts.dQuot_Date)
				mobjGrid.Columns("nID_AUX").DefValue = CStr(lclsQuot_parts.nID)
				
				ldblTotal_Spares = ldblTotal_Spares + (lclsQuot_parts.nAmount_Part * lclsQuot_parts.nQuantity_Parts)
				Response.Write(mobjGrid.DoRow())
			End If
		Next 
	End If
	'	End If
	Response.Write(mobjGrid.CloseTable())
	
	Response.Write(mobjValues.HiddenControl("tcnClaim", Request.QueryString("nClaim")))
	Response.Write(mobjValues.HiddenControl("tcnCase", Request.QueryString("nCase")))
	Response.Write(mobjValues.HiddenControl("tcnDeman_type", Request.QueryString("nDeman_type")))
	Response.Write(mobjValues.HiddenControl("tcnServ_Order", Request.QueryString("nServiceOrder")))
	
	lclsProf_ord = New eClaim.Prof_ord
	If lclsProf_ord.Find_nServ(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble)) Then
		ldblIVA = mobjValues.StringToType(CStr(lclsProf_ord.nIva), eFunctions.Values.eTypeData.etdDouble, True)
		ldblSendCost = mobjValues.StringToType(CStr(lclsProf_ord.nSendCost), eFunctions.Values.eTypeData.etdDouble)
		ldblFreightage = mobjValues.StringToType(CStr(lclsProf_ord.nFreightage), eFunctions.Values.eTypeData.etdDouble, True)
		If ldblIVA = eRemoteDB.Constants.intNull Then
			ldblIVA = 0
		End If
		If ldblFreightage = eRemoteDB.Constants.intNull Then
			ldblFreightage = 0
		End If
	Else
		ldblIVA = 0
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Total neto</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnNet_Total", 18, CStr(ldblTotal_Spares), False, "Valor total de los repuestos incluídos en la cotización", True, 6, False, "", "", "", True,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>I.V.A.</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnIVA", 5, CStr(ldblIVA), False, "Porcentaje de impuesto", True, 2, False, "", "", "", True,  ,  , False))


Response.Write("</TD>")

	
	If ldblIVA > 0 Then
		ldblIVA = ldblIVA / 100
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			<TD><LABEL>Costo de envío</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnSendCost", 18, ldblSendCost, False, "Costo por el envío de los repuestos", True, 6, False, "", "",  , True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Total</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnTotal", 18, CStr((ldblTotal_Spares * ldblIVA) + ldblTotal_Spares), False, "Monto total de la orden de compra", True, 6, False, "", "", "", True,  ,  , False))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>Flete</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnFreightage", 18, CStr(ldblFreightage), False, "Costos de envío no cargados al proveedor", True, 6, False, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("")

	
	If lclsBuy_Orders.nProvider <> eRemoteDB.Constants.intNull Then
		Response.Write("<SCRIPT>SetValues(""1"");</" & "Script>")
	End If
	
	'UPGRADE_NOTE: Object lclsBuy_Orders may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsBuy_Orders = Nothing
	'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsProf_ord = Nothing
	'UPGRADE_NOTE: Object lclsQuot_parts may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsQuot_parts = Nothing
End Sub

</script>
<%Response.Expires = -1
'- mintBranch_Fire = 1 -> Pertenece a un producto de incendio
'- mintBranch_Fire = 0 -> No pertenece a un producto de incendio
If Request.QueryString("nBranch_Fire") = "" Then
	mintBranch_Fire = 0
Else
	mintBranch_Fire = Request.QueryString("nBranch_Fire")
End If

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "si776"
%>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->


<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 12.31 $|$$Author: Nvaplat60 $"
    
//%ShowAddress: LLama la ventana de direcciones con el RUT del proveedor
//---------------------------------------------------------------------------------------------------    
function ShowAddress(){
//---------------------------------------------------------------------------------------------------    
    ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA101&sOnSeq=2&sClient=' + self.document.forms[0].tctClientCode.value,'ShowAddress',500,500,'yes','yes','no','no')
}    

//%insParameterLocat: Actualiza parametros de la region
//---------------------------------------------------------------------------
function insParameterLocat(Field){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		valLocal.Parameters.Param1.sValue=Field.value;
		valMunicipality.Parameters.Param1.sValue=0;
		valMunicipality.Parameters.Param2.sValue=Field.value;		
		valLocal.disabled=(Field.value=='')?true:false;
		valLocal.value='';
		UpdateDiv('valLocalDesc','')
		valMunicipality.value='';
		UpdateDiv('valMunicipalityDesc','')
	}
	
}	
//%insParameterMunicipality: Actualiza los parámetros de la comuna
//---------------------------------------------------------------------------
function insParameterMunicipality(Field){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		valMunicipality.Parameters.Param1.sValue=Field.value;
		valMunicipality.Parameters.Param2.sValue=cbeProvince.value;
		
		if (Field.value == ''){
		valMunicipality.Parameters.Param1.sValue=0;
		}
		
		valMunicipality.disabled=(Field.value=='')?true:false;
		if(valMunicipality_nLocal.value!=Field.value){
			valMunicipality.value='';
			UpdateDiv('valMunicipalityDesc','')
		}
	}
}	
    
//%InsChangeMunicipality: Busca la ciudad y la región dada la comuna
//-------------------------------------------------------------------------------------------	
function InsChangeMunicipality(nMunicipality){
//-------------------------------------------------------------------------------------------	
    insDefValues('Municipality', 'nMunicipality=' + nMunicipality)
    
}       

//%SetValues: Asignan e inhabilita/habilita los campos segun los valores de "Proveedor" y "Cliente"
//-------------------------------------------------------------------------------------------	
function SetValues(Option){
//-------------------------------------------------------------------------------------------	    
    switch(Option){
        case "1":
        {
            with(self.document.forms[0]){
                cbeProvider.disabled = true;
                btncbeProvider.disabled = true;
                tctClientCode.value = cbeProvider_sClient.value;
                tctClientCode_Digit.value = cbeProvider_sDigit.value;        
                UpdateDiv('tctClientCode_Name',cbeProvider_sClieName.value,'Normal');        
                tctClientCode.disabled = true;
                tctClientCode_Digit.disabled = true;
                btntctClientCode.disabled = true;
            }
            break;
        }    
        
        case "2":
        {
            with(self.document.forms[0]){
                if(cbeProvider.value!="" && cbeProvider.value!=0){
                    tctClientCode.value = cbeProvider_sClient.value;
                    tctClientCode_Digit.value = cbeProvider_sDigit.value;        
                    UpdateDiv('tctClientCode_Name',cbeProvider_sClieName.value,'Normal');
                    tctClientCode.disabled = true;
                    tctClientCode_Digit.disabled = true;
                    btntctClientCode.disabled = true;                    
                }   
                else {
                    tctClientCode.value = '';
                    tctClientCode_Digit.value = '';
                    UpdateDiv('tctClientCode_Name','','Normal');
                    tctClientCode.disabled = false;
                    tctClientCode_Digit.disabled = false;
                    btntctClientCode.disabled = false;
                }
            }
            break;    
        }        
    }        
}
</SCRIPT>

<HTML>
  <HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
mobjValues.ActionQuery = (Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery)
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString("nMainAction")) & "</SCRIPT>")
	If Request.QueryString("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "SI776", "SI776.aspx"))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmSI776" ACTION="ValClaim.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))
Call insDefineHeader()
Call insPreSI776()
%>	  
</FORM>
</BODY>
</HTML>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing

%>




