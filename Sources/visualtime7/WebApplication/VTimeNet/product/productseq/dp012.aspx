<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

Dim mclsSequen_pol As eProduct.Sequen_pol


'% insDefineHeader: Se definen las características del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	mobjGrid.sCodisplPage = "DP012"
	
	With mobjGrid.Columns
		Call .AddTextColumn(41293, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddCheckColumn(41294, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString,  , CStr(1),  ,  , GetLocalResourceObject("chkRequireColumnToolTip"))
		Call .AddHiddenColumn("hddSequence", CStr(0))
		Call .AddHiddenColumn("hddAuxRequire", CStr(2))
		Call .AddHiddenColumn("hddSel", CStr(2))
		Call .AddHiddenColumn("hddCodispl", vbNullString)
		Call .AddHiddenColumn("hddAutomatic", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP012"
		.DeleteButton = False
		.AddButton = False
		.ActionQuery = Session("bQuery") Or mclsSequen_pol.bError
		.bOnlyForQuery = .ActionQuery
		.Columns("Sel").OnClick = "InsSelected(this.value, this.checked)"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP012: muestra los datos en la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP012()
	'--------------------------------------------------------------------------------------------
	Dim lclsSequen_pol As eProduct.Sequen_pol
	Dim lcolSequen_pols As eProduct.Sequen_pols
	Dim lclsErrors As eFunctions.Errors
	Dim lintIndex As Short
	
	lclsSequen_pol = New eProduct.Sequen_pol
	lcolSequen_pols = New eProduct.Sequen_pols
	
	Response.Write(mobjValues.HiddenControl("hddMassive", "1"))
	
Response.Write("  " & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted""><LABEL ID=41277>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted""><LABEL ID=41278>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD CLASS=""HighLighted""><LABEL ID=41279>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(41281, "optBussityp", GetLocalResourceObject("optBussityp_CStr1Caption"), mclsSequen_pol.DefaultValueDP012("optDir_value"), CStr(1), "LoadSeqTratPol(0)",  , 1, GetLocalResourceObject("optBussityp_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(41282, "optPolitype", GetLocalResourceObject("optPolitype_CStr1Caption"), mclsSequen_pol.DefaultValueDP012("optInd_value"), CStr(1), "LoadSeqTratPol(0)", mclsSequen_pol.DefaultValueDP012("optInd_disabled"), 4, GetLocalResourceObject("optPolitype_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(41283, "optCompon", GetLocalResourceObject("optCompon_CStr1Caption"), mclsSequen_pol.DefaultValueDP012("optPol_value"), CStr(1), "LoadSeqTratPol(0)", mclsSequen_pol.DefaultValueDP012("optPol_disabled"), 7, GetLocalResourceObject("optCompon_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>    " & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(41285, "optBussityp", GetLocalResourceObject("optBussityp_CStr2Caption"), mclsSequen_pol.DefaultValueDP012("optCoa_value"), CStr(2), "LoadSeqTratPol(1)",  , 2, GetLocalResourceObject("optBussityp_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(41286, "optPolitype", GetLocalResourceObject("optPolitype_CStr2Caption"), mclsSequen_pol.DefaultValueDP012("optCol_value"), CStr(2), "LoadSeqTratPol(1)", mclsSequen_pol.DefaultValueDP012("optCol_disabled"), 5, GetLocalResourceObject("optPolitype_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(41287, "optCompon", GetLocalResourceObject("optCompon_CStr2Caption"), mclsSequen_pol.DefaultValueDP012("optCert_value"), CStr(2), "LoadSeqTratPol(1)", mclsSequen_pol.DefaultValueDP012("optCert_disabled"), 8, GetLocalResourceObject("optCompon_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(41289, "optBussityp", GetLocalResourceObject("optBussityp_CStr3Caption"), mclsSequen_pol.DefaultValueDP012("optRea_value"), CStr(3), "LoadSeqTratPol(2)",  , 3, GetLocalResourceObject("optBussityp_CStr3ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.OptionControl(41290, "optPolitype", GetLocalResourceObject("optPolitype_CStr3Caption"), mclsSequen_pol.DefaultValueDP012("optMul_value"), CStr(3), "LoadSeqTratPol(2)", mclsSequen_pol.DefaultValueDP012("optMul_disabled"), 6, GetLocalResourceObject("optPolitype_CStr3ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=20%><LABEL ID=0>" & GetLocalResourceObject("cbeTransactionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=70%>")

	
	mobjValues.BlankPosition = False
	mobjValues.TypeList = 2
	mobjValues.List = "5"
	Response.Write(mobjValues.PossiblesValues("cbeTransaction", "Table5588", eFunctions.Values.eValuesType.clngComboType, CStr(mclsSequen_pol.nTratypep),  ,  ,  ,  ,  , "LoadSeqTratPol()", mclsSequen_pol.bError,  , GetLocalResourceObject("cbeTransactionToolTip"),  , 9))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD WIDTH=20%><DIV ID=""divType_amend""  style=""display:none""><LABEL ID=11509>" & GetLocalResourceObject("cbeType_AmendCaption") & "</LABEL></DIV></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=20%><DIV ID=""divType_amend2"" style=""display:none"">")

	
	Response.Write(mobjValues.PossiblesValues("cbeType_Amend", "Table6059", eFunctions.Values.eValuesType.clngComboType, CStr(mclsSequen_pol.nType_Amend),  ,  ,  ,  ,  , "LoadSeqTratPol()", False,  , GetLocalResourceObject("cbeType_AmendToolTip"),  , 1))
	
Response.Write("" & vbCrLf)
Response.Write("			</DIV></TD>" & vbCrLf)
Response.Write("            ")

	mobjValues.ActionQuery = Session("bQuery") Or mclsSequen_pol.bError
	If Not mobjValues.ActionQuery Then
		Response.Write("<TD WIDTH=""10%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
	End If
	
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("	<BR>")

	
	With Response
		'+ Variables JScript para almacenar la última condición de búsqueda de la página
		.Write("<SCRIPT>")
		.Write("var mstrBussityp = '" & mclsSequen_pol.sBussityp & "';")
		.Write("var mstrPolitype = '" & mclsSequen_pol.sPolitype & "';")
		.Write("var mstrCompon = '" & mclsSequen_pol.sCompon & "';")
		.Write("var mintTratypep = '" & mclsSequen_pol.nTratypep & "';")
		.Write("var mintType_Amend = '" & mclsSequen_pol.nType_Amend & "';")
		.Write("ShowType_Amend(self.document.forms[0].cbeTransaction);")
		.Write("</" & "Script>")
	End With
	
	If mclsSequen_pol.bError Then
		lclsErrors = New eFunctions.Errors
		Response.Write(mobjGrid.closeTable())
		Response.Write(lclsErrors.ErrorMessage("DP012", 11349,  ,  ,  , True))
	Else
		If lcolSequen_pols.Find_Tab_winpol(Session("nBranch"), Session("nProduct"), mclsSequen_pol.sBussityp, mclsSequen_pol.nTratypep, mclsSequen_pol.sPolitype, mclsSequen_pol.sCompon, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate),  , Session("sBrancht"), mclsSequen_pol.nType_Amend) Then
			lintIndex = 0
			For	Each lclsSequen_pol In lcolSequen_pols
				With mobjGrid
					.Columns("tctDescript").DefValue = lclsSequen_pol.sDescript
					.Columns("hddAuxRequire").DefValue = CStr(2)
					.Columns("Sel").Checked = 2
					.Columns("hddSel").DefValue = CStr(2)
					If lclsSequen_pol.sRequirePol <> vbNullString Then
						.Columns("Sel").Checked = 1
						.Columns("hddSel").DefValue = CStr(1)
						If lclsSequen_pol.sRequirePol = "1" Then
							.Columns("hddAuxRequire").DefValue = CStr(1)
						End If
					End If
					.Columns("hddSequence").DefValue = CStr(lclsSequen_pol.nSequence)
					.Columns("hddCodispl").DefValue = lclsSequen_pol.sCodispl
					.Columns("chkRequire").Checked = mobjValues.StringToType(lclsSequen_pol.sRequirePol, eFunctions.Values.eTypeData.etdDouble)
					.Columns("chkRequire").OnClick = "checkValue(" & lintIndex & ", this.checked)"
					.Columns("hddAutomatic").DefValue = lclsSequen_pol.sAutomatic
					Response.Write(.DoRow)
				End With
				lintIndex = lintIndex + 1
			Next lclsSequen_pol
		End If
		Response.Write(mobjGrid.closeTable())
		Response.Write(mobjValues.BeginPageButton)
	End If
	
	lclsSequen_pol = Nothing
	lcolSequen_pols = Nothing
	lclsErrors = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mclsSequen_pol = New eProduct.Sequen_pol

mobjValues.sCodisplPage = "DP012"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP012", "DP012.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
	self.document.forms[0].hddMassive.value=2;
	top.frames['fraHeader'].ClientRequest(390,2);
}

//% InsSelected: Se actualiza el campo oculta imagen del campo Sel
//------------------------------------------------------------------------------------------
function InsSelected(nIndex, bChecked){
//------------------------------------------------------------------------------------------
	with(document.forms[0]){
		if(hddSel.length>0){
			hddSel[nIndex].value =(bChecked?1:2);
			if(!bChecked){
				chkRequire[nIndex].checked = false;
				hddAuxRequire[nIndex].value = 2;
			}
		}
		else {
			hddSel.value =(bChecked?1:2);
			if(!bChecked){
				chkRequire.checked = false;
				hddAuxRequire.value = 2;
			}
		}			
	}
}

//% checkValue: Si se selecciona la ventana como obligatoria, se marca por defecto la
//%			    columna Sel
//------------------------------------------------------------------------------------------
function checkValue(nIndex, bChecked){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(chkRequire.length>0){
			hddAuxRequire[nIndex].value = (bChecked?1:2);
			if(bChecked){
				Sel[nIndex].checked = true;
				hddSel[nIndex].value = 1;
			}
		}
		else{
			hddAuxRequire.value =(bChecked)?1:2;
			if (bChecked){
				Sel.checked = true;
				hddSel.value = 1;
			}
		}
    }
}

//% LoadSeqTratPol: Se recarga la página con los nuevos parámetros de búsqueda
//-------------------------------------------------------------------------------------------
function LoadSeqTratPol(nIndex){
//-------------------------------------------------------------------------------------------
	var lstrBussityp = '';
	var lstrPolitype = '';
	var lstrCompon = '';
	var lintTratypep = 1;
	var lintType_Amend = '';
	var lintIndex = 0;
	
	ShowType_Amend(self.document.forms[0].cbeTransaction);
	
	with (self.document.forms[0].elements){
        for (lintIndex=0;lintIndex<optBussityp.length;lintIndex++)
            if (optBussityp[lintIndex].checked)
                lstrBussityp = optBussityp[lintIndex].value;

        for (lintIndex=0;lintIndex<optPolitype.length;lintIndex++)
            if (optPolitype[lintIndex].checked)
                lstrPolitype = optPolitype[lintIndex].value;

		if (lstrPolitype == '1')
			lstrCompon = '1'
		else
	        for (lintIndex=0;lintIndex<optCompon.length;lintIndex++)
		        if (optCompon[lintIndex].checked)
		            lstrCompon = optCompon[lintIndex].value;

        lintTratypep = cbeTransaction.value;
        lintType_Amend = cbeType_Amend.value;

        if (mstrBussityp != lstrBussityp ||
		    mstrPolitype != lstrPolitype ||
		    mstrCompon != lstrCompon ||
		    mintTratypep != lintTratypep ||
		    mintType_Amend != lintType_Amend)
			self.document.location.href="DP012.aspx?sCodispl=DP012&sOnSeq=1&nMainAction=304&sBussityp=" + lstrBussityp + "&sPolitype=" + lstrPolitype + "&sCompon=" + lstrCompon + "&nTratypep=" + lintTratypep + "&nType_Amend=" + lintType_Amend
    }
}

//% ShowType_Amend: Muestra y oculta el campo de tipo de endoso.
//-----------------------------------------------------------------------------------------------------------------------------------
function ShowType_Amend(Field) {
//-----------------------------------------------------------------------------------------------------------------------------------
	
	if (Field.value!=2) {
	   ShowDiv('divType_amend', 'hide');
	   ShowDiv('divType_amend2', 'hide');
	   self.document.forms[0].cbeType_Amend.value = '';
	   UpdateDiv('cbeType_AmendDesc','');
	   
	}
	else {
	   ShowDiv('divType_amend', 'show');
	   ShowDiv('divType_amend2', 'show');
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP012" ACTION="valProductSeq.aspx?sContent=1">
<%Response.Write(mobjValues.ShowWindowsName("DP012"))
Call mclsSequen_pol.insPreDP012(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Request.QueryString.Item("sBussityp"), Request.QueryString.Item("sPolitype"), Request.QueryString.Item("sCompon"), CInt(Request.QueryString.Item("nTratypep")), mobjValues.StringToType(Request.QueryString.Item("nType_Amend"), eFunctions.Values.eTypeData.etdDouble))

Call insDefineHeader()
Call insPreDP012()
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




