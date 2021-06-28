<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objetos para mostrar los datos de la página
Dim mobjError As eErrors.ErrorTyp
Dim mobjErr_histor As eErrors.err_histor
Dim mintDefValue As String


</script>
<%Response.Expires = -1
Response.CacheControl = "private"

With Server
	mobjMenu = New eFunctions.Menues
	mobjValues = New eFunctions.Values
	mobjError = New eErrors.ErrorTyp
	mobjErr_histor = New eErrors.err_histor
	mobjGrid = New eFunctions.Grid
End With
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActioncut) Then
	mobjValues.ActionQuery = True
End If

mobjError.tDs_text = vbNullString

Call mobjError.Find(Session("nErrorNum"))

mobjValues.sCodisplPage = "er003"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "ER003", "ER003.aspx"))
%>
<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 5 $|$$Date: 15/09/04 8:22a $|$$Author: Calvarez $"

    //% insStateZone: habilita/deshabilita los campos de la ventana
    //---------------------------------------------------------------------------------------
    function insStateZone() {
        //---------------------------------------------------------------------------------------
    }

    //%insFormatHours.Esta funcion se encarga de formatear el campo horas
    //---------------------------------------------------------------------------------------
    function insFormatHours(Field) {
        //---------------------------------------------------------------------------------------
        Field.value = Field.value.replace(':', '')
        switch (Field.value.length) {
            case 1:
                Field.value = '00:0' + Field.value
                break;
            case 2:
                Field.value = '00:' + Field.value
                break;
            case 3:
                Field.value = '0' + Field.value.substr(0, 1) + ':' + Field.value.substr(1, 2)
                break;
            case 4:
                Field.value = Field.value.substr(0, 2) + ':' + Field.value.substr(2, 2)
                break;
        }
    }
    //---------------------------------------------------------------------------------------------
    function ReloadPage() {
        //---------------------------------------------------------------------------------------------
        self.document.location.href = self.document.location.href + '&Reload=1';
    }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmErroUpd" ACTION="ValErrors.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%=mobjValues.ShowWindowsName("ER003")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=6751>Responsable</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctUserDetect", 12, Session("sInitials_User"),  ,"Usuario que está realizando la operación")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=15280>Tipo de error</LABEL></TD>
            <TD><%mobjValues.TypeOrder = 1
                    Response.Write(mobjValues.PossiblesValues("cbeErrorType", "tab_typerr", 1, CStr(mobjError.nType_err), , False, , , , , , , "Tipo de error a asignar"))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=6752>Fecha</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDate", CStr(mobjError.dDate),  ,"Fecha de actualización del error")%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=6753>Hora</LABEL></TD>    
            <TD><%= mobjValues.TextControl("tctHourDetect", 10, mobjError.sHour_date, , "Hora de detección")%></TD>
        </TR>
        <TR>
        <TD><LABEL ID=6754>Estado a asignar</LABEL></TD>
        <TD><%
mobjValues.TypeList = 1
mobjValues.BlankPosition = True
Select Case mobjError.sStat_error
	Case 1
		mobjValues.List = "2,3,6,8,9,10"
		mintDefValue = "2"
	Case 2
		mobjValues.List = "3,6,8,9,10"
		mintDefValue = "3"
	Case 3
		mobjValues.List = "4,5,6"
		mintDefValue = "4"
	Case 4
		mobjValues.List = "6,11,12"
		mintDefValue = "12"
	Case 7
		mobjValues.List = "1,8,9,10"
		mintDefValue = "1"
	Case 8
		mobjValues.List = "3,6"
		mintDefValue = ""
	Case 9
		mobjValues.List = "1,8,12"
		mintDefValue = ""
	Case 10
		mobjValues.List = "3,6"
		mintDefValue = ""
	Case Else
		mobjValues.List = "6"
		mintDefValue = ""
End Select
Response.Write(mobjValues.PossiblesValues("cbeStaterr", "Table999", 1, mintDefValue,  ,  ,  ,  ,  ,  , False,  ,"Estado a asignar"))
%>
            </TD>
       </TR>
    </TABLE>    
    <TABLE WIDTH="100%">        
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=6755><a NAME="Tiempo">Tiempo</a></LABEL></TD>            
        </TR>    
        <TR>
            <TD VALIGN="TOP" COLSPAN="4" CLASS="HorLine"></TD>
        </TR>          
        <TR>                                     
            <TD><LABEL ID=6756>Días</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDays", 2, CStr(mobjError.nDate),  ,"Número de días que se empleó en el cambio de estado",  , 0)%></TD>
            <TD><LABEL ID=6757>Horas</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctHours", 6, mobjError.sHour_Time,  ,"Cantidad de horas que se empleó en el cambio de estado",  ,  ,  , "insFormatHours(this)")%></TD>
            <TD COLSPAN="4" WIDTH="50%">&nbsp<TD>            
        </TR>        
        <TR>
            <TD COLSPAN="8" CLASS="HighLighted"><LABEL ID=6758><a NAME="Estado">Descripción del error</a></LABEL></TD>
        </TR>
        <TR>
            <TD VALIGN="TOP" COLSPAN="8" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD COLSPAN="8">
            <%
If mobjValues.ActionQuery Then
	Response.Write(mobjValues.TextAreaControl("txtDescript", 10, 55, Replace(mobjError.tDs_text, vbCrLf, "<BR>")))
Else
	Response.Write(mobjValues.TextAreaControl("txtDescript", 10, 55, mobjError.tDs_text,  ,"Descripción completa del error"))
End If
%>
            </TD>
        </TR>
        <TR>
            <TD COLSPAN="8" CLASS="HighLighted"><LABEL ID=6760><a NAME="Estado">Historia del error</a></LABEL></TD>
         </TR>
        <TR>
            <TD VALIGN="TOP" COLSPAN="8" CLASS="HorLine"></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
Response.Write(mobjErr_histor.ReaErr_Histor(Session("nErrorNum")))

With Response
	.Write("<SCRIPT>")
	.Write("with(top.document.frames['fraHeader']){")
	.Write("UpdateDiv(""cbeStaterr"",""" & mobjValues.getMessage(mobjError.sStat_error, "Table999") & """);")
	.Write("UpdateDiv('tctCodisp','" & mobjError.sCodisp & "','Normal');")
	.Write("}")
	.Write("</SCRIPT>")
End With
mobjValues = Nothing
mobjError = Nothing
mobjMenu = Nothing
mobjErr_histor = Nothing
mobjGrid = Nothing

'+Codigo temporal que recarga la página para resolver el problema del cambio de numero de errores
'+por error 65197
If Request.QueryString.Item("Reload") = vbNullString Then
	Response.Write("<SCRIPT>ReloadPage();</SCRIPT>")
End If
%>










