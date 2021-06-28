<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

'- Declaraciòn de Variables locales
Dim mblnVisible As Object
Dim mblnDisabled As Boolean

'- Declaraciòn de Variables para la recarga y bùsqueda
Dim mintTariff As Object
Dim mstrTypeExcl As Object

'- Se define variable para manejo de funciones generales	
Dim lclsGeneral As eGeneral.GeneralFunction


'% insLoadDP058: Dibuja los campos no repetitivos de la pantalla, con sus respectivos
'  valores segùn sea el caso.
'------------------------------------------------------------------------------------------
Private Sub insLoadDP058()
	'------------------------------------------------------------------------------------------
	Dim lclsTar_am_basprod As eBranches.Tar_am_basprod
	Dim lclsErrors As eFunctions.Errors
	
	lclsTar_am_basprod = New eBranches.Tar_am_basprod
	lclsErrors = New eFunctions.Errors
	
	'+ Se envía advertencia si no se ha ingresado tarifa de atención médica
	If Not lclsTar_am_basprod.Load(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		'+ Se verifica que sólo se haga la primera vez que ingresa a la ventana	                               
		If Request.QueryString.Item("mintTariff") = vbNullString Then
			lclsErrors.sTypeMessage = 2
			Response.Write(lclsErrors.ErrorMessage("DP058", 55880,  ,  ,  , True))
		End If
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41300><A NAME=""Tipo de exclusión"">" & GetLocalResourceObject("AnchorTipo de exclusiónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("		</TR>   " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeExcl", GetLocalResourceObject("optTypeExcl_CStr1Caption"),  , CStr(1), "insdisable(this);",  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">&nbsp;</TD>                       " & vbCrLf)
Response.Write("            <TD COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeExcl", GetLocalResourceObject("optTypeExcl_CStr2Caption"),  , CStr(2), "insdisable(this);",  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2""></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeTariffCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            ")

	With mobjValues
		.Parameters.Add("nBranch", .StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", .StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sCertype", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
Response.Write("" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.PossiblesValues("cbeTariff", "tabTar_am_basprod", eFunctions.Values.eValuesType.clngComboType, CStr(CShort(mintTariff)), True,  ,  ,  ,  , "LoadSeqExcEnf()", True,  , GetLocalResourceObject("cbeTariffToolTip")))
	mobjValues.ActionQuery = Session("bQuery")
Response.Write("</TD>            " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	
	lclsErrors = Nothing
	lclsTar_am_basprod = Nothing
	
End Sub

'% insOldValues: Se encarga de asignar el valor de las variables  vbscript, a las
'% variables JavaScript
'------------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mintTariff <> eRemoteDB.Constants.intNull And mstrTypeExcl <> eRemoteDB.Constants.intNull Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mintTariff         = " & mintTariff & ";")
			.Write("var mstrTypeExcl       = " & mstrTypeExcl & ";")
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mintTariff         = 0;")
			.Write("var mstrTypeExcl       = 1;")
			.Write("</" & "Script>")
		End With
		
	End If
	Response.Write("<SCRIPT>insLocked(" & mstrTypeExcl & " )</" & "Script>")
End Sub

'% insReaInitial: Se encarga de asignar el valor del queryString a las variables declaradas como vbscript
'--------------------------------------------------------------------------------------------------------
Private Function insReaInitial() As Object
	'--------------------------------------------------------------------------------------------------------
	If Request.QueryString.Item("mintTariff") = vbNullString And Request.QueryString.Item("mstrTypeExcl") = vbNullString Then
		mstrTypeExcl = 1
		mintTariff = 0
	Else
		mintTariff = Request.QueryString.Item("mintTariff")
		mstrTypeExcl = Request.QueryString.Item("mstrTypeExcl")
	End If
	
End Function

'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		If mstrTypeExcl = 2 Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDescriptColumnCaption"), "cbeDescript", "tabtab_am_ill", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , 8, GetLocalResourceObject("cbeDescriptColumnToolTip"))
			mobjGrid.Columns("cbeDescript").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeDescript").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeDescript").Parameters.Add("nPolicy", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeDescript").Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeDescript").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			mobjGrid.Columns("cbeDescript").Parameters.Add("sClient", eRemoteDB.Constants.strNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeDescriptColumnCaption"), "cbeDescript", "Tab_am_ill", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , 8, GetLocalResourceObject("cbeDescriptColumnToolTip"))
		End If
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeExc_codeColumnCaption"), "cbeExc_code", "table271", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeExc_codeColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdInit_dateColumnCaption"), "tcdInit_date", Session("dEffecdate"),  , GetLocalResourceObject("tcdInit_dateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEnd_dateColumnCaption"), "tcdEnd_date", CStr(eRemoteDB.Constants.dtmnull),  , GetLocalResourceObject("tcdEnd_dateColumnToolTip"))
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("nTariff", mintTariff)
		Call .AddHiddenColumn("sType_Excl", mstrTypeExcl)
		Call .AddHiddenColumn("dEffecdate_reg", CStr(0))
		
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("Sel").GridVisible = False
			.Columns("cbeDescript").Disabled = True
		End If
		.Columns("Sel").GridVisible = True
		.Codispl = "DP058"
		.Width = 420
		.Height = 280
        .WidthDelete = 450
		.DeleteButton = True
		.AddButton = mstrTypeExcl = "1" Or (mstrTypeExcl = "2" And CDbl(Request.QueryString.Item("mintTariff")) <> 0)
		
		If Session("bQuery") Then
			.DeleteButton = False
			.AddButton = False
			.Columns("Sel").Disabled = True
			.bOnlyForQuery = True
		Else
			.Columns("cbeDescript").EditRecord = True
		End If
		
		
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		.sEditRecordParam = "mintTariff=' + self.document.forms[0].cbeTariff.value + '" & "&mstrTypeExcl=" & mstrTypeExcl
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'% insPreDP058: Se cargan los controles de la página, tanto de la parte fija como del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreDP058()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_am_excprod As Object
	Dim lcolTab_am_excprods As eBranches.Tab_am_excprods
	Dim lintIndex As Object
	
	lcolTab_am_excprods = New eBranches.Tab_am_excprods
	
	If lcolTab_am_excprods.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintTariff, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mstrTypeExcl) Then
		
		For	Each lclsTab_am_excprod In lcolTab_am_excprods
			With mobjGrid
				.Columns("cbeDescript").DefValue = lclsTab_am_excprod.sIllness
				.Columns("cbeExc_code").DefValue = lclsTab_am_excprod.nExc_code
				.Columns("tcdInit_Date").DefValue = lclsTab_am_excprod.dInit_date
				.Columns("tcdEnd_Date").DefValue = lclsTab_am_excprod.dEnd_date
				.Columns("dEffecdate_reg").DefValue = lclsTab_am_excprod.dEffecdate_Reg
				.Columns("nTariff").DefValue = mintTariff
				.Columns("sType_Excl").DefValue = mstrTypeExcl
				.Columns("sParam").DefValue = "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&dEffecdate=" & Session("dEffecdate") & "&nTariff=" & mintTariff & "&sType_Excl=" & mstrTypeExcl & "&sdescript=" & lclsTab_am_excprod.sIllness & "&nExc_code=" & lclsTab_am_excprod.nExc_code & "&dInit_date=" & lclsTab_am_excprod.dInit_date & "&dEnd_date=" & lclsTab_am_excprod.dEnd_date & "&dEffecdate_reg=" & lclsTab_am_excprod.dEffecdate_Reg & "&nUsercode=" & Session("nUsercode")
				
				
				Response.Write(.DoRow)
			End With
		Next lclsTab_am_excprod
		
Response.Write("" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("</" & "SCRIPT>		" & vbCrLf)
Response.Write("		")

		
	End If
	Call insReaInitial()
	Response.Write(mobjGrid.closeTable())
	
	lclsTab_am_excprod = Nothing
	lcolTab_am_excprods = Nothing
End Sub


'% insPreDP058Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP058Upd()
	'--------------------------------------------------------------------------------------------
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lclsTab_am_excprod As eBranches.Tab_am_excprod
	lclsTab_am_excprod = New eBranches.Tab_am_excprod
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		Call lclsTab_am_excprod.insPostDP058("DP058", Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTariff"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDescript"), CDate(Request.QueryString.Item("dInit_date")), CDate(Request.QueryString.Item("dEnd_date")), mobjValues.StringToType(Request.QueryString.Item("nExc_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CDate(Request.QueryString.Item("dEffecdate_reg")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	lclsTab_am_excprod = Nothing
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP058", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		Response.Write(mobjValues.HiddenControl("mintTariff", .QueryString.Item("mintTariff")))
		Response.Write(mobjValues.HiddenControl("mstrTypeExcl", .QueryString.Item("mstrTypeExcl")))
		
	End With
End Sub

</script>
<%
Response.Expires = -1

lclsGeneral = New eGeneral.GeneralFunction

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjGrid.sCodisplPage = "DP058"
mobjValues.sCodisplPage = "DP058"

mobjGrid.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var mintTariff= 0</SCRIPT>")
	.Write("<SCRIPT>var mstrTypeExcl= 1</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP058", "DP058.aspx"))
		mobjMenu = Nothing
	End If
End With%>
<SCRIPT LANGUAGE="JavaScript">

//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $"

// % locked: funciòn que se encarga de seleccionar los option segùn las opciones
//-------------------------------------------------------------------------------------------
function insLocked(sOpt){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0])
    {
        switch(sOpt)
        {
			case 1:
			{	
			   
			    optTypeExcl[0].checked=true;
			    optTypeExcl[1].checked=false;
			    if (typeof(self.document.forms[0].cbeTariff)!='undefined') {
			        cbeTariff.value=0;
			        cbeTariff.disabled=true;
			    }    
				break;
			}
			case 2:
			{  
			    optTypeExcl[0].checked=false;
			    optTypeExcl[1].checked=true;
			    if (typeof(self.document.forms[0].cbeTariff)!='undefined') 
			        cbeTariff.disabled=false;
				break;
			}
		}
	}
}

// % insdisable: Actualiza la ventana y habilita el campo ntariff
//-------------------------------------------------------------------------------------------
function insdisable(sField){
//-------------------------------------------------------------------------------------------
       self.document.forms[0].cbeTariff.value=0;
       self.document.forms[0].cbeTariff.disabled=true;
       self.document.location.href="DP058.aspx?sCodispl=DP058&mintTariff=" + 0 + "&mstrTypeExcl=" + sField.value;
 
      if (sField.value==2){ 
		self.document.forms[0].cbeTariff.disabled=false;
	  }
}

// % Enabled: funciòn que inhabilita los campos segùn el resultado de la bùsqueda 
//-------------------------------------------------------------------------------------------
function Enabled(){
//-------------------------------------------------------------------------------------------
   if (typeof(self.document.forms[0].cbeTariff)!='undefined') 
      self.document.forms[0].cbeTariff.disabled=false;
}

//%InsSelected: Verifica si está seleccionado el ckeck
//------------------------------------------------------------------------------------------
function InsSelected(nIndex, bChecked){
//------------------------------------------------------------------------------------------
	if(document.forms[0].sAuxSel.length>0){
		document.forms[0].sAuxSel[nIndex].value =(bChecked?1:2);
		document.forms[0].chkRequire[nIndex].checked = (document.forms[0].sAuxSel[nIndex].checked?true:false);
	}
	else 
	{	document.forms[0].sAuxSel.value =(bChecked?1:2);}
    
    if (bChecked )
		self.document.forms[0].tcnCountReg.value++ ;
	else
		self.document.forms[0].tcnCountReg.value--;
			
}

//%checkValue: Asigna valores al ckeck
//------------------------------------------------------------------------------------------
function checkValue(Field){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (!Sel[Field.value].checked)
		{ tcnCountReg.value++ }
		if(chkAuxRequire.length>0){
			chkAuxRequire[Field.value].value=(Field.checked?1:2);
			if (Field.checked) {
				Sel[Field.value].checked=true;
				sAuxSel[Field.value].value =(Field.checked?1:2);
			}
		}
		else {
			chkAuxRequire.value=(Field.checked)?1:2;
			if (Field.checked) {
				Sel.checked=true;
				InsSelected(Field.value,true);
			}
		}
    }
}
//% insCancel: Se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

// % LoadSeqTratPol: Se encarga de recargar la pàgina y enviar a variables tipo QueryString
// el valor introducido por el usuario al momento de ejecuratse la funciòn
//-------------------------------------------------------------------------------------------
function LoadSeqExcEnf(Field){
//-------------------------------------------------------------------------------------------
var OptExclutype;

   if (self.document.forms[0].optTypeExcl[0].checked)
       OptExclutype="1";
   else 
       OptExclutype="2";
	    self.document.location.href="DP058.aspx?sCodispl=DP058&mintTariff=" + self.document.forms[0].cbeTariff.value + "&mstrTypeExcl=" + OptExclutype
	    
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<%Response.Write(mobjValues.ShowWindowsName("DP058"))%>
	<FORM METHOD="POST" ID="FORM" NAME="frmDP058" ACTION="valProductSeq.aspx?sContent=1">
	    <TABLE WIDTH="100%">
			<%If Request.QueryString.Item("Action") = "Update" Then
	mblnDisabled = True
End If

Call insReaInitial()
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP058Upd()
Else
	Call insLoadDP058()
	Call insPreDP058()
	Call insOldValues()
End If%>
	    </TABLE>
	</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing

%>




