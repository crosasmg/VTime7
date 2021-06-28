<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para menjo de grid
Dim mobjGrid As eFunctions.Grid

'- Variables para almacenar parametros de pagina
Dim mstrCertype As Object
Dim mintBranch As String
Dim mintProduct As String
Dim mdteEffecdate As Date
Dim mstrClient As String
Dim mstrRegist As String
Dim mstrDigit As String

'- Variables utilizadas para guardar el valor de los distintos checked 
Dim nInd_cobra As Object
Dim nGen_cobra As Object


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "poldata"
	
	With mobjGrid
		.Codispl = "SCA848"
		.AddButton = False
		.DeleteButton = False
	End With
	With mobjGrid.Columns
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct")
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), "cbeBranch")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, "",  , GetLocalResourceObject("tcnCertifColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDate_OrigiColumnCaption"), "tcdDate_Origi",  ,  , GetLocalResourceObject("tcdDate_OrigiColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat",  ,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctClientCodeColumnCaption"), "tctClientCode", 60, "",  , GetLocalResourceObject("tctClientCodeColumnToolTip"),  ,  ,  , True)
		Call .AddHiddenColumn("hddRut","")
            Call .AddHiddenColumn("hddRegist", "")
            Call .AddHiddenColumn("hddAutoDigit", "")

        End With
	mobjGrid.Columns("Sel").GridVisible = False
End Sub

'% insPreSCA003: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSCA003()
	'--------------------------------------------------------------------------------------------
	Dim lcolVPolicyQueryss As ePolicy.VPolicyQuerys
	Dim lclsVPolicyQuerys As ePolicy.VPolicyQuery
	Dim lindex As Short
	Dim bSuccesfullyRead as boolean
	
	lclsVPolicyQuerys = New ePolicy.VPolicyQuery
	lcolVPolicyQueryss = New ePolicy.VPolicyQuerys
	
	lindex = 0
	If String.IsNullOrEmpty(mstrRegist) Then
		bSuccesfullyRead =  lcolVPolicyQueryss.Find_by_role(mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdLong), mstrClient, mdteEffecdate)
	Else
		bSuccesfullyRead =  lcolVPolicyQueryss.Find_by_regist(mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdLong), mstrRegist, mstrDigit , mdteEffecdate)
	End If
	If bSuccesfullyRead Then
		For	Each lclsVPolicyQuerys In lcolVPolicyQueryss
			With mobjGrid
				.Columns("cbeBranch").DefValue = CStr(lclsVPolicyQuerys.nBranch)
				.Columns("valProduct").DefValue = CStr(lclsVPolicyQuerys.nProduct)
				.Columns("tcnPolicy").DefValue = CStr(lclsVPolicyQuerys.nPolicy)
				.Columns("tcnCertif").DefValue = CStr(lclsVPolicyQuerys.nCertif)
				.Columns("tcdDate_Origi").DefValue = CStr(lclsVPolicyQuerys.dDate_Origi)
				.Columns("tcdExpirdat").DefValue = CStr(lclsVPolicyQuerys.dExpirdat)
				.Columns("tctClientCode").DefValue = lclsVPolicyQuerys.sClient
				.Columns("hddRut").DefValue = lclsVPolicyQuerys.sClientA
				.Columns("tcnCertif").HRefScript = "InsClickValue(" & lindex & ",'" & Request.QueryString.Item("sCodispl") & "');"
				.Columns("cbeBranch").HRefScript = "InsClickValue(" & lindex & ",'" & Request.QueryString.Item("sCodispl") & "');"
				.Columns("valProduct").HRefScript = "InsClickValue(" & lindex & ",'" & Request.QueryString.Item("sCodispl") & "');"
                    .Columns("tcnPolicy").HRefScript = "InsClickValue(" & lindex & ",'" & Request.QueryString.Item("sCodispl") & "');"
                    .Columns("hddRegist").DefValue = lclsVPolicyQuerys.sRegist
                    .Columns("hddAutoDigit").DefValue = lclsVPolicyQuerys.sAutoDigit
                    Response.Write(.DoRow)
				lindex = lindex + 1
			End With
		Next lclsVPolicyQuerys
	End If
	response.Write(mobjGrid.closeTable())
	lcolVPolicyQueryss = Nothing
	lclsVPolicyQuerys = Nothing
End Sub

</script>
<%response.Expires = -1

mobjValues = New eFunctions.Values

'+ Se deja la pagina en modo consulta     
mobjValues.ActionQuery = True

'+ Se asignan valores de parámetros     
mintBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
mintProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
mdteEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
mstrClient = Request.QueryString.Item("sClient")
mstrRegist = UCase(Request.QueryString.Item("sregist"))
mstrDigit = UCase(Request.QueryString.Item("sdigit"))
mobjValues.sCodisplPage = "poldata"
%>

<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 9/02/04 18:14 $"
	
//% InsClickValue: 
//----------------------------------------------------------------------------------------------------------------
function InsClickValue(Index,sCodispl){
//----------------------------------------------------------------------------------------------------------------
	if (sCodispl=="SI001"){
		opener.top.frames['fraHeader'].document.forms[0].cbeBranch.value = marrArray[Index].cbeBranch
		opener.top.frames['fraHeader'].document.forms[0].valProduct.value = marrArray[Index].valProduct
		opener.top.frames['fraHeader'].document.forms[0].tcnPolicy.value = marrArray[Index].tcnPolicy
		try {
			opener.top.frames['fraHeader'].document.forms[0].tcnCertificat.value = marrArray[Index].tcnCertif
		}
		catch (e) {
			opener.top.frames['fraHeader'].document.forms[0].tcnCertif.value = marrArray[Index].tcnCertif

		}
		opener.top.frames['fraHeader'].$('#tcnPolicy').change();
        try{
        	opener.top.frames['fraHeader'].document.forms[0].dtcClient.value = marrArray[Index].hddRut;
        	opener.top.frames['fraHeader'].$('#dtcClient').change();
		}
        catch(e){
		}
        try {
            opener.top.frames['fraHeader'].document.forms[0].tctRegister.value = marrArray[Index].hddRegist;
            opener.top.frames['fraHeader'].document.forms[0].tctDigit.value = marrArray[Index].hddAutoDigit;
        }
        catch (e) {
        }

		window.close();
	}
	if (sCodispl=="SI737"){
		opener.self.document.forms[0].tcnPolicy.value = marrArray[Index].tcnPolicy
		opener.self.document.forms[0].tcnCertif.value = marrArray[Index].tcnCertif
		opener.$('#tcnCertif').change();
		window.close();
	}
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
    <%=mobjValues.WindowsTitle("SCA848")%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPolData" ACTION="PolData.aspx">

<%With response
	.Write(mobjValues.ShowWindowsName("SCA848"))
	.Write("<BR>")
End With
%>

<%

Call insDefineHeader()
Call insPreSCA003()

With response
	
	.Write(mobjGrid.closeTable())
	.Write("<P ALIGN=""RIGHT"">")
	mobjValues.ActionQuery = False
	.Write(mobjValues.ButtonAcceptCancel("window.close();",  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
	.Write("</P>")
	
End With

mobjGrid = Nothing
mobjValues = Nothing

%>
</FORM>
</BODY>
</HTML>






