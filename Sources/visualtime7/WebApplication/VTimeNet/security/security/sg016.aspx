<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjGridQuery As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues

Dim llngCGlobal As Short


'%insDefineHeader(): Este procedimiento se encarga de definir las líneas del encabezado
'%					 del grid.
'---------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "SG016"
	
	'+ Se definen todas las columnas del Grid.
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SG016"
		.bOnlyForQuery = True
		.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303
	End With
	
	With mobjGrid.Columns
		Call .AddCheckColumn(100453, "", "nSelMain", "", 1, CStr(2),  , False)
		Call .AddTextColumn(100452, GetLocalResourceObject("sActionColumnCaption"), "sAction", 20, "",  ,  ,  ,  ,  , True)
		Call .AddAnimatedColumn(0, "", "btnImag_action",  ,  ,  ,  , True)
		Call .AddHiddenColumn("nAction", CStr(0))
		Call .AddHiddenColumn("nSelValueMain", CStr(0))
	End With
	
	mobjGrid.Columns("Sel").GridVisible = False
	
	mobjGridQuery = New eFunctions.Grid
	mobjGridQuery.sCodisplPage = "SG016"
	
	'+ Se definen todas las columnas del Grid.
	With mobjGridQuery
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SG016"
		.bOnlyForQuery = True
		.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
	End With
	
	With mobjGridQuery.Columns
		Call .AddCheckColumn(100454, "", "nSelQuery", "", 1, CStr(2),  , False)
		Call .AddTextColumn(100452, GetLocalResourceObject("sActionQueryColumnCaption"), "sActionQuery", 20, "",  ,  ,  ,  ,  , True)
		Call .AddAnimatedColumn(0, "", "btnImag_actionQuery",  ,  ,  ,  , True)
		Call .AddHiddenColumn("nActionQuery", CStr(0))
		Call .AddHiddenColumn("nSelValueQuery", CStr(0))
	End With
	
	mobjGridQuery.Columns("Sel").GridVisible = False
End Sub

'%insPreSG016: Esta ventana se encarga de mostrar en el grid los valores leídos.
'---------------------------------------------------------------------------------------
Private Sub insPreSG016(ByRef nIndic As Byte)
	'---------------------------------------------------------------------------------------
	Dim lclsWindows As Object
	Dim lcolWindowss As eSecurity.Windowss
	Dim lObjWindows As eSecurity.Windows
	Dim llngCount As Short
	Dim llngIndex As Double
	
	lcolWindowss = New eSecurity.Windowss
	lObjWindows = New eSecurity.Windows
	
	If nIndic = 1 Then
		If lcolWindowss.FindActions("3", Session("sCodispLog"), True) Then
			llngIndex = 0
			llngCount = 0
			
			For	Each lclsWindows In lcolWindowss
				With mobjGrid
					'+ OJO: Como no se han creado las imágenes para las acciones Moneda, Revisar
					'+ Primero y ültimo, las mismas no se van a mostrar pero una vez que se hagan 
					'+ se modifica estas líneas. Acciones 309 - 403 - 490 - 493.
					
					If (lclsWindows.nAction <> 300 And lclsWindows.nAction <> 309 And lclsWindows.nAction <> 390 And lclsWindows.nAction <> 391 And lclsWindows.nAction <> 392 And lclsWindows.nAction <> 393) Then
						.Columns("nSelMain").Checked = lclsWindows.sSel
						.Columns("nSelMain").OnClick = "insHandleGridQuery(this," & CStr(lclsWindows.nAction) & "," & CStr(llngIndex) & ")"
						.Columns("nSelValueMain").DefValue = lclsWindows.sSel
						.Columns("sAction").DefValue = lclsWindows.sDescript
						.Columns("nAction").DefValue = lclsWindows.nAction
						.Columns("btnImag_action").Src = lclsWindows.sPathImage
						'.Columns("btnImag_action").FieldName = "btnImag_action" & llngIndex
						'.Columns("sAction").Alias = lclsWindows.sHel_actio
						llngIndex = llngIndex + 1
						
						If lclsWindows.nAction = 400 Then 'clngMenuInquiry
							If lclsWindows.sSel = "1" Then
								llngCount = llngCount + 1
								llngCGlobal = llngCount
							End If
						End If
						
						Response.Write(.doRow)
					End If
				End With
			Next lclsWindows
		End If
		
		Response.Write(mobjGrid.closeTable())
	Else
		If lcolWindowss.FindActions("4", Session("sCodispLog"), True) Then
			
			llngIndex = 0
			
			For	Each lclsWindows In lcolWindowss
				With mobjGridQuery
					If (lclsWindows.nAction <> 400 And lclsWindows.nAction <> 403) Then
						.Columns("nSelQuery").Checked = lclsWindows.sSel
						.Columns("nSelQuery").Disabled = (llngCGlobal = 0)
						.Columns("nSelQuery").OnClick = "insHandleGridQueryStatus(this," & CStr(llngIndex) & ")"
						.Columns("nSelValueQuery").DefValue = lclsWindows.sSel
						.Columns("sActionQuery").DefValue = lclsWindows.sDescript
						.Columns("btnImag_actionQuery").Src = lclsWindows.sPathImage
						'.Columns("sActionQuery").Alias = lclsWindows.sHel_actio
						'.Columns("btnImag_actionQuery").FieldName = "btnImag_actionQuery" & llngIndex
						.Columns("nActionQuery").DefValue = lclsWindows.nAction
						
						llngIndex = llngIndex + 1
						Response.Write(.doRow)
					End If
				End With
			Next lclsWindows
		End If
		
		Response.Write(mobjGridQuery.closeTable())
	End If
	
	lclsWindows = Nothing
	lcolWindowss = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG016"

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 7/01/04 16:15 $|$$Author: Nvaplat11 $"

//% insHandleGridQuery:
//-------------------------------------------------------------------------------------------
function insHandleGridQuery(Field,nAct,nIndex){
//-------------------------------------------------------------------------------------------

//+ Se actualiza la columna oculta con la marcada.
    with(self.document.forms[0]){
		if (Field.checked)
		    nSelValueMain[nIndex].value = 1
		else 
			nSelValueMain[nIndex].value = 2    

//+ Se habilita o deshabilita el Grid de consultas.
		if(nAct==400){
		    if (Field.checked){
		        for(lintIndex = 0;lintIndex<nSelQuery.length;lintIndex++){
		             nSelQuery[lintIndex].disabled = false
		             nSelQuery[lintIndex].checked = false
		        }
		    }
		    else {
		        for(lintIndex = 0;lintIndex<nSelQuery.length;lintIndex++){
		             nSelQuery[lintIndex].checked = false
		             nSelQuery[lintIndex].disabled = true
		             nSelValueQuery[lintIndex].value = 2
		        }
		    }
		}
	}
}

//-------------------------------------------------------------------------------------------
function insHandleGridQueryStatus(Field,nIndex){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
//+ Se actualiza la columna oculta con la marcada.
		if (Field.checked)
		    nSelValueQuery[nIndex].value = 1
		else 
			nSelValueQuery[nIndex].value = 2    
	}
}
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>




<%
mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "SG016", "SG016.aspx"))
End If

With Response
	.Write(mobjValues.WindowsTitle("SG016"))
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SG016" ACTION="ValSecuritySeq.aspx?Time=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
   <%Response.Write(mobjValues.ShowWindowsName("SG016"))%>
   <%Call insDefineHeader()%>
    <TABLE WIDTH="100%" COLS=2>
        <TR>
            <TD WIDTH=10%>&nbsp;<TD>
            <TD VALIGN=TOP WIDTH="10%">
                <DIV ID="Scroll" STYLE="width:170;height:350;overflow:auto; outset gray">
                <%Call insPreSG016(1)%>
                </DIV>
            </TD>
            <TD WIDTH=20%>&nbsp;<TD>
            <TD VALIGN=TOP>
                <DIV ID="Scroll" STYLE="width:150;height:350;overflow:auto; outset gray">
                <%Call insPreSG016(2)%>
                </DIV>
            </TD>
        </TR>
    </TABLE>     
</FORM>
</BODY>
</HTML>






