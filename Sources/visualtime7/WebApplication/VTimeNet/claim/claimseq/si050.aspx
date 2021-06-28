<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de siniestro.
Dim mobjClaim As eClaim.Claim

Dim lblnActionQuery As Boolean
Dim lstrMessage As String
Dim lintcboWaitCode As Byte
Dim lblnEnabledcboWaitCode As Boolean

'- Indica si el check de impresión inmediata estará o no activo.    
Dim lblnEnabledPrintNow As Boolean
Dim lblnOk As Boolean

'- Indica si el siniestro tiene definido un caso
Dim lblnCase As Boolean

'- Contiene el estado del siniestro una vez culminado su proceso.
Dim lstrStaClaim As String
Dim lintUpdCl_Cover As Short


'%insLoadSI050 : Rutina que carga la información del proceso en la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insLoadSI050(ByRef lintWait_code As Integer)
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo de las ventanas de siniestro    
	Dim mobjClaimWin As eClaim.Claim_win
	
	'- Objeto para el manejo de casos
	Dim mobjClaimCases As eClaim.Claim_cases
	
	Dim lintListIndex As Integer
	Dim lblnAutomatic As Boolean
	
	mobjClaimWin = New eClaim.Claim_win
	mobjClaimCases = New eClaim.Claim_cases
	
	lblnOk = True
	
    '+ Si es diferente de Desistimiento (Table192 - 17).
    If Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngCaratula Then
        lblnCase = False
	Else
        lblnCase = True
    End If

    lstrMessage = vbNullString
	
	'- (2) Estado del siniestro en tramitación [por defecto]    
	lstrStaClaim = "2"
	lintListIndex = lintWait_code
	'+ Si los valores son mayores que 5 corresponden a estados manuales, en caso contrario a estados automáticos.
	If lintWait_code > 5 Then
		lblnAutomatic = False
	Else
		lblnAutomatic = True
	End If
	
	If CStr(Session("sCertype")) = vbNullString Then
		Session("sCertype") = "2"
	End If
	
	mobjClaimWin.sMessage = vbNullString
	'+Si existe alguna carpeta que no halla sido carga con información.
	If Not mobjClaimWin.insValSequence(mobjValues.StringToType(CStr(Session("nTransaction")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), CStr(Session("sCertype")), mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble), CBool(Session("bPolicyVigency")), mobjValues.StringToType(CStr(Session("nNotenum")), eFunctions.Values.eTypeData.etdDouble)) Then
		
		'+Si quedó alguna carpeta sin llenar, se procede a hacer el manejo automático de la ventana PopUp
		
		lblnAutomatic = True
		lintcboWaitCode = 1
		
		If lblnAutomatic Then
			lblnOk = False
			lblnEnabledcboWaitCode = True
			lintcboWaitCode = 1
		End If
	Else
		
		If lblnAutomatic Then
			
			If Not Session("mblnExcess") Then
				If mobjClaim.ValLimitsClaimDec(CStr(Session("sSche_code"))) Then
					lblnOk = False
				End If
			End If

			If Not mobjClaim.ValDocuments_Status(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble)) Then
				lblnOk = False
                lintcboWaitCode = 3
			End If

			' /* se comentan validaciones Para que no pararezcan  mensaje de error al terminar 
			'    un certificado   */
			' else
			' 	Response.Write "<NOTSCRIPT>alert('" & lblnOk & "')</" & "Script>"					
			'	lblnOk = False
			'	Response.Write "<NOTSCRIPT>alert('" & lblnOk & "')</" & "Script>"					
			' End If
		Else
			lblnOk = False
		End If
		
		If Not (lblnOk) Then
			'+ Se cambia el estado del combo al valor	: "Límite de declaración".
            If lintcboWaitCode <> 3 Then
			    lintcboWaitCode = 2
            End If
			'+ Estado del siniestro en pendiente de aprobación.            
			lstrStaClaim = "8"
		End If
	End If
	
	lintUpdCl_Cover = mobjClaimWin.nUpdCl_cover
	
	'+ Si corresponde a un estado automático.
	
	If lblnAutomatic And (Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimRejection) Then
		'+ Si el proceso terminó satisfactoriamente.
		If lblnOk Then
			Call mobjClaimWin.ConcatMessage(4327)
			lintcboWaitCode = 0
			lblnEnabledcboWaitCode = False
			
            '+ Si es diferente de Desistimiento (Table192 - 17).
            If Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngCaratula Then
                lblnEnabledPrintNow = False
		    Else
                lblnEnabledPrintNow = True
            End If
        
        Else
			Call mobjClaimWin.ConcatMessage(4325)
			lblnEnabledcboWaitCode = True
			lblnEnabledPrintNow = True
		End If
	Else
		lblnEnabledcboWaitCode = False
	End If
	
	'+ Si el siniestro no posee caso asociado
	
	If Not mobjClaimCases.Find(CDbl(Session("nClaim"))) Then
		Call mobjClaimWin.ConcatMessage(60476)
		lblnCase = True
	End If
	
	Call mobjClaimWin.ConcatMessage(4326)
	Call mobjClaimWin.ConcatMessage(3909)
	
	lstrMessage = mobjClaimWin.PrintMessage
	
	'UPGRADE_NOTE: Object mobjClaimWin may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjClaimWin = Nothing
	'UPGRADE_NOTE: Object mobjClaimCases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjClaimCases = Nothing
End Sub

</script>
<% Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si050")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si050"
mobjClaim = New eClaim.Claim

    If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
        mobjValues.ActionQuery = Session("bQuery")
    Else
        mobjValues.ActionQuery = False
        Session("bQuery") = False
    End If

'+ Se obtienen los datos del siniestro    
Call mobjClaim.Find(CDbl(Session("nClaim")))

'+ Carga los datos de la forma    
Call insLoadSI050((mobjClaim.nWaitCl_code))
%>

<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/ValFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Includes/Claim.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->

	<%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle("SI050"))
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 1/07/05 9:50 $|$$Author: Nvaplat28 $"
    
//% ChangeWaitCode: Actualiza los objetos de la forma, según el tipo de acción del combo.
//-------------------------------------------------------------------------------------------
function ChangeWaitCode(nWaitCode){
//-------------------------------------------------------------------------------------------
    if(nWaitCode.value==0)
        self.document.forms[0].chkPrintNow.disabled = false
    else
        self.document.forms[0].chkPrintNow.disabled = true;
}

//% ChangeCase: Habilita o desabilita el combo del caso cuando la impresion de denuncio se
//              encuentra con el valor chequeado.
//-------------------------------------------------------------------------------------------
function ChangeCase(){
//-------------------------------------------------------------------------------------------
    if(self.document.forms[0].chkDenPrint.checked == false)
        self.document.forms[0].cbeCase.disabled = true
    else
        self.document.forms[0].cbeCase.disabled = false;
}
//% ChangeValues: Habilita/Desabilita los optionControl de Vida/Oncologico.
//-------------------------------------------------------------------------------------------
function ChangeValues(){
//-------------------------------------------------------------------------------------------
    if (self.document.forms[0].chkDenPrint.checked==true)
    {
        self.document.forms[0].optClaim[0].disabled = false;
        self.document.forms[0].optClaim[1].disabled = false;
    }
    else
    {
        self.document.forms[0].optClaim[0].disabled = true;
        self.document.forms[0].optClaim[1].disabled = true;
     }
}

//% AddAlllCasesOption: 
//-------------------------------------------------------------------------------------------
function AddAlllCasesOption(selectField){
//-------------------------------------------------------------------------------------------
	$(selectField).append("<option value='-74796976'>(Todos los casos)</option>")
}

$(function(){AddAlllCasesOption($("[name='cbeCase']")[0]);});
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmEndClaimProc" ACTION="valClaimSeq.aspx?nAction=392">
    <TABLE BORDER=1 CELLPADDING=5 BGCOLOR=WHITE WIDTH="100%">
        <TR>
            <TD><%
With mobjValues
	lblnActionQuery = .ActionQuery
	.ActionQuery = True
	Response.Write(mobjValues.TextAreaControl("txtMessage", 10, 30, lstrMessage))
	.ActionQuery = lblnActionQuery
End With
%>
            </TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2"><%=mobjValues.PossiblesValues("cboWaitCode", "Table5604", eFunctions.Values.eValuesType.clngComboType, CStr(CInt(lintcboWaitCode)),  ,  ,  ,  ,  , "ChangeWaitCode(this)", CBool(lblnEnabledcboWaitCode))%></TD>
            <TD>&nbsp;</TD>
            <%If Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimRejection Then%>
                <TD COLSPAN="2"><%=mobjValues.CheckControl("chkPrintNow", "Impresión de carátula",  ,  ,  , CBool(lblnEnabledPrintNow))%></TD>
             <%Else%>
                 <TD>&nbsp;</TD>
            <%End If%>  

                <%With Response
	.Write(mobjValues.HiddenControl("lblnEnabledPrintNow", CStr(True)))
	.Write(mobjValues.HiddenControl("lblnEnabledcboWaitCode", CStr(Not CBool(lblnEnabledcboWaitCode))))
End With%>
        </TR>
        
        <%If Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimRejection Then%>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0>Denuncio</LABEL></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD COLSPAN="3"></TD>
        </TR>
        <%End If%>
        <TR>
            <TD COLSPAN="2"><%If Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimRejection Then
	If CStr(Session("sBrancht")) = "1" Then
		Response.Write(mobjValues.CheckControl("chkDenPrint", "Impresión denuncio",  ,  , "ChangeValues();", False))
	Else
		Response.Write(mobjValues.CheckControl("chkDenPrint", "Impresión denuncio",  ,  , "ChangeCase();", CBool(lblnCase)))
	End If
Else
	Response.Write(mobjValues.CheckControl("chkDenPrint", "Impresión Carta Rechazo",  ,  , "ChangeValues();", False))
End If
%>
            </TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0>Caso</LABEL></TD>
            <TD><%With mobjValues
	.BlankPosition = False
	.Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  ,  , True,  , "", eFunctions.Values.eTypeCode.eString))
End With
%>
            </TD>
        </TR>
        
        <%If Session("nTransaction") <> eClaim.Claim_win.eClaimTransac.clngClaimRejection Then%>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optClaim", "Vida", CStr(1), CStr(0),  , True)%></TD>
            <TD COLSPAN="3">&nbsp<TD>
        </TR>              
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optClaim", "Oncológico", CStr(0), CStr(1),  , True)%></TD>
            <TD COLSPAN="3">&nbsp<TD>
        </TR>  


        <%Else%>
         <TR>
             <TD COLSPAN="3">&nbsp<TD>
             <TD COLSPAN="3">&nbsp<TD>
        </TR>
        <%	If lintUpdCl_Cover = 0 Then%>
        <TR>
            <TD><LABEL ID=0>Causa de rechazo</LABEL></TD>
            <%		With mobjValues
			'+ Se excluye el cero ("No aplica")				
			.List = "0"
			.TypeList = 2
		End With
		If CDbl(Session("nTransaction")) = 15 Then%>  
			          <TD><%=mobjValues.PossiblesValues("cboNullClaim", "Table133", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  ,  ,  , 4)%></TD> 
			  <%		End If%>
        </TR> 
        <%	End If%>
            
        <%End If%>        
        <TR>
			<TD COLSPAN="5">&nbsp;</TD>
		</TR>
        <TR>
			<TD COLSPAN="5" CLASS="HORLINE"></TD>
		</TR>
		<TR>
			<TD><%
Response.Write(mobjValues.ButtonAbout("SI050"))
Response.Write(mobjValues.ButtonHelp("SI050"))
%>
			</TD>
		    <TD COLSPAN="4" ALIGN="RIGHT"><%=mobjValues.ButtonAcceptCancel("EnabledControl()",  , True)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjClaim = Nothing
%>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
Call mobjNetFrameWork.FinishPage("si050")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




