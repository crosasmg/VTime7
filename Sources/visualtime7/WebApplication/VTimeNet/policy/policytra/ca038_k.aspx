<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mclsPolicy As ePolicy.Policy
Dim mclsCertificat As ePolicy.Certificat

Dim mintBranch As Object
Dim mintProduct As Object
Dim mlngPolicy As Object
Dim mlngCertif As String


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddClientColumn(0, GetLocalResourceObject("deClientColumnCaption"), "deClient", vbNullString,  , GetLocalResourceObject("deClientColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeRoleColumnCaption"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CA038"
		.Columns("Sel").GridVisible = False
		.DeleteButton = False
		.AddButton = False
	End With
End Sub

'%insPreFolder: Permite cargar en los controles de la pagina los valores de 
'%la póliza/certificado
'--------------------------------------------------------------------------------------------
Private Sub insPreFolder()
	'--------------------------------------------------------------------------------------------
	With mclsPolicy
		If .Find("2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), True) Then
			
			Session("sColtimre") = .sColtimre
			Session("sPolitype") = .sPolitype
			
			If Not mclsCertificat.Find("2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then
				.nOffice = CShort(0)
			End If
		End If
	End With
End Sub

'%LoadGrid: Permite Cargar las figuras de las pólizas en el grid
'---------------------------------------------------------------------------------------------
Private Sub LoadGrid()
	'---------------------------------------------------------------------------------------------
	Dim lobjGrid As eFunctions.Grid
	Dim lcolRoleses As ePolicy.Roleses
	Dim lintCount As Integer
	
	lintCount = 0
	
	With Server
		lobjGrid = New eFunctions.Grid
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
		lobjGrid.sSessionID = Session.SessionID
		lobjGrid.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		lobjGrid.sCodisplPage = "ca038_k"
		Call lobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
		lcolRoleses = New ePolicy.Roleses
		lobjGrid = Nothing
	End With
	
	If lcolRoleses.Find_by_Policy("2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), vbNullString, Today) Then
		For lintCount = 1 To lcolRoleses.Count
        'For lintCount = 0 To lcolRoleses.Count -1
			With mobjGrid
				.Columns("deClient").DefValue = lcolRoleses.Item(lintCount).sClient
				.Columns("cbeRole").DefValue = CStr(lcolRoleses.Item(lintCount).nRole)
			End With
			Response.Write(mobjGrid.DoRow())
		Next 
	End If
	
	Response.Write(mobjGrid.closeTable())
	lcolRoleses = Nothing
End Sub

'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	If Request.QueryString.Item("mintBranch") <> vbNullString Then
		mintBranch = Request.QueryString.Item("mintBranch")
		Session("nBranch") = mintBranch
	End If
	If Request.QueryString.Item("mintProduct") <> vbNullString Then
		mintProduct = Request.QueryString.Item("mintProduct")
		Session("nProduct") = mintProduct
	End If
	If Request.QueryString.Item("mlngPolicy") <> vbNullString Then
		mlngPolicy = Request.QueryString.Item("mlngPolicy")
		Session("nPolicy") = mlngPolicy
	End If
	If Request.QueryString.Item("mlngCertif") <> vbNullString Then
		mlngCertif = Request.QueryString.Item("mlngCertif")
		Session("nCertif") = mlngCertif
	End If
End Sub

'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mintBranch <> 0 And mintProduct <> 0 And mlngPolicy <> 0 Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = " & CStr(mintBranch) & ";")
			.Write("var mintProduct = " & CStr(mintProduct) & ";")
			.Write("var mlngPolicy = " & CStr(mlngPolicy) & ";")
			If CStr(mlngCertif) = vbNullString Then
				.Write("var mlngCertif = 0;")
			Else
				.Write("var mlngCertif = " & CStr(mlngCertif) & ";")
			End If
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = 0;")
			.Write("var mintProduct = 0;")
			.Write("var mlngPolicy = 0;")
			.Write("var mlngCertif = 0;")
			.Write("</" & "Script>")
		End With
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca038_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca038_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "ca038_k"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
mclsPolicy = New ePolicy.Policy
mclsCertificat = New ePolicy.Certificat

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("CA038", Request.QueryString.Item("sWindowDescript")))
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//% ChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para 
//%					  el producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ChangeValues(sField){
//-------------------------------------------------------------------------------------------
	switch(sField){
		case "Branch":
			 with (self.document.forms[0]){			
				elements["tcnPolicy"].value = ""
				elements["tcnPolicy"].disabled = true
				elements["tcnCertif"].disabled = true
				elements["tcdExpirdate"].value = ""
				elements["tcdExpirdate"].disabled = true
				elements["tcdNextReceip"].value = ""
				elements["tcdNextReceip"].disabled = true
			 }
			 break;
		case "Product":
			 with (self.document.forms[0]){
				elements["tcnPolicy"].disabled = false
				elements["tcdExpirdate"].disabled = false
				elements["tcdNextReceip"].disabled = false
			 }
	}
}   

   
//%	LoadPolicy: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function LoadPolicy(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if ((mintBranch != cbeBranch.value) ||
		    (mintProduct != valProduct.value) ||
		    (mlngPolicy != tcnPolicy.value) ||
		    (mlngCertif != tcnCertif.value)){
		    if (tcnPolicy.value != "")
				self.document.location.href="CA038_k.aspx?sCodispl=CA038&mlngPolicy="+tcnPolicy.value+"&mintBranch=" + cbeBranch.value + "&mintProduct=" + valProduct.value + "&mlngCertif=" + tcnCertif.value + "&sField=" + Field.name
			else
				tcnCertif.value = ""
		}
    }
}
</SCRIPT>
<%
Response.Write("<SCRIPT>")
Response.Write("function insCancel(){")

Session("nBranch") = "0"
Session("nProduct") = 0
Session("nPolicy") = ""
Session("nCertif") = 0
Response.Write(" return true; } ")
Response.Write(" </SCRIPT> ")
%>
<%
With Response
	.Write(mobjMenu.MakeMenu("CA038", "CA038_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR></BR>
<FORM METHOD="post" ID="FORM" NAME="frmRenDateChange" ACTION="ValPolicyTra.aspx?x=1&sColtimre=<%=mclsPolicy.sColtimre%>&sPolitype=<%=mclsPolicy.sPolitype%>">
    <%=mobjValues.ShowWindowsName("CA038", Request.QueryString.Item("sWindowDescript"))%>
    <%
Call insReaInitial()
Call insPreFolder()
Call insOldValues()
%>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID=13937><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%
If Session("nBranch") <> 0 Then
	Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Session("nBranch"),  ,  ,  ,  , "ChangeValues(""Branch"")"))
Else
	Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "ChangeValues(""Branch"")"))
End If%>
			</TD>
		    <TD><LABEL ID=13947><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<%
With mobjValues.Parameters
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>          
			<%If CStr(Session("nProduct")) <> vbNullString And Session("nBranch") <> 0 Then%>
					<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType,  , Session("nProduct"),  ,  ,  , "ChangeValues(""Product"")")%></TD>
            <%Else%>
					<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  , "ChangeValues(""Product"")")%>
			<%End If%>
        </TR>
        <TR>
			<%Response.Write(mobjValues.HiddenControl("sCertype", "2"))%>
            <TD><LABEL ID=13946><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, Session("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "LoadPolicy(this)")%></TD>
			<TD><LABEL ID=13938><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <%If mclsPolicy.sPolitype <> "1" Then%> 
				<TD><%=mobjValues.NumericControl("tcnCertif", 10, Session("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "LoadPolicy(this)")%></TD>
            <%Else%> 
				<TD><%=mobjValues.NumericControl("tcnCertif", 10, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
			<%End If%> 
        </TR>
        <TR>
			<TD><LABEL ID=13939><%= GetLocalResourceObject("tcdExpirdateCaption") %></LABEL></TD>
			<%'	If mclsCertificat.nNullcode = 0 Then %> 
				<TD><%=mobjValues.DateControl("tcdExpirdate", mobjValues.TypeToString(mclsCertificat.dExpirdat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdExpirdateToolTip"))%></TD>
            
			<TD><LABEL ID=13942><%= GetLocalResourceObject("tcdNextReceipCaption") %></LABEL></TD>
			<%'	If mclsCertificat.nNullcode = 0 Then %> 
					<TD><%=mobjValues.DateControl("tcdNextReceip", mobjValues.TypeToString(mclsCertificat.dNextReceip, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdNextReceipToolTip"),  ,  ,  ,  , True)%></TD>
           
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Tipo de recibo"><%= GetLocalResourceObject("AnchorTipo de reciboCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><%=mobjValues.OptionControl(0, "optReceiptType", GetLocalResourceObject("optReceiptType_1Caption"), eFunctions.Values.vbChecked, "1")%></TD>
            <TD><%=mobjValues.OptionControl(0, "optReceiptType", GetLocalResourceObject("optReceiptType_2Caption"),  , "2")%></TD>
            <TD><%=mobjValues.OptionControl(0, "optReceiptType", GetLocalResourceObject("optReceiptType_3Caption"),  , "3")%></TD>
            <TD></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Datos de verificación de la póliza"><%= GetLocalResourceObject("AnchorDatos de verificación de la pólizaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
        <%mobjValues.ActionQuery = True%>
        <TR>
			<TD><LABEL ID=13945><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(mclsPolicy.nOffice),  , True,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeOfficeToolTip"))%> </TD>
			<TD><LABEL ID=13941><%= GetLocalResourceObject("tcdIssuedatCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdIssuedat", mobjValues.TypeToString(mclsCertificat.dIssuedat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdIssuedatToolTip"))%></TD>
		</TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL><A NAME="Vigencia"><%= GetLocalResourceObject("AnchorVigenciaCaption") %></A></LABEL></TD>
            
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Anulación"><%= GetLocalResourceObject("AnchorAnulaciónCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
            
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>        
            <TD><LABEL ID=13821><%= GetLocalResourceObject("tcdFromDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdFromDate", mobjValues.TypeToString(mclsCertificat.dStartdate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdFromDateToolTip"))%></TD>
			
			<TD><LABEL ID=13825><%= GetLocalResourceObject("tcdNullDatCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdNullDat", mobjValues.TypeToString(mclsCertificat.dNulldate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdNullDatToolTip"))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=13822><%= GetLocalResourceObject("tcdToDateCaption") %></LABEL></TD>
		    <TD><%=mobjValues.DateControl("tcdToDate", mobjValues.TypeToString(mclsCertificat.dExpirdat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdToDateToolTip"))%></TD>
    		
			<TD><LABEL ID=13824><%= GetLocalResourceObject("cbeNulldescCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("cbeNulldesc", "Table13", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCertificat.nNullcode),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeNulldescToolTip"))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Figuras presentes en la póliza"><%= GetLocalResourceObject("AnchorFiguras presentes en la pólizaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>
	</TABLE>
<%
Call insDefineHeader()
Call LoadGrid()
mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing
mclsPolicy = Nothing
mclsCertificat = Nothing
Response.Write("<SCRIPT>document.forms[0].cbeBranch.focus()</SCRIPT>")
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca038_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




