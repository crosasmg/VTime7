<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mclsGen_cover As eProduct.Gen_cover
Dim mblnCapitalBasic As Boolean


'%insPreDP052A:función que realiza el llenado de cada uno de los campos de la transacción
'%en caso de existir previamente el registro en la tabla Gen_cover.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP052A1()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("				")

	
	If mclsGen_cover.sRoucapit <> vbNullString Then
		Response.Write(mobjValues.OptionControl(0, "optCapital", GetLocalResourceObject("optCapital_CStr1Caption"), CStr(1), CStr(1), "insReload(1)",  ,  , GetLocalResourceObject("optCapital_CStr1ToolTip")))
	Else
		Response.Write(mobjValues.OptionControl(0, "optCapital", GetLocalResourceObject("optCapital_CStr1Caption"),  , CStr(1), "insReload(1)",  ,  , GetLocalResourceObject("optCapital_CStr1ToolTip")))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14593>" & GetLocalResourceObject("tctCapitalRouCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctCapitalRou", 12, mclsGen_cover.sRoucapit,  , GetLocalResourceObject("tctCapitalRouToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optCapital", GetLocalResourceObject("optCapital_CStr2Caption"), mclsGen_cover.sCacalfri, CStr(2), "insReload(2)",  ,  , GetLocalResourceObject("optCapital_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optCapital", GetLocalResourceObject("optCapital_CStr3Caption"), mclsGen_cover.sCacalili, CStr(3), "insReload(3)",  ,  , GetLocalResourceObject("optCapital_CStr3ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optCapital", GetLocalResourceObject("optCapital_CStr4Caption"), mclsGen_cover.DefaultValueDP052A("optCapitalFix"), CStr(4), "insReload(4)",  ,  , GetLocalResourceObject("optCapital_CStr4ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14592>" & GetLocalResourceObject("tcnCapitalFixCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnCapitalFix", 18, CStr(mclsGen_cover.nCacalfix),  , GetLocalResourceObject("tcnCapitalFixToolTip"), True, 6,  ,  ,  ,  , CBool(mclsGen_cover.DefaultValueDP052A("tcnCapitalFix.disabled"))))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(0, "optCapital", GetLocalResourceObject("optCapital_CStr5Caption"), mclsGen_cover.DefaultValueDP052A("optOtherCover"), CStr(5), "insReload(5)",  ,  , GetLocalResourceObject("optCapital_CStr5ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14595>" & GetLocalResourceObject("tcnOtherCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnOtherCover", 5, CStr(mclsGen_cover.nCacalper),  , GetLocalResourceObject("tcnOtherCoverToolTip"), True, 2,  ,  ,  ,  , CBool(mclsGen_cover.DefaultValueDP052A("tcnOtherCover.disabled"))))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14594>" & GetLocalResourceObject("valOtherCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

	
	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCover", Session("nCover"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nModulec", Session("nModulec"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("valOtherCover", "tabgen_cover2", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsGen_cover.nCacalcov), True,  ,  ,  ,  ,  , CBool(mclsGen_cover.DefaultValueDP052A("valOtherCover.disabled")),  , GetLocalResourceObject("valOtherCoverToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	Call insPreDP052A()
End Sub
'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Call mclsGen_cover.insPreDP052A(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	With mclsGen_cover
		If .sCacalfri = "2" And .sCacalili = "2" And .sRoucapit = vbNullString And CDbl(.DefaultValueDP052A("optCapitalFix")) = 2 And CDbl(.DefaultValueDP052A("optOtherCover")) = 2 Then
			Session("CheckedCapBas") = 1
		Else
			Session("CheckedCapBas") = 2
		End If
	End With
	If Request.QueryString.Item("nCapital") = "6" Then
		Session("CheckedCapBas") = 1
	ElseIf Request.QueryString.Item("nCapital") <> vbNullString Then 
		Session("CheckedCapBas") = 2
	End If
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 14, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnSumins_ratColumnCaption"), "tcnSumins_rat", 5, CStr(0),  , GetLocalResourceObject("tcnSumins_ratColumnToolTip"), True, 2)
		Call .AddHiddenColumn("tctAuxDescript", CStr(eRemoteDB.Constants.strnull))
		Call .AddHiddenColumn("tcnAuxSumins_rat", CStr(0))
		Call .AddHiddenColumn("tcnSumins_co", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP052A"
		.DeleteButton = False
		.AddButton = False
		
		.Columns("Sel").GridVisible = False
		.Columns("tctDescript").EditRecord = Session("CheckedCapBas") = 1
		.bOnlyForQuery = Session("bQuery")
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
	End With
End Sub
'% insPreDP052A: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP052A()
	'--------------------------------------------------------------------------------------------
	Dim lclsBas_sumins As eProduct.Bas_sumins
	Dim lcolBas_suminses As eProduct.Bas_suminses
	Dim lblnBasic As Object
	Dim lintchecked As Object
	Dim lblnCapital As Object
	Dim lblCapBas As Boolean
	lclsBas_sumins = New eProduct.Bas_sumins
	lcolBas_suminses = New eProduct.Bas_suminses
	lintchecked = Session("CheckedCapBas")
	If Session("CheckedCapBas") = 1 Then
		lblCapBas = True
	Else
		lblCapBas = False
	End If
	If lcolBas_suminses.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True, "DP052A", mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), lblCapBas) Then
		
		
		
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TD WIDTH=""30%"">")


Response.Write(mobjValues.OptionControl(0, "optCapital", GetLocalResourceObject("optCapital_CStr6Caption"), lintchecked, CStr(6), "insReload(6)",  ,  , GetLocalResourceObject("optCapital_CStr6ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD WIDTH=""70%"">" & vbCrLf)
Response.Write("			<DIV>" & vbCrLf)
Response.Write("			")

		
		For	Each lclsBas_sumins In lcolBas_suminses
			With mobjGrid
				.Columns("tctDescript").DefValue = lclsBas_sumins.sDescript
				.Columns("tcnSumins_rat").DefValue = CStr(lclsBas_sumins.nSumins_rat)
				.Columns("tctAuxDescript").DefValue = lclsBas_sumins.sDescript
				.Columns("tcnAuxSumins_rat").DefValue = CStr(lclsBas_sumins.nSumins_rat)
				.Columns("tcnSumins_co").DefValue = CStr(lclsBas_sumins.nSumins_co)
				Response.Write(.DoRow)
			End With
		Next lclsBas_sumins
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	")

		
	End If
	Response.Write(mobjGrid.closeTable())
	lclsBas_sumins = Nothing
	lcolBas_suminses = Nothing
End Sub
'% insPreDP052AUpd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP052AUpd()
	Dim lbnCapitalBsic As Object
	mblnCapitalBasic = True
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoverSeq.aspx", "DP052A", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
mclsGen_cover = New eProduct.Gen_cover
mobjValues.ActionQuery = Session("bQuery")

If CDbl(Request.QueryString.Item("nCapital")) <> 6 And Request.QueryString.Item("nCapital") <> vbNullString Then
	mobjGrid.ActionQuery = Not Session("bQuery")
ElseIf Session("CheckedCapBas") = 1 Or Request.QueryString.Item("nCapital") = vbNullString Then 
	mobjGrid.ActionQuery = Session("bQuery")
End If

mobjValues.sCodisplPage = "dp052a"

mobjGrid.sCodisplPage = "dp052a"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP052"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjValues.ShowWindowsName("DP052A"))
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP052A", "DP052A.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>

//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:04 $|$$Author: Nvaplat61 $"

//% insReload: Setea la variable blnCapitalBasic, para determinar si es capital básico
//%------------------------------------------------------------------------------------------
function insReload(nField){
//%------------------------------------------------------------------------------------------
	var lstrhref="";
	var lintCapital ='<%=Request.QueryString.Item("nCapital")%>';
	var blnCapitalBasic = (nField==6)?"true":"false";
	if(nField!=lintCapital){
		lstrhref="DP052A.aspx?sCodispl=DP052A&nMainAction=<%=Request.QueryString.Item("nMainAction")%>&blnCapitalBasic=" + blnCapitalBasic + "&nCapital=" + nField;
		self.document.location.href = lstrhref;
	}
}
//%insEnabledFields : Habilita o inhabilita los campos
//-------------------------------------------------------------------------------------------
function insEnabledFields(nField){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0])
	{
		switch(nField)
		{
//+Libre
            case 2:
			{
				optCapital[1].checked=true;
				tcnCapitalFix.disabled=true;
				tcnOtherCover.disabled=true;
				valOtherCover.disabled=true;
				btnvalOtherCover.disabled=true; 
				tcnCapitalFix.value=0;
				tcnOtherCover.value=0;
				valOtherCover.value=0;
				UpdateDiv("valOtherCoverDesc", "")
				tctCapitalRou.value = "";
				tctCapitalRou.disabled = true;
				break;
			}
//+Ilimitado
            case 3:
			{
				optCapital[2].checked=true;
				tcnCapitalFix.disabled=true;
				tcnOtherCover.disabled=true;
				valOtherCover.disabled=true;
				btnvalOtherCover.disabled=true;
				tcnCapitalFix.value=0;
				tcnOtherCover.value=0;
				valOtherCover.value=0;
				UpdateDiv("valOtherCoverDesc", "")
				tctCapitalRou.value = "";
				tctCapitalRou.disabled = true;
				break;
			}
//+Fijo
			case 4:
			{
				optCapital[3].checked=true;
				tcnCapitalFix.disabled=false;
				tcnOtherCover.disabled=true;
				valOtherCover.disabled=true;
				btnvalOtherCover.disabled=true;
				tcnOtherCover.value=0;
				valOtherCover.value=0;
				UpdateDiv("valOtherCoverDesc", "")
				tctCapitalRou.value = "";
				tctCapitalRou.disabled = true;
				break;
			}
//+Otra Cobertura
			case 5:
			{
				optCapital[4].checked=true;
				tcnOtherCover.disabled=false;
				valOtherCover.disabled=false;
				btnvalOtherCover.disabled=false;
				tcnCapitalFix.value=0;
				tcnCapitalFix.disabled=true;
				tctCapitalRou.value = "";
				tctCapitalRou.disabled = true;
				break;
			}
//+Capitales básicos
            case 6:
            {
				optCapital[5].checked=true;
				tcnCapitalFix.disabled=true;
				tcnOtherCover.disabled=true;
				valOtherCover.disabled=true;
				btnvalOtherCover.disabled=true;
				tcnCapitalFix.value=0;
				tcnOtherCover.value=0;
				valOtherCover.value=0;
				UpdateDiv("valOtherCoverDesc", "")
				tctCapitalRou.value = "";
				tctCapitalRou.disabled = true;
				break;
			}
//+Sólo rutina
			default:
			{
				tcnCapitalFix.disabled=true;
				tcnOtherCover.disabled=true;
				valOtherCover.disabled=true;
				btnvalOtherCover.disabled=true;
				tcnCapitalFix.value=0;
				tcnOtherCover.value=0;
				valOtherCover.value=0;
				UpdateDiv("valOtherCoverDesc", "")
				if(nField==1)
					optCapital[0].checked=true;
				if(nField==2)
					optCapital[1].checked=true;
				if(nField==3)
					optCapital[2].checked=true;
			}
		}
	}
}

</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP052A" ACTION="valCoverSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP052A1()
Else
	Call insPreDP052AUpd()
End If
%>
</FORM>
</HTML>
<%
If Request.QueryString.Item("nCapital") <> vbNullString Then
	Response.Write("<SCRIPT>insEnabledFields(" & Request.QueryString.Item("nCapital") & ");</SCRIPT>")
End If
%>
<%
mobjValues = Nothing
mobjGrid = Nothing
mclsGen_cover = Nothing

%>




