<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddHiddenColumn("chkSel", "1")
		Call .AddNumericColumn(0, "Cantidad", "tcnQuantity", 5, "",  , "Cantidad de Vehículos a reponer", True,  ,  ,  ,  , False)
		Call .AddTextColumn(0, "Descripción", "tctDescript", 60, "",  , "Descripción del vehículo a reponer",  ,  ,  , False)
		Call .AddNumericColumn(0, "Valor unitario", "tcnAmount", 18, "",  , "Valor del vehículo a reponer", True, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, "Total", "tcnTotalAmount", 18, "",  , "Valor Total de Repuestos", True, 6,  ,  ,  , True)
		Call .AddHiddenColumn("tcnId", CStr(0))
	End With
	
	With mobjGrid
		.Codispl = "SI831"
		.Codisp = "SI831"
		.sCodisplPage = "si831"
		.DeleteButton = False
		.AddButton = False
		.ActionQuery = False
		.Columns("Sel").GridVisible = False
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = True
		.Columns("Sel").Disabled = True
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'%insPreSI831. Se crea la ventana madre (Principal)
'-------------------------------------------------------------------------------------------------------------
Private Sub insPreSI831()
	'-------------------------------------------------------------------------------------------------------------
	Dim lclsBuy_Orders As eClaim.Buy_ord
	Dim lclsQuot_Auto As eClaim.Quot_auto
	Dim lclsQuot_Autos As eClaim.Quot_autos
	Dim lclsTax_Fixval As eAgent.tax_fixval
	
	Dim ldblIva As Double
	Dim ldblTotalAmount As Double
	Dim ldblTotalAmountNet As Double
	Dim ldblTotalAmountIva As Double
	Dim lintCount As Integer
	Dim lintId As Integer
	Dim lstrAtention As String
	Dim lstrVehbrand As Integer
	Dim lstrVehmodel As String
	Dim lstrYear As Integer
	Dim lblnCreateClient As Boolean
	Dim FindAuto As Boolean
	
	lblnCreateClient = False
	
	lclsBuy_Orders = New eClaim.Buy_ord
	lclsQuot_Autos = New eClaim.Quot_autos
	Call lclsBuy_Orders.LocateProvider(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdLong))
	If Request.QueryString("nMainAction") = 401 Then
		FindAuto = lclsQuot_Autos.Find(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble), 7)
	Else
		FindAuto = lclsQuot_Autos.Find(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble), 6)
	End If
	
	If FindAuto Then
		lstrAtention = lclsQuot_Autos(1).sCliename
		lstrVehbrand = lclsQuot_Autos(1).nVehbrand
		lstrVehmodel = lclsQuot_Autos(1).sVehmodel
		lstrYear = lclsQuot_Autos(1).nYear
	End If
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=0>Proveedor</LABEL></TD>" & vbCrLf)
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
Response.Write("            ")


Response.Write(mobjValues.HiddenControl("hddClientCode", lclsBuy_Orders.sClient))


Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>Dirección</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.AnimatedButtonControl("cmdAddress", "/VTimeNet/images/ShowAddress.png", "Dirección del proveedor",  , "ShowAddress();"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Atendido por</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctAtention", 60, lstrAtention,  , "Nombre del vendedor que atendio la compra", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>    " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41077>Detalle </LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""Horline""></TD>		" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD WIDTH=""15%""><LABEL>Marca </LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeVehbrand", "table7042", eFunctions.Values.eValuesType.clngComboType, CStr(lstrVehbrand),  , True,  ,  ,  ,  ,  ,  , "Marca del vehículo seleccionado"))


Response.Write("</TD> " & vbCrLf)
Response.Write("		    <TD WIDTH=""15%""><LABEL>Modelo</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("tctVehmodel", 20, lstrVehmodel,  , "Modelo del vehículo", True))


Response.Write("</TD> " & vbCrLf)
Response.Write("		    <TD WIDTH=""15%""><LABEL>Año</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnyear", 4, CStr(lstrYear),  , "Año del Vehiculo",  ,  , True))


Response.Write("</TD> " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<BR>")

	
	If FindAuto Then
		lintCount = lclsQuot_Autos.Count
		ldblTotalAmountNet = 0
		For	Each lclsQuot_Auto In lclsQuot_Autos
			If lclsQuot_Auto.sSel = "1" Then
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
				End With
				ldblTotalAmountNet = ldblTotalAmountNet + ldblTotalAmount
				lintId = lclsQuot_Auto.nId
				Response.Write(mobjGrid.DoRow())
			End If
		Next lclsQuot_Auto
	End If
	Response.Write(mobjGrid.CloseTable())
	
	'+ Se obtiene el porcentaje fijo de IVA (Tabla Tax_Fixval) 
	lclsTax_Fixval = New eAgent.tax_fixval
	If lclsTax_Fixval.Find(1, Request.QueryString("dBuyDate")) Then
		ldblIva = lclsTax_Fixval.nPercent
		ldblTotalAmountIva = ldblTotalAmountNet * (1 + (ldblIva / 100))
	Else
		ldblIva = 0
		ldblTotalAmountIva = ldblTotalAmountNet
	End If
	'UPGRADE_NOTE: Object lclsTax_Fixval may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsTax_Fixval = Nothing
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL>Total Neto</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnTotalNet", 18, CStr(ldblTotalAmountNet), False, "Sumatoria de la columna de los repuestos en la cotización", True, 6, False, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>Iva</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnTotalIva", 18, CStr(ldblIva), False, "Porcentaje del Impuesto", True, 6, False, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL>Total</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnTotal", 18, CStr(ldblTotalAmountIva), False, "Monto total de la cotización", True, 6, False, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	Dim FindBuy As Boolean
	Dim lclsBuy_Auto As eClaim.Buy_Auto
	lclsBuy_Auto = New eClaim.Buy_Auto
	FindBuy = lclsBuy_Auto.Find(mobjValues.StringToType(Request.QueryString("nServiceOrder"), eFunctions.Values.eTypeData.etdDouble))
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>    " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41077>Condiciones de Compra</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""Horline""></TD>		" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR> " & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=0>Condiciones de</LABEL></TD> " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctCondic", 60, lclsBuy_Auto.sCondic,  , "Condiciones"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Compra para</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.ClientControl("tctClientCon", lclsBuy_Auto.sClient1,  , "RUT del Comprador", "ShowClientcon(this)", False, "lstrClienameCon", True,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR> " & vbCrLf)
Response.Write("            <TD WIDTH=""20%""><LABEL ID=0>Nombre</LABEL></TD> " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctNombreCon", 60, lclsBuy_Orders.sName_Cont, False, "Nombre del vendedor que atendio la compra", False,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Dirección</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctAdd_Contact", 50, lclsBuy_Orders.sAdd_Contact,  , "Dirección del Contacto",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Región</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType, CStr(lclsBuy_Orders.nProvince),  ,  ,  ,  ,  , "insParameterLocat(this)", True,  , "Región donde debe realizarse el despacho"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Ciudad</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valLocal", "tabTab_locat_a", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsBuy_Orders.nLocal), True,  ,  ,  ,  , "insParameterMunicipality(this)", True,  , "Ciudad donde debe realizarse el despacho"))
Response.Write("" & vbCrLf)
Response.Write("            </TD>			" & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Comuna</LABEL></TD> " & vbCrLf)
Response.Write("            <TD>")

	mobjValues.Parameters.Add("nLocat", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("nLocal", False, vbNullString, True)
	Response.Write(mobjValues.PossiblesValues("valMunicipality", "tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsBuy_Orders.nMunicipality), True,  ,  ,  ,  , "InsChangeMunicipality(this.value)", True,  , "Comuna donde debe realizarse el despacho"))
Response.Write(" " & vbCrLf)
Response.Write("            </TD> " & vbCrLf)
Response.Write("		</TR> " & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Teléfono</LABEL></TD> " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctPhone_Cont", 11, lclsBuy_Orders.sPhone_Cont, True, "Número de teléfono del contacto",  ,  ,  ,  , True))


Response.Write("</TD>  " & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.HiddenControl("blnCtrateClient", CStr(lblnCreateClient)))


Response.Write("  " & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("	</TABLE>  ")

	
	Response.Write(mobjValues.HiddenControl("tcnClaim", Request.QueryString("nClaim")))
	Response.Write(mobjValues.HiddenControl("tcnCase_Num", Request.QueryString("nCase_Num")))
	Response.Write(mobjValues.HiddenControl("tcnDeman_type", Request.QueryString("nDeman_type")))
	Response.Write(mobjValues.HiddenControl("tcnServ_Order", Request.QueryString("nServiceOrder")))
	Response.Write(mobjValues.HiddenControl("tcdBuyDate", Request.QueryString("dBuyDate")))
	Response.Write(mobjValues.HiddenControl("nActionOrd", Request.QueryString("nMainAction")))
	
	If lclsBuy_Orders.nProvider <> eRemoteDB.Constants.intNull Then
		Response.Write("<SCRIPT>SetValues(""1"");</" & "Script>")
	End If
	
	'UPGRADE_NOTE: Object lclsQuot_Autos may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsQuot_Autos = Nothing
	'UPGRADE_NOTE: Object lclsQuot_Auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsQuot_Auto = Nothing
	'UPGRADE_NOTE: Object lclsBuy_Orders may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsBuy_Orders = Nothing
	'UPGRADE_NOTE: Object lclsBuy_Auto may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsBuy_Auto = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "si831"
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
//+Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 10/12/03 17:28 $|$$Author: Nvaplat22 $"
    
//%ShowAddress: LLama la ventana de direcciones con el RUT del proveedor
//---------------------------------------------------------------------------------------------------    
function ShowAddress(){
//---------------------------------------------------------------------------------------------------    
    ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA101&sOnSeq=2&sClient=' + self.document.forms[0].hddClientCode.value,'ShowAddress',600,500,'yes','yes',100,100,'yes')
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
		
//		valMunicipality.disabled=(Field.value=='')?true:false;
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
				if(nMainAction!=401){
					cbeProvider.disabled = true;
					btncbeProvider.disabled = true;
					tctClientCode.value = cbeProvider_sClient.value;
					tctClientCode_Digit.value = cbeProvider_sDigit.value;        
					UpdateDiv('tctClientCode_Name',cbeProvider_sClieName.value,'Normal');        
					tctClientCode.disabled = true;
					tctClientCode_Digit.disabled = true;
					btntctClientCode.disabled = true;
				}
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

//%ShowClientcon: Asignan e inhabilita/habilita los campos segun los valores de las 
//%               condiciones para el Comprador
//-------------------------------------------------------------------------------------------	
function ShowClientcon(Field){
//-------------------------------------------------------------------------------------------	    
	var strParams; 
	with(self.document.forms[0]){
		if(tctClientCon.value!="") {
			strParams = "sClient=" + tctClientCon.value;
			insDefValues('ClientConSI831',strParams,'/VTimeNet/Claim/Claim'); 
		}
    }
}
</SCRIPT>
<%
mobjValues.ActionQuery = (Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery)
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString("nMainAction")) & "</SCRIPT>")
	If Request.QueryString("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "SI831", "SI831.aspx"))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmSI831" ACTION="ValClaim.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl")))
Call insDefineHeader()
Call insPreSI831()
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




