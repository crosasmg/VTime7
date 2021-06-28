<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.48
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Variables para establecer el número de siniestro a trabajar en la página.
'- La primera para el combo de casos.  La segunda, para el campo de siniestros.
Dim mlngClaim As Object
Dim mstrClaim As String
Dim mstrCase_num As Object
Dim mstrCase() As String

'- Variables para establecer el movimiento.
Dim mstrMovement As String

'-Se define la variable que me indicara si los campos se deben o no habilitar    
Dim mblnDisabled As Boolean


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sic004_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sic004_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")

'UPGRADE_WARNING: Use of Null/IsNull() detected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1049.aspx'
mlngClaim = System.DBNull.Value
'mstrClaim = ""
'mstrCase_num = ""
mstrMovement = ""
mblnDisabled = True

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sic004_k"

Response.Write("<SCRIPT>var mlngClaim</SCRIPT>")

If Request.QueryString("nClaim") <> vbNullString Then
	mlngClaim = Request.QueryString("nClaim")
	mstrClaim = Request.QueryString("nClaim")
	Response.Write("<SCRIPT>mlngClaim=" & Request.QueryString("nClaim") & "</SCRIPT>")
	mstrCase_num = 1
	mblnDisabled = False
End If

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SIC004", "SIC004_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
End With

'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.31 $|$$Author: Nvaplat60 $"

//% AddClaimParameter: Actualiza el Valor del Parametro para el control de Casos 
//%                    de Siniestros y la Ubicación
//-----------------------------------------------------------------------------
function AddClaimParameter(nValue){
//-----------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(tcnClaim.value==0){
			self.document.forms[0].cbeCase.options.length=0;
			self.document.forms[0].cbeCase.disabled=true;	
			self.document.forms[0].tcnCase_num.value="";
			self.document.forms[0].tcnDeman_Type.value="";
		}
		else 
		{
			if(mlngClaim!=tcnClaim.value)
			    self.document.location.href = "SIC004_K.aspx?sCodispl=SIC004" +
														 "&nClaim=" + tcnClaim.value + "&sConfig=InSequence" +
														 "&nHeight=150"
        }  														       
    }
}

//%insParam: Asigna los valores del movimiento. Busca un valor dentro de un String.
//%          ademas habilita y desabilita el boton del movimiento del siniestro.
//%------------------------------------------------------------------------------------------
function insParam(){
//%------------------------------------------------------------------------------------------
	var lstrCampo=self.document.forms[0].cbeCase.value;
	var lstrStart=lstrCampo.indexOf("/");
	var lstrCase_num = unescape(lstrCampo.substring(0,lstrStart));
	var lstrCampo1 = lstrCampo.substring(lstrStart+1,lstrCampo.length);
    var lstrStart1 = lstrCampo1.indexOf("/");		
	var lstrDemanType = unescape(lstrCampo1.substring(0,lstrStart1));

    if (self.document.forms[0].cbeCase.value==0){
       self.document.forms[0].tcnCase_num.value = -32768;
       self.document.forms[0].tcnDeman_Type.value = -32768;
	}
	else{
       self.document.forms[0].tcnCase_num.value = lstrCase_num;
       self.document.forms[0].tcnDeman_Type.value = lstrDemanType;
     }
}

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcnClaim.disabled=false;
		tcnClaim.focus();		
	}
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insPreZone: Se maneja la Acción para la Busqueda por Condición
//------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="SIC004" ACTION="valClaim.aspx?sMode=1">
	<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
            <TD><LABEL ID=0>Siniestro </LABEL> </TD>
            <TD> <%=mobjValues.NumericControl("tcnClaim", 10, mstrClaim,  , "Número identificativo del siniestro al cual se le desea consultar un movimiento",  , 0,  ,  ,  , "AddClaimParameter(this.value);", CBool(mblnDisabled), 1)%></TD>
            <TD><LABEL ID=0>Caso</LABEL></TD>
			<TD><%
With mobjValues
	.BlankPosition = False
	.Parameters.Add("nClaim", mlngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeCase", "tabClaim_cases", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , "insParam()", CBool(mblnDisabled),  , "Número del caso involucrado en el pago de siniestro",  , 2))
End With
%>
            </TD>
        </TR>
        <TR>
			<TD><%If mobjValues.CodeValue <> vbNullString Then
    mstrCase = mobjValues.CodeValue.Split("/")
	Response.Write(mobjValues.HiddenControl("tcnCase_num", mstrCase(0)))%></TD>
			           <TD><%=mobjValues.HiddenControl("tcnDeman_Type", mstrCase(1))%></TD> <%	
Else
	%></TD><%	Response.Write(mobjValues.HiddenControl("tcnCase_num", CStr(1)))%></TD>
			           <TD><%=mobjValues.HiddenControl("tcnDeman_Type", "0")%></TD><%	
End If
%>
			
			
            <TD><LABEL ID=0>Movimiento</LABEL></TD>
			<TD><%
With mobjValues.Parameters
	.Add("nClaim", mstrClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCase_num", mstrCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valMovement", "TabMovementsClaim", eFunctions.Values.eValuesType.clngWindowType, mstrMovement, True,  ,  ,  ,  ,  , CBool(mblnDisabled),  , "Movimiento del Siniestro sobre el que se desea realizar la consulta del detalle",  , 3))
%>
            </TD>
		</TR>
	</TABLE>
	<P>&nbsp;</P>
	<P>&nbsp;</P>
</FORM>
</BODY>
</HTML>

<%'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
Call mobjNetFrameWork.FinishPage("sic004_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




