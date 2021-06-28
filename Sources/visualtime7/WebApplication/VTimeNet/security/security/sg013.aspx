<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim llngAction As String
Dim lstrAction As String



'% InsPreSG013:
'--------------------------------------------------------------------------------------------
Private Sub InsPreSG013()
	'--------------------------------------------------------------------------------------------
	Dim lclsSecurity As eSecurity.Secur_sche
	Dim X As Object
	
	lclsSecurity = New eSecurity.Secur_sche
	
	Response.Write(mobjValues.ShowWindowsName("SG013"))
	
	Call lclsSecurity.reaSchema(Session("sSche_codeWin"), True)
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD WIDTH=""25%""><LABEL ID=15023>" & GetLocalResourceObject("tctSchemaDesCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=4> ")


Response.Write(mobjValues.TextControl("tctSchemaDes", 30, lclsSecurity.sLongdesc, False, GetLocalResourceObject("tctSchemaDesToolTip"),  ,  ,  ,  ,  , 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>            " & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=15024>" & GetLocalResourceObject("tctShort_desCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD> ")


Response.Write(mobjValues.TextControl("tctShort_des", 12, lclsSecurity.sShortdes, False, GetLocalResourceObject("tctShort_desToolTip"),  ,  ,  ,  ,  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <BR>" & vbCrLf)
Response.Write("            </TR>        " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD CLASS=""HighLighted""><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("                <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""40%"" CLASS=""HighLighted""><LABEL ID=100441><A NAME=""Condición"">" & GetLocalResourceObject("AnchorCondiciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD> <HR> </TD>" & vbCrLf)
Response.Write("                <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("                <TD> <HR> </TD>" & vbCrLf)
Response.Write("            </TR>            " & vbCrLf)
Response.Write("        </TABLE>" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD WIDTH=""5%""><LABEL ID=15027>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""5%""> ")


Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(lclsSecurity.dDate_from), False, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  ,  , 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("                <TD WIDTH=""5%""><LABEL ID=15025>" & GetLocalResourceObject("tcdNulldateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD> ")


Response.Write(mobjValues.DateControl("tcdNulldate", CStr(lclsSecurity.dDate_to), False, GetLocalResourceObject("tcdNulldateToolTip"),  ,  ,  ,  ,  , 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.CheckControl("chkPermission", GetLocalResourceObject("chkPermissionCaption"), lclsSecurity.sUsequery, CStr(1), "DisabledMenu()",  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>                " & vbCrLf)
Response.Write("        </TABLE>            " & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">            " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD CLASS=""HighLighted""><LABEL ID=100442><A NAME=""Horario"">" & GetLocalResourceObject("AnchorHorarioCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("                <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""50%"" CLASS=""HighLighted""><LABEL ID=100443><A NAME=""Horario"">" & GetLocalResourceObject("AnchorHorario2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD> <HR> </TD>" & vbCrLf)
Response.Write("                <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("                <TD> <HR> </TD>" & vbCrLf)
Response.Write("            </TR>            " & vbCrLf)
Response.Write("        </TABLE>" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100444>" & GetLocalResourceObject("tctFrom1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctFrom1", 5, lclsSecurity.sTime1_from, False, GetLocalResourceObject("tctFrom1ToolTip"),  ,  ,  ,  ,  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100445>" & GetLocalResourceObject("tctTo1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctTo1", 5, lclsSecurity.sTime1_to, False, GetLocalResourceObject("tctTo1ToolTip"),  ,  ,  ,  ,  , 7))


Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100446>" & GetLocalResourceObject("tctFromQ1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctFromQ1", 5, lclsSecurity.sTimeq1_fro, False, GetLocalResourceObject("tctFromQ1ToolTip"),  ,  ,  ,  ,  , 8))


Response.Write("</TD>" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100447>" & GetLocalResourceObject("tctToQ1Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctToQ1", 5, lclsSecurity.sTimeq1_to, False, GetLocalResourceObject("tctToQ1ToolTip"),  ,  ,  ,  ,  , 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100448>" & GetLocalResourceObject("tctFrom2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctFrom2", 5, lclsSecurity.sTime2_from, False, GetLocalResourceObject("tctFrom2ToolTip"),  ,  ,  ,  ,  , 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100449>" & GetLocalResourceObject("tctTo2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctTo2", 5, lclsSecurity.sTime2_to, False, GetLocalResourceObject("tctTo2ToolTip"),  ,  ,  ,  ,  , 11))


Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100450>" & GetLocalResourceObject("tctFromQ2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctFromQ2", 5, lclsSecurity.sTimeq2_fro, False, GetLocalResourceObject("tctFromQ2ToolTip"),  ,  ,  ,  ,  , 12))


Response.Write("</TD>" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""><LABEL ID=100451>" & GetLocalResourceObject("tctToQ2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""12.5%""> ")


Response.Write(mobjValues.TextControl("tctToQ2", 5, lclsSecurity.sTimeq2_to, False, GetLocalResourceObject("tctToQ2ToolTip"),  ,  ,  ,  ,  , 13))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("        </TABLE>" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">            " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD  COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Cotizador"">" & GetLocalResourceObject("AnchorCotizadorCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("                <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""50%"" >&nbsp;</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD COLSPAN=2> <HR> </TD>" & vbCrLf)
Response.Write("            </TR>            " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("tcnDurationCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD> ")


Response.Write(mobjValues.NumericControl("tcnDuration", 3, CStr(lclsSecurity.nDuration),  , GetLocalResourceObject("tcnDurationToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""30%"">&nbsp;</TD>" & vbCrLf)
Response.Write("                <TD WIDTH=""30%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD ><LABEL ID=15025>" & GetLocalResourceObject("tcnDaysAdvCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD> ")


Response.Write(mobjValues.NumericControl("tcnDaysAdv", 3, CStr(lclsSecurity.nDaysAdv),  , GetLocalResourceObject("tcnDaysAdvToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("                <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            </TR>                " & vbCrLf)
Response.Write("        </TABLE>     " & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <BR> </BR>" & vbCrLf)
Response.Write("                <TD WIDTH=""28%""> &nbsp; </TD>" & vbCrLf)
Response.Write("                <TD><LABEL ID=15026>" & GetLocalResourceObject("cbeStatregtCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("                <TD> " & vbCrLf)
Response.Write("					")

	
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
		mobjValues.TypeList = 2
		mobjValues.List = "2"
		mobjValues.BlankPosition = False
	End If
	
Response.Write("" & vbCrLf)
Response.Write("                        " & vbCrLf)
Response.Write("                    ")

	
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionAdd) Then
		mobjValues.BlankPosition = False
		If CDbl(lclsSecurity.sStatregt) > 0 And Not IsNothing(lclsSecurity.dDate_from) And Not IsNothing(lclsSecurity.dDate_to) Then
			
Response.Write("" & vbCrLf)
Response.Write("								")


Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, lclsSecurity.sStatregt,  ,  ,  ,  ,  ,  , False,  , "",  , 14))


Response.Write("" & vbCrLf)
Response.Write("					")

			
		Else
			
Response.Write("" & vbCrLf)
Response.Write("								")


Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  ,  , True,  , "",  , 14))


Response.Write("" & vbCrLf)
Response.Write("					")

			
		End If
		
Response.Write("" & vbCrLf)
Response.Write("                    ")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("                    ")

		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
			mobjValues.BlankPosition = False
			
Response.Write("" & vbCrLf)
Response.Write("								")


Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, lclsSecurity.sStatregt,  ,  ,  ,  ,  ,  , True,  , "",  , 14))


Response.Write("" & vbCrLf)
Response.Write("					")

			
		Else
			
Response.Write("" & vbCrLf)
Response.Write("					")

			
			If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
				
Response.Write("" & vbCrLf)
Response.Write("									")


Response.Write(mobjValues.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, lclsSecurity.sStatregt,  ,  ,  ,  ,  ,  , False,  , "",  , 14))


Response.Write("" & vbCrLf)
Response.Write("					")

				
			End If
			
Response.Write("" & vbCrLf)
Response.Write("					")

			
		End If
		
Response.Write("" & vbCrLf)
Response.Write("                    ")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("                </TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("        </TABLE>        " & vbCrLf)
Response.Write("    </TABLE>")

	
	lclsSecurity = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG013"

lstrAction = Request.QueryString.Item("nMainAction")
llngAction = Request.QueryString.Item("nMainAction")
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
	mobjValues.ActionQuery = True
Else
	mobjValues.ActionQuery = False
End If

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SG013"))
	
	mobjMenu = New eFunctions.Menues
	
	.Write(mobjMenu.setZone(2, "SG013", "SG013.aspx"))
End With

mobjMenu = Nothing
%>

<SCRIPT>
//-------------------------------------------------------------------------------------------
function insUpdHeader(lstrSche_code){
//-------------------------------------------------------------------------------------------
    var lblnAgain = true;
    
    if (typeof(top.fraHeader.document)!='undefined')
        if (typeof(top.fraHeader.document.forms[0])!='undefined')
            if (typeof(top.fraHeader.document.forms[0].valScheCode)!='undefined')
            {
		        top.fraHeader.document.forms[0].valScheCode.value=lstrSche_code;
                lblnAgain = false;
            }
   if (lblnAgain)
      setTimeout("insUpdHeader(lstrSche_code)",50);
}

//% DisabledMenu: Permite habilitar e inhabilitar los campos "Menú que lo invoca" y
//% "Orden de aparición".
//------------------------------------------------------------------------------------------
function DisabledMenu(){
//------------------------------------------------------------------------------------------
    if (document.forms[0].chkPermission.checked == true) 
    {
        document.forms[0].tctFromQ1.disabled = false;
        document.forms[0].tctToQ1.disabled = false;
        document.forms[0].tctFromQ2.disabled = false;
        document.forms[0].tctToQ2.disabled = false;
    }
    else
    {
        document.forms[0].tctFromQ1.disabled = true;
        document.forms[0].tctFromQ1.value = "";
        document.forms[0].tctToQ1.disabled = true;
        document.forms[0].tctToQ1.value = "";
        document.forms[0].tctFromQ2.disabled = true;
        document.forms[0].tctFromQ2.value = "";
        document.forms[0].tctToQ2.disabled = true;
        document.forms[0].tctToQ2.value = "";
    }
}


//- Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 14/03/06 20:08 $|$$Author: Mvazquez $"

</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SG013" ACTION="valSecuritySeqSchema.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

<%
Response.Write("<SCRIPT>setTimeout(""insUpdHeader('" & Session("sSche_codeWin") & "')"",50)</SCRIPT>")

Call InsPreSG013()
%>

</FORM>
</BODY>
</HTML>





