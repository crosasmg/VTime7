<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


'% insShowAuto_db1: Se muestran los datos asociados al auto seleccionado
'%                  Se utiliza para el campo Código del vehiculo de la página BV001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowAuto_db1()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	lclsAuto = New ePolicy.Automobile
	If lclsAuto.Find_Tab_au_veh(Request.QueryString.Item("nVehcode")) Then
		With Response
			.Write("top.frames['fraFolder'].UpdateDiv('lblVehMark','" & lclsAuto.sDesBrand & "','Normal');")
			.Write("top.frames['fraFolder'].UpdateDiv('lblVehModel','" & lclsAuto.sVehmodel1 & "','Normal');")
			.Write("top.frames['fraFolder'].UpdateDiv('lblType','" & lclsAuto.sDesTypeVeh & "','Normal');")
			.Write("top.frames['fraFolder'].UpdateDiv('lblDSeat','" & lclsAuto.nVehplace & "','Normal');")
			.Write("top.frames['fraFolder'].UpdateDiv('lblTonMet','" & lclsAuto.nVehpma & "','Normal');")
			.Write("top.frames['fraFolder'].document.forms[0].hddType.value=" & lclsAuto.nVehType & ";")
		End With
	End If
	
	lclsAuto = Nothing
End Sub

'% insShowAuto_db: Se muestran los datos asociados al auto seleccionado
'%                 Se utiliza para el campo Código del vehiculo de la página BV001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowAuto_db()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	
	lclsAuto = New ePolicy.Automobile
	If lclsAuto.Find_Tab_au_veh(Request.QueryString.Item("nVehcode")) Then
		With Response
			.Write("opener.UpdateDiv(""lblVehMark"",'" & lclsAuto.sDesBrand & "','Normal');")
			.Write("opener.UpdateDiv(""lblVehModel"",'" & lclsAuto.sVehmodel1 & "','Normal');")
			.Write("opener.UpdateDiv(""lblType"",'" & lclsAuto.sDesTypeVeh & "','Normal');")
			.Write("opener.UpdateDiv(""lblDSeat"",'" & lclsAuto.nVehplace & "','Normal');")
			.Write("opener.UpdateDiv(""lblTonMet"",'" & lclsAuto.nVehpma & "','Normal');")
			.Write("top.opener.document.forms[0].tcnType.value=" & lclsAuto.nVehType & ";")
		End With
	End If
	
	lclsAuto = Nothing
End Sub

'% insShowDigit: Calcula el digito verificador de la patente ingresada
'--------------------------------------------------------------------------------------------
Sub insShowDigit()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	
	lclsAuto = New ePolicy.Automobile
	
	If Request.QueryString.Item("sLicense_ty") = "1" Then
		With Response
			If lclsAuto.InsCalDigitSerie(Request.QueryString.Item("sRegist")) Then
				.Write("top.frames['fraFolder'].document.forms[0].tctDigit.value=""" & Trim(lclsAuto.sDigit) & """;")
			Else
				.Write("top.frames['fraFolder'].document.forms[0].tctDigit.value=""" & """;")
				.Write("top.frames['fraFolder'].document.forms[0].tctRegister.value=""" & """;")
			End If
		End With
	End If
	lclsAuto = Nothing
End Sub

'% insShowRegist: Verifica si existe el automóvil en la base de datos de automóviles
'%                Se utiliza para el campo patente de la página AU557
'--------------------------------------------------------------------------------------------
Sub insShowRegist()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto_db As ePolicy.Auto_db
	
	lclsAuto_db = New ePolicy.Auto_db
	If lclsAuto_db.Find_db1(Request.QueryString.Item("sLicense_ty"), Request.QueryString.Item("sRegist")) Then
		With Response
			.Write("top.frames['fraHeader'].document.forms[0].tctDigit.value='" & lclsAuto_db.sDigit & "';")
			.Write("top.frames['fraHeader'].document.forms[0].tctDigit.disabled=true;")
		End With
	End If
	lclsAuto_db = Nothing
End Sub

'% insShowAuto_Capital: Se muestra el capital de auto según el código y el año.
'%                      Se utiliza para el campo Capital de la página BV001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowAuto_Capital()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto As ePolicy.Automobile
	Dim lobjValues As eFunctions.Values
	
	lclsAuto = New ePolicy.Automobile
	lobjValues = New eFunctions.Values
	If lclsAuto.Find_Tab_au_val(Request.QueryString.Item("nVehcode"), mobjValues.StringToType(Request.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble)) Then
		With Response
			.Write("top.frames['fraFolder'].document.forms[0].tcnValue.value='" & mobjValues.TypeToString(lclsAuto.nCapital, eFunctions.Values.eTypeData.etdDouble, True, 2) & "';")
		End With
	End If
	lclsAuto = Nothing
	lobjValues = Nothing
	
End Sub

'% insShowData_Auto:  Se muestran los datos asociados al auto seleccionado,
'%					   si el número de placa ya está registrado en el sistema
'%					   Se utiliza en el campo Matrícula de la ventana AU001.aspx
'--------------------------------------------------------------------------------------------
Sub insShowData_Auto()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto_db As ePolicy.Auto_db
	Dim lclsAuto As ePolicy.Automobile
	Dim lobjValues As eFunctions.Values
	Dim sDigit As Object
	
	lclsAuto_db = New ePolicy.Auto_db
	lclsAuto = New ePolicy.Automobile
	lobjValues = New eFunctions.Values
	
	With Response
		lclsAuto_db.nAction = CInt(Request.QueryString.Item("nMainAction"))
		If Request.QueryString.Item("Field") = "Data_Motor" Then
			If IsNothing(Request.QueryString.Item("sMotor")) Then
				Call insDisabled()
			Else
				If lclsAuto_db.insValExistFields(Request.QueryString.Item("sMotor"), 1) Then
					.Write("top.frames['fraHeader'].document.forms[0].tctChassis.value='" & lclsAuto_db.sChassis & "';")
					.Write("top.frames['fraHeader'].document.forms[0].tctRegister.value='" & lclsAuto_db.sregist & "';")
					.Write("top.frames['fraHeader'].document.forms[0].tctDigit.value='" & lclsAuto_db.sDigit & "';")
					.Write("top.frames['fraHeader'].document.forms[0].cbeLicense_ty.value='" & lclsAuto_db.sLicense_ty & "';")
					.Write("top.frames['fraHeader'].document.forms[0].cbeNlic_special.value='" & lclsAuto_db.nLic_special & "';")
					
					.Write("top.frames['fraHeader'].document.forms[0].tctChassis.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].tctRegister.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].tctDigit.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].cbeLicense_ty.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].cbeNlic_special.disabled=true;")
				Else
					Call insDisabled()
				End If
			End If
		ElseIf Request.QueryString.Item("Field") = "Data_Chassis" Then 
			If IsNothing(Request.QueryString.Item("sChassis")) Then
				Call insDisabled()
			Else
				If lclsAuto_db.insValExistFields(Request.QueryString.Item("sChassis"), 2) Then
					.Write("top.frames['fraHeader'].document.forms[0].tctMotor.value=""" & lclsAuto_db.sMotor & """;")
					.Write("top.frames['fraHeader'].document.forms[0].tctRegister.value=""" & lclsAuto_db.sregist & """;")
					.Write("top.frames['fraHeader'].document.forms[0].tctDigit.value=""" & lclsAuto_db.sDigit & """;")
					.Write("top.frames['fraHeader'].document.forms[0].cbeLicense_ty.value=""" & lclsAuto_db.sLicense_ty & """;")
					.Write("top.frames['fraHeader'].document.forms[0].cbeNlic_special.value=""" & lclsAuto_db.nLic_special & """;")
					
					.Write("top.frames['fraHeader'].document.forms[0].tctMotor.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].tctRegister.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].tctDigit.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].cbeLicense_ty.disabled=true;")
					.Write("top.frames['fraHeader'].document.forms[0].cbeNlic_special.disabled=true;")
				Else
					Call insDisabled()
				End If
			End If
		ElseIf Request.QueryString.Item("Field") = "Data_Regist" Then 
			If IsNothing(Request.QueryString.Item("sRegist")) Then
				Call insDisabled()
			Else
				If lclsAuto_db.nAction <> 301 Then
					If lclsAuto_db.insValExistFields(Request.QueryString.Item("sRegist"), 3) Then
						.Write("top.frames['fraHeader'].document.forms[0].tctMotor.value=""" & lclsAuto_db.sMotor & """;")
						.Write("top.frames['fraHeader'].document.forms[0].tctChassis.value=""" & lclsAuto_db.sChassis & """;")
						.Write("top.frames['fraHeader'].document.forms[0].cbeLicense_ty.value=""" & lclsAuto_db.sLicense_ty & """;")
						.Write("top.frames['fraHeader'].document.forms[0].cbeNlic_special.value=""" & lclsAuto_db.nLic_special & """;")
						.Write("top.frames['fraHeader'].document.forms[0].tctDigit.value=""" & lclsAuto_db.sDigit & """;")
						
						.Write("top.frames['fraHeader'].document.forms[0].tctMotor.disabled=true;")
						.Write("top.frames['fraHeader'].document.forms[0].tctChassis.disabled=true;")
						.Write("top.frames['fraHeader'].document.forms[0].tctDigit.disabled=true;")
						.Write("top.frames['fraHeader'].document.forms[0].cbeLicense_ty.disabled=true;")
						.Write("top.frames['fraHeader'].document.forms[0].cbeNlic_special.disabled=true;")
					Else
						Call insDisabled()
					End If
				Else
					If Request.QueryString.Item("sLicense_ty") = "1" Then
						If lclsAuto.InsCalDigitSerie(Request.QueryString.Item("sRegist")) Then
							Response.Write("top.frames['fraHeader'].document.forms[0].tctDigit.value=""" & Trim(lclsAuto.sDigit) & """;")
                            Else
                                .Write("top.frames['fraHeader'].document.forms[0].tctDigit.value=""" & """;")
                            End If
					End If
				End If
			End If
		ElseIf Request.QueryString.Item("Field") = "Data_License_ty" Then 
			If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
				If Request.QueryString.Item("sLicense_ty") = "3" Then
					If lclsAuto.next_seqregistauto() Then
						.Write("top.frames['fraHeader'].document.forms[0].tctRegister.value=" & lclsAuto.sregist & ";")
						.Write("top.frames['fraHeader'].document.forms[0].tctRegister.disabled=true;")
						.Write("top.frames['fraHeader'].document.forms[0].tctDigit.disabled=true;")
					End If
				End If
			End If
		End If
	End With

        
        
        
        lclsAuto_db = Nothing
	lobjValues = Nothing
	lclsAuto = Nothing
End Sub
'% insDisabled:  Deshabilita los campos de la transaccion BV001
'--------------------------------------------------------------------------------------------
Sub insDisabled()
	'--------------------------------------------------------------------------------------------
	With Response
		.Write("top.frames['fraHeader'].document.forms[0].tctChassis.disabled=false;")
		.Write("top.frames['fraHeader'].document.forms[0].tctRegister.disabled=false;")
		.Write("top.frames['fraHeader'].document.forms[0].tctMotor.disabled=false;")
		.Write("top.frames['fraHeader'].document.forms[0].tctDigit.disabled=false;")
		.Write("top.frames['fraHeader'].document.forms[0].cbeLicense_ty.disabled=false;")
		.Write("top.frames['fraHeader'].document.forms[0].cbeNlic_special.disabled=false;")
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:45 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write(mobjValues.StyleSheet() & vbCrLf)
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "Auto_db"
		Call insShowAuto_db()
	Case "Auto_db1"
		Call insShowAuto_db1()
	Case "Capital"
		Call insShowAuto_Capital()
	Case "Data_Motor"
		Call insShowData_Auto()
	Case "Data_Chassis"
		Call insShowData_Auto()
	Case "Data_Regist"
		Call insShowData_Auto()
	Case "Data_License_ty"
		Call insShowData_Auto()
	Case "Regist"
		Call insShowRegist()
	Case "Digit"
		Call insShowDigit()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
mobjValues = Nothing
%>




