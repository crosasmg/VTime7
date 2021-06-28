<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjnAply As Boolean

Dim mstrNoteDescript As String


'% insPreDP08B1: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP08B1()
	'--------------------------------------------------------------------------------------------
	Dim lclsDisco_exp As eProduct.Disco_expr
	Dim lcolDisco_exp As eProduct.Disco_exprs
	Dim sRequire As Object
	Dim sDefaulti As Object
	Dim sProrate As Object
	Dim sDevoallo As Object
	Dim lcolNotes As eGeneralForm.Notess
	
	lclsDisco_exp = New eProduct.Disco_expr
	lcolDisco_exp = New eProduct.Disco_exprs
	
	mstrNoteDescript = vbNullString
	
	Call lclsDisco_exp.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDisexprc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")))
	
	'+ Se obtiene la descripción de la nota para ser mostrada sobre el frame principal	                        
	If lclsDisco_exp.nNotenum <> eRemoteDB.Constants.intNull Then
		lcolNotes = New eGeneralForm.Notess
		If lcolNotes.Find(lclsDisco_exp.nNotenum) Then
            if lcolNotes.Count > 0 Then
			    mstrNoteDescript = lcolNotes(1).tDs_text
            End If
		End If
	End If
	'+ Se obtiene el tipo de producto para saber si se despliega el campo nprodclass
	
	Dim lclsProduct As eProduct.Product
	lclsProduct = New eProduct.Product
	mobjnAply = False
	With lclsProduct
		If .FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) Then
			If .nProdClas = 3 Or .nProdClas = 4 Or .nProdClas = 6 Or .nProdClas = 7 Then
				mobjnAply = True
			End If
		End If
		
	End With
	lclsProduct = Nothing
	
	With lclsDisco_exp
		
Response.Write("" & vbCrLf)
Response.Write("   <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkRequired", GetLocalResourceObject("chkRequiredCaption"), CStr(False),  , "Disabled();",  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkPreSel", GetLocalResourceObject("chkPreSelCaption"),  ,  ,  ,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkFraction", GetLocalResourceObject("chkFractionCaption"),  ,  ,  ,  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkReturn", GetLocalResourceObject("chkReturnCaption"),  ,  ,  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkTypMar", GetLocalResourceObject("chkTypMarCaption"), lclsDisco_exp.sTypMar, "1",  ,  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkIVA", GetLocalResourceObject("chkIVACaption"), lclsDisco_exp.sIVA, "1",  ,  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>    " & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<TABLE>	        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14656>" & GetLocalResourceObject("tcnBill_itemCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

		mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjValues.Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("tcnBill_item", "tabTab_bill_i", 2, CStr(.nBill_item), True,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("tcnBill_itemToolTip"),  , 7))
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>	" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=14656>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>		" & vbCrLf)
Response.Write("			<TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=14656>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>		" & vbCrLf)
Response.Write("			<TD WIDTH=""10%""></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14657>" & GetLocalResourceObject("cbeBranchLedgerCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranchLedger", "table75", 1, CStr(.nBranch_led),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeBranchLedgerToolTip"),  , 8))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.CheckControl("chkCapitalAdd", GetLocalResourceObject("chkCapitalAddCaption"), CStr(False),  , "Enabled(this);",  , 11))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>")


            Response.Write(mobjValues.NumericControl("tcnCapitalAdd", 9, CStr(.nDisexAddper), , GetLocalResourceObject("tcnCapitalAddToolTip"), , 6, , , , , True, 12))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14656>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14658>" & GetLocalResourceObject("cbeBranchReinsuCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranchReinsu", "table5000", 1, CStr(.nBranch_rei),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeBranchReinsuToolTip"),  , 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.CheckControl("chkCapitalSub", GetLocalResourceObject("chkCapitalSubCaption"), CStr(False),  , "Enabled(this);",  , 13))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


            Response.Write(mobjValues.NumericControl("tcnCapitalSub", 9, CStr(.nDisexSubper), , GetLocalResourceObject("tcnCapitalSubToolTip"), , 6, , , , , True, 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14656>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchStatisCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranchStatis", "table71", 1, CStr(.nBranch_est),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeBranchStatisToolTip"),  , 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=14660>" & GetLocalResourceObject("tcnCapitalLevCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnCapitalLev", 5, CStr(.nAmelevel),  , GetLocalResourceObject("tcnCapitalLevToolTip"),  , 0,  ,  ,  ,  ,  , 15))


Response.Write("</TD> " & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("      		<TR>" & vbCrLf)
Response.Write("      		")

		If mobjnAply Then
Response.Write("" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbenaplyCaption") & " </LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbenaply", "table2000", 1, CStr(.nAply),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbenaplyToolTip"),  , 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

		Else
Response.Write("" & vbCrLf)
Response.Write("			<TD><LABEL ID=0> </LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.HiddenControl("cbenaply", "7"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

		End If
Response.Write("" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD></TD> " & vbCrLf)
Response.Write("        </TR> " & vbCrLf)
Response.Write("    </TABLE>      " & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>	" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=14656>" & GetLocalResourceObject("txtNoteCaption") & "</LABEL></TD>				" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

		
		Response.Write(mobjValues.TextAreaControl("txtNote", 5, 60, mstrNoteDescript,  , GetLocalResourceObject("txtNoteToolTip"),  , True, 16))
		Response.Write(mobjValues.ButtonNotes("SCA2-X", .nNotenum, False, mobjValues.ActionQuery))
		
		Response.Write("<SCRIPT language=Javascript>")
		Response.Write("Disabled(" & mobjValues.StringToType(.sRequire, eFunctions.Values.eTypeData.etdDouble, 0) & ");")
		
            Response.Write("Values(" & mobjValues.StringToType(.sRequire, eFunctions.Values.eTypeData.etdDouble, 0) & "," & mobjValues.StringToType(.sDefaulti, eFunctions.Values.eTypeData.etdDouble, 0) & "," & mobjValues.StringToType(.sProrate, eFunctions.Values.eTypeData.etdDouble, 0) & "," & mobjValues.StringToType(.sDevoallo, eFunctions.Values.eTypeData.etdDouble, 0) & "," & Replace(.nDisexSubper, ",", ".") & "," & Replace(.nDisexAddper, ",", ".") & "," & mobjValues.StringToType(.sChanallo, eFunctions.Values.eTypeData.etdDouble, 0) & ");")
		
		Response.Write("</" & "Script>")
		lclsDisco_exp = Nothing
		lcolDisco_exp = Nothing
	End With
	lcolNotes = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp08b1"
%>
	<SCRIPT>
		var ActionQuery = 0;
	</SCRIPT>
<%
If mobjValues.ActionQuery Then
	Response.Write("<SCRIPT>ActionQuery=1;</SCRIPT>")
Else
	Response.Write("<SCRIPT>ActionQuery=2;</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT LANGUAGE="JavaScript">

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:05 $|$$Author: Nvaplat61 $"

//%Disabled: Habilita/Inhabilita los campos del frame Información General Recargos/Descuentos
//---------------------------------------------------------------------------------------------------
function Disabled(nRequire){	
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		chkPreSel.checked = true;
		chkPreSel.disabled=true;
		if (!chkRequired.checked)
		{	if (ActionQuery == 2)
				chkPreSel.disabled=false;
		}
	}
}
//%Values: Habilita/Inhabilita, los campos del frame cambios
//---------------------------------------------------------------------------------------------------
function Values(nRequired,nPresel,nProrate,nReturn,nDisexSubper,nDisexAddper,sChanallo){	
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (nRequired == 1)
			chkRequired.checked = true;
				
		if (nPresel == 1){
			chkPreSel.checked = true;
			if (nRequired == 1)
				chkPreSel.disabled = true;
			else
				chkPreSel.disabled = false;	
			}
		else
			chkPreSel.checked = false;
				
		if (nProrate == 1)
			chkFraction.checked = true;
				
		if (nReturn == 1)
			chkReturn.checked = true;
		
		if (sChanallo == 3 || sChanallo  == 2) 
			chkCapitalSub.checked = true;

		if (sChanallo == 3 || sChanallo == 1) 
			chkCapitalAdd.checked = true;
			
		if (chkCapitalSub.checked == true)
		{	if (ActionQuery == 2)
				tcnCapitalSub.disabled = false;
		}
		else
		{
			if (ActionQuery == 2)
			{	tcnCapitalSub.value = 0;
				tcnCapitalSub.disabled = true;
			}
		}

		if (chkCapitalAdd.checked == true)
		{	if (ActionQuery == 2)
				tcnCapitalAdd.disabled = false;
		}
		else
		{	if (ActionQuery == 2) 
			{	tcnCapitalAdd.value = 0;
				tcnCapitalAdd.disabled = true;
			}
		}			
	}	
}

//%Enabled: Habilita/Inhabilita, los campos de porcentaje máximo y mínimo de descuento
//---------------------------------------------------------------------------------------------------
function Enabled(Field){	
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (chkCapitalSub.checked == true)
		{	if (ActionQuery == 2)
				tcnCapitalSub.disabled = false;
		}
		else
		{
			if (ActionQuery == 2)
			{	tcnCapitalSub.value = 0;
				tcnCapitalSub.disabled = true;
			}
		}

		if (chkCapitalAdd.checked == true)
		{	if (ActionQuery == 2)
				tcnCapitalAdd.disabled = false;
		}
		else
		{	if (ActionQuery == 2) 
			{	tcnCapitalAdd.value = 0;
				tcnCapitalAdd.disabled = true;
			}
		}
	}	
}		

</SCRIPT>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP08B1", "DP08B1.aspx"))
		mobjMenu = Nothing
	End If
End With

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP08B1" ACTION="valDiscoExprSeq.aspx?mode=2;">
<%Response.Write(mobjValues.ShowWindowsName("DP08B1"))
Call insPreDP08B1()
%>
</FORM>
</BODY>
</HTML>





