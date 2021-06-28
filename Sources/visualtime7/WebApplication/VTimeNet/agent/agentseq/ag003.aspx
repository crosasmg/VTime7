<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim llngAction As Object


'------------------------------------------------------------------------------------------------
Private Sub InsPreAG003()
	'------------------------------------------------------------------------------------------------
	Dim lclsIntermedia As eAgent.Intermedia
	Dim lclsCommis_his As eAgent.commis_his
	Dim lclsInter_Superv As eAgent.Intermedia
	Dim ldtmEffecdate As Object
	Dim ldtmEffecdate_his As Object
	Dim llngIntermedia As Object
	Dim lblnIntermedia As Boolean
	
	lclsIntermedia = New eAgent.Intermedia
	lclsInter_Superv = New eAgent.Intermedia
	lclsCommis_his = New eAgent.commis_his
	
	Response.Write(mobjValues.ShowWindowsName("AG003"))
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 And CStr(Session("nLastIntermediary")) <> vbNullString Then
		lblnIntermedia = lclsIntermedia.Find(Session("nIntermed"))
		
		If (Not lblnIntermedia) Or lclsIntermedia.nLife_sche = eRemoteDB.Constants.intNull Then
			lblnIntermedia = lclsIntermedia.Find(Session("nLastIntermediary"))
		End If
		llngIntermedia = Session("nLastIntermediary")
	Else
		llngIntermedia = Session("nIntermed")
		lblnIntermedia = lclsIntermedia.Find(llngIntermedia)
	End If
	
	If lblnIntermedia Then
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
			ldtmEffecdate = lclsIntermedia.dInpdate
			ldtmEffecdate_his = lclsIntermedia.dInpdate
		Else
			With lclsCommis_his
				.nIntermed = llngIntermedia
				If .ReaLastDateCommis_his Then
					ldtmEffecdate = .dEffecdate
					ldtmEffecdate_his = .dEffecdate
					If ldtmEffecdate > Today Then
						ldtmEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, ldtmEffecdate)
					Else
						ldtmEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, Today)
					End If
				Else
					ldtmEffecdate = lclsIntermedia.dInpdate
				End If
			End With
		End If
		
		If CStr(llngAction) <> CStr(eFunctions.Menues.TypeActions.clngactionquery) Then
			With lclsInter_Superv
				If .Find(lclsIntermedia.nSupervis) Then
					If .sCol_agree = "1" Then
						lclsIntermedia.sCol_agree = .sCol_agree
					End If
				End If
			End With
		End If
	End If
	
	mobjValues.ActionQuery = llngAction = eFunctions.Menues.TypeActions.clngactionquery
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8246>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""75%"">")


Response.Write(mobjValues.DateControl("tcdEffecDate", ldtmEffecdate,  , GetLocalResourceObject("tcdEffecDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("tcdEffecDate_old", ldtmEffecdate_his))


Response.Write("</TD>            " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>		" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">    		" & vbCrLf)
Response.Write("		<TR>            " & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Comisiones-Ramos Vida"">" & GetLocalResourceObject("AnchorComisiones-Ramos VidaCaption") & "</A></LABEL></TD>            " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""Horline""></TD>	    " & vbCrLf)
Response.Write("		</TR>        	" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=8249>" & GetLocalResourceObject("cbeLifeComTableCaption") & "</LABEL></TD>                        " & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeLifeComTable", "Tab_comlif2", 2, CStr(lclsIntermedia.nComtabli),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeLifeComTableToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""5%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			")

	If lclsIntermedia.nLife_sche = CDbl("1") Then
Response.Write("			" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD><LABEL ID=8249>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optLife_Sche", GetLocalResourceObject("optLife_Sche_1Caption"), CStr(1), "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>				" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optLife_Sche", GetLocalResourceObject("optLife_Sche_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>	" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	ElseIf lclsIntermedia.nLife_sche = CDbl("2") Then 
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD><LABEL ID=8249>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optLife_Sche", GetLocalResourceObject("optLife_Sche_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>				" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optLife_Sche", GetLocalResourceObject("optLife_Sche_2Caption"), CStr(1), "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>	" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD><LABEL ID=8249>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optLife_Sche", GetLocalResourceObject("optLife_Sche_1Caption"), CStr(1), "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>				" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optLife_Sche", GetLocalResourceObject("optLife_Sche_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR>	" & vbCrLf)
Response.Write("					" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		 </TR>	" & vbCrLf)
Response.Write("		 <TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=8249>" & GetLocalResourceObject("cbeGoal_LifeCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeGoal_Life", "TabTab_Goals2", 2, CStr(lclsIntermedia.nGoal_life),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeGoal_LifeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""5%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("valSpec_LifeCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("valSpec_Life", "TABTAB_SPEC_COMM", 2, CStr(lclsIntermedia.nSlc_Tab_nr),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valSpec_LifeToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""5%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>            " & vbCrLf)
Response.Write("			<TR><TD>&nbsp;</TD></TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Comisiones-Ramos Generales"">" & GetLocalResourceObject("AnchorComisiones-Ramos GeneralesCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("		<TR>					" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=8248>" & GetLocalResourceObject("cbeGralComTableCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeGralComTable", "Tab_comgen2", 2, CStr(lclsIntermedia.nComtabge),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeGralComTableToolTip")))


Response.Write("</TD>            " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			")

	If lclsIntermedia.nGen_sche = CDbl("1") Then
Response.Write("					" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD><LABEL ID=8249>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>			" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optGen_Sche", GetLocalResourceObject("optGen_Sche_1Caption"), CStr(1), "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optGen_Sche", GetLocalResourceObject("optGen_Sche_2Caption"),  , "2"))


Response.Write("</TD>			" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	ElseIf lclsIntermedia.nGen_sche = CDbl("2") Then 
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD><LABEL ID=8249>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>			" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optGen_Sche", GetLocalResourceObject("optGen_Sche_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optGen_Sche", GetLocalResourceObject("optGen_Sche_2Caption"), CStr(1), "2"))


Response.Write("</TD>			" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD><LABEL ID=8249>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>			" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optGen_Sche", GetLocalResourceObject("optGen_Sche_1Caption"), CStr(1), "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.OptionControl(0, "optGen_Sche", GetLocalResourceObject("optGen_Sche_2Caption"),  , "2"))


Response.Write("</TD>			" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=8248>" & GetLocalResourceObject("cbeGoal_GenCaption") & "</LABEL></TD>		             " & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeGoal_Gen", "TabTab_Goals2", 2, CStr(lclsIntermedia.nGoal_gen),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeGoal_GenToolTip")))


Response.Write("</TD>            " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    ")

	
	
	Session("sOriginalForm") = "CA025"
	
	lclsIntermedia = Nothing
	lclsCommis_his = Nothing
	lclsInter_Superv = Nothing
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
llngAction = Request.QueryString.Item("nMainAction")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("AG003"))
	.Write(mobjMenu.setZone(2, "AG003", "AG003.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//% insSetState : Establece el estado innicial de la página
//--------------------------------------------------------------------------------------
function insSetState(){
//--------------------------------------------------------------------------------------
//+ Estado inicial de la fecha de efecto.
        if (top.fraSequence.plngMainAction==301)
        {
            self.document.forms[0].tcdEffecDate.disabled = true;
        }
        else
        {
			if(typeof(self.document.forms[0].tcdEffecDate)!='undefined')
				self.document.forms[0].tcdEffecDate.disabled = false;
		}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAG003" ACTION="valAgentSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call InsPreAG003()

%>
</FORM>
</BODY>
</HTML>





