<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para la carga de los valores en la forma
Dim mclsRequest As ePolicy.Request

'-Tabla usada para Orden de pago según tipo de producto
Dim mstrPayOrderTable As String
Dim mstrPayOrderValue As String


'% Obtiene los datos 
'%--------------------------------------------------------------------------------------
Private Sub insPreCA767()
	'%----------------------------------------------------------------------------------------
	
	'+ Se capturan los parametros de transaccion    
	With Request
		If .QueryString.Item("sCertype") <> vbNullString Then
			Session("sCertype") = .QueryString.Item("sCertype")
		End If
		If .QueryString.Item("nBranch") <> vbNullString Then
			Session("nBranch") = .QueryString.Item("nBranch")
		End If
		If .QueryString.Item("nProduct") <> vbNullString Then
			Session("nProduct") = .QueryString.Item("nProduct")
		End If
		If .QueryString.Item("nCertif") <> vbNullString Then
			Session("nCertif") = .QueryString.Item("nCertif")
		End If
		If .QueryString.Item("dEffecdate") <> vbNullString Then
			Session("dEffecdate") = .QueryString.Item("dEffecdate")
		End If
		If .QueryString.Item("nOperat") <> vbNullString Then
			Session("nOperat") = .QueryString.Item("nOperat")
		End If
	End With
	
	'+En búsqueda se pasa fecha del día para que recupere la última solicitud registrada
	Call mclsRequest.insPreCA767(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nOperat"), eFunctions.Values.eTypeData.etdDouble))
	
	'+Se diferencia los valores posibles de la Orden de pago para los productos 
	'+de Unit Linked o Universal Life y el resto
	If mclsRequest.nProdClass = 3 Or mclsRequest.nProdClass = 4 Then
		mstrPayOrderTable = "table5636"
		mstrPayOrderValue = CStr(mclsRequest.nType_payment)
	Else
		mstrPayOrderTable = "table193"
		mstrPayOrderValue = mclsRequest.sPayorder
	End If
	
	Session("dEffecdate") = mobjValues.TypeToString(mclsRequest.dEffecdate, eFunctions.Values.eTypeData.etdDate)
	Session("nOrigin_apv") = mclsRequest.nOrigin_apv
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca767")
mclsRequest = New ePolicy.Request

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca767"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
Call insPreCA767()
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA767", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA767" ACTION="ValPolicyTra.aspx?sMode=2">
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 24-05-06 19:31 $|$$Author: Clobos $"
</SCRIPT>
    <%=mobjValues.ShowWindowsName("CA767", Request.QueryString.Item("sWindowDescript"))%>
<TABLE WIDTH="100%">
    <TR>
		<TD>&nbsp;</TD>
    </TR>
    <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
        <TD><%=mobjValues.DateControl("tcdEffecdate", CStr(mclsRequest.dEffecdate),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
		<TD WIDTH=10%>&nbsp;</TD>
		<TD><LABEL ID=0><%= GetLocalResourceObject("cboStatquotaCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("cboStatquota", "table5526", eFunctions.Values.eValuesType.clngComboType, CStr(mclsRequest.nStatQuota),  ,  ,  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctDescript", 30, mclsRequest.sDescript,  , GetLocalResourceObject("tctDescriptToolTip"))%></TD>
		<TD>&nbsp;</TD>
        <TD><LABEL ID=11756><%= GetLocalResourceObject("SCA2-808Caption") %></LABEL></TD>
        <TD><%=mobjValues.ButtonNotes("SCA2-808", mclsRequest.nNoteNum, False, mobjValues.ActionQuery)%></TD>
    </TR>
    <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		<TD><%mobjValues.sQueryString = "sCertype=" & Request.QueryString.Item("sCertype") & "!nBranch=" & Request.QueryString.Item("nBranch") & "!nProduct=" & Request.QueryString.Item("nProduct") & "!nPolicy=" & Request.QueryString.Item("nPolicy") & "!nCertif=" & Request.QueryString.Item("nCertif") & "!dEffecdate=" & Session("dEffecdate") & "!dStartdate=" & Request.QueryString.Item("dEffecdate") & "!LoadWithAction=" & Request.QueryString.Item("nMainAction")
Response.Write(mobjValues.ButtonAssociate(17, "btnQuery", True))

%>
        </TD>
    </TR>
    <%
If Session("nOrigin") = 8 Then
	%>
        <TR>
			<TD ROWSPAN="2" COLSPAN="2" VALIGN=TOP>
				<TABLE WIDTH=100%>	
					<TR>
						<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Tipo de rescate"><%= GetLocalResourceObject("AnchorTipo de rescateCaption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD COLSPAN="2" CLASS="HorLine"></TD>
					</TR>
					<TR>
						<TD><%=mobjValues.OptionControl(0, "optTyp_surr", GetLocalResourceObject("optTyp_surr_1Caption"), CStr(2 - CShort(mclsRequest.sTyp_surr)), "1",  , True)%> </TD>
						<TD><%=mobjValues.OptionControl(0, "optTyp_surr", GetLocalResourceObject("optTyp_surr_2Caption"), CStr(3 - CShort(mclsRequest.sTyp_surr)), "2",  , True)%></TD>
					</TR>
				</TABLE>	
			</TD>
		    <TD WIDTH=10%>&nbsp;</TD>
            <TD><LABEL ID=13850><%= GetLocalResourceObject("cbeTypepayCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTypepay", "Table5527", eFunctions.Values.eValuesType.clngComboType, CStr(mclsRequest.nTypepay),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypepayToolTip"))%></TD>
        </TR>
        <TR>
		    <TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cboPayorderCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("cboPayorder", mstrPayOrderTable, eFunctions.Values.eValuesType.clngComboType, mstrPayOrderValue,  ,  ,  ,  ,  ,  , True)%></TD>
        </TR>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkNull_Rec", GetLocalResourceObject("chkNull_RecCaption"), mclsRequest.sNull_rec,  ,  , True)%></TD>
		    <TD>&nbsp;</TD>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnSurrAmountCaption") %></LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnAmount", 18, CStr(mclsRequest.nAmount),  , GetLocalResourceObject("tcnSurrAmountToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
		    <TD><LABEL><%= GetLocalResourceObject("cbeSurrReasCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("cbeSurrReas", "Table5635", eFunctions.Values.eValuesType.clngComboType, CStr(mclsRequest.nSurr_reason),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSurrReasToolTip"),  , 1)%></TD>
		</TR>
		<TR>
		    <TD><LABEL ID="0"><%= GetLocalResourceObject("valOriginCaption") %></LABEL></TD>
			<TD><%	mobjValues.BlankPosition = True
	mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nCollecDocTyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngComboType, CStr(mclsRequest.nOrigin_apv), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valOriginToolTip")))
	%>
			</TD>
		</TR>
    <%	
ElseIf Session("nOrigin") = 6 Or Session("nOrigin") = 7 Then 
	%>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkNull_Rec", GetLocalResourceObject("chkNull_RecCaption"), mclsRequest.sNull_rec,  ,  , True)%></TD>
		</TR>
    <%	
ElseIf Session("nOrigin") = 9 Then 
	%>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cboPayorderCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("cboPayorder", mstrPayOrderTable, eFunctions.Values.eValuesType.clngComboType, mstrPayOrderValue,  ,  ,  ,  ,  ,  , True)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnLoansAmountCaption") %></LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnAmount", 18, CStr(mclsRequest.nAmount),  , GetLocalResourceObject("tcnLoansAmountToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
		</TR>
	<%	
ElseIf Session("nOrigin") = 10 Then 
	%>   
		<TR>
		    <TD><LABEL ID="0"><%= GetLocalResourceObject("valOriginCaption") %></LABEL></TD>
			<TD><%	mobjValues.BlankPosition = True
	mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nCollecDocTyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngComboType, CStr(mclsRequest.nOrigin_apv), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valOriginToolTip")))
	%>
			</TD>
		</TR>     		
    <%	
ElseIf Session("nOrigin") = 4 Then 
	%>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cboNo_conversCaption") %></LABEL></TD>
		    <TD><%	mobjValues.Parameters.Add("sBrancht", Request.QueryString.Item("sBrancht"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cboNo_convers", "Tabnoconversbrancht", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboNo_conversToolTip")))%></TD>
		    <TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cboNullcodeCaption") %> </LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("cboNullcode", "table13", eFunctions.Values.eValuesType.clngComboType, CStr(mclsRequest.nNullcode),  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Recibo"><%= GetLocalResourceObject("AnchorReciboCaption") %></A></LABEL></TD>
		    <TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkNull_Rec", GetLocalResourceObject("chkNull_RecCaption"), mclsRequest.sNull_rec,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD><%=mobjValues.OptionControl(0, "optTyp_rec", GetLocalResourceObject("optTyp_rec_1Caption"), "1", "1",  , True)%> </TD>
		</TR>
		<TR>
			<TD><%=mobjValues.OptionControl(0, "optTyp_rec", GetLocalResourceObject("optTyp_rec_2Caption"),  , "2",  , True)%></TD>
		</TR>
		<TR>
			<TD><%=mobjValues.OptionControl(0, "optTyp_rec", GetLocalResourceObject("optTyp_rec_3Caption"),  , "3",  , True)%></TD>
		</TR>
    <%	
ElseIf Session("nOrigin") = 5 Then 
	%>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkNull_Rec", GetLocalResourceObject("chkNull_RecCaption"), mclsRequest.sNull_rec,  ,  , True)%></TD>
		    <TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkReh_lrec", GetLocalResourceObject("chkReh_lrecCaption"), mclsRequest.sReh_lrec,  ,  , True)%></TD>
		</TR>
	<%	
End If
Response.Write(mobjValues.HiddenControl("hddnAgency", CStr(mclsRequest.nAgency)))
Response.Write(mobjValues.HiddenControl("hddProdClass", CStr(mclsRequest.nProdClass)))
Response.Write(mobjValues.HiddenControl("hddInd_Insur", mclsRequest.sInd_Insur))
%>
</TABLE>
</FORM> 
</BODY>
</HTML>
<%
mclsRequest = Nothing
mobjValues = Nothing
%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca767")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




