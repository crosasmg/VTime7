<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim mobjfolder As eGeneralQue.Folder

Dim mstrfolderName As String


'%insPreGE099. Esta funcion se encarga de buscar los datos que se van a mostrar en 
'%la ventana
'-----------------------------------------------------------------------------------
Private Sub insPreGE099()
	'-----------------------------------------------------------------------------------
	Dim lobjGeneralQue As eGeneralQue.GenFunct
	Dim lstrCodispl As Object
	Dim lobjParamValue As String
	Dim lintIndex As Short
	Dim lobjParam As String
	
	lobjGeneralQue = New eGeneralQue.GenFunct
	
	With lobjGeneralQue.Parameters
		.Add("nCurrentQuery").Valor = Session("nCurrentQuery")
		.Add("HdEffecdate").Valor= mobjValues.StringToType(Session("dEffecdate_GQ"), eFunctions.Values.eTypeData.etdDate)
		Select Case Session("nCurrentQuery")
			Case 1, 3, 5, 11 'Poliza/Certificado/Solicitud/Cotización
				Select Case Session("nCurrentQuery")
					Case 1, 3 'Poliza/Certificado
						.Add("HsCertype").Valor = "2"
					Case 5 'Solicitud
						.Add("HsCertype").Valor = "1"
					Case 11 ' Cotización
						.Add("HsCertype").Valor = "3"
				End Select
				.Add("HnBranch").Valor= Session("nBranch_GQ")
				.Add("HnProduct").Valor=Session("nProduct_GQ")
				.Add("HnPolicy").Valor=Session("nPolicy_GQ")
				.Add("HnCertif").Valor=Session("nCertif_GQ")
				.Add("HsClient").Valor=""
				
			Case 4 'Cliente
				.Add("HsClient").Valor=Session("sClient_GQ")
			Case 6 'Siniestro
				.Add("HnClaim").Valor=Session("nClaim_GQ")
			Case 7 'Recibo
				.Add("HnBranch").Valor=Session("nBranch_GQ")
				.Add("HnProduct").Valor=Session("nProduct_GQ")
				.Add("HnReceipt").Valor=Session("nReceipt_GQ")
			Case 8 'Cheque
				.Add("HsCheque").Valor=Session("sCheque_GQ")
			Case 9 'Contrato
				.Add("HnContrat").Valor=Session("nContrat_GQ")
			Case 40 'Intermediario
				.Add("HnProvider").Valor=Session("nProvider_GQ")
			Case 60 'Préstamo'
				.Add("HsLoan").Valor=Session("sLoan_GQ")
			Case 77 'Intermediario
				.Add("nIntermed").Valor=Session("nIntermed_GQ")
			Case 13, 76, 18, 78 'Reaseguro
				.Add("nCompany").Valor=Session("nCompany_GQ")
				.Add("dEffecdate").Valor=mobjValues.StringToType(Session("dEffecdate_GQ"), eFunctions.Values.eTypeData.etdDate)
			Case 80, 81, 82 'Reaseguro-- Prima Cedida, Siniestro Cedido, Distribucion Capital
				.Add("nPolicy").Valor=Session("nPolicy_GQ")
				.Add("dEffecdate").Valor=mobjValues.StringToType(Session("dEffecdate_GQ"), eFunctions.Values.eTypeData.etdDate)
		End Select
	End With
	
	'+Si se recarga la página, se toman los valores de los parametros de la forma
	
	If Request.QueryString.Item("sReload") = "1" Then
		If Request.QueryString.Item("nExistSubF") <> "2" Then
			lobjGeneralQue.bCreateSubFolder = False
		End If
		lobjGeneralQue.Parameters.Add("nCurrentFolder").Valor = Request.QueryString.Item("nFolder")
		If Not IsNothing(Request.QueryString.Item("PN")) Then
            For lintIndex = 0 To Request.QueryString.getvalues("PN").Count -1
                lobjParam = Request.QueryString.getvalues("PN").getvalue(lintIndex)
                lobjParamValue =  Request.QueryString.getvalues("PV").getvalue(lintIndex)
				lobjGeneralQue.Parameters.Add(Cstr(lobjParam)).Valor = lobjParamValue
			Next lintIndex
		End If
		If lobjGeneralQue.Find("GE099", Session("nCurrentQuery"), CInt(Request.QueryString.Item("nFolder")), CInt(Request.QueryString.Item("nParentFolder")), Request.QueryString.Item("sKey"), Session("nUserCode")) Then
                Response.Write(lobjGeneralQue.HTMLQuery)
		End If
	Else
		Response.Write(lobjGeneralQue.HTMLParentNode(Session("nCurrentQuery")))
		lobjGeneralQue.Parameters.Add("nCurrentFolder").Valor = Session("nCurrentQuery")
		
		If lobjGeneralQue.Find("GE099", Session("nCurrentQuery"), Session("nCurrentQuery"), 0, "", Session("nUserCode")) Then
                Response.Write(lobjGeneralQue.HTMLQuery)
		End If
		
	End If
	Response.Write(mobjValues.BeginPageButton)
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjfolder = New eGeneralQue.Folder

Call mobjfolder.Find(mobjValues.StringToType(Request.QueryString.Item("nFolder"), eFunctions.Values.eTypeData.etdDouble), True)

mstrfolderName = mobjfolder.sRootName

mobjfolder = Nothing

mobjValues.sCodisplPage = "GE099"

%>

		
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%Response.Write(mobjValues.StyleSheet())
Response.Write("<H2 CLASS=""" & "WindowsName""" & ">&nbsp;" & mstrfolderName & "</H2><HR><BR>")%>
</HEAD>
<BODY>  
<FORM METHOD=POST NAME=GE099 ACTION="valGeneralQue.aspx?x=1">

<%

Call insPreGE099()
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.setZone(2, "GE099", "GE099.aspx"))
mobjMenu = Nothing

%>    
</FORM>
</BODY>
</HTML>




