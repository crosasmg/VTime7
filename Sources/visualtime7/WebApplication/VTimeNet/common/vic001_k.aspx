<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo y evaluación de los procesos de póliza.
Dim mclsPolicy As ePolicy.Policy
Dim mdtmEffectDate As Object

'- Variables que guardan la informacion de la póliza
Dim mdtmIssuedate As Object
Dim mdtmExpirdate As Object
Dim mdtmExp_Dat_Pre As Object
Dim mdtmExpire As Object
Dim mdtmEffecdate_Pol As Object
Dim mintQuanti_Pen As Object
Dim mintAmount_Pen As Object
Dim mintLoans As Object
Dim mintYears As Object
Dim mintMonths As Object
Dim mintSalvage As Object
Dim mintAvailMax As Object
Dim mintCap_Reduc As Object
Dim mintCapInitial As Object
Dim mintProdClas As Object
Dim mdtmCertif As Object


'% insFindPolicy: se realiza la busqueda de los datos que corresponden a la poliza de vida.
'--------------------------------------------------------------------------------------------
Private Sub insFindPolicy()
	'--------------------------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	Dim mobjValPolicyTra As ePolicy.ValPolicyTra
	lclsProduct = New eProduct.Product
	
	mobjValPolicyTra = New ePolicy.ValPolicyTra
	
	With mobjValues
		If lclsProduct.FindProduct_li(.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mdtmEffectDate) Then
			mintProdClas = lclsProduct.nProdClas
		End If
		lclsProduct = Nothing
		
		Call mobjValPolicyTra.InsPreVI009("1", "1", Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mdtmCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mdtmEffectDate, eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), eRemoteDB.Constants.intNull, CInt("1"), eRemoteDB.Constants.intNull, "VI009")
		
		If mclsPolicy.FindVIC001(.StringToType(Request.QueryString.Item("nTypeProce"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCertype"), .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(mdtmCertif, eFunctions.Values.eTypeData.etdDouble), mdtmEffectDate) Then
			
			If mclsPolicy.dIssuedat = eRemoteDB.Constants.dtmNull Then
				mdtmIssuedate = vbNullString
			Else
				mdtmIssuedate = mclsPolicy.dIssuedat
			End If
			If mclsPolicy.dEffecdateV = eRemoteDB.Constants.dtmNull Then
				mdtmEffecdate_Pol = vbNullString
			Else
				mdtmEffecdate_Pol = mclsPolicy.dEffecdateV
			End If
			If mclsPolicy.dExpirdat = eRemoteDB.Constants.dtmNull Then
				mdtmExpirdate = vbNullString
			Else
				mdtmExpirdate = mclsPolicy.dExpirdat
			End If
			If mclsPolicy.nQuanti_pen = 0 Then
				mintQuanti_Pen = 0
			Else
				mintQuanti_Pen = mclsPolicy.nQuanti_pen
			End If
			If mclsPolicy.nAmount_pen = 0 Then
				mintAmount_Pen = 0
			Else
				mintAmount_Pen = mclsPolicy.nAmount_pen
			End If
			If mclsPolicy.dExp_dat_pre = eRemoteDB.Constants.dtmNull Then
				mdtmExp_Dat_Pre = vbNullString
			Else
				mdtmExp_Dat_Pre = mclsPolicy.dExp_dat_pre
			End If
			If mclsPolicy.nLoans = 0 Then
				mintLoans = 0
			Else
				mintLoans = mclsPolicy.nLoans
			End If
			
			
			
			mintYears = mobjValPolicyTra.nYear
			mintMonths = mobjValPolicyTra.nMonth
			
			
			If mobjValues.StringToType(mobjValPolicyTra.DefaultValueVI009("tcnSurrVal"), eFunctions.Values.eTypeData.etdDouble) = 0 Then
				mintSalvage = 0
			Else
				mintSalvage = mobjValues.StringToType(mobjValPolicyTra.DefaultValueVI009("tcnSurrVal"), eFunctions.Values.eTypeData.etdDouble)
			End If
			'            If mclsPolicy.nSalvage = 0 Then
			'                mintSalvage = 0
			'            Else
			'                mintSalvage =  mclsPolicy.nSalvage        
			'            end If
			
			If mobjValues.StringToType(mobjValPolicyTra.DefaultValueVI009("tcnSurrAmount", eRemoteDB.Constants.intNull), eFunctions.Values.eTypeData.etdDouble) = 0 Then
				mintAvailMax = 0
			Else
				mintAvailMax = mobjValues.StringToType(mobjValPolicyTra.DefaultValueVI009("tcnSurrAmount", eRemoteDB.Constants.intNull), eFunctions.Values.eTypeData.etdDouble)
			End If
			
			'            If mclsPolicy.nAvailMax < 0 Then
			'                mintAvailMax = 0
			'            Else
			'                mintAvailMax = mclsPolicy.nAvailMax      
			'            end If
			If mclsPolicy.nCap_reduc = 0 Then
				mintCap_Reduc = 0
			Else
				mintCap_Reduc = mclsPolicy.nCap_reduc
			End If
			If mclsPolicy.dExpire = eRemoteDB.Constants.dtmNull Then
				mdtmExpire = vbNullString
			Else
				mdtmExpire = mclsPolicy.dExpire
			End If
			If mintCapInitial = eRemoteDB.Constants.intNull Then
				mintCapInitial = 0
			Else
				mintCapInitial = mclsPolicy.nCap_initial
			End If
		End If
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mclsPolicy = New ePolicy.Policy

mobjValues.sCodisplPage = "vic001_k"

'+ Se inicializa la action de la página 
mobjValues.ActionQuery = True

If Request.QueryString.Item("dEffectDate") > vbNullString Then
	mdtmEffectDate = Mid(Request.QueryString.Item("dEffectDate"), 1, 2) & "/" & Mid(Request.QueryString.Item("dEffectDate"), 4, 2) & "/" & Mid(Request.QueryString.Item("dEffectDate"), 7)
Else
	mdtmEffectDate = 0
End If
If Not IsNothing(Request.QueryString.Item("nCertif")) Then
	mdtmCertif = Request.QueryString.Item("nCertif")
Else
	mdtmCertif = 0
End If
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.34 $|$$Author: Nvaplat60 $"

//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true;
}
</SCRIPT>
</HEAD>
<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle("VIC001"))
%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VIC001_K" ACTION="VIC001_K.aspx">
<%
Response.Write(mobjValues.ShowWindowsName("VIC001"))
Call insFindPolicy()
%>  
<BR>
<TABLE WIDTH="100%">
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctIssuedateCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctIssuedate", 10, mdtmIssuedate, False, GetLocalResourceObject("tctIssuedateToolTip"),  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>
        <TD>&nbsp;</TD>
    </TR>
    <TR>
        <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Vigencia"><%= GetLocalResourceObject("AnchorVigenciaCaption") %></A></LABEL><hr></TD>
        <TD>&nbsp;</TD>
        <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="RecibosPend"><%= GetLocalResourceObject("AnchorRecibosPendCaption") %></A></LABEL><hr></TD>
    </TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctEffecdatCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctEffecdat", 10, mdtmEffecdate_Pol, False, GetLocalResourceObject("tctEffecdatToolTip"),  ,  ,  ,  , True)%></TD>
        <TD>&nbsp;</TD>
        <TD ALIGN="RIGHT"><LABEL ID=0><%= GetLocalResourceObject("tcnQuant_penCaption") %></LABEL></TD>
        <TD ALIGN="RIGHT"><%=mobjValues.NumericControl("tcnQuant_pen", 4, mintQuanti_Pen, False, GetLocalResourceObject("tcnQuant_penToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>    
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctExpirdateCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctExpirdate", 10, mdtmExpirdate, False, GetLocalResourceObject("tctExpirdateToolTip"),  ,  ,  ,  , True, 4)%></TD>
        <TD>&nbsp;</TD>
        <TD ALIGN="RIGHT"><LABEL ID=0><%= GetLocalResourceObject("tcnAmount_penCaption") %></LABEL></TD>
        <TD ALIGN="RIGHT"><%=mobjValues.NumericControl("tcnAmount_pen", 18, mintAmount_Pen, False, GetLocalResourceObject("tcnAmount_penToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>
        <TD>&nbsp;</TD>
    </TR>
    <TR>
        <TD>&nbsp;</TD>
        <TD>&nbsp;</TD>
        <TD>&nbsp;</TD>
        <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="TiempoTrans"><%= GetLocalResourceObject("AnchorTiempoTransCaption") %></A></LABEL><hr></TD>
    </TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctExp_date_preCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctExp_date_pre", 10, mdtmExp_Dat_Pre, False, GetLocalResourceObject("tctExp_date_preToolTip"),  ,  ,  ,  , True)%></TD>
        <TD>&nbsp;</TD>            
        <TD ALIGN="RIGHT"><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
        <TD ALIGN="RIGHT"><%=mobjValues.NumericControl("tcnYear", 6, mintYears, False, GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnLoansCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnLoans", 18, mintLoans, False, GetLocalResourceObject("tcnLoansToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        <TD>&nbsp;</TD>            
        <TD ALIGN="RIGHT"><LABEL ID=0><%= GetLocalResourceObject("tcnMonthCaption") %></LABEL></TD>
        <TD ALIGN="RIGHT"><%=mobjValues.NumericControl("tcnMonth", 6, mintMonths, False, GetLocalResourceObject("tcnMonthToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctSalvageCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctSalvage", 10, mintSalvage, False, GetLocalResourceObject("tctSalvageToolTip"),  ,  ,  ,  , True)%></TD>
    </TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctAvailMCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctAvailM", 10, mintAvailMax, False, GetLocalResourceObject("tctAvailMToolTip"),  ,  ,  ,  , True)%></TD>
    </TR>
    <%If Request.QueryString.Item("nTypeProce") = "1" Then%> 
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCapInitialCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnCapInitial", 18, mintCapInitial, False, GetLocalResourceObject("tcnCapInitialToolTip"), True, 6,  ,  , CStr(True))%></TD>
    </TR>
    <%End If%>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCap_redCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnCap_red", 18, mintCap_Reduc, False, GetLocalResourceObject("tcnCap_redToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
    </TR>
    <%If Request.QueryString.Item("nTypeProce") = "2" Then%>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tctExpirdateCaption") %></LABEL></TD>
        <TD><%=mobjValues.TextControl("tctExpired", 10, mdtmExpire, False, GetLocalResourceObject("tctExpiredToolTip"),  ,  ,  ,  , True)%></TD>
    </TR>
		<%	If mintProdClas = "1" Then%>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCap_redCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnCap_red", 18, mintCap_Reduc, False, GetLocalResourceObject("tcnCap_redToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
    </TR>
		<%	End If
End If%>
    <TR>
        <TD>&nbsp;</TD>
        <TD>&nbsp;</TD>
        <TD>&nbsp;</TD>
        <TD>&nbsp;</TD>
        <TD ALIGN="RIGHT">
        <%
mobjValues.ActionQuery = False
Response.Write(mobjValues.ButtonAcceptCancel( ,  ,  ,  , 2, 14))
%>
		</TD>
    </TR>
</TABLE>
<%
mobjValues = Nothing
mclsPolicy = Nothing
%>
</FORM>
</BODY>
</HTML>




