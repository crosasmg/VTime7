<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mclsPolicy As ePolicy.Policy
    Dim mclsProduct As eProduct.Product
    Dim mclsPolicy_Win As ePolicy.Policy_Win
    Dim mobjMenu As eFunctions.Menues
    Dim mclsCertificat As ePolicy.Certificat
    Dim lstrTariff As Object
    Dim lstrColtimre As Byte
    Dim lstrTemp As Object
    Dim lstrColReint As String
    Dim lstrTypModule As String
    Dim lstrColInvot As String
    Dim lstrTypDiscxp As String
    Dim lstrTypClause As String
    Dim lstrDocuTyp As String
    Dim lstrQCertif As Object
    Dim lstrNclaim As Object
    Dim lstrColtpres As String
    Dim mblnFindAM003 As Boolean
    


'% insPreCA006: hace la lectura de los campos a mostrar en pantalla
'----------------------------------------------------------------------------------------------
Private Sub insPreCA006()
	'----------------------------------------------------------------------------------------------
	Dim lblnExistPolicy As Boolean
	
        If mclsPolicy.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), 2), mobjValues.StringToType(Session("nProduct"), 2), mobjValues.StringToType(Session("nPolicy"), 3)) Then
            If mclsPolicy.sColtpres = vbNullString Then
                mclsPolicy.sColtpres = "2"
            End If
            lblnExistPolicy = True
            mclsCertificat = New ePolicy.Certificat
            Call mclsCertificat.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), 2), mobjValues.StringToType(Session("nProduct"), 2), mobjValues.StringToType(Session("nPolicy"), 3), mobjValues.StringToType(Session("nCertif"), 3))
        Else
            lblnExistPolicy = False
        End If
	
	Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), 2), mobjValues.StringToType(Session("nProduct"), 2), mobjValues.StringToDate(Session("dEffecdate")))
	
	With mclsPolicy
		If .nTariff <= 0  Then
			If mclsProduct.nTariff = 0 Or IsDBNull(mclsProduct.nTariff) Then
				lstrTariff = "1"
			Else
				lstrTariff = mclsProduct.nTariff
			End If
		Else
			lstrTariff = .nTariff
		End If
		
		'+Se asigna el Valor al campo "Dias de Denuncio"
		'+ En caso de ser Nulo se asigna la cantidad de días indicado para el producto
		If .nClaim_notice = eRemoteDB.Constants.intNull Then
			lstrNclaim = mclsProduct.nClaim_notice
		Else
			lstrNclaim = .nClaim_notice
		End If
		
		'+Se asigna el Valor al campo "Tipo de Renovación"
		If .sColtimre = vbNullString Then
			If mclsProduct.sTimeren = vbNullString Then
				lstrTemp = "2"
			Else
				lstrTemp = mclsProduct.sTimeren
			End If
		Else
			lstrTemp = .sColtimre
		End If
		
		Select Case lstrTemp
			Case "1"
				lstrColtimre = 1
			Case "2"
				lstrColtimre = 2
			Case "3"
				lstrColtimre = 3
		End Select
		
		'+Se asigna el Valor al campo "Tipo de Reaseguro"
		If .sColreint Is System.DBNull.Value Or .sColreint = "" Then
			If mclsProduct.sReintype = vbNullString Then
				lstrColReint = ""
			Else
				lstrColReint = mclsProduct.sReintype
			End If
		Else
			lstrColReint = .sColreint
		End If
		
		'+Se asigna el Valor al campo "Módulos/Coberturas"
		If .sTyp_module Is System.DBNull.Value Or .sTyp_module = "" Then
			If mclsProduct.sTyp_module = vbNullString Then
				lstrTypModule = vbNullString
			Else
				lstrTypModule = mclsProduct.sTyp_module
			End If
		Else
			lstrTypModule = .sTyp_module
		End If
		
		'+Se asigna el Valor al campo "Recargos/Descuentos"
		If .sTyp_discxp Is System.DBNull.Value Or .sTyp_discxp = "" Then
			If mclsProduct.sTyp_discxp = vbNullString Then
				lstrTypDiscxp = vbNullString
			Else
				lstrTypDiscxp = mclsProduct.sTyp_discxp
			End If
		Else
			lstrTypDiscxp = .sTyp_discxp
		End If
		
		'+Se asigna el Valor al campo "Cláusulas"
		If .sTyp_clause Is System.DBNull.Value Or .sTyp_clause = "" Then
			If mclsProduct.sTyp_clause = vbNullString Then
				lstrTypClause = vbNullString
			Else
				lstrTypClause = mclsProduct.sTyp_clause
			End If
		Else
			lstrTypClause = .sTyp_clause
		End If
		
		'+Se asigna el Valor al campo "Tipo de Recibo"
		If .sColinvot Is System.DBNull.Value Or .sColinvot = "" Then
			If mclsProduct.sColinvot = vbNullString Then
				lstrColInvot = vbNullString
			Else
				lstrColInvot = mclsProduct.sColinvot
			End If
		Else
			lstrColInvot = .sColinvot
		End If
		
		'+Se asigna el Valor al campo "Cantidad de Certificados"
		'+ En caso de no tener se asigna el mínimo indicado para el producto 
		If lblnExistPolicy Then
			lstrQCertif = .nQ_certif
		Else
			lstrQCertif = mclsProduct.nInsminiq
		End If
		
		'+Se asigna el Valor al campo "Tipo de definición de prestaciones"        
		lstrColtpres = CStr(.sColtpres)
		
		'+Se asigna el Valor al campo "Tipo de Evaluación"
		If .sDocuTyp Is System.DBNull.Value Or .sDocuTyp = "" Then
			lstrDocuTyp = "3"
		Else
			lstrDocuTyp = .sDocuTyp
		End If
		
		'+ Se verifica que la forma AM003 se encuentre dentro de la secuencia		
		mblnFindAM003 = False
		If mclsPolicy_Win.Find_Codispl(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), 2), mobjValues.StringToType(Session("nProduct"), 2), mobjValues.StringToType(Session("nPolicy"), 3), mobjValues.StringToType(Session("nCertif"), 3), mobjValues.StringToDate(Session("dEffecdate")), "AM003") Then
			mblnFindAM003 = True
		End If
		
		
	End With
	
End Sub

</script>
<%
    Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA006")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mclsPolicy = New ePolicy.Policy
    mclsProduct = New eProduct.Product
    mclsPolicy_Win = New ePolicy.Policy_Win
    mobjMenu = New eFunctions.Menues
    
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    mobjValues.ActionQuery = Session("bQuery")
    Call insPreCA006()
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
    Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
    mobjMenu = Nothing
    Response.Write(mobjValues.StyleSheet())
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $$Author: Iusr_llanquihue $"
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
    	<%=mobjValues.ShowWindowsName("CA006", Request.QueryString.Item("sWindowDescript"))%>
	<FORM METHOD="post" ID="FORM" NAME="CA006" ACTION="ValPolicySeq.aspx?mode=1">
	<TABLE WIDTH="100%" border=0>
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40719><A NAME="Tipo de renovación"><%= GetLocalResourceObject("AnchorTipo de renovaciónCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN=2></TD>
			<TD WIDTH="50%" COLSPAN="2"><HR></TD>
		<TR>
			<TD><LABEL ID=13016><%= GetLocalResourceObject("txtTariffCaption") %></LABEL></TD>
            <%If lstrTariff Is System.DBNull.Value Or IsNothing(lstrTariff) Or lstrTariff = 0 Or lstrTariff = "-32768" Then%>
				<TD><%=mobjValues.TextControl("txtTariff", 5, "1",  , GetLocalResourceObject("txtTariffToolTip"),  ,  ,  ,  , True)%></TD>
			<%Else%>				
				<TD><%=mobjValues.TextControl("txtTariff", 5, lstrTariff,  , GetLocalResourceObject("txtTariffToolTip"),  ,  ,  ,  , True)%></TD>
			<%End If%>
		    <TD COLSPAN=2>
		        <TABLE>
			        <%If lstrColtimre = 2 Then%>
			        	<TD><%	Response.Write(mobjValues.OptionControl(40720, "sColtimre", GetLocalResourceObject("sColtimre_CStr1Caption"), CStr(0), CStr(1),  ,  ,  , GetLocalResourceObject("sColtimre_CStr1ToolTip")))%></TD>
			        	<TD><%	Response.Write(mobjValues.OptionControl(40721, "sColtimre", GetLocalResourceObject("sColtimre_CStr2Caption"), CStr(1), CStr(2),  ,  ,  , GetLocalResourceObject("sColtimre_CStr2ToolTip")))%></TD>
			        	<TD><%	Response.Write(mobjValues.OptionControl(40722, "sColtimre", GetLocalResourceObject("sColtimre_CStr3Caption"), CStr(0), CStr(3),  ,  ,  , GetLocalResourceObject("sColtimre_CStr3ToolTip")))%></TD>
			        <%Else%>
			        	<%	If lstrColtimre = 3 Then%>
			        		<TD><%		Response.Write(mobjValues.OptionControl(40723, "sColtimre", GetLocalResourceObject("sColtimre_CStr1Caption"), CStr(0), CStr(1),  ,  ,  , GetLocalResourceObject("sColtimre_CStr1ToolTip")))%></TD>
			        		<TD><%		Response.Write(mobjValues.OptionControl(40724, "sColtimre", GetLocalResourceObject("sColtimre_CStr2Caption"), CStr(0), CStr(2),  ,  ,  , GetLocalResourceObject("sColtimre_CStr2ToolTip")))%></TD>
			        		<TD><%		Response.Write(mobjValues.OptionControl(40725, "sColtimre", GetLocalResourceObject("sColtimre_CStr3Caption"), CStr(1), CStr(3),  ,  ,  , GetLocalResourceObject("sColtimre_CStr3ToolTip")))%></TD>
			        	<%	Else%>
			        		<TD><%		Response.Write(mobjValues.OptionControl(40726, "sColtimre", GetLocalResourceObject("sColtimre_CStr1Caption"), CStr(1), CStr(1),  ,  ,  , GetLocalResourceObject("sColtimre_CStr1ToolTip")))%></TD>
			        		<TD><%		Response.Write(mobjValues.OptionControl(40727, "sColtimre", GetLocalResourceObject("sColtimre_CStr2Caption"), CStr(0), CStr(2),  ,  ,  , GetLocalResourceObject("sColtimre_CStr2ToolTip")))%></TD>
			        		<TD><%		Response.Write(mobjValues.OptionControl(40728, "sColtimre", GetLocalResourceObject("sColtimre_CStr3Caption"), CStr(0), CStr(3),  ,  ,  , GetLocalResourceObject("sColtimre_CStr3ToolTip")))%></TD>
			        	<%	End If%>
			        <%End If%>
			    </TABLE>
			</TD>
		</TR>
		<TR>
			<TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
		</TR>
		<TR>
			<TD><LABEL ID=13014><%= GetLocalResourceObject("cbeColReintCaption") %></LABEL></TD>
			<%If lstrColReint Is System.DBNull.Value.ToString Or lstrColReint = "" Then%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeColReint", "table49", 1, CStr(1),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeColReintToolTip")))%></TD>
			<%Else%>				
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeColReint", "table49", 1, CStr(lstrColReint),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeColReintToolTip")))%></TD>
			<%End If%>
			<TD><LABEL ID=13018><%= GetLocalResourceObject("cbeTypModuleCaption") %></LABEL></TD>
			<%If lstrTypModule = vbNullString Then%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypModule", "Table92", 1, CStr(2),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypModuleToolTip")))%></TD>
			<%Else%>				
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypModule", "Table92", 1, CStr(lstrTypModule),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypModuleToolTip")))%></TD>
			<%End If%>
		</TR>
		<TR>
		    <TD><LABEL ID=13017><%= GetLocalResourceObject("cbeTypDiscxpCaption") %></LABEL></TD>
		    <%If lstrTypDiscxp Is System.DBNull.Value.ToString Or lstrTypDiscxp = "" Then%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypDiscxp", "table92", 1, CStr(2),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypDiscxpToolTip")))%></TD>
			<%Else%>				
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypDiscxp", "table92", 1, CStr(lstrTypDiscxp),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypDiscxpToolTip")))%></TD>
			<%End If%>		
			<TD><LABEL ID=13019><%= GetLocalResourceObject("cbeTypClauseCaption") %></LABEL></TD>
			<%If lstrTypClause = vbNullString Then%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypClause", "table92", 1, CStr(2),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypClauseToolTip")))%></TD>
			<%Else%>				
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeTypClause", "table92", 1, CStr(lstrTypClause),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypClauseToolTip")))%></TD>
			<%End If%>	
		</TR>
		<TR>
			<TD><LABEL ID=13013><%= GetLocalResourceObject("cbeColInvotCaption") %></LABEL></TD>
			<%If lstrColInvot = vbNullString Then%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeColInvot", "table50", 1, CStr(2),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeColInvotToolTip")))%></TD>
			<%Else%>				
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeColInvot", "table50", 1, CStr(lstrColInvot),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeColInvotToolTip")))%></TD>
			<%End If%>
			<%If mblnFindAM003 Then%>
			    <TD><LABEL ID=13017><%= GetLocalResourceObject("cbeColtPresCaption") %></LABEL></TD>
			    <%	mobjValues.TypeList = 1
			        mobjValues.List = "2,4"
			        mobjValues.BlankPosition = True%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeColtPres", "table92", eFunctions.Values.eValuesType.clngComboType, mclsPolicy.sColtpres,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeColtPresToolTip")))%></TD>		  
		    <%End If%>		
		</TR>
		<TR>
			<TD><LABEL ID=13015><%= GetLocalResourceObject("tcnQCertifCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.NumericControl("tcnQCertif", 9, lstrQCertif,  , GetLocalResourceObject("tcnQCertifToolTip")))%></TD> 
			<TD><LABEL><%= GetLocalResourceObject("tcnCNoticeCaption") %></LABEL></TD> 
			<TD><%Response.Write(mobjValues.NumericControl("tcnCNotice", 5, lstrNclaim,  , GetLocalResourceObject("tcnCNoticeToolTip")))%></TD>
		</TR>
		<TR>
		    <%If mclsPolicy.sMassive <> vbNullString Then%>
		        <TD COLSPAN="1"><%=mobjValues.CheckControl("chkMassive", GetLocalResourceObject("chkMassiveCaption"), mclsPolicy.sMassive, "1",  ,  ,  , GetLocalResourceObject("chkMassiveToolTip"))%><TD>
		    <%Else%>
		        <TD COLSPAN="1"><%=mobjValues.CheckControl("chkMassive", GetLocalResourceObject("chkMassiveCaption"), mclsProduct.sMassive, "1",  ,  ,  , GetLocalResourceObject("chkMassiveToolTip"))%><TD>
		    <%End If%>
		    <TD COLSPAN=2><%=mobjValues.CheckControl("chkRepPrintCov", GetLocalResourceObject("chkRepPrintCovCaption"), mclsPolicy.sRepPrintCov, "1",  , Session("nCertif") <> 0,  , "")%></TD>
		</TR>
		<TR>
		    <TD><LABEL><%= GetLocalResourceObject("cbeDocuTypCaption") %></LABEL></TD>
		    <%If lstrDocuTyp Is System.DBNull.Value.ToString Or lstrDocuTyp = "" Then%>
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeDocuTyp", "table8020", 1, CStr(3),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDocuTypToolTip")))%></TD>
			<%Else%>				
				<TD><%	Response.Write(mobjValues.PossiblesValues("cbeDocuTyp", "table8020", 1, CStr(lstrDocuTyp),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDocuTypToolTip")))%></TD>
			<%End If%>		
			<!--<TD COLSPAN=2><%'=mobjValues.CheckControl("chkRepPrintCov", GetLocalResourceObject("chkRepPrintCovCaption"),mclspolicy.sRepPrintCov,"1",,Session("nCertif")<>0,,"")%></TD>-->
		</TR>

        <TR>
			<TD><LABEL ID=LABEL1><%= GetLocalResourceObject("cbeTypeExcCaption") %></LABEL></TD>
            <% mobjValues.BlankPosition = False%>
			<TD><%Response.Write(mobjValues.PossiblesValues("cbenTypeExc", "table2", eFunctions.Values.eValuesType.clngComboType, mclsCertificat.nTypeExc, , , , , , , , 5, GetLocalResourceObject("cbeTypeExcToolTip")))%>
			</TD>
        </TR>        


	</TABLE>
</FORM>
</BODY>
</HTML>
<%
    mobjValues = Nothing
    mclsPolicy = Nothing
    mclsProduct = Nothing
    mclsPolicy_Win = Nothing

    '^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Call mobjNetFrameWork.FinishPage("CA006")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer
%>




