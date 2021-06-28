<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 31/3/03 17.17.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

' Pagina que realiza la calidacion del control de cliente cuando este es digitado 

'Manejo de objetos de la página 
Dim mclsValClient As Object 
Dim mclsClient As eClient.Client
Dim mobjErrors As eFunctions.Errors
Dim mobjValues As eFunctions.Values


'% ValidateClient: Valida la estructura del cliente
'--------------------------------------------------------------------------------------------
Private Sub ValidateClient()
	'--------------------------------------------------------------------------------------------
	'- Código del cliente temporal
	Dim lstrDigit As String
	Dim lintvalue As Byte
	
	'- Código del cliente    
	Dim lstrClientCode As Object
	
	Dim lstrClientControl As String
	
	'- Se llena con el Request.QueryString del control del cliente    
	lstrClientCode = Request.QueryString.Item("sClientCode")
	lstrClientControl = Request.QueryString.Item("ControlName")
	
	If Len(CStr(lstrClientCode)) Then
		If Not mclsValClient.Validate(CStr(lstrClientCode), 301, Request.QueryString.Item("CreateClient") = "1", Request.QueryString.Item("bAllowInvalidFormat") = "1") Then
			Select Case mclsValClient.Status
				Case eClient.ValClient.eTypeValClientErr.TypeNotFound
					Response.Write(mobjErrors.ErrorMessage("CA010", 2013,  ,  ,  , True))
				Case eClient.ValClient.eTypeValClientErr.StructInvalid
					Response.Write(mobjErrors.ErrorMessage("CA010", 2012,  ,  ,  , True))
				Case eClient.ValClient.eTypeValClientErr.IsNotNumeric
					lstrClientCode = vbNullString
					Response.Write(mobjErrors.ErrorMessage("CA010", 1937,  ,  ,  , True))
				Case "0", "4"
					lstrClientCode = mclsValClient.ClientCode
					If Request.QueryString.Item("CreateClient") = "1" Then
						Session("Digit") = vbNullString
						Response.Redirect(("/VTimeNet/Common/GoTo.aspx?sCodispl=BC003_K&LinkSpecial=1&LinkParamsClient=" & lstrClientCode & "&LinkParamsClientControl=" & lstrClientControl & "&LinkSpecialAction=301"))
					Else
						Session("Digit") = vbNullString
					End If
			End Select
		Else
			If mclsValClient.Status = eClient.ValClient.eTypeValClientErr.FieldNew Then
				lstrDigit = "E"
				Session("Digit") = "E"
			Else
				Session("Div") = Request.QueryString.Item("sDIVControlName")
				lstrDigit = mclsValClient.sDigit
			End If
			
		End If
		lstrClientCode = mclsValClient.ClientCode
	End If
	
	'+ Actualiza para JScript el valor del código y nombre del cliente.        
	Response.Write("<SCRIPT>UpdateClientCode('" & lstrClientCode & "','" & "" & "','" & Request.QueryString.Item("ControlName") & "','" & Request.QueryString.Item("sDIVControlName") & "','" & lstrDigit & "')</" & "Script>")
        
    If Request.QueryString.Item("bAllowInvalidFormat") = "1" Then
        lintvalue = 1        
    Else         
        If mobjValues.StringToType(lstrClientCode, Values.eTypeData.etdLong) < 50000000 Then
            lintvalue = 1
        Else
            lintvalue = 2
        End If
     End If        
	
	With Response
		.Write("<SCRIPT>")
		.Write("if (typeof(opener.document.forms[0].cbePerson_typ) != 'undefined') opener.document.forms[0].cbePerson_typ.value='" & lintvalue & "';")
		.Write("</" & "Script>")
	End With
	
	Response.Write("<SCRIPT>window.close()</" & "Script>")
End Sub

'--------------------------------------------------------------------------------------------
'% ValidateDigit: Valida el digito verificador
'--------------------------------------------------------------------------------------------
Private Sub ValidateDigit()
	Dim lclsRoleses As Object
	'--------------------------------------------------------------------------------------------
	'- Código del cliente    
	Dim lstrClientCode As String
	'- Dígito verificador
	Dim lstrDigit As String
	'- Dígito verificador
	Dim lstrDigit_prov As String
	'- Manejo de errores 
	Dim lblnError As Boolean
	'- variables de validación 
	Dim lblnFound As Boolean
	'- Objeto de manejo de errores 
	Dim mobjErrors As eGeneral.GeneralFunction
	'- Mensaje de tipo alerta enviado a la pantalla 
	Dim lstrAlert As String
	'- Nombre del cliente
	Dim lstrClieName As String
	'-Tipo de búsqueda 1-Cliente 2-Clientes de póliza 3-Clientes de Siniestro
	Dim lintTypeForm As Object
	'-Objeto para guardar el cliente obtenido en la colección
	Dim lclsRoles As Object
	'-Número de error a mostrar
	Dim llngError As Integer
	'-Valida si se crea el cliente
	Dim lblnCreate As Boolean
	'- Obtiene el sCodispl de la transacción de la cual es llamada    
	Dim lstrForm As String
	Dim lstrClientControl As String
	
	lstrForm = Request.QueryString.Item("sForm")
	'+ Obtiene el código del cliente pasado por parámetro.
	lstrClientCode = Request.QueryString.Item("sClientCode")
	lstrClientControl = Request.QueryString.Item("ControlName")
	lstrDigit = vbNullString
	lblnError = False
	lintTypeForm = Request.QueryString.Item("nTypeForm")
	lblnCreate = False
	
	lblnCreate = UCase(Trim(Request.QueryString.Item("sDigit"))) = "E"
	
	If Trim(Request.QueryString.Item("sDigit")) = "e" Then
		Response.Write("<SCRIPT>UpdateClientCode('" & lstrClientCode & "','" & "" & "','" & Request.QueryString.Item("ControlName") & "','" & Request.QueryString.Item("sDivName") & "','" & "E" & "')</" & "Script>")
	End If
	
	If Trim(lstrClientCode) = vbNullString Then
		If lblnCreate Then
			lstrClientCode = mclsClient.GetNewClientCode
			'+ Actualiza para JScript el valor del código y nombre del cliente.        
			Response.Write("<SCRIPT>UpdateClientCode('" & lstrClientCode & "','" & "" & "','" & Request.QueryString.Item("ControlName") & "','" & Request.QueryString.Item("sDivName") & "','" & "E" & "')</" & "Script>")
			If lstrForm = "CA025" Then
				Session("Digit") = "E"
				Response.Redirect(("/VTimeNet/Common/GoTo.aspx?sCodispl=BC003_K&LinkSpecial=1&LinkParamsClient=" & lstrClientCode & "&LinkParamsClientControl=" & lstrClientControl & "&LinkSpecialAction=301"))
			End If
		Else
			lblnError = True
		End If
	Else
		If Not lblnCreate Then
			lstrDigit = mclsClient.GetRUT(lstrClientCode)
			lblnError = lstrDigit <> UCase(Trim(Request.QueryString.Item("sDigit")))
		End If
	End If
	
	If (lblnError And Trim(Request.QueryString.Item("sDigit")) <> "") Or (lblnError And Trim(Request.QueryString.Item("sDigit")) = "" And Trim(Request.QueryString.Item("sClientCode")) <> "") Then
		
		mobjErrors = New eGeneral.GeneralFunction
		lstrAlert = "Err. 55032 " & mobjErrors.insLoadMessage(55032)
		mobjErrors = Nothing
		
		Response.Write("<SCRIPT>alert('" & lstrAlert & "')</" & "Script>")
		'+ Se blanquea el dígito verificador cuando no coincide con la rutina de validación. 
		Response.Write("<SCRIPT>UpdateClientCode('" & lstrClientCode & "','" & "" & "','" & Request.QueryString.Item("ControlName") & "','" & Request.QueryString.Item("sDivName") & "','" & "D" & "')</" & "Script>")
	Else
		
		Select Case lintTypeForm
			Case 1
				'Para manejo control de clientes estandar
				lblnCreate = True
				lblnFound = mclsValClient.Validate(CStr(lstrClientCode), 301,,Request.QueryString.Item("bAllowInvalidFormat") = "1")
				llngError = 1007
				lclsRoles = mclsValClient
				
			Case 2
				'Para manejo control de clientes por poliza  
				mclsValClient = New ePolicy.Roleses
				llngError = 4025
				lblnFound = mclsValClient.Find_by_Policy(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), CStr(lstrClientCode), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nTypeList"), eFunctions.Values.eTypeData.etdInteger, True), Request.QueryString.Item("sClientRole"), Request.QueryString.Item("sCalAge") = "1")
				
				If Not lblnFound Then
					'+ Si el control se encuentra en la transacción SI004 se hace el siguiente tratamiento.
					If lstrForm = "SI004" Then
						mobjErrors = New eGeneral.GeneralFunction
						lstrAlert = "Adv. " & llngError & " " & mobjErrors.insLoadMessage(llngError)
						mobjErrors = Nothing
						Response.Write("<SCRIPT>alert('" & lstrAlert & "')</" & "Script>")
						
                            mclsValClient = New eClient.ValClient
						lblnFound = mclsValClient.Validate(CStr(lstrClientCode), 301,,Request.QueryString.Item("bAllowInvalidFormat") = "1")
						lblnCreate = True
						If lblnFound Then
							lclsRoles = mclsValClient
						End If
					End If
				Else
					lclsRoles = mclsValClient.Item(1)
				End If
				
				'Para manejo control de clientes por siniestro              
			Case 3
                    mclsValClient = New eClaim.ClaimBenef
                    llngError = 4204
				lclsRoleses = mclsValClient.Find_ClaimBenefAsoc(mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDeman_type"), eFunctions.Values.eTypeData.etdDouble))
				lblnFound = mclsValClient.bClientAsoc
				
				If lblnFound Then
					lclsRoles = lclsRoleses.Item(1)
				End If
				lclsRoleses = Nothing
				
		End Select
		
		If lblnFound Then
			
			lstrClieName = Replace(lclsRoles.sCliename, "'", "´")
			
			Session("scliename") = lstrClieName
			lstrDigit_prov = lclsRoles.sDigit
			If lintTypeForm = 1 Then
				With Response
					.Write("<SCRIPT>")
					.Write("if (typeof(opener.document.forms[0].cbePerson_typ) != 'undefined') opener.document.forms[0].cbePerson_typ.value='" & lclsRoles.ClientType & "';")
					.Write("</" & "Script>")
				End With
			End If
			If lstrDigit_prov = "E" Then
				lstrDigit = UCase(lstrDigit_prov)
			End If
			
			Response.Write("<SCRIPT>UpdateClientCode('" & lstrClientCode & "','" & lstrClieName & "','" & Request.QueryString.Item("ControlName") & "','" & Request.QueryString.Item("sDivName") & "','" & lstrDigit & "')</" & "Script>")
			
			If Request.QueryString.Item("sOnChange") <> vbNullString Then
				Response.Write("<SCRIPT>opener." & Request.QueryString.Item("sOnChange") & ";</" & "Script>")
			End If
		Else
			If Not lblnCreate Then
				mobjErrors = New eGeneral.GeneralFunction
				lstrAlert = "Err. " & llngError & " " & mobjErrors.insLoadMessage(llngError)
				mobjErrors = Nothing
				Response.Write("<SCRIPT>alert('" & lstrAlert & "')</" & "Script>")
				Response.Write("<SCRIPT>UpdateClientCode('" & lstrClientCode & "','" & lstrClieName & "','" & Request.QueryString.Item("ControlName") & "','" & Request.QueryString.Item("sDivName") & "','" & lstrDigit & "')</" & "Script>")
			Else
				If Request.QueryString.Item("AllowInvalid") = "1" Then
					If Request.QueryString.Item("sOnChange") <> vbNullString Then
						Response.Write("<SCRIPT>opener." & Request.QueryString.Item("sOnChange") & ";</" & "Script>")
					End If
				End If
			End If
		End If
	End If
	Response.Write("<SCRIPT>window.close()</" & "Script>")
	mclsClient = Nothing
	mclsValClient = Nothing
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("clientqueryvalidate")

mclsValClient = New eClient.ValClient
mclsClient = New eClient.Client
mobjErrors = New eFunctions.Errors
mobjValues = New eFunctions.Values
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

//- Variable para el control de versiones
document.VssVersion="$$Revision: 6 $|$$Date: 19/05/04 11:59 $|$$Author: Nvaplat60 $" 

//%    UpdateClientCode: Actualiza el código del cliente sobre la ventana madre 
//--------------------------------------------------------------------------------------------------
function UpdateClientCode(ClientCode,ClientName,ControlName,DIVName, DigitCode){ 
//--------------------------------------------------------------------------------------------------

    var error 
    var DigitPar 

    if(opener.document.forms[0].elements[ControlName].disabled){  
        opener.document.forms[0].elements[ControlName].value=ClientCode;
        if (ClientCode!='')            
            opener.document.forms[0].elements[ControlName + "_Digit"].value=DigitCode;
        else
            opener.document.forms[0].elements[ControlName + "_Digit"].value='';
        if (typeof(DigitCode)!='undefined'){
            if (DigitCode!='')
                opener.$("#" + ControlName + "_Digit").change();
            else
                UpdateDiv (DIVName,"",'PopUp');
        }
    }
    if(opener.document.forms[0].elements[ControlName + "_Digit"].value=='e') 
    {
        DigitPar = 'E' 
        opener.document.forms[0].elements[ControlName + "_Digit"].value='E'
    }
    else if(opener.document.forms[0].elements[ControlName + "_Digit"].value=='k')
    {
        DigitPar = 'K' 
        opener.document.forms[0].elements[ControlName + "_Digit"].value='K'
    }
    else 
        DigitPar = opener.document.forms[0].elements[ControlName + "_Digit"].value

    if (typeof(opener.document.forms[0])!='undefined')
        try {
            if (opener.document.forms[0].elements[ControlName].value == ''){
                if (opener.document.forms[0].elements[ControlName + "_Digit"].value != ''){
                    UpdateDiv (DIVName,"",'PopUp');
                    opener.document.forms[0].elements[ControlName + "_Digit"].value ='';
                }
            }
            else
                if (opener.document.forms[0].elements[ControlName + "_Digit"].value == ''){
                    UpdateDiv (DIVName,"",'PopUp');
                    opener.document.forms[0].elements[ControlName + "_Digit"].value ='';
                    DigitCode='';
                }
                else {
                    if (DigitPar != DigitCode ||
                        opener.document.forms[0].elements[ControlName].value != opener.document.forms[0].elements[ControlName + "_Old"].value){
                        if (opener.document.forms[0].elements[ControlName + "_Old"].value != ''){
                            UpdateDiv (DIVName,"",'PopUp');
                            opener.document.forms[0].elements[ControlName + "_Digit"].value ='';
                            DigitCode='';
                        }
                        else{
                            UpdateDiv (DIVName,ClientName,'PopUp');
                        }
                        opener.document.forms[0].elements[ControlName + "_Old"].value = ClientCode;
                    }
                    else{
                    
                        if (ClientName!='')
                            UpdateDiv (DIVName,ClientName,'PopUp');
                    }
                }    
            opener.document.forms[0].elements[ControlName].value = ClientCode;
            opener.document.forms[0].elements[ControlName + "_Old"].value = ClientCode;
            
            with (opener.document.forms[0].elements[ControlName + "_Digit"])
                if (typeof(DigitCode)!='undefined'){
                    if (DigitCode!='')
                        value = DigitCode;

                    if (DigitCode=='D')
                        value = '';                        
                    if (DigitCode == 'E' || DigitCode == 'e')
                        disabled = true;
                    else{
                        if (typeof(opener.top.frames['fraSequence'])!='undefined'){
                            disabled = false;
                        }
                    }
                    opener.document.forms[0].elements[ControlName + "_Digit" + "_Old"].value = value;
                    
                    <%If Request.QueryString.Item("sField") = vbNullString Then%>
                        if (!disabled) {
                            focus();
                        }
                    <%End If%>
                }
        } catch (error){}
}
</SCRIPT>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%=mobjValues.StyleSheet%>
</HEAD>
<BODY>
<FORM>
<%
If Request.QueryString.Item("sField") = "Digit" Then
	Call ValidateDigit()
Else
	Call ValidateClient()
End If
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 17.17.03
Call mobjNetFrameWork.FinishPage("clientqueryvalidate")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




