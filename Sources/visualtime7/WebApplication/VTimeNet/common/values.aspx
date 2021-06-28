<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

    Dim mobjValues As Object
Dim mblnShowDescript As Boolean
Dim mblnNeedParameters As Boolean
Dim mintIndex As Integer
    

    
'% insReturnCode: Actualiza los campos de la ventana que invoca los valores posibles
'--------------------------------------------------------------------------------------------
Private Function insReturnCode() As Object
	'--------------------------------------------------------------------------------------------
	With Response
		If mobjValues.reaTable(Request.QueryString.Item("sTabName"), Request.QueryString.Item("sCode")) Then
			.Write("<SCRIPT>")
			.Write("opener.document.forms[0].elements[""" & Request.QueryString.Item("sName") & """].value ='" & Request.QueryString.Item("sCode") & "';")
			If Request.QueryString.Item("sShowDescript") = "1" Then
                    .Write("chan('" & Request.QueryString.Item("sName") & "Desc','" & Replace(mobjValues.Fields(mobjValues.DescriptField),"'","\'") & "');")
			End If
			If Request.QueryString.Item("nRCount") > "0" Then
				For mintIndex = 1 To CInt(Request.QueryString.Item("nRCount"))
					If CBool(Request.QueryString.Item("rParam" & mintIndex & "Create")) Then
						.Write("opener.document.forms[0]." & Request.QueryString.Item("sName") & "_" & Request.QueryString.Item("rParam" & mintIndex & "Name") & ".value='" & mobjValues.Fields(Request.QueryString.Item("rParam" & mintIndex & "Name")) & "';")
					End If
				Next 
			End If
			.Write("CloseWindow(true);")
			.Write("</" & "Script>")
		Else
			.Write("<SCRIPT>")
                If Request.QueryString.Item("sAllowInvalid") <> "1" Then
                    .Write("window.resizeTo(750, 450);")
                    .Write("opener.document.forms[0].elements[""" & Request.QueryString.Item("sName") & """].value ='';")
                    .Write("alert(""" & "Código no válido" & """);")
                    .Write("opener.document.forms[0].elements[""" & Request.QueryString.Item("sName") & """].focus();")
                End If
			.Write("CloseWindow(false);")
			.Write("</" & "Script>")
		End If
	End With
	mobjValues = Nothing
End Function

</script>
<%Response.Expires = -1

mblnNeedParameters = False
mblnShowDescript = True
%>
<SCRIPT>
//% ShowValues: Carga la ventana de Valores posibles
//-------------------------------------------------------------------------------------------
function ShowValues(Control, bCheckCode){
//-------------------------------------------------------------------------------------------
    var lstrQueryString;
    var lobjParam;
    var lintIndex;
       
    lstrQueryString = "?sName=" + Control.name + "&nCount=" + Control.Parameters.nCount + "&sTabname=" + Control.sTabName;
    for (lintIndex=1;lintIndex<=Control.Parameters.nCount;lintIndex++){
        try{eval("lobjParam = Control.Parameters.Param" + lintIndex + ";");}
        catch(e){alert('falló');break;}
        finally{}
        if (lobjParam.sValue == "VT_EMPTY") {
           alert("Se deben indicar todos los parámetros");
           return;
        }
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sName=" + lobjParam.sName;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sValue=" + lobjParam.sValue;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sDirection=" + lobjParam.sDirection;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sParType=" + lobjParam.sParType;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sSize=" + lobjParam.sSize;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sNumericScale=" + lobjParam.sNumericScale;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sPrecision=" + lobjParam.sPrecision;
        lstrQueryString = lstrQueryString + "&Param" + lintIndex + "sAtributes=" + lobjParam.sAtributes;
    }
    if (bCheckCode){
        lstrQueryString = lstrQueryString + "&sCode=" + Control.value;
        ShowPopUp('/VTimeNet/Common/Values.aspx'+ lstrQueryString, 'Valpos', 1, 1,'no','no',2000,2000);
    }
    else {
        ShowPopUp('/VTimeNet/Common/Values.aspx'+ lstrQueryString, 'Valpos', 330, 300);}
}

//% chan: Actualiza la descripción del valor posible
//-------------------------------------------------------------------------------------------
function chan(DivName,lstrValue){
//-------------------------------------------------------------------------------------------
    opener.$("#"+DivName).html(lstrValue);
}

//% insReturnValues: Actualiza los campos de la ventana que invoca los valores posibles
//-------------------------------------------------------------------------------------------
function insReturnValues(Index){
//-------------------------------------------------------------------------------------------}
    var lstrCode;
    var lstrDescript;
    lstrCode = marrArray[Index].tctCode
    lstrDescript = marrArray[Index].tctDescript
    opener.document.forms[0].elements["<%=Request.QueryString.Item("sName")%>"].value = lstrCode;
    <%If Request.QueryString.Item("sShowDescript") = "1" Then%>
        chan("<%=Request.QueryString.Item("sName") & "Desc"%>",lstrDescript);
    <%End If
If Request.QueryString.Item("nRCount") > "0" Then
	For mintIndex = 1 To CShort(Request.QueryString.Item("nRCount"))
		If CBool(Request.QueryString.Item("rParam" & mintIndex & "Create")) Then
			Response.Write("opener.document.forms[0]." & Request.QueryString.Item("sName") & "_" & Request.QueryString.Item("rParam" & mintIndex & "Name") & ".value=marrArray[Index]." & Request.QueryString.Item("rParam" & mintIndex & "Name") & ";")
		End If
	Next 
End If
%>
    CloseWindow(1)
}

//% CloseWindow: Controla el cierre de la ventana
//-------------------------------------------------------------------------------------------
function CloseWindow(lblnSetInfo){
//-------------------------------------------------------------------------------------------
    if (!lblnSetInfo){
        chan("<%=Request.QueryString.Item("sName") & "Desc"%>","");
    }
    opener.mblnShowValues=false;
	opener.$("#<%=Request.QueryString("sName")%>").change();
    opener.mblnShowValues=true;
    self.window.close();
}

//% ApplyCondition: Controla la condición de búsqueda
//-------------------------------------------------------------------------------------------
function ApplyCondition(){
//-------------------------------------------------------------------------------------------
    document.forms[0].action = document.forms[0].action + "&<%=Request.Params.Get("Query_String")%>"; 
    document.forms[0].submit();
}
</SCRIPT>

<%
If Request.QueryString.Item("sCode") <> vbNullString Then
	mobjValues = New eFunctions.Tables
Else
	mobjValues = New eFunctions.Values
End If

If Request.QueryString.Item("nCount") > "0" Then
	mblnNeedParameters = True
	For mintIndex = 1 To CInt(Request.QueryString.Item("nCount"))
		With mobjValues.Parameters
			.Add(Request.QueryString.Item("Param" & mintIndex & "sName"), Request.QueryString.Item("Param" & mintIndex & "sValue"), Request.QueryString.Item("Param" & mintIndex & "sDirection"), Request.QueryString.Item("Param" & mintIndex & "sParType"), Request.QueryString.Item("Param" & mintIndex & "sSize"), Request.QueryString.Item("Param" & mintIndex & "sNumericScale"), Request.QueryString.Item("Param" & mintIndex & "sPrecision"), Request.QueryString.Item("Param" & mintIndex & "sAttributes"))
		End With
	Next 
End If

'+ Se carga la colección con los valores que retorna del tabTable
If Request.QueryString.Item("nRCount") > "0" Then
	For mintIndex = 1 To CInt(Request.QueryString.Item("nRCount"))
		mobjValues.Parameters.ReturnValue(Request.QueryString.Item("rParam" & mintIndex & "Name"), CBool(IIf(IsNothing(Request.QueryString.Item("rParam" & mintIndex & "Visible")), False, Request.QueryString.Item("rParam" & mintIndex & "Visible"))), Request.QueryString.Item("rParam" & mintIndex & "Title"), CBool(IIf(IsNothing(Request.QueryString.Item("rParam" & mintIndex & "Create")), False, Request.QueryString.Item("rParam" & mintIndex & "Create"))))
	Next 
End If

If Request.QueryString.Item("sCode") <> vbNullString Then
	insReturnCode()
Else
	%>
<HTML>
<HEAD>
    <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\common\VTime\Scripts\GenFunctions.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"> </script>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%	
	With Response
		.Write(mobjValues.WindowsTitle("GE004"))
		.Write(mobjValues.StyleSheet())
	End With
	%>
</HEAD>
<BODY ONLOAD="window.focus()">
    <FORM METHOD="POST" ACTION="Values.aspx?REQUERY=1" ID=FORM1 NAME=FORM1>
    <DIV style="text-align:center; height: 312px; width: 99%;"> 
    <TABLE COLS="2" style="margin-left:auto;margin-right:auto">
        <TR><TD COLSPAN="2" ALIGN="RIGHT"><LABEL CLASS="HighLighted"><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD></TR>
        <TR><TD COLSPAN="2" CLASS="HORLINE"></TD></TR>
        <TR><TD COLSPAN="2"></TD></TR>
        <TR><TD ALIGN="CENTER" COLSPAN=2>
            <DIV ID="Scroll" STYLE="width:100%; height:170;overflow:auto;outset gray">
                <%	If Request.QueryString.Item("sShowDescript") = "0" Then
		mblnShowDescript = False
	End If
	With mobjValues
		.TypeList = Request.QueryString.Item("TypeList")
		.List = Request.QueryString.Item("List")
                        .TypeOrder = Request.QueryString.Item("TypeOrder")
                        'Control de búsquedas sin valores null
                        Dim scONDITIon As Object = String.Empty
                        If Request.Form("tctCondition") <> String.Empty Then
                            scONDITIon = "%" + Request.Form("tctCondition") + "%"
                        End If
                        Response.Write(.LoadValues(Request.QueryString.Item("sTabName"), eFunctions.Values.VTReplace(scONDITIon, "'", "''"), , , mblnNeedParameters, mblnShowDescript))
                    End With
	%>
            </DIV>    
            </TD>
        </TR>
        <TR>
        <TR><TD COLSPAN="2" ALIGN="RIGHT"><LABEL CLASS="HighLighted"><%= GetLocalResourceObject("tctConditionCaption") %></LABEL></TD></TR>
        <TR><TD COLSPAN="2" CLASS="HORLINE"></TD></TR>
        <TR>
            <TD><%=mobjValues.TextControl("tctCondition", 30, "", False, GetLocalResourceObject("tctConditionToolTip"))%></TD>
            <TD><%=mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "ApplyCondition();")%></TD>
        </TR>
    </TABLE>
    </DIV> 
    <TABLE WIDTH=100%>
        <TD WIDTH ALIGN=RIGHT><%= mobjValues.AnimatedButtonControl("btn_Cancel", "/VTimeNet/images/btnCancelOff.png", GetLocalResourceObject("btn_CancelToolTip"), , "CloseWindow(0);")%></TD>
    </TABLE>    
</BODY>
</HTML>
<%End If
%>