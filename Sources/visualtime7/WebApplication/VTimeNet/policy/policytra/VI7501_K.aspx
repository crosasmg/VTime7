<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las funciones del menú
Dim mobjMenu As eFunctions.Menues

Dim mPuntual As String
Dim mPolicy As Byte
Dim mPproponum As Byte
Dim mdEffecdate_saapv As String

Dim mblnControl As Boolean


'% LoadHeader: se cargan los datos del encabezado
'--------------------------------------------------------------------------------------------
Private Sub LoadHeader()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Tipo de SAAPV</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeType_saapv", "table5742", eFunctions.Values.eValuesType.clngComboType, CStr(Session("nType_saapv")),  ,  ,  ,  ,  , "insLimitDate();insShowInstitution();insChangeNtype_saapv(this)", mblnControl,  , "Tipo de SAAPV"))


Response.Write("</TD>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>Institución</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("valInstitution", "TabTab_Fn_Institu", eFunctions.Values.eValuesType.clngWindowType, CStr(Session("nInstitution")),  ,  ,  ,  ,  ,  , True,  , "Institución que genera el SAAPV"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>Folio</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcncod_saapv", 10, CStr(Session("nCod_saapv")),  , "Número de Folio",  ,  ,  ,  ,  , "insShowSaapv2()", CStr(Session("nCod_saapv")) <> ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("hddCertype", CStr(Session("sCertype_saapv"))))


Response.Write("</TD>         " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("hddPuntual", mPuntual))


Response.Write("</TD>            " & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("         </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14941>Fecha </LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdissue_dat", mdEffecdate_saapv,  , "Fecha en que se crea la saapv",  ,  ,  , "insLimitDate()", mblnControl))


Response.Write("</TD> " & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            <TD><LABEL ID=14941>Fecha Limite</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdLimitDate", CStr(Session("dlimitdate")),  , "Fecha Limite de la saapv",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            <TD><LABEL ID=14942> Estado</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbestatus_saapv", "table5741", eFunctions.Values.eValuesType.clngComboType, CStr(Session("nStatus")),  ,  ,  ,  ,  ,  , True,  , "Estado"))


Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14940>Ramo</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", "Código del ramo al que pertenece la póliza a tratar", CStr(Session("nBranch_saapv")),  ,  ,  ,  ,  , mblnControl))


Response.Write("</TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            <TD><LABEL ID=14943>Producto</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ProductControl("valProduct", "Producto al que pertenece la póliza o certificado a tratar",  ,  ,  , CStr(Session("nProduct_saapv"))))


Response.Write("</TD>" & vbCrLf)
Response.Write("                " & vbCrLf)
Response.Write("           ")

	If mblnControl Then
Response.Write("" & vbCrLf)
Response.Write("        		<TD>")


Response.Write(mobjValues.OptionControl(0, "optCertype", "Propuesta", CStr(mPproponum), CStr(1),  , True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("        		<TD>")


Response.Write(mobjValues.OptionControl(0, "optCertype", "Poliza", CStr(mPolicy), CStr(2),  , True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("			" & vbCrLf)
Response.Write("        		<TD>")


Response.Write(mobjValues.OptionControl(0, "optCertype", "Propuesta", CStr(mPproponum), CStr(1),  , False, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("        		<TD>")


Response.Write(mobjValues.OptionControl(0, "optCertype", "Poliza", CStr(mPolicy), CStr(2),  , False, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("			" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, CStr(Session("nPolicy_saapv")),  , "Número de Propuesta o poliza",  ,  ,  ,  ,  ,  , mblnControl))


Response.Write("</TD>            " & vbCrLf)
Response.Write("        </TR>           " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=14942>Tipo de endoso</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbetype_ameapv", "table5743", eFunctions.Values.eValuesType.clngComboType, CStr(Session("Ntype_ameapv")),  ,  ,  ,  ,  ,  , mblnControl,  , "Tipo de endoso"))


Response.Write("</TD>        " & vbCrLf)
Response.Write("            <TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    </TABLE>")

	
	'Response.Write"<script>alert(""" &   Request.QueryString ("nType_saapv")& """)</" & "Script>"
End Sub

</script>
<%Response.Expires = -1441

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values
'mobjValues.sCodisplPage="VI7501_K"
'mobjValues.sCodisplPage="vi7501_k"
mobjValues.sCodisplPage = "cal006_k"

mblnControl = False
If Request.QueryString("Policy") = vbNullString Then
	Session("nCod_saapv") = ""
	Session("nInstitution") = ""
	Session("nType_saapv") = ""
	Session("dEffecdate_saapv") = ""
	mdEffecdate_saapv = ""
	Session("sCertype_saapv") = ""
	Session("nBranch_saapv") = ""
	Session("nProduct_saapv") = ""
	Session("nPolicy_saapv") = ""
	Session("sLinkSpecial") = ""
	mPuntual = "1"
	Session("nStatus") = ""
	Session("Ntype_ameapv") = ""
	Session("dlimitdate") = ""
	mPolicy = 1
	mPproponum = 0
	
Else
	'La página VI7500.aspx invoca a ésta página con las variables que terminan en 2. 	
	Session("nCod_saapv") = Request.QueryString("nCod_saapv")
	Session("nInstitution") = Request.QueryString("nInstitution")
	Session("sCertype_saapv") = Request.QueryString("sCertype2")
	Session("nBranch_saapv") = Request.QueryString("nBranch2")
	Session("nProduct_saapv") = Request.QueryString("nProduct2")
	Session("dEffecdate_saapv") = Request.QueryString("dEffecdate")
	mPuntual = "2"
	mblnControl = True
	If Request.QueryString("Policy") = "2" Then
		Session("nType_saapv") = Request.QueryString("nType_saapv")
		Session("nStatus") = Request.QueryString("nStatus")
		Session("sCertype_saapv") = Request.QueryString("sCertype")
		Session("nBranch_saapv") = Request.QueryString("nBranch")
		Session("nProduct_saapv") = Request.QueryString("nProduct")
		Session("nPolicy_saapv") = Request.QueryString("nPolicy")
		Session("nCertif_saapv") = Request.QueryString("nCertif")
		Session("dEffecdate_saapv") = Request.QueryString("dEffecdate")
		Session("Ntype_ameapv") = Request.QueryString("Ntype_ameapv")
		Session("dlimitdate") = Request.QueryString("dlimitdate")
		mdEffecdate_saapv = Session("dEffecdate_saapv")
	Else
		If Request.QueryString("nType_saapv") = "" Then
			If CStr(Session("sCertype_saapv")) = "6" Then
				Session("nType_saapv") = "2"
			Else
				Session("nType_saapv") = "1"
			End If
			mdEffecdate_saapv = Session("dEffecdate_saapv")
			Session("nStatus") = "1"
		Else
			mdEffecdate_saapv = Request.QueryString("dEffecdate")
			Session("nType_saapv") = Request.QueryString("nType_saapv")
			Session("nStatus") = Request.QueryString("nStatus")
			Session("Ntype_ameapv") = Request.QueryString("Ntype_ameapv")
			Session("dlimitdate") = Request.QueryString("dlimitdate")
			Session("nPolicy_saapv") = Request.QueryString("npolicy2")
		End If
	End If
	
	If CStr(Session("sCertype_saapv")) = "2" Then
		mPolicy = 1
		mPproponum = 0
	Else
		mPolicy = 0
		mPproponum = 1
	End If
End If

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
        <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/ValFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("VI7501_K", "VI7501_K.aspx", 1, ""))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>

<SCRIPT>
//% insCancel: se controla la acción Cancelar de la ventana
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	<%If Request.QueryString("nPolicy_saapv") = vbNullString Then%>
		top.location.reload();
	<%Else%>
	    top.window.close();
	    top.opener.location.reload();
	<%End If%>
}

//% insCancel: se controla la acción Cancelar de la ventana
//--------------------------------------------------------------------------------------------
function insShowSaapv(){
//--------------------------------------------------------------------------------------------
	<%If Request.QueryString("nPolicy_saapv") = vbNullString Or Request.QueryString("nPolicy_saapv") = "2" Then%>
	ShowPopUp("/VTimeNet/Policy/Policytra/ShowDefValues.aspx?Field=ShowSaapv" + "&ncod_saapv=" + self.document.forms[0].tcncod_saapv.value , "ShowDefValuesCancel", 1, 1,"no","no",2000,2000);
	<%End If%>
}

//--------------------------------------------------------------------------------------------
function insShowSaapv2(){
//--------------------------------------------------------------------------------------------
	<%If Request.QueryString("nPolicy_saapv") = vbNullString Or Request.QueryString("nPolicy_saapv") = "2" Then%>
	ShowPopUp("/VTimeNet/Policy/Policytra/ShowDefValues.aspx?Field=ShowSaapv" + "&ncod_saapv=" + self.document.forms[0].tcncod_saapv.value + "&nInstitution=" + self.document.forms[0].valInstitution.value, "ShowDefValuesCancel", 1, 1,"no","no",2000,2000);
	<%End If%>
}

//--------------------------------------------------------------------------------------------
function insShowInstitution(){
//--------------------------------------------------------------------------------------------
	<%If Request.QueryString("nPolicy_saapv") = vbNullString Or Request.QueryString("nPolicy_saapv") = "2" Then%>
	ShowPopUp("/VTimeNet/Policy/Policytra/ShowDefValues.aspx?Field=ShowInstitution" + "&nType_saapv=" + self.document.forms[0].cbeType_saapv.value, "ShowDefValuesCancel", 1, 1,"no","no",2000,2000);
	<%End If%>
}

//% insLimitDate: Calcula la fecha limite del saapv
//--------------------------------------------------------------------------------------------
function insLimitDate(){
//--------------------------------------------------------------------------------------------

	if ((self.document.forms[0].tcdissue_dat.value!='') && (self.document.forms[0].cbeType_saapv.value!='')){
		
	    lstrParams = "dissue_dat=" + self.document.forms[0].tcdissue_dat.value +
	     			 "&nType_saapv=" + self.document.forms[0].cbeType_saapv.value
	    insDefValues("LimitDate", lstrParams,"/VTimeNet/Policy/PolicyTra"); 			 
	}
		
}


//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	if(top.frames["fraSequence"].pblnQuery==true)
		return true;
	else
		<%If Request.QueryString("nPolicy_saapv") = vbNullString Then%>
			top.location.reload();
		<%Else%>
		    top.window.close();
		    top.opener.location.reload();
		<%End If%>	
}
//% insChangeNtype_saapv: Según el tipo de SAAPV se habilita o deshabilita el campo Tipo de Endoso.
//------------------------------------------------------------------------------------------------
function insChangeNtype_saapv(lobjNtype_saapv) {
//------------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){		
		switch (lobjNtype_saapv.value) {
  			 case "2":
             cbetype_ameapv.disabled = false;
			 break;
		   default:
		     cbetype_ameapv.disabled = true;
			}
    }
}
</SCRIPT>    
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VI7501" ACTION="valVI7501tra.aspx?sMode=1">
	<P>&nbsp;</P>
<%
Call LoadHeader()
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
If Request.QueryString("nPolicy_saapv") <> vbNullString Then
	Response.Write("<SCRIPT>ClientRequest(302,2);</script>")
End If
%>
</FORM>
</BODY>
</HTML> 





